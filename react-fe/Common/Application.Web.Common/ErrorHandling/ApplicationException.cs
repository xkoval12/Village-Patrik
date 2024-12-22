namespace Application.Web.Common.ErrorHandling;

public class ApplicationException : Exception
{
    public Error Error { get; }

    public ApplicationException(Error error)
    {
        Error = error;
    }
}