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
    public class NationalitiesAggregator : INationalitiesAggregator
    {
        private readonly IDriversDataAccess _driversDataAccess;
        private readonly IResultsDataAccess _resultsDataAccess;
        private readonly IStandingsDataAccess _standingsDataAccess;
        private readonly INameHelper _nameHelper;

        public NationalitiesAggregator(IDriversDataAccess driversDataAccess, IResultsDataAccess resultsDataAccess, IStandingsDataAccess standingsDataAccess, INameHelper nameHelper)
        {
            _driversDataAccess = driversDataAccess;
            _resultsDataAccess = resultsDataAccess;
            _standingsDataAccess = standingsDataAccess;
            _nameHelper = nameHelper;
        }

        public List<NationalityDriversModel> GetDriversNationalities(int from, int to)
        {
            var driversNationalities = new List<NationalityDriversModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var drivers = _driversDataAccess.GetDriversFrom(year);

                foreach (var driver in drivers)
                {
                    var driverNationality = driver.nationality;
                    var driverName = _nameHelper.GetDriverName(driver);

                    lock (lockObject)
                    {
                        if (!driversNationalities.Where(nationality => nationality.Nationality == driverNationality).Any())
                        {
                            var newNationalityDriversModel = new NationalityDriversModel { Nationality = driverNationality, Drivers = new List<string> { driverName } };
                            driversNationalities.Add(newNationalityDriversModel);
                        }
                        else
                        {
                            if (!driversNationalities.Where(nationality => nationality.Nationality == driverNationality).First().Drivers.Contains(driverName))
                            {
                                driversNationalities.Where(nationality => nationality.Nationality == driverNationality).First().Drivers.Add(driverName);
                            }
                        }
                    }
                }
            });

            return driversNationalities;
        }

        public List<NationalityWinsModel> GetNationalitiesRaceWins(int from, int to)
        {
            var nationalitiesRaceWins = new List<NationalityWinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var raceWinner = race.Results[0].Driver;
                    var raceWinnerNationality = raceWinner.nationality;
                    var raceWinnerName = _nameHelper.GetDriverName(raceWinner);

                    lock (lockObject)
                    {
                        if (!nationalitiesRaceWins.Where(nationality => nationality.Nationality == raceWinnerNationality).Any())
                        {
                            var newNationalityWinsModel = new NationalityWinsModel { Nationality = raceWinnerNationality, Winners = new List<string> { raceWinnerName } };
                            nationalitiesRaceWins.Add(newNationalityWinsModel);
                        }
                        else
                        {
                            nationalitiesRaceWins.Where(nationality => nationality.Nationality == raceWinnerNationality).First().Winners.Add(raceWinnerName);
                        }
                    }
                }
            });

            return nationalitiesRaceWins;
        }

        public List<NationalityWinsModel> GetNationalitiesSeasonWins(int from, int to)
        {
            var nationalitiesSeasonWins = new List<NationalityWinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var standings = _standingsDataAccess.GetDriverStandingsFrom(year);

                if (standings.Count == 0)
                {
                    return;
                }

                var seasonWinner = standings[0].Driver;
                var seasonWinnerNationality = seasonWinner.nationality;
                var seasonWinnerName = _nameHelper.GetDriverName(seasonWinner);

                lock (lockObject)
                {
                    if (!nationalitiesSeasonWins.Where(nationality => nationality.Nationality == seasonWinnerNationality).Any())
                    {
                        var newNationalityWinsModel = new NationalityWinsModel { Nationality = seasonWinnerNationality, Winners = new List<string> { seasonWinnerName } };
                        nationalitiesSeasonWins.Add(newNationalityWinsModel);
                    }
                    else
                    {
                        nationalitiesSeasonWins.Where(nationality => nationality.Nationality == seasonWinnerNationality).First().Winners.Add(seasonWinnerName);
                    }
                }
            });

            return nationalitiesSeasonWins;
        }
    }
}
