using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService forecastService)
        {
            _logger = logger;
            _weatherForecastService = forecastService;
        }

        [HttpGet]
        [Route("weathers")]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = _weatherForecastService.Get();
            return result;
        }
    }
}
