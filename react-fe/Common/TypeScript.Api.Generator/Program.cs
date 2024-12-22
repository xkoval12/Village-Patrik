// See https://aka.ms/new-console-template for more information

using Application.Web.Common.ApiGeneration;
using Server.Base;

Console.WriteLine(TypeScriptApiGenerator.Generate(typeof(ControllersAssemblyTarget).Assembly));
