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
    public class PolesAggregator : IPolesAggregator
    {
        private readonly IQualifyingDataAccess _qualifyingDataAccess;

        public PolesAggregator(IQualifyingDataAccess qualifyingDataAccess)
        {
            _qualifyingDataAccess = qualifyingDataAccess;
        }

        public List<PolesModel> GetPoleSittersDrivers(int from, int to)
        {
            var driversPoles = new List<PolesModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                foreach (var qualifying in qualifyings)
                {
                    var poleSitter = $"{qualifying.QualifyingResults[0].Driver.givenName} {qualifying.QualifyingResults[0].Driver.familyName}";

                    lock (lockObject)
                    {
                        if (!driversPoles.Where(driver => driver.Name == poleSitter).Any())
                        {
                            var newPolesModel = new PolesModel { Name = poleSitter, PoleCount = 1 };
                            driversPoles.Add(newPolesModel);
                        }
                        else
                        {
                            driversPoles.Where(driver => driver.Name == poleSitter).First().PoleCount++;
                        } 
                    }
                }
            });

            return driversPoles;
        }

        public List<PolesModel> GetPoleSittersConstructors(int from, int to)
        {
            var constructorsPoles = new List<PolesModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                foreach (var qualifying in qualifyings)
                {
                    var poleSitter = $"{qualifying.QualifyingResults[0].Constructor.name}";

                    lock (lockObject)
                    {
                        if (!constructorsPoles.Where(constructor => constructor.Name == poleSitter).Any())
                        {
                            var newPolesModel = new PolesModel { Name = poleSitter, PoleCount = 1 };
                            constructorsPoles.Add(newPolesModel);
                        }
                        else
                        {
                            constructorsPoles.Where(constructor => constructor.Name == poleSitter).First().PoleCount++;
                        } 
                    }
                }
            });

            return constructorsPoles;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersDrivers(int from, int to)
        {
            var uniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, QualificationsCount = qualifyings.Count, PoleSitters = new List<string>() };

                foreach (var qualifying in qualifyings)
                {
                    var poleSitter = $"{qualifying.QualifyingResults[0].Driver.givenName} {qualifying.QualifyingResults[0].Driver.familyName}";

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonPoleSittersModel.PoleSitters.Where(winner => winner == poleSitter).Any())
                        {
                            newUniqueSeasonPoleSittersModel.PoleSitters.Add(poleSitter);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniquePoleSittersDrivers.Add(newUniqueSeasonPoleSittersModel); 
                }
            });

            return uniquePoleSittersDrivers;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersConstructors(int from, int to)
        {
            var uniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year => 
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, QualificationsCount = qualifyings.Count, PoleSitters = new List<string>() };

                foreach (var qualifying in qualifyings)
                {
                    var winnerName = $"{qualifying.QualifyingResults[0].Constructor.name}";

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonPoleSittersModel.PoleSitters.Where(winner => winner == winnerName).Any())
                        {
                            newUniqueSeasonPoleSittersModel.PoleSitters.Add(winnerName);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniquePoleSittersConstructors.Add(newUniqueSeasonPoleSittersModel); 
                }
            });

            return uniquePoleSittersConstructors;
        }
    }
}
