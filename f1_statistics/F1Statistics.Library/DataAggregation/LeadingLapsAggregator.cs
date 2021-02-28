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
        // 1. http://ergast.com/api/f1/{year}/results.json?limit=1000 - pasiimti sezono rezultatus, kad suzinociau roundu skaiciu
        // 2. http://ergast.com/api/f1/{year}/{round}/laps.json?limit=2000 - pasiimti kiekvieno rato pirmo lenktynininko driverId
        // 3. http://ergast.com/api/f1/drivers.json?limit=1000 - pagal driverId susirandu lenktynininko varda ir pavarde
        // 4. http://ergast.com/api/f1/2020/1/drivers/vettel/constructors.json?limit=1000 - pasiimti lenktynininko id pasiimti construcot pavadinima

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
                var rounds = _resultsDataAccess.GetRacesFrom(year).Count;

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
                            if (!driversLeadingLapsCount.Where(driver => driver.Name == leadingDriver).Any())
                            {
                                var newLeadingLapsModel = new LeadingLapsModel { Name = leadingDriver, LeadingLapCount = 1 };
                                driversLeadingLapsCount.Add(newLeadingLapsModel);
                            }
                            else
                            {
                                driversLeadingLapsCount.Where(driver => driver.Name == leadingDriver).First().LeadingLapCount++;
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
                var rounds = _resultsDataAccess.GetRacesFrom(year).Count;

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
                            if (!constructorsLeadingLapsCount.Where(constructor => constructor.Name == leadingConstructor).Any())
                            {
                                var newLeadingLapsModel = new LeadingLapsModel { Name = leadingConstructor, LeadingLapCount = 1 };
                                constructorsLeadingLapsCount.Add(newLeadingLapsModel);
                            }
                            else
                            {
                                constructorsLeadingLapsCount.Where(constructor => constructor.Name == leadingConstructor).First().LeadingLapCount++;
                            }
                        }
                    });
                }
            }

            return constructorsLeadingLapsCount;
        }
    }
}
