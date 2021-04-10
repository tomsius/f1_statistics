using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class SeasonsAggregator : ISeasonsAggregator
    {
        private readonly IResultsDataAccess _resultsDataAccess;

        public SeasonsAggregator(IResultsDataAccess resultsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
        }

        public List<SeasonModel> GetSeasonRaces(int from, int to)
        {
            var seasons = new List<SeasonModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);
                var newSeasonModel = new SeasonModel { Year = year, Races = new List<RaceModel>() };

                foreach (var race in races)
                {
                    var round = int.Parse(race.round);
                    var circuitName = race.raceName;

                    var newRaceModel = new RaceModel { Round = round, RaceName = circuitName };
                    newSeasonModel.Races.Add(newRaceModel);
                }

                lock (lockObject)
                {
                    seasons.Add(newSeasonModel);
                }
            });

            return seasons;
        }
    }
}
