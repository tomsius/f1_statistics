using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.DataAggregation
{
    public class PolesAggregator : IPolesAggregator
    {
        private IQualifyingDataAccess _qualifyingDataAccess;
        private IResultsDataAccess _resultsDataAccess;

        public PolesAggregator(IQualifyingDataAccess qualifyingDataAccess, IResultsDataAccess resultsDataAccess)
        {
            _qualifyingDataAccess = qualifyingDataAccess;
            _resultsDataAccess = resultsDataAccess;
        }

        public List<PolesModel> GetPoleSittersDrivers(int from, int to)
        {
            var driversPoles = new List<PolesModel>();

            for (int year = from; year <= to; year++)
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                foreach (var qualifying in qualifyings)
                {
                    string poleSitter = $"{qualifying.QualifyingResults[0].Driver.givenName} {qualifying.QualifyingResults[0].Driver.familyName}";

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

            return driversPoles;
        }

        public List<PolesModel> GetPoleSittersConstructors(int from, int to)
        {
            var constructorsPoles = new List<PolesModel>();

            for (int year = from; year <= to; year++)
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                foreach (var qualifying in qualifyings)
                {
                    string poleSitter = $"{qualifying.QualifyingResults[0].Constructor.name}";

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

            return constructorsPoles;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersDrivers(int from, int to)
        {
            var uniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>();

            for (int year = from; year <= to; year++)
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, PoleSitters = new List<string>() };
                uniquePoleSittersDrivers.Add(newUniqueSeasonPoleSittersModel);

                foreach (var qualifying in qualifyings)
                {
                    string poleSitter = $"{qualifying.QualifyingResults[0].Driver.givenName} {qualifying.QualifyingResults[0].Driver.familyName}";

                    if (!uniquePoleSittersDrivers[year - from].PoleSitters.Where(winner => winner == poleSitter).Any())
                    {
                        uniquePoleSittersDrivers[year - from].PoleSitters.Add(poleSitter);
                    }
                }
            }

            return uniquePoleSittersDrivers;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersConstructors(int from, int to)
        {
            var uniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel>();

            for (int year = from; year <= to; year++)
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, PoleSitters = new List<string>() };
                uniquePoleSittersConstructors.Add(newUniqueSeasonPoleSittersModel);

                foreach (var qualifying in qualifyings)
                {
                    string winnerName = $"{qualifying.QualifyingResults[0].Constructor.name}";

                    if (!uniquePoleSittersConstructors[year - from].PoleSitters.Where(winner => winner == winnerName).Any())
                    {
                        uniquePoleSittersConstructors[year - from].PoleSitters.Add(winnerName);
                    }
                }
            }

            return uniquePoleSittersConstructors;
        }

        public List<WinnersFromPoleModel> GetWinnersFromPole(int from, int to)
        {
            var winsFromPole = new List<WinnersFromPoleModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                var newWinnersFromPoleModel = new WinnersFromPoleModel { Season = year, WinnersFromPole = new List<string>() };
                winsFromPole.Add(newWinnersFromPoleModel);

                for (int i = 0; i < qualifyings.Count; i++)
                {
                    string poleSitter = $"{qualifyings[i].QualifyingResults[0].Driver.givenName} {qualifyings[i].QualifyingResults[0].Driver.familyName}";
                    var winner = races.Where(race => race.round == qualifyings[i].round).First().Results[0].Driver;
                    string winnerName = $"{winner.givenName} {winner.familyName}";

                    if (winnerName == poleSitter)
                    {
                        winsFromPole.Where(x => x.Season == year).First().WinnersFromPole.Add(winnerName);
                    }
                }
            }

            return winsFromPole;
        }
    }
}
