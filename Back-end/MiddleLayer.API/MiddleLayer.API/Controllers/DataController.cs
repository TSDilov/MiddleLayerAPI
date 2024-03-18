using Microsoft.AspNetCore.Mvc;
using MiddleLayer.Infrastructure.Contracts;
using MiddleLayer.Infrastructure.Services;
using System.Text.Json;

namespace MiddleLayer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataProviderHttpService dataProviderService;
        private readonly ILogger<DataProviderHttpService> logger;

        public DataController(IDataProviderHttpService dataProviderService, ILogger<DataProviderHttpService> logger)
        {
            this.dataProviderService = dataProviderService;
            this.logger = logger;
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteData()
        {
            try
            {
                await this.dataProviderService.DeleteData();
                return NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while deleting data.");
                return StatusCode(500, "An error occurred while deleting data.");
            }
        }
    }
}
