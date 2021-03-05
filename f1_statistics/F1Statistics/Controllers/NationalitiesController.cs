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
    public class NationalitiesController : ControllerBase
    {
        private readonly INationalitiesService _service;

        public NationalitiesController(INationalitiesService service)
        {
            _service = service;
        }

        [HttpPost("drivers")]
        public List<NationalityDriversModel> GetDriversNationalities(OptionsModel options)
        {
            var data = _service.AggregateDriversNationalities(options);

            return data;
        }

        [HttpPost("racewins")]
        public List<NationalityWinsModel> GetNationalitiesRaceWins(OptionsModel options)
        {
            var data = _service.AggregateNationalitiesRaceWins(options);

            return data;
        }

        [HttpPost("seasonwins")]
        public List<NationalityWinsModel> GetNationalitiesSeasonWins(OptionsModel options)
        {
            var data = _service.AggregateNationalitiesSeasonWins(options);

            return data;
        }
    }
}
