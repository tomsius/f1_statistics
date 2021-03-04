using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Statistics.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class WinsController : ControllerBase
    {
        private readonly IWinsService _service;

        public WinsController(IWinsService service)
        {
            _service = service;
        }

        [HttpPost("drivers")]
        public List<WinsModel> GetDriversWins(OptionsModel options)
        {
            var data = _service.AggregateDriversWins(options);

            return data;
        }

        [HttpPost("constructors")]
        public List<WinsModel> GetConstructorsWins(OptionsModel options)
        {
            var data = _service.AggregateConstructorsWins(options);

            return data;
        }

        [HttpPost("drivers/percent")]
        public List<AverageWinsModel> GetDriversWinPercent(OptionsModel options)
        {
            var data = _service.AggregateDriversWinPercent(options);

            return data;
        }

        [HttpPost("constructors/percent")]
        public List<AverageWinsModel> GetConstructorsWinPercent(OptionsModel options)
        {
            var data = _service.AggregateConstructorsWinPercent(options);

            return data;
        }

        [HttpPost("drivers/tracks")]
        public List<CircuitWinsModel> GetCircuitWinners(OptionsModel options)
        {
            var data = _service.AggregateCircuitsWinners(options);

            return data;
        }

        [HttpPost("drivers/uniqueperseason")]
        public List<UniqueSeasonWinnersModel> GetUniqueSeasonDriverWinners(OptionsModel options)
        {
            var data = _service.AggregateUniqueSeasonDriverWinners(options);

            return data;
        }

        [HttpPost("constructors/uniqueperseason")]
        public List<UniqueSeasonWinnersModel> GetUniqueSeasonConstructorWinners(OptionsModel options)
        {
            var data = _service.AggregateUniqueSeasonConstructorWinners(options);

            return data;
        }

        [HttpPost("frompole")]
        public List<WinnersFromPoleModel> GetWinnersFromPole(OptionsModel options)
        {
            var data = _service.AggregateWinnersFromPole(options);

            return data;
        }
    }
}
