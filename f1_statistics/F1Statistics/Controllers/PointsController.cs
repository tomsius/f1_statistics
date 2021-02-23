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
        private IPointsService _service;

        public PointsController(IPointsService service)
        {
            _service = service;
        }

        // GET: api/points/drivers
        [HttpPost]
        [Route("drivers")]
        public List<SeasonPointsModel> GetDriversPointsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateDriversPointsPerSeason(options);

            return data;
        }

        // GET: api/points/constructors
        [HttpPost]
        [Route("constructors")]
        public List<SeasonPointsModel> GetConstructorsPointsPerSeason(OptionsModel options)
        {
            var data = _service.AggregateConstructorsPointsPerSeason(options);

            return data;
        }
    }
}
