using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.DataAggregation
{
    public class FastestLapsAggregator : IFastestLapsAggregator
    {
        private IResultsDataAccess _resultsDataAccess;

        public FastestLapsAggregator(IResultsDataAccess resultsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
        }

        public List<FastestLapModel> GetDriversFastestLaps(int from, int to)
        {
            var driversFastestLaps = new List<FastestLapModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    var fastestDriver = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Driver).First();
                    string fastestLapper = $"{fastestDriver.givenName} {fastestDriver.familyName}";

                    if (!driversFastestLaps.Where(driver => driver.Name == fastestLapper).Any())
                    {
                        var newFastestLapModel = new FastestLapModel { Name = fastestLapper, FastestLapsCount = 1 };
                        driversFastestLaps.Add(newFastestLapModel);
                    }
                    else
                    {
                        driversFastestLaps.Where(driver => driver.Name == fastestLapper).First().FastestLapsCount++;
                    }
                }
            }

            return driversFastestLaps;
        }

        public List<FastestLapModel> GetConstructorsFastestLaps(int from, int to)
        {
            var constructorsFastestLaps = new List<FastestLapModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    var fastestConstructor = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Constructor).First();
                    string fastestLapper = $"{fastestConstructor.name}";

                    if (!constructorsFastestLaps.Where(constructor => constructor.Name == fastestLapper).Any())
                    {
                        var newFastestLapModel = new FastestLapModel { Name = fastestLapper, FastestLapsCount = 1 };
                        constructorsFastestLaps.Add(newFastestLapModel);
                    }
                    else
                    {
                        constructorsFastestLaps.Where(driver => driver.Name == fastestLapper).First().FastestLapsCount++;
                    }
                }
            }

            return constructorsFastestLaps;
        }

        public List<UniqueSeasonFastestLapModel> GetUniqueDriversFastestLaps(int from, int to)
        {
            var uniqueDriverFastestLaps = new List<UniqueSeasonFastestLapModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                var newUniqueSeasonFastestLapModel = new UniqueSeasonFastestLapModel { Season = year, FastestLapAchievers = new List<string>() };
                uniqueDriverFastestLaps.Add(newUniqueSeasonFastestLapModel);

                foreach (var race in races)
                {
                    var fastestDriver = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Driver).First();
                    string fastestLapper = $"{fastestDriver.givenName} {fastestDriver.familyName}";

                    if (!uniqueDriverFastestLaps[year - from].FastestLapAchievers.Where(driver => driver == fastestLapper).Any())
                    {
                        uniqueDriverFastestLaps[year - from].FastestLapAchievers.Add(fastestLapper);
                    }
                }
            }

            return uniqueDriverFastestLaps;
        }

        public List<UniqueSeasonFastestLapModel> GetUniqueConstructorsFastestLaps(int from, int to)
        {
            var uniqueConstructorFastestLaps = new List<UniqueSeasonFastestLapModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                var newUniqueSeasonFastestLapModel = new UniqueSeasonFastestLapModel { Season = year, FastestLapAchievers = new List<string>() };
                uniqueConstructorFastestLaps.Add(newUniqueSeasonFastestLapModel);

                foreach (var race in races)
                {
                    var fastestConstructor = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Constructor).First();
                    string fastestLapper = $"{fastestConstructor.name}";

                    if (!uniqueConstructorFastestLaps[year - from].FastestLapAchievers.Where(driver => driver == fastestLapper).Any())
                    {
                        uniqueConstructorFastestLaps[year - from].FastestLapAchievers.Add(fastestLapper);
                    }
                }
            }

            return uniqueConstructorFastestLaps;
        }
    }
}
