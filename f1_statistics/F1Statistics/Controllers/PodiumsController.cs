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
    public class PodiumsController : ControllerBase
    {
        private readonly IPodiumsService _service;

        public PodiumsController(IPodiumsService service)
        {
            _service = service;
        }

        [HttpPost("drivers")]
        public List<PodiumsModel> GetDriversPodiums(OptionsModel options)
        {
            var data = _service.AggregateDriversPodiums(options);

            return data;
        }

        [HttpPost("constructors")]
        public List<PodiumsModel> GetConstructorsPodiums(OptionsModel options)
        {
            var data = _service.AggregateConstructorsPodiums(options);

            return data;
        }

        [HttpPost("drivers/same")]
        public List<SamePodiumsModel> GetSameDriverPodiums(OptionsModel options)
        {
            var data = _service.AggregateSameDriverPodiums(options);

            return data;
        }

        [HttpPost("constructors/same")]
        public List<SamePodiumsModel> GetSameConstructorsPodiums(OptionsModel options)
        {
            var data = _service.AggregateSameConstructorsPodiums(options);

            return data;
        }
    }
}
