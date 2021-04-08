using MarsRoverApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarsRoverApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarsRoverController : ControllerBase
    {
        private readonly IMarsRoverService _marsRoverService;
        private readonly ILogger<MarsRoverController> _logger;

        public MarsRoverController(ILogger<MarsRoverController> logger, IMarsRoverService marsRoverService)
        {
            _logger = logger;
            _marsRoverService = marsRoverService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var apod = _marsRoverService.GetApod();
            return Ok(apod);
        }
    }
}
