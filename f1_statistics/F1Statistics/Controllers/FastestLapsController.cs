using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Statistics.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class FastestLapsController : ControllerBase
    {
        private readonly IFastestLapsService _service;

        public FastestLapsController(IFastestLapsService service)
        {
            _service = service;
        }

        [HttpPost("drivers")]
        public ActionResult<List<FastestLapModel>> GetDriversFastestLaps(OptionsModel options)
        {
            try
            {
                var data = _service.AggregateDriversFastestLaps(options);

                return data;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("constructors")]
        public ActionResult<List<FastestLapModel>> GetConstructorsFastestLaps(OptionsModel options)
        {
            try
            {
                var data = _service.AggregateConstructorsFastestLaps(options);

                return data;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("drivers/uniqueperseason")]
        public ActionResult<List<UniqueSeasonFastestLapModel>> GetUniqueDriversFastestLapsPerSeason(OptionsModel options)
        {
            try
            {
                var data = _service.AggregateUniqueDriversFastestLapsPerSeason(options);

                return data;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("constructors/uniqueperseason")]
        public ActionResult<List<UniqueSeasonFastestLapModel>> GetUniqueConstructorsFastestLapsPerseason(OptionsModel options)
        {
            try
            {
                var data = _service.AggregateUniqueConstructorsFastestLapsPerseason(options);

                return data;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
