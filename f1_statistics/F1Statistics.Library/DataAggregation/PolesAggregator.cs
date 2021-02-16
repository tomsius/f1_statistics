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
                        driversPoles.Add(new PolesModel { Name = poleSitter, PoleCount = 1 });
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
                        constructorsPoles.Add(new PolesModel { Name = poleSitter, PoleCount = 1 });
                    }
                    else
                    {
                        constructorsPoles.Where(constructor => constructor.Name == poleSitter).First().PoleCount++;
                    }
                }
            }

            return constructorsPoles;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniqueDriverPoleSitters(int from, int to)
        {
            var uniquePoleSitters = new List<UniqueSeasonPoleSittersModel>();

            for (int year = from; year <= to; year++)
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                uniquePoleSitters.Add(new UniqueSeasonPoleSittersModel { Season = year, PoleSitters = new List<string>() });

                foreach (var qualifying in qualifyings)
                {
                    string poleSitter = $"{qualifying.QualifyingResults[0].Driver.givenName} {qualifying.QualifyingResults[0].Driver.familyName}";

                    if (!uniquePoleSitters[year - from].PoleSitters.Where(winner => winner == poleSitter).Any())
                    {
                        uniquePoleSitters[year - from].PoleSitters.Add(poleSitter);
                    }
                }
            }

            return uniquePoleSitters;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniqueConstructorPoleSitters(int from, int to)
        {
            var uniquePoleSitters = new List<UniqueSeasonPoleSittersModel>();

            for (int year = from; year <= to; year++)
            {
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                uniquePoleSitters.Add(new UniqueSeasonPoleSittersModel { Season = year, PoleSitters = new List<string>() });

                foreach (var qualifying in qualifyings)
                {
                    string winnerName = $"{qualifying.QualifyingResults[0].Constructor.name}";

                    if (!uniquePoleSitters[year - from].PoleSitters.Where(winner => winner == winnerName).Any())
                    {
                        uniquePoleSitters[year - from].PoleSitters.Add(winnerName);
                    }
                }
            }

            return uniquePoleSitters;
        }

        public List<WinsFromPoleModel> GetWinCountFromPole(int from, int to)
        {
            var winsFromPole = new List<WinsFromPoleModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                winsFromPole.Add(new WinsFromPoleModel { Season = year, WinnersFromPole = new List<string>() });

                for (int i = 0; i < races.Count; i++)
                {
                    string winnerName = $"{races[i].Results[0].Driver.givenName} {races[i].Results[0].Driver.familyName}";
                    string poleSitter = $"{qualifyings[i].QualifyingResults[0].Driver.givenName} {qualifyings[i].QualifyingResults[0].Driver.familyName}";

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
