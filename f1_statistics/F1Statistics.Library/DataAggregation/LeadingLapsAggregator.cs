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
    public class LeadingLapsAggregator : ILeadingLapsAggregator
    {
        private readonly IResultsDataAccess _resultsDataAccess;
        private readonly ILapsDataAccess _lapsDataAccess;
        private readonly IDriversDataAccess _driversDataAccess;
        private readonly IConstructorsDataAccess _constructorsDataAccess;

        public LeadingLapsAggregator(IResultsDataAccess resultsDataAccess, ILapsDataAccess lapsDataAccess, IDriversDataAccess driversDataAccess, IConstructorsDataAccess constructorsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
            _lapsDataAccess = lapsDataAccess;
            _driversDataAccess = driversDataAccess;
            _constructorsDataAccess = constructorsDataAccess;
        }

        public List<LeadingLapsModel> GetDriversLeadingLapsCount(int from, int to)
        {
            var driversLeadingLapsCount = new List<LeadingLapsModel>(to - from + 1);
            var driversNames = new Dictionary<string, string>();
            var lockObject = new object();
            var lockCheck = new object();

            for (int year = from; year <= to; year++)
            {
                var rounds = _resultsDataAccess.GetResultsFrom(year).Count;

                for (int round = 1; round <= rounds; round++)
                {
                    var laps = _lapsDataAccess.GetLapsFrom(year, round);

                    Parallel.ForEach(laps, lap =>
                    {
                        var leadingDriverId = lap.Timings[0].driverId;
                        string leadingDriver;

                        lock (lockCheck)
                        {
                            if (driversNames.ContainsKey(leadingDriverId))
                            {
                                leadingDriver = driversNames[leadingDriverId];
                            }
                            else
                            {
                                leadingDriver = _driversDataAccess.GetDriverName(leadingDriverId);
                                driversNames.Add(leadingDriverId, leadingDriver);
                            }
                        }

                        lock (lockObject)
                        {
                            var newLeadingLapsByYearModel = new LeadingLapsByYearModel { Year = year, LeadingLapCount = 1 };

                            if (!driversLeadingLapsCount.Where(driver => driver.Name == leadingDriver).Any())
                            {
                                var newLeadingLapsModel = new LeadingLapsModel { Name = leadingDriver, LeadingLapsByYear = new List<LeadingLapsByYearModel> { newLeadingLapsByYearModel } };
                                driversLeadingLapsCount.Add(newLeadingLapsModel);
                            }
                            else
                            {
                                var driver = driversLeadingLapsCount.Where(driver => driver.Name == leadingDriver).First();

                                if (!driver.LeadingLapsByYear.Where(model => model.Year == year).Any())
                                {
                                    driver.LeadingLapsByYear.Add(newLeadingLapsByYearModel);
                                }
                                else
                                {
                                    driver.LeadingLapsByYear.Where(model => model.Year == year).First().LeadingLapCount++;
                                }
                            }
                        }
                    });
                }
            }

            return driversLeadingLapsCount;
        }

        public List<LeadingLapsModel> GetConstructorsLeadingLapsCount(int from, int to)
        {
            var constructorsLeadingLapsCount = new List<LeadingLapsModel>(to - from + 1);
            var constructorNames = new Dictionary<string, string>();
            var lockObject = new object();
            var lockCheck = new object();

            for (int year = from; year <= to; year++)
            {
                var rounds = _resultsDataAccess.GetResultsFrom(year).Count;

                for (int round = 1; round <= rounds; round++)
                {
                    var laps = _lapsDataAccess.GetLapsFrom(year, round);

                    Parallel.ForEach(laps, lap =>
                    {
                        var leadingDriverId = lap.Timings[0].driverId;
                        string leadingConstructor;

                        lock (lockCheck)
                        {
                            if (constructorNames.ContainsKey(leadingDriverId))
                            {
                                leadingConstructor = constructorNames[leadingDriverId];
                            }
                            else
                            {
                                leadingConstructor = _constructorsDataAccess.GetDriverConstructor(year, round, leadingDriverId);
                                constructorNames.Add(leadingDriverId, leadingConstructor);
                            }
                        }

                        lock (lockObject)
                        {
                            var newLeadingLapsByYearModel = new LeadingLapsByYearModel { Year = year, LeadingLapCount = 1 };

                            if (!constructorsLeadingLapsCount.Where(constructor => constructor.Name == leadingConstructor).Any())
                            {
                                var newLeadingLapsModel = new LeadingLapsModel { Name = leadingConstructor, LeadingLapsByYear = new List<LeadingLapsByYearModel> { newLeadingLapsByYearModel } };
                                constructorsLeadingLapsCount.Add(newLeadingLapsModel);
                            }
                            else
                            {
                                var constructor = constructorsLeadingLapsCount.Where(constructor => constructor.Name == leadingConstructor).First();

                                if (!constructor.LeadingLapsByYear.Where(model => model.Year == year).Any())
                                {
                                    constructor.LeadingLapsByYear.Add(newLeadingLapsByYearModel);
                                }
                                else
                                {
                                    constructor.LeadingLapsByYear.Where(model => model.Year == year).First().LeadingLapCount++;
                                }
                            }
                        }
                    });
                }
            }

            return constructorsLeadingLapsCount;
        }
    }
}
