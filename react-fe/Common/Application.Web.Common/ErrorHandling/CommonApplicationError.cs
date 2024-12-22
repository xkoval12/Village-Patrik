namespace Application.Web.Common.ErrorHandling;

public class CommonApplicationError // todo to extension pattern
{
    public static Error UnexpectedProblem => new Error("Unexpected problem has occured");
}