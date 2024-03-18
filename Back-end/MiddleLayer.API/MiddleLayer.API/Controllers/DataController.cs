using Microsoft.AspNetCore.Mvc;
using MiddleLayer.Infrastructure.Contracts;
using System.Text.Json;

namespace MiddleLayer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataProviderHttpService dataProviderService;

        public DataController(IDataProviderHttpService dataProviderService)
        {
            this.dataProviderService = dataProviderService;
        }


        [HttpGet]
        public IActionResult FetchData()
        {
            try
            {
                var character = this.dataProviderService.GetData();
                var serializedCharacter = JsonSerializer.Serialize(character);
                return Ok(serializedCharacter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
