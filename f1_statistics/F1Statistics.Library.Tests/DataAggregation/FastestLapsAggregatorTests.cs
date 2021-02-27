using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.DataAggregation
{
    [TestClass]
    public class FastestLapsAggregatorTests
    {
        private FastestLapsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();

            _aggregator = new FastestLapsAggregator(_resultsDataAccess.Object);
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
                                },
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "1"
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
                                },
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "2"
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
                                },
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "1"
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
                                },
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "1"
                                }
                            }
                        }
                    }
                }
            };

            return racesList;
        }

        [TestMethod]
        public void GetDriversFastestLaps_ReturnAggregatedDriversFastestLappersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<FastestLapModel> { new FastestLapModel { Name = "FirstName FirstFamily", FastestLapsCount = 2 }, new FastestLapModel { Name = "SecondName SecondFamily", FastestLapsCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetDriversFastestLaps(from, to);
            actual.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].FastestLapsCount, actual[i].FastestLapsCount);
            }
        }

        [TestMethod]
        public void GetDriversFastestLaps_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<FastestLapModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetDriversFastestLaps(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnAggregatedConstructorsFastestLappersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<FastestLapModel> { new FastestLapModel { Name = "FirstConstructor", FastestLapsCount = 2 }, new FastestLapModel { Name = "SecondConstructor", FastestLapsCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetConstructorsFastestLaps(from, to);
            actual.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].FastestLapsCount, actual[i].FastestLapsCount);
            }
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<FastestLapModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetConstructorsFastestLaps(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueDriversFastestLaps_ReturnAggregatedUniqueSeasonDriversFastestLappersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonFastestLapModel>
            {
                new UniqueSeasonFastestLapModel
                {
                    Season = 1,
                    FastestLapAchievers = new List<string>
                    {
                        "FirstName FirstFamily"
                    }
                },
                new UniqueSeasonFastestLapModel
                {
                    Season = 2,
                    FastestLapAchievers = new List<string>
                    {
                        "FirstName FirstFamily",
                        "SecondName SecondFamily"
                    }
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetUniqueDriversFastestLaps(from, to);
            actual.Sort((x, y) => x.Season.CompareTo(y.Season));

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
            }
        }

        [TestMethod]
        public void GetUniqueDriversFastestLaps_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonFastestLapModel> { new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() }, new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetUniqueDriversFastestLaps(from, to);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
            Assert.AreEqual(expectedUniqueWinners[0].FastestLapAchievers.Count, actual[0].FastestLapAchievers.Count);
            Assert.AreEqual(expectedUniqueWinners[1].FastestLapAchievers.Count, actual[1].FastestLapAchievers.Count);
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLaps_ReturnAggregatedUniqueSeasonConstructorsFastestLappersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonFastestLapModel>
            {
                new UniqueSeasonFastestLapModel
                {
                    Season = 1,
                    FastestLapAchievers = new List<string>
                    {
                        "FirstConstructor"
                    }
                },
                new UniqueSeasonFastestLapModel
                {
                    Season = 2,
                    FastestLapAchievers = new List<string>
                    {
                        "FirstConstructor",
                        "SecondConstructor"
                    }
                }
            };

            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetUniqueConstructorsFastestLaps(from, to);
            actual.Sort((x, y) => x.Season.CompareTo(y.Season));

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
            }
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLaps_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueWinners = new List<UniqueSeasonFastestLapModel> { new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() }, new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetRacesFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetUniqueConstructorsFastestLaps(from, to);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
            Assert.AreEqual(expectedUniqueWinners[0].FastestLapAchievers.Count, actual[0].FastestLapAchievers.Count);
            Assert.AreEqual(expectedUniqueWinners[1].FastestLapAchievers.Count, actual[1].FastestLapAchievers.Count);
        }
    }
}
