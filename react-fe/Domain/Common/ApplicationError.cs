using Application.Web.Common.ErrorHandling;

namespace Domain.Common;

public class ApplicationError
{
    public static Error ApplicationShuttingDown => new Error(
        "Request cannot be processed, because application is not available right now. Please try it in a few seconds later");

    public static Error UserLogInFailed => new Error("User login has failed");
}
