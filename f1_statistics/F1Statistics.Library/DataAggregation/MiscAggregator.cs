using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class MiscAggregator : IMiscAggregator
    {
        private const string FINISHED_STATUS = "Finished";
        private const string LAPPED_STATUS = "+";
        private const string FASTEST = "1";

        private readonly IRacesDataAccess _racesDataAccess;
        private readonly IResultsDataAccess _resultsDataAccess;
        private readonly IQualifyingDataAccess _qualifyingDataAccess;
        private readonly ILapsDataAccess _lapsDataAccess;

        public MiscAggregator(IRacesDataAccess racesDataAccess, 
                              IResultsDataAccess resultsDataAccess, 
                              IQualifyingDataAccess qualifyingDataAccess,
                              ILapsDataAccess lapsDataAccess)
        {
            _racesDataAccess = racesDataAccess;
            _resultsDataAccess = resultsDataAccess;
            _qualifyingDataAccess = qualifyingDataAccess;
            _lapsDataAccess = lapsDataAccess;
        }

        public List<SeasonRacesModel> GetRaceCountPerSeason(int from, int to)
        {
            var seasonRaces = new List<SeasonRacesModel>(to - from + 1);
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var raceCount = _racesDataAccess.GetRacesCountFrom(year);

                lock (lockAdd)
                {
                    var newSeasonRacesModel = new SeasonRacesModel { Season = year, RaceCount = raceCount };
                    seasonRaces.Add(newSeasonRacesModel);
                }
            });

            return seasonRaces;
        }

        public List<HatTrickModel> GetHatTricks(int from, int to)
        {
            var hatTricks = new List<HatTrickModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                for (int i = 0; i < qualifyings.Count; i++)
                {
                    var poleSitter = $"{qualifyings[i].QualifyingResults[0].Driver.givenName} {qualifyings[i].QualifyingResults[0].Driver.familyName}";

                    var winner = races.Where(race => race.round == qualifyings[i].round).First().Results[0].Driver;
                    var winnerName = $"{winner.givenName} {winner.familyName}";

                    var fastestRank = races.Where(race => race.round == qualifyings[i].round).First().Results[0].FastestLap.rank;

                    if (poleSitter == winnerName && fastestRank == FASTEST)
                    {
                        lock (lockObject)
                        {
                            if (!hatTricks.Where(driver => driver.Name == winnerName).Any())
                            {
                                var newHatTrickModel = new HatTrickModel { Name = winnerName, HatTrickCount = 1 };
                                hatTricks.Add(newHatTrickModel); 
                            }
                            else
                            {
                                hatTricks.Where(driver => driver.Name == winnerName).First().HatTrickCount++;
                            }
                        }
                    }
                }
            });

            return hatTricks;
        }

        public List<GrandSlamModel> GetGrandSlams(int from, int to)
        {
            var grandSlams = new List<GrandSlamModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);
                var qualifyings = _qualifyingDataAccess.GetQualifyingsFrom(year);

                for (int i = 0; i < qualifyings.Count; i++)
                {
                    var poleSitter = $"{qualifyings[i].QualifyingResults[0].Driver.givenName} {qualifyings[i].QualifyingResults[0].Driver.familyName}";

                    var winner = races.Where(race => race.round == qualifyings[i].round).First().Results[0].Driver;
                    var winnerName = $"{winner.givenName} {winner.familyName}";

                    var fastestRank = races.Where(race => race.round == qualifyings[i].round).First().Results[0].FastestLap.rank;

                    if (poleSitter == winnerName && fastestRank == FASTEST) 
                    {
                        var laps = _lapsDataAccess.GetLapsFrom(year, int.Parse(qualifyings[i].round));
                        var leaderId = winner.driverId;
                        var isLeadingAllLaps = true;

                        foreach (var lap in laps)
                        {
                            if (lap.Timings[0].driverId != leaderId)
                            {
                                isLeadingAllLaps = false;
                                break;
                            }
                        }

                        if (isLeadingAllLaps)
                        {
                            lock (lockObject)
                            {
                                if (!grandSlams.Where(driver => driver.Name == winnerName).Any())
                                {
                                    var newHatTrickModel = new GrandSlamModel { Name = winnerName, GrandSlamCount = 1 };
                                    grandSlams.Add(newHatTrickModel);
                                }
                                else
                                {
                                    grandSlams.Where(driver => driver.Name == winnerName).First().GrandSlamCount++;
                                }
                            }
                        }
                    }
                }
            });

            return grandSlams;
        }

        public List<DidNotFinishModel> GetNonFinishers(int from, int to)
        {
            var nonFinishers = new List<DidNotFinishModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    foreach (var result in race.Results)
                    {
                        var status = result.status;

                        if (status != FINISHED_STATUS && !status.Contains(LAPPED_STATUS))
                        {
                            var driverName = $"{result.Driver.givenName} {result.Driver.familyName}";

                            lock (lockObject)
                            {
                                if (!nonFinishers.Where(driver => driver.Name == driverName).Any())
                                {
                                    var newDidNotFinishModel = new DidNotFinishModel { Name = driverName, DidNotFinishCount = 1 };
                                    nonFinishers.Add(newDidNotFinishModel); 
                                }
                                else
                                {
                                    nonFinishers.Where(driver => driver.Name == driverName).First().DidNotFinishCount++;
                                }
                            }
                        }
                    }
                }
            });

            return nonFinishers;
        }

        public List<SeasonPositionChangesModel> GetSeasonPositionChanges(int from, int to)
        {
            var positionChanges = new List<SeasonPositionChangesModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var seasonPositionChanges = new SeasonPositionChangesModel { Season = year, PositionChanges = new List<DriverPositionChangeModel>() };

                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    foreach (var result in race.Results)
                    {
                        var driver = result.Driver;
                        var driverName = $"{driver.givenName} {driver.familyName}";

                        var gridPosition = int.Parse(result.grid);
                        var finishPosition = int.Parse(result.position);
                        var positionChange = gridPosition - finishPosition;

                        if (!seasonPositionChanges.PositionChanges.Where(driver => driver.Name == driverName).Any())
                        {
                            var newDriverPositionChangeModel = new DriverPositionChangeModel { Name = driverName, PositionChange = positionChange };
                            seasonPositionChanges.PositionChanges.Add(newDriverPositionChangeModel);
                        }
                        else
                        {
                            seasonPositionChanges.PositionChanges.Where(driver => driver.Name == driverName).First().PositionChange += positionChange;
                        }
                    }
                }

                lock (lockAdd)
                {
                    positionChanges.Add(seasonPositionChanges);
                }
            });

            return positionChanges;
        }
    }
}
