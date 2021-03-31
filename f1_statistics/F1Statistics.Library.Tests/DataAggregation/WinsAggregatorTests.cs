using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Tests.DataAggregation
{
    [TestClass]
    public class WinsAggregatorTests
    {
        private WinsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();

            _aggregator = new WinsAggregator(_resultsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                { 
                    new RacesDataResponse
                    { 
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "FirstCircuit"
                        },
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse 
                            {
                                Driver = new DriverDataResponse 
                                { 
                                    familyName = "FirstFamily", 
                                    givenName= "FirstName" 
                                },
                                Constructor = new ConstructorDataResponse 
                                { 
                                    name = "FirstConstructor"
                                },
                                position = "1",
                                grid = "1"
                            },
                            new ResultsDataResponse
                            { 
                                Driver = new DriverDataResponse 
                                { 
                                    familyName = "SecondFamily", 
                                    givenName= "SecondName" 
                                }, 
                                Constructor = new ConstructorDataResponse 
                                { 
                                    name = "SecondConstructor"
                                },
                                position = "2",
                                grid = "2"
                            }
                        }
                    } 
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse 
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "SecondCircuit"
                        },
                        Results = new List<ResultsDataResponse> 
                        { 
                            new ResultsDataResponse 
                            {
                                Driver = new DriverDataResponse 
                                { 
                                    familyName = "FirstFamily", 
                                    givenName = "FirstName" 
                                }, 
                                Constructor = new ConstructorDataResponse 
                                { 
                                    name = "FirstConstructor" 
                                },
                                position = "1",
                                grid = "1"
                            } 
                        } 
                    },
                    new RacesDataResponse 
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "ThirdCircuit"
                        },
                        Results = new List<ResultsDataResponse>
                        { 
                            new ResultsDataResponse 
                            { 
                                Driver = new DriverDataResponse 
                                { 
                                    familyName = "SecondFamily", 
                                    givenName = "SecondName" 
                                }, 
                                Constructor = new ConstructorDataResponse 
                                { 
                                    name = "SecondConstructor" 
                                },
                                position = "1",
                                grid = "1"
                            }
                        }
                    }
                }
            };

            return racesList;
        }

        [TestMethod]
        public void GetDriversWins_ReturnAggregatedDriversList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel> { new WinsModel { Name = "FirstName FirstFamily", WinCount = 2 }, new WinsModel { Name = "SecondName SecondFamily", WinCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversWins(from, to);
                actual.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count);

                for (int i = 0; i < expectedWinners.Count; i++)
                {
                    Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
                } 
            }
        }

        [TestMethod]
        public void GetDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversWins(from, to);

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnAggregatedConstructorsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel> { new AverageWinsModel { Name = "FirstConstructor", WinCount = 2 }, new AverageWinsModel { Name = "SecondConstructor", WinCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWins(from, to);
                actual.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count);

                for (int i = 0; i < expectedWinners.Count; i++)
                {
                    Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWins(from, to);

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnAggregatedDriversWithAverageWinsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel> { new AverageWinsModel { Name = "FirstName FirstFamily", WinCount = 2, ParticipationCount = 2 }, new AverageWinsModel { Name = "SecondName SecondFamily", WinCount = 1, ParticipationCount = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversWinPercent(from, to);
                actual.Sort((x, y) => y.AverageWins.CompareTo(x.AverageWins));

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count);

                for (int i = 0; i < expectedWinners.Count; i++)
                {
                    Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
                    Assert.AreEqual(expectedWinners[i].ParticipationCount, actual[i].ParticipationCount);
                    Assert.AreEqual(expectedWinners[i].AverageWins, actual[i].AverageWins);
                } 
            }
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversWinPercent(from, to);

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnAggregatedConstructorsWithAverageWinsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel> { new AverageWinsModel { Name = "FirstConstructor", WinCount = 2, ParticipationCount = 2 }, new AverageWinsModel { Name = "SecondConstructor", WinCount = 1, ParticipationCount = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWinPercent(from, to);
                actual.Sort((x, y) => y.AverageWins.CompareTo(x.AverageWins));

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count);

                for (int i = 0; i < expectedWinners.Count; i++)
                {
                    Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
                    Assert.AreEqual(expectedWinners[i].ParticipationCount, actual[i].ParticipationCount);
                    Assert.AreEqual(expectedWinners[i].AverageWins, actual[i].AverageWins);
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWinPercent(from, to);

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetCircuitWinners_ReturnAggregatedCircuitWinnersList_IfThereAreAnyCircuits()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedCircuitWinners = new List<CircuitWinsModel>
            {
                new CircuitWinsModel
                {
                    Name = "FirstCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstName FirstFamily",
                            WinCount = 1,
                            ParticipationsCount = 1
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "SecondCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstName FirstFamily",
                            WinCount = 1,
                            ParticipationsCount = 1
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "ThirdCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "SecondName SecondFamily",
                            WinCount = 1,
                            ParticipationsCount = 1
                        }
                    }
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetCircuitWinners(from, to);
                actual.ForEach(circuit => circuit.Winners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount)));
                actual.Sort((x, y) => x.Name.CompareTo(y.Name));

                // Assert
                Assert.AreEqual(expectedCircuitWinners.Count, actual.Count);

                for (int i = 0; i < expectedCircuitWinners.Count; i++)
                {
                    Assert.AreEqual(expectedCircuitWinners[i].Name, actual[i].Name);

                    for (int j = 0; j < expectedCircuitWinners[i].Winners.Count; j++)
                    {
                        Assert.AreEqual(expectedCircuitWinners[i].Winners[j].Name, actual[i].Winners[j].Name);
                        Assert.AreEqual(expectedCircuitWinners[i].Winners[j].WinCount, actual[i].Winners[j].WinCount);
                        Assert.AreEqual(expectedCircuitWinners[i].Winners[j].ParticipationsCount, actual[i].Winners[j].ParticipationsCount);
                    }
                } 
            }
        }

        [TestMethod]
        public void GetCircuitWinners_ReturnEmptyList_IfThereAreNoCircuits()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedCircuitWinners = new List<CircuitWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());
            
            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetCircuitWinners(from, to);

                // Assert
                Assert.AreEqual(expectedCircuitWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverWinners_ReturnAggregatedUniqueSeasonWinners_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>
            {
                new UniqueSeasonWinnersModel
                {
                    Season = 1,
                    Winners = new List<string>
                    {
                        "FirstName FirstFamily"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2,
                    Winners = new List<string>
                    {
                        "FirstName FirstFamily",
                        "SecondName SecondFamily"
                    },
                    RacesCount = 2
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonDriverWinners(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

                for (int i = 0; i < expectedUniqueWinners.Count; i++)
                {
                    Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                    Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel> { new UniqueSeasonWinnersModel { Winners = new List<string>() }, new UniqueSeasonWinnersModel { Winners = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonDriverWinners(from, to);

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
                Assert.AreEqual(expectedUniqueWinners[0].Winners.Count, actual[0].Winners.Count);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorWinners_ReturnAggregatedUniqueSeasonWinners_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>
            {
                new UniqueSeasonWinnersModel
                {
                    Season = 1,
                    Winners = new List<string>
                    {
                        "FirstConstructor"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2,
                    Winners = new List<string>
                    {
                        "FirstConstructor",
                        "SecondConstructor"
                    },
                    RacesCount = 2
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonConstructorWinners(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

                for (int i = 0; i < expectedUniqueWinners.Count; i++)
                {
                    Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                    Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel> { new UniqueSeasonWinnersModel { Winners = new List<string>() }, new UniqueSeasonWinnersModel { Winners = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniqueSeasonConstructorWinners(from, to);

                // Assert
                Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
                Assert.AreEqual(expectedUniqueWinners[0].Winners.Count, actual[0].Winners.Count);
            }
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnAggregatedWinnersFromPoleList_IfThereAreAnyWinnersFromPole()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinnersFromPole = new List<WinnersFromPoleModel> { new WinnersFromPoleModel { Season = 1, WinnersFromPole = new List<string> { "FirstName FirstFamily" } }, new WinnersFromPoleModel { Season = 2, WinnersFromPole = new List<string> { "FirstName FirstFamily", "SecondName SecondFamily" } } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetWinnersFromPole(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);

                for (int i = 0; i < expectedWinnersFromPole.Count; i++)
                {
                    Assert.AreEqual(expectedWinnersFromPole[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedWinnersFromPole[i].WinsFromPoleCount, actual[i].WinsFromPoleCount);
                }
            }
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnEmptyList_IfThereAreNoWinnersFromPole()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersConstructors = new List<WinnersFromPoleModel> { new WinnersFromPoleModel { WinnersFromPole = new List<string>() }, new WinnersFromPoleModel { WinnersFromPole = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetWinnersFromPole(from, to);

                // Assert
                Assert.AreEqual(expectedUniquePoleSittersConstructors.Count, actual.Count);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[0].WinnersFromPole.Count, actual[0].WinnersFromPole.Count);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[1].WinnersFromPole.Count, actual[1].WinnersFromPole.Count);
            }
        }

        [TestMethod]
        public void GetWinnersByGridPosition_ReturnAggregatedWinnersByGridPositionList_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinnersByGridPosition = new List<WinsByGridPositionModel> 
            {
                new WinsByGridPositionModel
                {
                    GridPosition = 1, 
                    Winners = new List<string> 
                    {
                        "FirstName FirstFamily",
                        "FirstName FirstFamily",
                        "SecondName SecondFamily"
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetWinnersByGridPosition(from, to);
                actual.Sort((x, y) => x.GridPosition.CompareTo(y.GridPosition));
                actual.ForEach(model => model.Winners.Sort((x, y) => x.CompareTo(y)));

                // Assert
                Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);

                for (int i = 0; i < expectedWinnersByGridPosition.Count; i++)
                {
                    Assert.AreEqual(expectedWinnersByGridPosition[i].GridPosition, actual[i].GridPosition);
                    Assert.AreEqual(expectedWinnersByGridPosition[i].WinCount, actual[i].WinCount);
                    Assert.AreEqual(expectedWinnersByGridPosition[i].Winners.Count, actual[i].Winners.Count);

                    for (int j = 0; j < expectedWinnersByGridPosition[i].Winners.Count; j++)
                    {
                        Assert.AreEqual(expectedWinnersByGridPosition[i].Winners[j], actual[i].Winners[j]);
                    }
                }
            }
        }

        [TestMethod]
        public void GetWinnersByGridPosition_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinnersByGridPosition = new List<WinsByGridPositionModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetWinnersByGridPosition(from, to);

                // Assert
                Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);
            }
        }
    }
}
