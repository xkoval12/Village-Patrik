namespace Application.Web.Common.ErrorHandling;

public class ErrorResponse
{
    public string? Message { get; set; }

    public ErrorResponse(Error? error)
    {
        Message = error?.Message;
    }
    
    public ErrorResponse(string? message = null)
    {
        Message = message;
    }
}