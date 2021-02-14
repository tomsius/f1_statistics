using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.DataAggregation
{
    public class WinsAggregator : IWinsAggregator
    {
        private IResultsDataAccess _resultsDataAccess;

        public WinsAggregator(IResultsDataAccess resultsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
        }

        public List<WinsModel> GetDriversWins(int from, int to)
        {
            var driversWins = new List<WinsModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    string winner = $"{race.Results[0].Driver.givenName} {race.Results[0].Driver.familyName}";

                    if (!driversWins.Where(driver => driver.Name == winner).Any())
                    {
                        driversWins.Add(new WinsModel { Name = winner, WinCount = 1 });
                    }
                    else
                    {
                        driversWins.Where(driver => driver.Name == winner).First().WinCount++;
                    }
                }
            }

            return driversWins;
        }

        public List<WinsModel> GetConstructorsWins(int from, int to)
        {
            var constructorsWins = new List<WinsModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    string winner = $"{race.Results[0].Constructor.name}";

                    if (!constructorsWins.Where(constructor => constructor.Name == winner).Any())
                    {
                        constructorsWins.Add(new WinsModel { Name = winner, WinCount = 1 });
                    }
                    else
                    {
                        constructorsWins.Where(constructor => constructor.Name == winner).First().WinCount++;
                    }
                }
            }

            return constructorsWins;
        }

        public List<AverageWinsModel> GetDriversAverageWins(int from, int to)
        {
            var driversAverageWins = new List<AverageWinsModel>();

            for (int year = from; year <= to; year++)
            {
                var races = _resultsDataAccess.GetRacesFrom(year);

                foreach (var race in races)
                {
                    // Fill participations
                    foreach (var result in race.Results)
                    {
                        string driverName = $"{result.Driver.givenName} {result.Driver.familyName}";

                        if (!driversAverageWins.Where(driver => driver.Name == driverName).Any())
                        {
                            driversAverageWins.Add(new AverageWinsModel { Name = driverName, ParticipationCount = 1 });
                        }
                        else
                        {
                            driversAverageWins.Where(driver => driver.Name == driverName).First().ParticipationCount++;
                        }
                    }

                    // Fill winner
                    string winner = $"{race.Results[0].Driver.givenName} {race.Results[0].Driver.familyName}";

                    driversAverageWins.Where(driver => driver.Name == winner).First().WinCount++;
                }
            }

            return driversAverageWins;
        }
    }
}
