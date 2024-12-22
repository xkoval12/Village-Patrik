using Application.Web.Common.ErrorHandling;
using Domain.Common;
using Microsoft.Extensions.Hosting;

namespace Domain;

public class DummyDomain : IDummyDomain
{
    private CancellationToken _applicationStopping;

    public DummyDomain(IHostApplicationLifetime hostApplicationLifetime)
    {
        _applicationStopping = hostApplicationLifetime.ApplicationStopping;
    }

    public Error? ProcessDummyInput(string something)
    {
        if (_applicationStopping.IsCancellationRequested)
        {
            return ApplicationError.ApplicationShuttingDown;
        }

        return null;
    }

    public string GetDummyValue()
    {
        return "země";
    }
}
