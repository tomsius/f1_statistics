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
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonsService _service;

        public SeasonsController(ISeasonsService service)
        {
            _service = service;
        }

        [HttpPost("races")]
        public List<SeasonModel> GetSeasonRaces(OptionsModel options)
        {
            var data = _service.AggregateSeasonRaces(options);

            return data;
        }
    }
}
