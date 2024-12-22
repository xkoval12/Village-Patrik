using Application.Web.Common.ErrorHandling;

namespace Domain;

public interface IDummyDomain
{
    Error? ProcessDummyInput(string something);

    string GetDummyValue();
}
