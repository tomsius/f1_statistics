using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class WinsAggregator : IWinsAggregator
    {
        private readonly IResultsDataAccess _resultsDataAccess;

        public WinsAggregator(IResultsDataAccess resultsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
        }

        public List<WinsModel> GetDriversWins(int from, int to)
        {
            var driversWins = new List<WinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var winner = $"{race.Results[0].Driver.givenName} {race.Results[0].Driver.familyName}";

                    lock (lockObject) 
                    {
                        var newWinsByYearModel = new WinsByYearModel { Year = year, WinCount = 1 };

                        if (!driversWins.Where(driver => driver.Name == winner).Any())
                        {
                            var newWinsModel = new WinsModel { Name = winner, WinsByYear = new List<WinsByYearModel> { newWinsByYearModel } };
                            driversWins.Add(newWinsModel);
                        }
                        else
                        {
                            var driver = driversWins.Where(driver => driver.Name == winner).First();

                            if (!driver.WinsByYear.Where(model => model.Year == year).Any())
                            {
                                driver.WinsByYear.Add(newWinsByYearModel);
                            }
                            else
                            {
                                driver.WinsByYear.Where(model => model.Year == year).First().WinCount++;
                            }
                        }
                    }
                }
            });

            return driversWins;
        }

        public List<WinsModel> GetConstructorsWins(int from, int to)
        {
            var constructorsWins = new List<WinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var winner = $"{race.Results[0].Constructor.name}";

                    lock (lockObject)
                    {
                        var newWinsByYearModel = new WinsByYearModel { Year = year, WinCount = 1 };

                        if (!constructorsWins.Where(constructor => constructor.Name == winner).Any())
                        {
                            var newWinsModel = new WinsModel { Name = winner, WinsByYear = new List<WinsByYearModel> { newWinsByYearModel } };
                            constructorsWins.Add(newWinsModel);
                        }
                        else
                        {
                            var constructor = constructorsWins.Where(constructor => constructor.Name == winner).First();

                            if (!constructor.WinsByYear.Where(model => model.Year == year).Any())
                            {
                                constructor.WinsByYear.Add(newWinsByYearModel);
                            }
                            else
                            {
                                constructor.WinsByYear.Where(model => model.Year == year).First().WinCount++;
                            }
                        }
                    }
                }
            });

            return constructorsWins;
        }

        public List<AverageWinsModel> GetDriversWinPercent(int from, int to)
        {
            var driversAverageWins = new List<AverageWinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {

                    lock (lockObject)
                    {
                        // Fill participations
                        foreach (var result in race.Results)
                        {
                            var driverName = $"{result.Driver.givenName} {result.Driver.familyName}";

                            if (!driversAverageWins.Where(driver => driver.Name == driverName).Any())
                            {
                                var newAverageWinsModel = new AverageWinsModel { Name = driverName, ParticipationCount = 1 };
                                driversAverageWins.Add(newAverageWinsModel);
                            }
                            else
                            {
                                driversAverageWins.Where(driver => driver.Name == driverName).First().ParticipationCount++;
                            }
                        }

                        // Fill winner
                        var winner = $"{race.Results[0].Driver.givenName} {race.Results[0].Driver.familyName}";

                        driversAverageWins.Where(driver => driver.Name == winner).First().WinCount++;  
                    }
                }
            });

            return driversAverageWins;
        }

        public List<AverageWinsModel> GetConstructorsWinPercent(int from, int to)
        {
            var constructorsAverageWins = new List<AverageWinsModel>(to - from + 1);
            var lockObject = new object();
            var lockIncrement = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    lock (lockObject)
                    {
                        // Fill participations
                        foreach (var result in race.Results)
                        {
                            var constructorName = $"{result.Constructor.name}";

                            if (!constructorsAverageWins.Where(constructor => constructor.Name == constructorName).Any())
                            {
                                var newAverageWinsModel = new AverageWinsModel { Name = constructorName, ParticipationCount = 1 };
                                constructorsAverageWins.Add(newAverageWinsModel);
                            }
                            else
                            {
                                constructorsAverageWins.Where(constructor => constructor.Name == constructorName).First().ParticipationCount++;
                            }
                        }

                        RemoveDoubleCarCountingInARace(constructorsAverageWins, race);  
                    }

                    lock (lockIncrement)
                    {
                        // Fill winner
                        var winner = $"{race.Results[0].Constructor.name}";

                        constructorsAverageWins.Where(driver => driver.Name == winner).First().WinCount++; 
                    }
                }
            });

            return constructorsAverageWins;
        }

        // TODO - galbut iskelti
        private void RemoveDoubleCarCountingInARace(List<AverageWinsModel> constructorsAverageWins, RacesDataResponse race)
        {
            foreach (var constructor in constructorsAverageWins)
            {
                if (AreTwoConstructorsCarsInARace(race, constructor.Name) == 2)
                {
                    constructor.ParticipationCount--;
                }
            }
        }

        private int AreTwoConstructorsCarsInARace(RacesDataResponse race, string constructor)
        {
            int carCount = 0;

            foreach (var result in race.Results)
            {
                var constructorName = $"{result.Constructor.name}";

                if (constructorName == constructor)
                {
                    carCount++;
                }
            }

            return carCount;
        }

        public List<CircuitWinsModel> GetCircuitWinners(int from, int to)
        {
            var circuitWinners = new List<CircuitWinsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var circuitName = race.Circuit.circuitName;
                    var winnerName = $"{race.Results[0].Driver.givenName} {race.Results[0].Driver.familyName}";

                    lock (lockObject)
                    {
                        foreach (var result in race.Results)
                        {
                            var driverName = $"{result.Driver.givenName} {result.Driver.familyName}";
                            var newWinsAndParticipationsModel = new WinsAndParticipationsModel { Name = driverName, WinCount = 0, ParticipationsCount = 1 };

                            if (!circuitWinners.Where(circuit => circuit.Name == circuitName).Any())
                            {
                                var newCircuitWinsModel = new CircuitWinsModel { Name = circuitName, Winners = new List<WinsAndParticipationsModel> { newWinsAndParticipationsModel } };
                                circuitWinners.Add(newCircuitWinsModel);
                            }
                            else
                            {
                                if (!circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Where(driver => driver.Name == driverName).Any())
                                {
                                    circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Add(newWinsAndParticipationsModel);
                                }
                                else
                                {
                                    circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Where(driver => driver.Name == driverName).First().ParticipationsCount++;
                                }
                            }
                        }

                        circuitWinners.Where(circuit => circuit.Name == circuitName).First().Winners.Where(winner => winner.Name == winnerName).First().WinCount++; 
                    }
                }
            });

            return circuitWinners;
        }

        public List<UniqueSeasonWinnersModel> GetUniqueSeasonDriverWinners(int from, int to)
        {
            var uniqueDriverWinners = new List<UniqueSeasonWinnersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year => 
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newUniqueSeasonWinnersModel = new UniqueSeasonWinnersModel { Season = year, Winners = new List<string>(), RacesCount = races.Count };

                foreach (var race in races)
                {
                    var winnerName = $"{race.Results[0].Driver.givenName} {race.Results[0].Driver.familyName}";

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonWinnersModel.Winners.Where(winner => winner == winnerName).Any())
                        {
                            newUniqueSeasonWinnersModel.Winners.Add(winnerName);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniqueDriverWinners.Add(newUniqueSeasonWinnersModel); 
                }
            });

            return uniqueDriverWinners;
        }

        public List<UniqueSeasonWinnersModel> GetUniqueSeasonConstructorWinners(int from, int to)
        {
            var uniqueConstructorWinners = new List<UniqueSeasonWinnersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newUniqueSeasonWinnersModel = new UniqueSeasonWinnersModel { Season = year, Winners = new List<string>(), RacesCount = races.Count };

                foreach (var race in races)
                {
                    var winnerName = $"{race.Results[0].Constructor.name}";

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonWinnersModel.Winners.Where(winner => winner == winnerName).Any())
                        {
                            newUniqueSeasonWinnersModel.Winners.Add(winnerName);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniqueConstructorWinners.Add(newUniqueSeasonWinnersModel); 
                }
            });

            return uniqueConstructorWinners;
        }

        public List<WinnersFromPoleModel> GetWinnersFromPole(int from, int to)
        {
            var winsFromPole = new List<WinnersFromPoleModel>(to - from + 1);
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                var newWinnersFromPoleModel = new WinnersFromPoleModel { Season = year, RacesCount = races.Count, WinnersFromPole = new List<string>() };

                foreach (var race in races)
                {
                    var winnerInformation = race.Results[0];
                    var gridPosition = winnerInformation.grid;
                    var finishPosition = winnerInformation.position;

                    var winnerName = $"{winnerInformation.Driver.givenName} {winnerInformation.Driver.familyName}";

                    if (gridPosition == finishPosition)
                    {
                        newWinnersFromPoleModel.WinnersFromPole.Add(winnerName);
                    }
                }

                lock (lockAdd)
                {
                    winsFromPole.Add(newWinnersFromPoleModel);
                }
            });

            return winsFromPole;
        }

        public List<WinsByGridPositionModel> GetWinnersByGridPosition(int from, int to)
        {
            var winnersByGridPosition = new List<WinsByGridPositionModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var winnerInformation = race.Results[0];
                    var gridPosition = int.Parse(winnerInformation.grid);
                    var winnerName = $"{winnerInformation.Driver.givenName} {winnerInformation.Driver.familyName}";

                    lock (lockObject)
                    {
                        if (!winnersByGridPosition.Where(grid => grid.GridPosition == gridPosition).Any())
                        {
                            var winnersList = new List<string> { winnerName };
                            var newWinsByGridPositionModel = new WinsByGridPositionModel { GridPosition = gridPosition, Winners = winnersList };
                            winnersByGridPosition.Add(newWinsByGridPositionModel);
                        }
                        else
                        {
                            winnersByGridPosition.Where(grid => grid.GridPosition == gridPosition).First().Winners.Add(winnerName);
                        }
                    }
                }
            });

            return winnersByGridPosition;
        }
    }
}
