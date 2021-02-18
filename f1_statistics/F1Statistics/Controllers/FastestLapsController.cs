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
    [Route("api/[controller]")]
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
        [Route("api/[controller]/drivers")]
        public List<FastestLapModel> GetDriversFastestLaps(OptionsModel options)
        {
            var data = _service.AggregateDriversFastestLaps(options);

            return data;
        }

        // GET: api/fastestlaps/constructors
        [HttpPost]
        [Route("api/[controller]/constructors")]
        public List<FastestLapModel> GetConstructorsFastestLaps(OptionsModel options)
        {
            var data = _service.AggregateConstructorsFastestLaps(options);

            return data;
        }

        // GET: api/fastestlaps/uniquedriversperseason
        [HttpPost]
        [Route("api/[controller]/uniquedriversperseason")]
        public List<UniqueSeasonFastestLapModel> GetUniqueDriversFastestLapsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateUniqueDriversFastestLapsPerSeason(options);

            return data;
        }

        // GET: api/fastestlaps/uniqueconstructorsperseason
        [HttpPost]
        [Route("api/[controller]/uniqueconstructorsperseason")]
        public List<UniqueSeasonFastestLapModel> GetUniqueConstructorsFastestLapsPerseason(OptionsModel options)
        {
            var data = _service.AggregateUniqueConstructorsFastestLapsPerseason(options);

            return data;
        }
    }
}
