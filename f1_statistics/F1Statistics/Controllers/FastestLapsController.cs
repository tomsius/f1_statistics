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
        private IFastestLapsService _service;

        public FastestLapsController(IFastestLapsService service)
        {
            _service = service;
        }

        // GET: api/fastestlaps/drivers
        [HttpPost]
        [Route("drivers")]
        public List<FastestLapModel> GetDriversFastestLaps(OptionsModel options)
        {
            var data = _service.AggregateDriversFastestLaps(options);

            return data;
        }

        // GET: api/fastestlaps/constructors
        [HttpPost]
        [Route("constructors")]
        public List<FastestLapModel> GetConstructorsFastestLaps(OptionsModel options)
        {
            var data = _service.AggregateConstructorsFastestLaps(options);

            return data;
        }

        // GET: api/fastestlaps/drivers/uniqueperseason
        [HttpPost]
        [Route("drivers/uniqueperseason")]
        public List<UniqueSeasonFastestLapModel> GetUniqueDriversFastestLapsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateUniqueDriversFastestLapsPerSeason(options);

            return data;
        }

        // GET: api/fastestlaps/constructors/uniqueperseason
        [HttpPost]
        [Route("constructors/uniqueperseason")]
        public List<UniqueSeasonFastestLapModel> GetUniqueConstructorsFastestLapsPerseason(OptionsModel options)
        {
            var data = _service.AggregateUniqueConstructorsFastestLapsPerseason(options);

            return data;
        }
    }
}
