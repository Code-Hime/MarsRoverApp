using MarsRoverApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MarsRoverApp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MarsRoverController : ControllerBase
    {
        private readonly IMarsRoverService _marsRoverService;

        public MarsRoverController(IMarsRoverService marsRoverService)
        {
            _marsRoverService = marsRoverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetApod()
        {
            try
            {
                var apod = await _marsRoverService.GetApod();
                return Ok(apod);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApodByDate(string date)
        {
            try
            {
                var apod = await _marsRoverService.GetApodByDate(date);
                return Ok(apod);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DownloadAndSaveApods()
        {
            _marsRoverService.DownloadAndSaveApods();

            return Accepted();
        }
    }
}
