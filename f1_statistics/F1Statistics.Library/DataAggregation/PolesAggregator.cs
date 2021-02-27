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
        private readonly IResultsDataAccess _resultsDataAccess;

        public PolesAggregator(IQualifyingDataAccess qualifyingDataAccess, IResultsDataAccess resultsDataAccess)
        {
            _qualifyingDataAccess = qualifyingDataAccess;
            _resultsDataAccess = resultsDataAccess;
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
                    string poleSitter = $"{qualifying.QualifyingResults[0].Driver.givenName} {qualifying.QualifyingResults[0].Driver.familyName}";

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
                    string poleSitter = $"{qualifying.QualifyingResults[0].Constructor.name}";

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

            Parallel.For(from, to + 1, year =>
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, PoleSitters = new List<string>() };

                foreach (var qualifying in qualifyings)
                {
                    string poleSitter = $"{qualifying.QualifyingResults[0].Driver.givenName} {qualifying.QualifyingResults[0].Driver.familyName}";

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonPoleSittersModel.PoleSitters.Where(winner => winner == poleSitter).Any())
                        {
                            newUniqueSeasonPoleSittersModel.PoleSitters.Add(poleSitter);
                        } 
                    }
                }

                uniquePoleSittersDrivers.Add(newUniqueSeasonPoleSittersModel);
            });

            return uniquePoleSittersDrivers;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersConstructors(int from, int to)
        {
            var uniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year => 
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, PoleSitters = new List<string>() };

                foreach (var qualifying in qualifyings)
                {
                    string winnerName = $"{qualifying.QualifyingResults[0].Constructor.name}";

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonPoleSittersModel.PoleSitters.Where(winner => winner == winnerName).Any())
                        {
                            newUniqueSeasonPoleSittersModel.PoleSitters.Add(winnerName);
                        } 
                    }
                }

                uniquePoleSittersConstructors.Add(newUniqueSeasonPoleSittersModel);
            });

            return uniquePoleSittersConstructors;
        }

        public List<WinnersFromPoleModel> GetWinnersFromPole(int from, int to)
        {
            var winsFromPole = new List<WinnersFromPoleModel>(to - from + 1);

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetRacesFrom(year);
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newWinnersFromPoleModel = new WinnersFromPoleModel { Season = year, WinnersFromPole = new List<string>() };

                for (int i = 0; i < qualifyings.Count; i++)
                {
                    string poleSitter = $"{qualifyings[i].QualifyingResults[0].Driver.givenName} {qualifyings[i].QualifyingResults[0].Driver.familyName}";
                    var winner = races.Where(race => race.round == qualifyings[i].round).First().Results[0].Driver;
                    string winnerName = $"{winner.givenName} {winner.familyName}";

                    if (winnerName == poleSitter)
                    {
                        newWinnersFromPoleModel.WinnersFromPole.Add(winnerName);
                    }
                }

                winsFromPole.Add(newWinnersFromPoleModel);
            });

            return winsFromPole;
        }
    }
}
