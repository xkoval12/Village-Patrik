using System.Runtime.CompilerServices;

namespace TypeScript.Api.Generator.Test;

public static class VerifyExtensions
{
    public static void Verify(this string value, [CallerFilePath] string sourceFile = "")
    {
        Verifier.Verify("xxx", sourceFile: sourceFile);
    }
}