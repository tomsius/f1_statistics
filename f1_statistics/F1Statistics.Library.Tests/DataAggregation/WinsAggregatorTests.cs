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
                                    familyName = "FirstFamily", givenName= "FirstName" 
                                },
                                Constructor = new ConstructorDataResponse 
                                { 
                                    name = "FirstConstructor"
                                } 
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
                                } 
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
                                } 
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
                                }
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
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetDriversWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void GetDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetDriversWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnAggregatedConstructorsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel> { new AverageWinsModel { Name = "FirstConstructor", WinCount = 2 }, new AverageWinsModel { Name = "SecondConstructor", WinCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetConstructorsWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetConstructorsWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversAverageWins_ReturnAggregatedDriversWithAverageWinsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel> { new AverageWinsModel { Name = "FirstName FirstFamily", WinCount = 2, ParticipationCount = 2 }, new AverageWinsModel { Name = "SecondName SecondFamily", WinCount = 1, ParticipationCount = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetDriversAverageWins(from, to);

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

        [TestMethod]
        public void GetDriversAverageWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetDriversAverageWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsAverageWins_ReturnAggregatedConstructorsWithAverageWinsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel> { new AverageWinsModel { Name = "FirstConstructor", WinCount = 2, ParticipationCount = 2 }, new AverageWinsModel { Name = "SecondConstructor", WinCount = 1, ParticipationCount = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetConstructorsAverageWins(from, to);

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

        [TestMethod]
        public void GetConstructorsAverageWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<AverageWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetConstructorsAverageWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
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
                    Winners = new List<WinsModel>
                    {
                        new WinsModel
                        {
                            Name = "FirstName FirstFamily",
                            WinCount = 1
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "SecondCircuit",
                    Winners = new List<WinsModel>
                    {
                        new WinsModel
                        {
                            Name = "FirstName FirstFamily",
                            WinCount = 1
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "ThirdCircuit",
                    Winners = new List<WinsModel>
                    {
                        new WinsModel
                        {
                            Name = "SecondName SecondFamily",
                            WinCount = 1
                        }
                    }
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetCircuitWinners(from, to);

            // Assert
            Assert.AreEqual(expectedCircuitWinners.Count, actual.Count);

            for (int i = 0; i < expectedCircuitWinners.Count; i++)
            {
                Assert.AreEqual(expectedCircuitWinners[i].Name, actual[i].Name);

                for (int j = 0; j < expectedCircuitWinners[i].Winners.Count; j++)
                {
                    Assert.AreEqual(expectedCircuitWinners[i].Winners[j].Name, actual[i].Winners[j].Name);
                    Assert.AreEqual(expectedCircuitWinners[i].Winners[j].WinCount, actual[i].Winners[j].WinCount);
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
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetCircuitWinners(from, to);

            // Assert
            Assert.AreEqual(expectedCircuitWinners.Count, actual.Count);
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
                    }
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2,
                    Winners = new List<string>
                    {
                        "FirstName FirstFamily",
                        "SecondName SecondFamily"
                    }
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetUniqueSeasonDriverWinners(from, to);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel> { new UniqueSeasonWinnersModel { Winners = new List<string>() }, new UniqueSeasonWinnersModel { Winners = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetUniqueSeasonDriverWinners(from, to);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
            Assert.AreEqual(expectedUniqueWinners[0].Winners.Count, actual[0].Winners.Count);
            Assert.AreEqual(expectedUniqueWinners[1].Winners.Count, actual[1].Winners.Count);
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
                    }
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2,
                    Winners = new List<string>
                    {
                        "FirstConstructor",
                        "SecondConstructor"
                    }
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetUniqueSeasonConstructorWinners(from, to);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel> { new UniqueSeasonWinnersModel { Winners = new List<string>() }, new UniqueSeasonWinnersModel { Winners = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetUniqueSeasonConstructorWinners(from, to);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
            Assert.AreEqual(expectedUniqueWinners[0].Winners.Count, actual[0].Winners.Count);
            Assert.AreEqual(expectedUniqueWinners[1].Winners.Count, actual[1].Winners.Count);
        }
    }
}
