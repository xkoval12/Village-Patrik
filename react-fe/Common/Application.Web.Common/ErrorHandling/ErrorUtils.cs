
namespace Application.Web.Common.ErrorHandling;

public static class ErrorUtils
{
    public static async Task<ErrorResponse> Handle<TResult>(Func<Task<TResult>> action)
        where TResult : ErrorResponse
    {
        try
        {
            return await action();
        }
        catch (ApplicationException e)
        {
            return new ErrorResponse(e.Error);
        }
        catch (Exception e)
        {
            //todo log
            return new ErrorResponse(CommonApplicationError.UnexpectedProblem);
        }
    }
    
    public static ErrorResponse Handle<TResult>(Func<TResult> action)
        where TResult : ErrorResponse
    {
        try
        {
            return action();
        }
        catch (ApplicationException e)
        {
            return new ErrorResponse(e.Error);
        }
        catch (Exception e)
        {
            //todo log
            return new ErrorResponse(CommonApplicationError.UnexpectedProblem);
        }
    }
}