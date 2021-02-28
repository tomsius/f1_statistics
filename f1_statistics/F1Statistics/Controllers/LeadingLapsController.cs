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
    public class LeadingLapsController : ControllerBase
    {
        private readonly ILeadingLapsService _service;

        public LeadingLapsController(ILeadingLapsService service)
        {
            _service = service;
        }

        [HttpPost("drivers")]
        public List<LeadingLapsModel> GetDriversLeadingLapsCount(OptionsModel options)
        {
            var data = _service.AggregateDriversLeadingLapsCount(options);

            return data;
        }

        [HttpPost("constructor")]
        public List<LeadingLapsModel> GetConstructorsLeadingLapsCount(OptionsModel options)
        {
            var data = _service.AggregateConstructorsLeadingLapsCount(options);

            return data;
        }
    }
}
