using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Common.ApiGeneration;

public class TypeScriptApiGenerator
{
    public static string Generate(Assembly assembly)
    {
        var controllerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ControllerBase)))
            .ToList();

        return Generate(controllerTypes);
    }

    public static string Generate(IList<Type> controllerTypes)
    {
        var sb = new StringBuilder();
        var interfaces = new HashSet<string>();
        var enums = new HashSet<string>();

        // Start the ApiContext class
        sb.AppendLine("// generated file");
        sb.AppendLine("import {GetJson, PostJson} from '../Common/Api/Fetch.ts'");
        sb.AppendLine("import {IApplicationContext} from '../Common/Contexts.tsx'");
        sb.AppendLine();
        
        // Generate TypeScript for each controller and collect interfaces and enums
        foreach (var controllerType in controllerTypes)
        {
            sb.AppendLine(GenerateTypeScriptForController(controllerType, interfaces, enums));
        }

        GenerateApiContext(controllerTypes, sb);

        
        // Generate TypeScript enums
        foreach (var enumDef in enums)
        {
            sb.AppendLine(enumDef);
        }

        // Generate TypeScript interfaces
        foreach (var iface in interfaces)
        {
            sb.AppendLine(iface);
        }

        return sb.ToString();
    }

    private static void GenerateApiContext(IList<Type> controllerTypes, StringBuilder sb)
    {
        sb.AppendLine("export class ApiContext {");

        // Loop over each controller and add to ApiContext
        foreach (var controllerType in controllerTypes)
        {
            var controllerName = controllerType.Name.Replace("Controller", "");
            sb.AppendLine($"    {controllerName}: {controllerName};");
        }

        sb.AppendLine();
        sb.AppendLine("    constructor() {");

        // Initialize each controller in the ApiContext constructor
        foreach (var controllerType in controllerTypes)
        {
            var controllerName = controllerType.Name.Replace("Controller", "");
            sb.AppendLine($"        this.{controllerName} = new {controllerName}();");
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();
        sb.AppendLine("export const Api = new ApiContext();");
        sb.AppendLine();
    }

    public static string GenerateTypeScriptForController(Type controllerType, HashSet<string> interfaces, HashSet<string> enums)
    {
        var sb = new StringBuilder();

        // Generate TypeScript class for the controller
        sb.AppendLine($"export class {controllerType.Name.Replace("Controller", "")} {{");

        // Get the methods of the controller
        var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        foreach (var method in methods)
        {
            var methodName = method.Name;
            var parameters = method.GetParameters();
            var returnType = method.ReturnType;

            // Determine TypeScript method name and parameters
            var tsMethodName = methodName;

            // Handle input parameters and generate interface
            string tsParameters = string.Empty;
            if (parameters.Length > 0)
            {
                var paramType = parameters[0].ParameterType;
                tsParameters = $"{parameters[0].Name}: {GenerateTypeScriptInterface(paramType, enums, interfaces)}";
                tsParameters += ", ";
            }
            tsParameters += "applicationContext: IApplicationContext";

            // Handle return type and generate interface
            var tsReturnType = MapToTypeScriptReturnType(returnType, interfaces, enums);

            // Generate TypeScript method
            sb.AppendLine($"    {tsMethodName}({tsParameters}): Promise<{tsReturnType}> {{");
            sb.AppendLine($"        return {GetMethodName(method)}({GetInputParameter(parameters)}\"{controllerType.Name.Replace("Controller", "").ToLower()}\", \"{methodName.ToLower()}\", applicationContext);");
            sb.AppendLine("    }");
            sb.AppendLine();
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    private static string GetInputParameter(ParameterInfo[] parameters)
    {
        if (parameters.Length == 0)
        {
            return "";
        }

        return $"{parameters.First().Name}, ";
    }

    private static string GetMethodName(MethodInfo method)
    {
        var attributes = method.GetCustomAttributes().ToList();

        if (attributes.Any(a => a.GetType() == typeof(HttpPostAttribute)))
        {
            return "PostJson";
        }
        if (attributes.Any(a => a.GetType() == typeof(HttpGetAttribute)))
        {
            return "GetJson";
        }

        throw new NotSupportedException();
    }
    
    public static string GenerateTypeScriptInterface(Type type, HashSet<string> enums,
        HashSet<string> interfaces)
    {
        var sb = new StringBuilder();
        var typeName = $"I{type.Name}";
        sb.AppendLine($"export interface {typeName.Replace("[]", "")} {{");

        // Get the properties of the class and map to TypeScript types, omitting array []
        var properties = (type.IsArray ? type.GetElementType() : type)!.GetProperties();
        foreach (var property in properties)
        {
            var propertyName = property.Name;
            var tsType = TypeToTypeScriptCode(property.PropertyType, enums, interfaces);
            sb.AppendLine($"    {char.ToLower(propertyName[0]) + propertyName.Substring(1)}: {tsType};");
        }

        sb.AppendLine("}");
        
        interfaces.Add(sb.ToString());

        return typeName;
    }

    private static string TypeToTypeScriptCode(Type type, HashSet<string> enums, HashSet<string> interfaces)
    {
        if (type == typeof(string)) return "string";
        if (IsNumber(type)) return "number";
        if (type == typeof(bool)) return "boolean";
        if (type.IsEnum)
        {
            // Generate TypeScript enum for C# enum
            var enumName = type.Name;
            enums.Add(GenerateTypeScriptEnum(type));
            return enumName;
        }
        if (type.IsPrimitive)
        {
            throw new NotSupportedException($"Type '{type.FullName}' is primitive");
        }

        return GenerateTypeScriptInterface(type, enums, interfaces);
    }

    private static bool IsNumber(Type type)
    {
        return type == typeof(sbyte) ||
               type == typeof(byte) ||
               type == typeof(short) ||
               type == typeof(ushort) ||
               type == typeof(int) ||
               type == typeof(uint) ||
               type == typeof(long) ||
               type == typeof(ulong) ||
               type == typeof(float) ||
               type == typeof(double) ||
               type == typeof(decimal);
    }

    private static string MapToTypeScriptReturnType(Type returnType, HashSet<string> interfaces, HashSet<string> enums)
    {
        if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            var itemType = returnType.GetGenericArguments()[0];
            return $"{GenerateTypeScriptInterface(itemType, enums, interfaces)}[]";
        }
        if (returnType == typeof(void)) return "void";

        // For complex return types, generate an interface
        if (!returnType.IsPrimitive && returnType != typeof(string))
        {
            return GenerateTypeScriptInterface(returnType, enums, interfaces);
        }

        return TypeToTypeScriptCode(returnType, enums, interfaces);
    }

    public static string GenerateTypeScriptEnum(Type enumType)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"export enum {enumType.Name} {{");

        var enumNames = Enum.GetNames(enumType);
        foreach (var name in enumNames)
        {
            var value = Convert.ToInt32(Enum.Parse(enumType, name));
            sb.AppendLine($"    {name} = {value},");
        }

        sb.AppendLine("}");
        return sb.ToString();
    }
}
