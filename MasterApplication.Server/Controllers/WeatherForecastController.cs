using MasterApplication.DB.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MasterApplication.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IAttributeDB _attributeDB;
        public WeatherForecastController(IAttributeDB attributeDB)
        {
            _attributeDB = attributeDB;
        }

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet, Route("category")]
        public IActionResult GetAllCategory()
        {
            var encryptedData = _attributeDB.GetAllCategory();
            //var decryptedData = encryptedData.Select(p => _encriptionService.DecryptModel(p)).ToList();
            return Ok(encryptedData);
        }
    }
}
