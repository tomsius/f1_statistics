using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class MiscAggregator : IMiscAggregator
    {
        private readonly IRacesDataAccess _racesDataAccess;

        public MiscAggregator(IRacesDataAccess racesDataAccess)
        {
            _racesDataAccess = racesDataAccess;
        }

        public List<SeasonRacesModel> GetRaceCountPerSeason(int from, int to)
        {
            var seasonRaces = new List<SeasonRacesModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var watch = new Stopwatch();
                watch.Start();
                var raceCount = _racesDataAccess.GetRacesCountFrom(year);
                watch.Stop();
                Debug.WriteLine(watch.ElapsedMilliseconds);
                lock (lockObject)
                {
                    var newSeasonRacesModel = new SeasonRacesModel { Season = year, RaceCount = raceCount };
                    seasonRaces.Add(newSeasonRacesModel);
                }
            });

            return seasonRaces;
        }
    }
}
