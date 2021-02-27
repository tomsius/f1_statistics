using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace F1Statistics.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class WinsController : ControllerBase
    {
        private IWinsService _service;

        public WinsController(IWinsService service)
        {
            _service = service;
        }

        // GET: api/wins/drivers
        [HttpPost]
        [Route("drivers")]
        public List<WinsModel> GetDriversWins(OptionsModel options)
        {
            var data = _service.AggregateDriversWins(options);

            return data;
        }

        // GET: api/wins/constructors
        [HttpPost]
        [Route("constructors")]
        public List<WinsModel> GetConstructorsWins(OptionsModel options)
        {
            var data = _service.AggregateConstructorsWins(options);

            return data;
        }

        // GET: api/wins/drivers/average
        [HttpPost]
        [Route("drivers/average")]
        public List<AverageWinsModel> GetDriversWinAverage(OptionsModel options)
        {
            var data = _service.AggregateDriversWinAverage(options);

            return data;
        }

        // GET: api/wins/constructors/average
        [HttpPost]
        [Route("constructors/average")]
        public List<AverageWinsModel> GetConstructorsWinAverage(OptionsModel options)
        {
            var data = _service.AggregateConstructorsWinAverage(options);

            return data;
        }

        // GET: api/wins/drivers/tracks
        [HttpPost]
        [Route("drivers/tracks")]
        public List<CircuitWinsModel> GetCircuitWinners(OptionsModel options)
        {
            var data = _service.AggregateCircuitsWinners(options);

            return data;
        }

        // GET: api/wins/drivers/uniqueperseason
        [HttpPost]
        [Route("drivers/uniqueperseason")]
        public List<UniqueSeasonWinnersModel> GetUniqueSeasonDriverWinners(OptionsModel options)
        {
            var data = _service.AggregateUniqueSeasonDriverWinners(options);

            return data;
        }

        // GET: api/wins/constructors/uniqueperseason
        [HttpPost]
        [Route("constructors/uniqueperseason")]
        public List<UniqueSeasonWinnersModel> GetUniqueSeasonConstructorWinners(OptionsModel options)
        {
            var data = _service.AggregateUniqueSeasonConstructorWinners(options);

            return data;
        }
    }
}
