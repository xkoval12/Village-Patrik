namespace Application.Web.Common.ErrorHandling;

public static class ErrorExtensions
{
    public static ErrorResponse ToResponse(this Error? error)
    {
        return new ErrorResponse(error);
    }
}