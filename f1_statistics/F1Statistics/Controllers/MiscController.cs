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

        [HttpPost("hattricks")]
        public List<HatTrickModel> GetHatTricks(OptionsModel options)
        {
            var data = _service.AggregateHatTricks(options);

            return data;
        }

        [HttpPost("grandslams")]
        public List<GrandSlamModel> GetGrandSlams(OptionsModel options)
        {
            var data = _service.AggregateGrandSlams(options);

            return data;
        }

        [HttpPost("dnfs")]
        public List<DidNotFinishModel> GetNonFinishers(OptionsModel options)
        {
            var data = _service.AggregateNonFinishers(options);

            return data;
        }
    }
}
