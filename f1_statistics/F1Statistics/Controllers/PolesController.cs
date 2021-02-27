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
    public class PolesController : ControllerBase
    {
        private IPolesService _service;

        public PolesController(IPolesService service)
        {
            _service = service;
        }

        [HttpPost("drivers")]
        public List<PolesModel> GetPoleSittersDrivers(OptionsModel options)
        {
            var data = _service.AggregatePoleSittersDrivers(options);

            return data;
        }

        [HttpPost("constructors")]
        public List<PolesModel> GetPoleSittersConstructors(OptionsModel options)
        {
            var data = _service.AggregatePoleSittersConstructors(options);

            return data;
        }

        [HttpPost("drivers/uniqueperseason")]
        public List<UniqueSeasonPoleSittersModel> GetUniqueSeasonDriverPoleSitters(OptionsModel options)
        {
            var data = _service.AggregateUniqueSeasonPoleSittersDrivers(options);

            return data;
        }

        [HttpPost("constructors/uniqueperseason")]
        public List<UniqueSeasonPoleSittersModel> GetUniqueSeasonConstructorsPoleSitters(OptionsModel options)
        {
            var data = _service.AggregateUniqueSeasonPoleSittersConstructors(options);

            return data;
        }

        [HttpPost("winners")]
        public List<WinnersFromPoleModel> GetWinnersFromPole(OptionsModel options)
        {
            var data = _service.AggregateWinnersFromPole(options);

            return data;
        }
    }
}
