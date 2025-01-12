using Application.Web.Common.ErrorHandling;
using Domain;
using Domain.Contract;
using Microsoft.AspNetCore.Mvc;
using Server.Api.Models;

namespace Server.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillageController : ControllerBase
    {
        private readonly IDummyDomain _dummyDomain;
        private readonly HttpClient _httpClient;

        public VillageController(
            IDummyDomain dummyDomain,
            HttpClient httpClient
        )
        {
            _dummyDomain = dummyDomain;
            _httpClient = httpClient;
        }


        //TODO: Create stone, and Coal
        [HttpGet("[action]")]
        public VillageDto GetVillage()
        {
            var villageUri = new Uri("http://localhost:5555/village");

            var village = _httpClient.GetFromJsonAsync<VillageDto>(villageUri).Result;

            return village;
            //return new DummyResult(_dummyDomain.GetDummyValue());
        }
    }
}





// Ensure the response was successful


