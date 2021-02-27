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
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _service;

        public PointsController(IPointsService service)
        {
            _service = service;
        }

        [HttpPost("drivers")]
        public List<SeasonPointsModel> GetDriversPointsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateDriversPointsPerSeason(options);

            return data;
        }

        [HttpPost("constructors")]
        public List<SeasonPointsModel> GetConstructorsPointsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateConstructorsPointsPerSeason(options);

            return data;
        }

        [HttpPost("drivers/winners")]
        public List<SeasonWinnersPointsModel> GetDriversWinnersPointsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateDriversWinnersPointsPerSeason(options);

            return data;
        }

        [HttpPost("constructors/winners")]
        public List<SeasonWinnersPointsModel> GetConstructorsWinnersPointsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateConstructorsWinnersPointsPerSeason(options);

            return data;
        }
    }
}
