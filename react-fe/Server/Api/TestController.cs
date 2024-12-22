using Application.Web.Common.ErrorHandling;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Server.Api.Models;

namespace Server.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDummyDomain _dummyDomain;

        public TestController(
            IDummyDomain dummyDomain
        )
        {
            _dummyDomain = dummyDomain;
        }

        [HttpPost("[action]")]
        public ErrorResponse PostSomething(DummyInput input)
        {
            var error = _dummyDomain.ProcessDummyInput(input.SomeValue);

            return error.ToResponse();
        }

        [HttpGet("[action]")]
        public DummyResult GetSomething()
        {
            return new DummyResult(_dummyDomain.GetDummyValue());
        }

        [HttpGet("[action]")]
        public ErrorResponse GetError()
        {
            return new ErrorResponse("Server error");
        }
    }
}
