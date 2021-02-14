using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Statistics.Controllers
{
    [Route("api/[controller]")]
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
        [Route("api/[controller]/drivers")]
        public List<WinsModel> GetDriversWins(OptionsModel options)
        {
            var data = _service.AggregateDriversWins(options);

            return data;
        }

        // GET: api/wins/constructors
        [HttpPost]
        [Route("api/[controller]/constructors")]
        public List<WinsModel> GetConstructorsWins(OptionsModel options)
        {
            var data = _service.AggregateConstructorsWins(options);

            return data;
        }

        // GET: api/wins/drivers/average
        [HttpPost]
        [Route("api/[controller]/drivers/average")]
        public List<AverageWinsModel> GetDriversAverageWins(OptionsModel options)
        {
            var data = _service.AggregateDriversAverageWins(options);

            return data;
        }
    }
}
