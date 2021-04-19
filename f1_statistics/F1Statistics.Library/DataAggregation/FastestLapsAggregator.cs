using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Helpers.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class FastestLapsAggregator : IFastestLapsAggregator
    {
        private readonly IResultsDataAccess _resultsDataAccess;
        private readonly INameHelper _nameHelper;

        public FastestLapsAggregator(IResultsDataAccess resultsDataAccess, INameHelper nameHelper)
        {
            _resultsDataAccess = resultsDataAccess;
            _nameHelper = nameHelper;
        }

        public List<FastestLapModel> GetDriversFastestLaps(int from, int to)
        {
            var driversFastestLaps = new List<FastestLapModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var fastestDriver = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Driver).First();
                    var fastestLapper = _nameHelper.GetDriverName(fastestDriver);
                    var circuitName = race.Circuit.circuitName;
                    var gridPosition = int.Parse(race.Results[0].grid);
                    var newFastestLapInformationModel = new FastestLapInformationModel { CircuitName = circuitName, GridPosition = gridPosition };

                    lock (lockObject)
                    {
                        var newFastestLapsByYearModel = new FastestLapsByYearModel { Year = year, FastestLapInformation = new List<FastestLapInformationModel> { newFastestLapInformationModel } };

                        if (!driversFastestLaps.Where(driver => driver.Name == fastestLapper).Any())
                        {
                            var newFastestLapModel = new FastestLapModel { Name = fastestLapper, FastestLapsByYear = new List<FastestLapsByYearModel> { newFastestLapsByYearModel } };
                            driversFastestLaps.Add(newFastestLapModel);
                        }
                        else
                        {
                            var driver = driversFastestLaps.Where(driver => driver.Name == fastestLapper).First();

                            if (!driver.FastestLapsByYear.Where(model => model.Year == year).Any())
                            {
                                driver.FastestLapsByYear.Add(newFastestLapsByYearModel);
                            }
                            else
                            {
                                driver.FastestLapsByYear.Where(model => model.Year == year).First().FastestLapInformation.Add(newFastestLapInformationModel);
                            }
                        } 
                    }
                }
            });

            return driversFastestLaps;
        }

        public List<FastestLapModel> GetConstructorsFastestLaps(int from, int to)
        {
            var constructorsFastestLaps = new List<FastestLapModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var fastestConstructor = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Constructor).First();
                    var fastestLapper = _nameHelper.GetConstructorName(fastestConstructor);
                    var circuitName = race.Circuit.circuitName;
                    var gridPosition = int.Parse(race.Results[0].grid);
                    var newFastestLapInformationModel = new FastestLapInformationModel { CircuitName = circuitName, GridPosition = gridPosition };

                    lock (lockObject)
                    {
                        var newFastestLapsByYearModel = new FastestLapsByYearModel { Year = year, FastestLapInformation = new List<FastestLapInformationModel> { newFastestLapInformationModel } };

                        if (!constructorsFastestLaps.Where(constructor => constructor.Name == fastestLapper).Any())
                        {
                            var newFastestLapModel = new FastestLapModel { Name = fastestLapper, FastestLapsByYear = new List<FastestLapsByYearModel> { newFastestLapsByYearModel } };
                            constructorsFastestLaps.Add(newFastestLapModel);
                        }
                        else
                        {
                            var constructor = constructorsFastestLaps.Where(driver => driver.Name == fastestLapper).First();

                            if (!constructor.FastestLapsByYear.Where(model => model.Year == year).Any())
                            {
                                constructor.FastestLapsByYear.Add(newFastestLapsByYearModel);
                            }
                            else
                            {
                                constructor.FastestLapsByYear.Where(model => model.Year == year).First().FastestLapInformation.Add(newFastestLapInformationModel);
                            }
                        } 
                    }
                }
            });

            return constructorsFastestLaps;
        }

        public List<UniqueSeasonFastestLapModel> GetUniqueDriversFastestLaps(int from, int to)
        {
            var uniqueDriverFastestLaps = new List<UniqueSeasonFastestLapModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newUniqueSeasonFastestLapModel = new UniqueSeasonFastestLapModel { Season = year, RacesCount = races.Count, FastestLapAchievers = new List<string>() };

                foreach (var race in races)
                {
                    var fastestDriver = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Driver).First();
                    var fastestLapper = _nameHelper.GetDriverName(fastestDriver);

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonFastestLapModel.FastestLapAchievers.Where(driver => driver == fastestLapper).Any())
                        {
                            newUniqueSeasonFastestLapModel.FastestLapAchievers.Add(fastestLapper);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniqueDriverFastestLaps.Add(newUniqueSeasonFastestLapModel); 
                }
            });

            return uniqueDriverFastestLaps;
        }

        public List<UniqueSeasonFastestLapModel> GetUniqueConstructorsFastestLaps(int from, int to)
        {
            var uniqueConstructorFastestLaps = new List<UniqueSeasonFastestLapModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newUniqueSeasonFastestLapModel = new UniqueSeasonFastestLapModel { Season = year, RacesCount = races.Count, FastestLapAchievers = new List<string>() };

                foreach (var race in races)
                {
                    var fastestConstructor = race.Results.Where(r => r.FastestLap.rank == "1").Select(r => r.Constructor).First();
                    var fastestLapper = _nameHelper.GetConstructorName(fastestConstructor);

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonFastestLapModel.FastestLapAchievers.Where(driver => driver == fastestLapper).Any())
                        {
                            newUniqueSeasonFastestLapModel.FastestLapAchievers.Add(fastestLapper);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniqueConstructorFastestLaps.Add(newUniqueSeasonFastestLapModel); 
                }
            });

            return uniqueConstructorFastestLaps;
        }
    }
}
