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
        private readonly IStandingsDataAccess _standingsDataAccess;
        private readonly IDriversDataAccess _driversDataAccess;

        public MiscAggregator(IRacesDataAccess racesDataAccess, 
                              IResultsDataAccess resultsDataAccess, 
                              IQualifyingDataAccess qualifyingDataAccess,
                              ILapsDataAccess lapsDataAccess,
                              IStandingsDataAccess standingsDataAccess,
                              IDriversDataAccess driversDataAccess)
        {
            _racesDataAccess = racesDataAccess;
            _resultsDataAccess = resultsDataAccess;
            _qualifyingDataAccess = qualifyingDataAccess;
            _lapsDataAccess = lapsDataAccess;
            _standingsDataAccess = standingsDataAccess;
            _driversDataAccess = driversDataAccess;
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

        public List<FrontRowModel> GetConstructorsFrontRows(int from, int to)
        {
            var constructorsWithFrontRow = new List<FrontRowModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var firstOnGrid = $"{race.Results.Where(result => result.grid == "1").FirstOrDefault()?.Constructor.name}";
                    var secondOnGrid = $"{race.Results.Where(result => result.grid == "2").FirstOrDefault()?.Constructor.name}";

                    if (firstOnGrid == secondOnGrid && firstOnGrid != null)
                    {
                        lock (lockObject)
                        {
                            if (!constructorsWithFrontRow.Where(constructor => constructor.Name == firstOnGrid).Any())
                            {
                                var newFrontRowModel = new FrontRowModel { Name = firstOnGrid, FrontRowCount = 1 };
                                constructorsWithFrontRow.Add(newFrontRowModel);
                            }
                            else
                            {
                                constructorsWithFrontRow.Where(constructor => constructor.Name == firstOnGrid).First().FrontRowCount++;
                            }
                        }
                    }
                }
            });

            return constructorsWithFrontRow;
        }

        public List<DriverFinishingPositionsModel> GetDriversFinishingPositions(int from, int to)
        {
            var driversFinishingPositions = new List<DriverFinishingPositionsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    foreach (var result in race.Results)
                    {
                        var driver = result.Driver;
                        var driverName = $"{driver.givenName} {driver.familyName}";
                        var driverFinishingPosition = int.Parse(result.position);

                        lock (lockObject)
                        {
                            if (!driversFinishingPositions.Where(driver => driver.Name == driverName).Any())
                            {
                                var newFinishingPositionModel = new FinishingPositionModel { FinishingPosition = driverFinishingPosition, Count = 1 };
                                var newFinishingPositionModelList = new List<FinishingPositionModel> { newFinishingPositionModel };
                                var newDriverFinishingPositionsModel = new DriverFinishingPositionsModel { Name = driverName, FinishingPositions = newFinishingPositionModelList };

                                driversFinishingPositions.Add(newDriverFinishingPositionsModel);
                            }
                            else
                            {
                                var driverFinishingPositionsList = driversFinishingPositions.Where(driver => driver.Name == driverName).First().FinishingPositions;

                                if (!driverFinishingPositionsList.Where(positions => positions.FinishingPosition == driverFinishingPosition).Any())
                                {
                                    var newFinishingPositionModel = new FinishingPositionModel { FinishingPosition = driverFinishingPosition, Count = 1 };

                                    driverFinishingPositionsList.Add(newFinishingPositionModel);
                                }
                                else
                                {
                                    driverFinishingPositionsList.Where(position => position.FinishingPosition == driverFinishingPosition).First().Count++;
                                }
                            }
                        }
                    }
                }
            });

            return driversFinishingPositions;
        }

        public List<SeasonStandingsChangesModel> GetDriversStandingsChanges(int from, int to)
        {
            var driversStandingsChanges = new List<SeasonStandingsChangesModel>(to - from + 1);
            var lockAdd = new object();

            for (int year = from; year <= to; year++)
            {
                var newSeasonStandingsChangesModel = new SeasonStandingsChangesModel { Season = year, Rounds = new List<RoundModel>() };

                var racesCount = _racesDataAccess.GetRacesCountFrom(year);

                Parallel.For(1, racesCount + 1, round =>
                {
                    var standings = _standingsDataAccess.GetDriverStandingsFromRace(year, round);

                    var newRoundModel = new RoundModel { Round = round, Standings = new List<StandingModel>() };

                    for (int i = 0; i < standings.Count; i++)
                    {
                        var driverName = $"{standings[i].Driver.givenName} {standings[i].Driver.familyName}";
                        var points = double.Parse(standings[i].points);
                        var position = i + 1;

                        var newStandingModel = new StandingModel { Name = driverName, Points = points, Position = position };
                        newRoundModel.Standings.Add(newStandingModel);
                    }

                    lock (lockAdd)
                    {
                        newSeasonStandingsChangesModel.Rounds.Add(newRoundModel);
                    }
                });

                driversStandingsChanges.Add(newSeasonStandingsChangesModel);
            }

            return driversStandingsChanges;
        }

        public List<SeasonStandingsChangesModel> GetConstructorsStandingsChanges(int from, int to)
        {
            var constructorsStandingsChanges = new List<SeasonStandingsChangesModel>(to - from + 1);
            var lockAdd = new object();

            for (int year = from; year <= to; year++)
            {
                var newSeasonStandingsChangesModel = new SeasonStandingsChangesModel { Season = year, Rounds = new List<RoundModel>() };

                var racesCount = _racesDataAccess.GetRacesCountFrom(year);

                Parallel.For(1, racesCount + 1, round =>
                {
                    var standings = _standingsDataAccess.GetConstructorStandingsFromRace(year, round);

                    var newRoundModel = new RoundModel { Round = round, Standings = new List<StandingModel>() };

                    for (int i = 0; i < standings.Count; i++)
                    {
                        var constructorName = $"{standings[i].Constructor.name}";
                        var points = double.Parse(standings[i].points);
                        var position = i + 1;

                        var newStandingModel = new StandingModel { Name = constructorName, Points = points, Position = position };
                        newRoundModel.Standings.Add(newStandingModel);
                    }

                    lock (lockAdd)
                    {
                        newSeasonStandingsChangesModel.Rounds.Add(newRoundModel);
                    }
                });

                constructorsStandingsChanges.Add(newSeasonStandingsChangesModel);
            }

            return constructorsStandingsChanges;
        }

        public List<RacePositionChangesModel> GetDriversPositionChangesDuringRace(int season, int race)
        {
            var laps = _lapsDataAccess.GetLapsFrom(season, race);
            var driversPositionChangesDuringRace = new List<RacePositionChangesModel>(laps.Count);
            var lockAdd = new object();

            Parallel.ForEach(laps, lap =>
            {
                var lapNumber = int.Parse(lap.number);
                var newRacePositionChangesModel = new RacePositionChangesModel { LapNumber = lapNumber, DriversPositions = new List<DriverPositionModel>() };

                foreach (var timing in lap.Timings)
                {
                    var driverName = _driversDataAccess.GetDriverName(timing.driverId);
                    var position = int.Parse(timing.position);

                    var newDriverPositionModel = new DriverPositionModel { Name = driverName, Position = position };
                    newRacePositionChangesModel.DriversPositions.Add(newDriverPositionModel);
                }

                lock (lockAdd)
                {
                    driversPositionChangesDuringRace.Add(newRacePositionChangesModel);
                }
            });

            return driversPositionChangesDuringRace;
        }
    }
}
