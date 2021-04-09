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
        private readonly IQualifyingDataAccess _qualifyingsDataAccess;
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
            _qualifyingsDataAccess = qualifyingDataAccess;
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
                var qualifyings = _qualifyingsDataAccess.GetQualifyingsFrom(year);

                for (int i = 0; i < qualifyings.Count; i++)
                {
                    var poleSitter = $"{qualifyings[i].QualifyingResults[0].Driver.givenName} {qualifyings[i].QualifyingResults[0].Driver.familyName}";

                    var winner = races.Where(race => race.round == qualifyings[i].round).First().Results[0].Driver;
                    var winnerName = $"{winner.givenName} {winner.familyName}";
                    var winnerNationality = winner.nationality;

                    var fastestRank = races.Where(race => race.round == qualifyings[i].round).First().Results[0].FastestLap.rank;

                    if (poleSitter == winnerName && fastestRank == FASTEST)
                    {
                        lock (lockObject)
                        {
                            if (!hatTricks.Where(driver => driver.Name == winnerName).Any())
                            {
                                var newHatTrickModel = new HatTrickModel { Name = winnerName, Nationality = winnerNationality, HatTrickCount = 1 };
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
                var qualifyings = _qualifyingsDataAccess.GetQualifyingsFrom(year);

                for (int i = 0; i < qualifyings.Count; i++)
                {
                    var poleSitter = $"{qualifyings[i].QualifyingResults[0].Driver.givenName} {qualifyings[i].QualifyingResults[0].Driver.familyName}";

                    var winner = races.Where(race => race.round == qualifyings[i].round).First().Results[0].Driver;
                    var winnerName = $"{winner.givenName} {winner.familyName}";
                    var winnerNationality = winner.nationality;

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
                                    var newHatTrickModel = new GrandSlamModel { Name = winnerName, Nationality = winnerNationality, GrandSlamCount = 1 };
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
                    var circuitName = race.Circuit.circuitName;

                    foreach (var result in race.Results)
                    {
                        var status = result.status;

                        if (status != FINISHED_STATUS && !status.Contains(LAPPED_STATUS))
                        {
                            var driverName = $"{result.Driver.givenName} {result.Driver.familyName}";
                            var lapsCompleted = int.Parse(result.laps);
                            var newDidNotFinishInformationModel = new DidNotFinishInformationModel { CircuitName = circuitName, LapsCompleted = lapsCompleted };

                            lock (lockObject)
                            {
                                var newDidNotFinishByYearModel = new DidNotFinishByYearModel { Year = year, DidNotFinishInformation = new List<DidNotFinishInformationModel> { newDidNotFinishInformationModel } };

                                if (!nonFinishers.Where(driver => driver.Name == driverName).Any())
                                {
                                    var newDidNotFinishModel = new DidNotFinishModel { Name = driverName, DidNotFinishByYear = new List<DidNotFinishByYearModel> { newDidNotFinishByYearModel } };
                                    nonFinishers.Add(newDidNotFinishModel); 
                                }
                                else
                                {
                                    var driver = nonFinishers.Where(driver => driver.Name == driverName).First();

                                    if (!driver.DidNotFinishByYear.Where(model => model.Year == year).Any())
                                    {
                                        driver.DidNotFinishByYear.Add(newDidNotFinishByYearModel);
                                    }
                                    else
                                    {
                                        driver.DidNotFinishByYear.Where(model => model.Year == year).First().DidNotFinishInformation.Add(newDidNotFinishInformationModel);
                                    }
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
                var seasonPositionChanges = new SeasonPositionChangesModel { Year = year, PositionChanges = new List<DriverPositionChangeModel>() };

                var races = _resultsDataAccess.GetResultsFrom(year);
                var standings = _standingsDataAccess.GetDriverStandingsFrom(year);

                foreach (var race in races)
                {
                    var circuitName = race.Circuit.circuitName;

                    foreach (var result in race.Results)
                    {
                        var driver = result.Driver;
                        var driverName = $"{driver.givenName} {driver.familyName}";
                        var gridPosition = int.Parse(result.grid);
                        var finishPosition = int.Parse(result.position);
                        var positionChange = gridPosition - finishPosition;
                        var newDriverPositionChangeInformationModel = new DriverPositionChangeInformationModel { CircuitName = circuitName, RacePositionChange = positionChange };

                        if (!seasonPositionChanges.PositionChanges.Where(driver => driver.Name == driverName).Any())
                        {
                            var newDriverPositionChangeModel = new DriverPositionChangeModel { Name = driverName, DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel> { newDriverPositionChangeInformationModel } };
                            seasonPositionChanges.PositionChanges.Add(newDriverPositionChangeModel);
                        }
                        else
                        {
                            seasonPositionChanges.PositionChanges.Where(driver => driver.Name == driverName).First().DriverPositionChangeInformation.Add(newDriverPositionChangeInformationModel);
                        }
                    }
                }

                for (int i = 0; i < standings.Count; i++)
                {
                    var driver = standings[i].Driver;
                    var driverName = $"{driver.givenName} {driver.familyName}";
                    var championshipPosition = i + 1;

                    seasonPositionChanges.PositionChanges.Where(driver => driver.Name == driverName).First().ChampionshipPosition = championshipPosition;
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
                var qualifyings = _qualifyingsDataAccess.GetQualifyingsFrom(year);

                foreach (var qualifying in qualifyings)
                {
                    var firstOnGrid = $"{qualifying.QualifyingResults.Where(result => result.position == "1").FirstOrDefault()?.Constructor.name}";
                    var secondOnGrid = $"{qualifying.QualifyingResults.Where(result => result.position == "2").FirstOrDefault()?.Constructor.name}";

                    if (firstOnGrid == secondOnGrid && firstOnGrid != null)
                    {
                        var circuitName = qualifying.Circuit.circuitName;
                        var newFrontRowInformationModel = new FrontRowInformationModel { CircuitName = circuitName, CircuitFrontRowCount = 1 };

                        lock (lockObject)
                        {
                            if (!constructorsWithFrontRow.Where(constructor => constructor.Name == firstOnGrid).Any())
                            {
                                var newFrontRowModel = new FrontRowModel { Name = firstOnGrid, FrontRowInformation = new List<FrontRowInformationModel> { newFrontRowInformationModel } };
                                constructorsWithFrontRow.Add(newFrontRowModel);
                            }
                            else
                            {
                                var frontRow = constructorsWithFrontRow.Where(constructor => constructor.Name == firstOnGrid).First();

                                if (!frontRow.FrontRowInformation.Where(circuit => circuit.CircuitName == circuitName).Any())
                                {
                                    frontRow.FrontRowInformation.Add(newFrontRowInformationModel);
                                }
                                else
                                {
                                    frontRow.FrontRowInformation.Where(circuit => circuit.CircuitName == circuitName).First().CircuitFrontRowCount++;
                                }
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
            var lockObject = new object();

            for (int year = from; year <= to; year++)
            {
                var newSeasonStandingsChangesModel = new SeasonStandingsChangesModel { Season = year, Standings = new List<StandingModel>() };

                var results = _resultsDataAccess.GetResultsFrom(year);
                var racesCount = results.Count;

                Parallel.For(1, racesCount + 1, round =>
                {
                    var standings = _standingsDataAccess.GetDriverStandingsFromRace(year, round);
                    var roundName = results[round - 1].raceName;

                    for (int i = 0; i < standings.Count; i++)
                    {
                        var driverName = $"{standings[i].Driver.givenName} {standings[i].Driver.familyName}";
                        var points = double.Parse(standings[i].points);
                        var position = i + 1;

                        lock (lockObject)
                        {
                            if (!newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == driverName).Any())
                            {
                                var newStandingModel = new StandingModel { Name = driverName, Rounds = new List<RoundModel>() };
                                newSeasonStandingsChangesModel.Standings.Add(newStandingModel);
                            }

                            var newRoundModel = new RoundModel { Round = round, RoundName = roundName, Points = points, Position = position };
                            newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == driverName).First().Rounds.Add(newRoundModel);
                        }
                    }
                });

                driversStandingsChanges.Add(newSeasonStandingsChangesModel);
            }

            return driversStandingsChanges;
        }

        public List<SeasonStandingsChangesModel> GetConstructorsStandingsChanges(int from, int to)
        {
            var constructorsStandingsChanges = new List<SeasonStandingsChangesModel>(to - from + 1);
            var lockObject = new object();

            for (int year = from; year <= to; year++)
            {
                var newSeasonStandingsChangesModel = new SeasonStandingsChangesModel { Season = year, Standings = new List<StandingModel>() };

                var results = _resultsDataAccess.GetResultsFrom(year);
                var racesCount = results.Count;

                Parallel.For(1, racesCount + 1, round =>
                {
                    var standings = _standingsDataAccess.GetConstructorStandingsFromRace(year, round);
                    var roundName = results[round - 1].raceName;

                    for (int i = 0; i < standings.Count; i++)
                    {
                        var constructorName = $"{standings[i].Constructor.name}";
                        var points = double.Parse(standings[i].points);
                        var position = i + 1;
                        var newRoundModel = new RoundModel { Round = round, RoundName = roundName, Points = points, Position = position };

                        lock (lockObject)
                        {
                            if (!newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == constructorName).Any())
                            {
                                var newStandingModel = new StandingModel { Name = constructorName, Rounds = new List<RoundModel>() };
                                newStandingModel.Rounds.Add(newRoundModel);

                                newSeasonStandingsChangesModel.Standings.Add(newStandingModel);
                            }
                            else
                            {
                                newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == constructorName).First().Rounds.Add(newRoundModel);
                            }
                        }
                    }
                });

                constructorsStandingsChanges.Add(newSeasonStandingsChangesModel);
            }

            return constructorsStandingsChanges;
        }

        public List<RacePositionChangesModel> GetDriversPositionChangesDuringRace(int season, int race)
        {
            var laps = _lapsDataAccess.GetLapsFrom(season, race);
            var driversPositionChangesDuringRace = new List<RacePositionChangesModel>();
            var driversNames = new Dictionary<string, string>();
            var lockObject = new object();
            var lockCheck = new object();

            Parallel.ForEach(laps, lap =>
            {
                var lapNumber = int.Parse(lap.number);

                foreach (var timing in lap.Timings)
                {
                    var driverId = timing.driverId;
                    string driverName;

                    lock (lockCheck)
                    {
                        if (driversNames.ContainsKey(driverId))
                        {
                            driverName = driversNames[driverId];
                        }
                        else
                        {
                            driverName = _driversDataAccess.GetDriverName(driverId);
                            driversNames.Add(driverId, driverName);
                        }
                    }

                    var position = int.Parse(timing.position);
                    var newLapPositionModel = new LapPositionModel { LapNumber = lapNumber, Position = position };

                    lock (lockObject)
                    {
                        if (!driversPositionChangesDuringRace.Where(driver => driver.Name == driverName).Any())
                        {
                            var newRacePositionChangesModel = new RacePositionChangesModel { Name = driverName, Laps = new List<LapPositionModel>(laps.Count) };
                            newRacePositionChangesModel.Laps.Add(newLapPositionModel);

                            driversPositionChangesDuringRace.Add(newRacePositionChangesModel);
                        }
                        else
                        {
                            driversPositionChangesDuringRace.Where(driver => driver.Name == driverName).First().Laps.Add(newLapPositionModel);
                        }
                    }
                }
            });

            return driversPositionChangesDuringRace;
        }

        public List<LapTimesModel> GetDriversLapTimes(int season, int race)
        {
            var laps = _lapsDataAccess.GetLapsFrom(season, race);
            var driversLapTimes = new List<LapTimesModel>();
            var driversNames = new Dictionary<string, string>();
            var lockObject = new object();
            var lockCheck = new object();

            Parallel.ForEach(laps, lap =>
            {
                foreach (var timing in lap.Timings)
                {
                    var driverId = timing.driverId;
                    var lapTime = ConvertTimeToSeconds(timing.time);
                    string driverName;

                    lock (lockCheck)
                    {
                        //TODO - iskelti
                        if (driversNames.ContainsKey(driverId))
                        {
                            driverName = driversNames[driverId];
                        }
                        else
                        {
                            driverName = _driversDataAccess.GetDriverName(driverId);
                            driversNames.Add(driverId, driverName);
                        }
                    }

                    lock (lockObject)
                    {
                        if (!driversLapTimes.Where(driver => driver.Name == driverName).Any())
                        {
                            var newLapTimesModel = new LapTimesModel { Name = driverName, Timings = new List<double>(laps.Count) };
                            newLapTimesModel.Timings.Add(lapTime);

                            driversLapTimes.Add(newLapTimesModel);
                        }
                        else
                        {
                            driversLapTimes.Where(driver => driver.Name == driverName).First().Timings.Add(lapTime);
                        }
                    }
                }
            });

            return driversLapTimes;
        }

        // TODO - iskelti
        private double ConvertTimeToSeconds(string time)
        {
            int minutes = 0;
            double seconds = 0;

            if (time.Contains(':'))
            {
                var splitMinutesFromRest = time.Split(':');

                int.TryParse(splitMinutesFromRest[0], out minutes);
                double.TryParse(splitMinutesFromRest[1], out seconds);
            }
            else
            {
                double.TryParse(time, out seconds);
            }

            var timeInSeconds = Math.Round(minutes * 60 + seconds, 3);

            return timeInSeconds;
        }
    }
}
