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
    public class MiscController : ControllerBase
    {
        private readonly IMiscService _service;

        public MiscController(IMiscService service)
        {
            _service = service;
        }

        [HttpPost("racesperseason")]
        public List<SeasonRacesModel> GetRaceCountPerSeason(OptionsModel options)
        {
            var data = _service.AggregateRaceCountPerSeason(options);

            return data;
        }
    }
}
