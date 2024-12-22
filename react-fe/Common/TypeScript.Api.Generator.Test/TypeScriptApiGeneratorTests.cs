using Application.Web.Common.ApiGeneration;
using Server.Base;

namespace TypeScript.Api.Generator.Test;

public class TypeScriptApiGeneratorTests
{
    [Test]
    public async Task GenerateTypeScriptClient_OrdersController()
    {
        string directoryPath = Environment.CurrentDirectory!;

        while (!ContainsDirectory(directoryPath, ".git"))
        {
            directoryPath = Path.GetDirectoryName(directoryPath)!;
        }

        string clientDirectoryPath = Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly)
            .First(d => Path.GetFileName(d.ToLower()).Contains("client"));

        string apiDirectoryPath = Path.Combine(clientDirectoryPath, "src", "Api");
        Directory.CreateDirectory(apiDirectoryPath);

        string apiFilePath = Path.Combine(apiDirectoryPath, "Api.ts");

        string typeScriptContent = TypeScriptApiGenerator.Generate(typeof(ControllersAssemblyTarget).Assembly);

        await File.WriteAllTextAsync(apiFilePath, typeScriptContent);
    }

    private static bool ContainsDirectory(string directoryPath, string directoryName)
    {
        return Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly)
            .Any(d => Path.GetFileName(d.ToLower()) == directoryName.ToLower());
    }
}
