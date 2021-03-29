using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [HttpPost("positionchanges")]
        public List<SeasonPositionChangesModel> GetSeasonPositionChanges(OptionsModel options)
        {
            var data = _service.AggregateSeasonPositionChanges(options);

            return data;
        }

        [HttpPost("frontrows")]
        public List<FrontRowModel> GetConstructorsFrontRows(OptionsModel options)
        {
            var data = _service.AggregateConstructorsFrontRows(options);

            return data;
        }

        [HttpPost("finishingpositions")]
        public List<DriverFinishingPositionsModel> GetDriversFinishingPositions(OptionsModel options)
        {
            var data = _service.AggregateDriversFinishingPositions(options);

            return data;
        }

        [HttpPost("drivers/standingschanges")]
        public List<SeasonStandingsChangesModel> GetDriversStandingsChanges(OptionsModel options)
        {
            var data = _service.AggregateDriversStandingsChanges(options);

            return data;
        }

        [HttpPost("constructors/standingschanges")]
        public List<SeasonStandingsChangesModel> GetConstructorsStandingsChanges(OptionsModel options)
        {
            var data = _service.AggregateConstructorsStandingsChanges(options);

            return data;
        }

        [HttpPost("{season}/{race}/positionchangesduringrace")]
        public List<RacePositionChangesModel> GetDriversPositionChangesDuringRace(int season, int race)
        {
            var data = _service.AggregateDriversPositionChangesDuringRace(season, race);

            return data;
        }

        [HttpPost("{season}/{race}/laptimes")]
        public List<LapTimesModel> GetDriversLapTimes(int season, int race)
        {
            var data = _service.AggregateDriversLapTimes(season, race);

            return data;
        }
    }
}
