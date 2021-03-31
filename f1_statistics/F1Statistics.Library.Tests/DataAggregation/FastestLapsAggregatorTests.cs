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
            var expectedFastestDrivers = new List<FastestLapModel> { new FastestLapModel { Name = "FirstName FirstFamily", FastestLapsCount = 2 }, new FastestLapModel { Name = "SecondName SecondFamily", FastestLapsCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversFastestLaps(from, to);
                actual.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));

                // Assert
                Assert.AreEqual(expectedFastestDrivers.Count, actual.Count);

                for (int i = 0; i < expectedFastestDrivers.Count; i++)
                {
                    Assert.AreEqual(expectedFastestDrivers[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedFastestDrivers[i].FastestLapsCount, actual[i].FastestLapsCount);
                } 
            }
        }

        [TestMethod]
        public void GetDriversFastestLaps_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedFastestDrivers = new List<FastestLapModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversFastestLaps(from, to);

                // Assert
                Assert.AreEqual(expectedFastestDrivers.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnAggregatedConstructorsFastestLappersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedFastestConstructors = new List<FastestLapModel> { new FastestLapModel { Name = "FirstConstructor", FastestLapsCount = 2 }, new FastestLapModel { Name = "SecondConstructor", FastestLapsCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsFastestLaps(from, to);
                actual.Sort((x, y) => y.FastestLapsCount.CompareTo(x.FastestLapsCount));

                // Assert
                Assert.AreEqual(expectedFastestConstructors.Count, actual.Count);

                for (int i = 0; i < expectedFastestConstructors.Count; i++)
                {
                    Assert.AreEqual(expectedFastestConstructors[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedFastestConstructors[i].FastestLapsCount, actual[i].FastestLapsCount);
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedFastestConstructors = new List<FastestLapModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsFastestLaps(from, to);

                // Assert
                Assert.AreEqual(expectedFastestConstructors.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetUniqueDriversFastestLaps_ReturnAggregatedUniqueSeasonDriversFastestLappersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueFastestDrivers = new List<UniqueSeasonFastestLapModel>
            {
                new UniqueSeasonFastestLapModel
                {
                    Season = 1,
                    FastestLapAchievers = new List<string>
                    {
                        "FirstName FirstFamily"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonFastestLapModel
                {
                    Season = 2,
                    FastestLapAchievers = new List<string>
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
                var actual = _aggregator.GetUniqueDriversFastestLaps(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniqueFastestDrivers.Count, actual.Count);

                for (int i = 0; i < expectedUniqueFastestDrivers.Count; i++)
                {
                    Assert.AreEqual(expectedUniqueFastestDrivers[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniqueFastestDrivers[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
                    Assert.AreEqual(expectedUniqueFastestDrivers[i].RacesCount, actual[i].RacesCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniqueDriversFastestLaps_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueFastestDrivers = new List<UniqueSeasonFastestLapModel> { new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() }, new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniqueDriversFastestLaps(from, to);

                // Assert
                Assert.AreEqual(expectedUniqueFastestDrivers.Count, actual.Count);
                Assert.AreEqual(expectedUniqueFastestDrivers[0].FastestLapAchievers.Count, actual[0].FastestLapAchievers.Count);
                Assert.AreEqual(expectedUniqueFastestDrivers[1].FastestLapAchievers.Count, actual[1].FastestLapAchievers.Count); 
            }
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLaps_ReturnAggregatedUniqueSeasonConstructorsFastestLappersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueFastestConstructors = new List<UniqueSeasonFastestLapModel>
            {
                new UniqueSeasonFastestLapModel
                {
                    Season = 1,
                    FastestLapAchievers = new List<string>
                    {
                        "FirstConstructor"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonFastestLapModel
                {
                    Season = 2,
                    FastestLapAchievers = new List<string>
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
                var actual = _aggregator.GetUniqueConstructorsFastestLaps(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniqueFastestConstructors.Count, actual.Count);

                for (int i = 0; i < expectedUniqueFastestConstructors.Count; i++)
                {
                    Assert.AreEqual(expectedUniqueFastestConstructors[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniqueFastestConstructors[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
                    Assert.AreEqual(expectedUniqueFastestConstructors[i].RacesCount, actual[i].RacesCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLaps_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniqueFastestConstructors = new List<UniqueSeasonFastestLapModel> { new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() }, new UniqueSeasonFastestLapModel { FastestLapAchievers = new List<string>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniqueConstructorsFastestLaps(from, to);

                // Assert
                Assert.AreEqual(expectedUniqueFastestConstructors.Count, actual.Count);
                Assert.AreEqual(expectedUniqueFastestConstructors[0].FastestLapAchievers.Count, actual[0].FastestLapAchievers.Count);
                Assert.AreEqual(expectedUniqueFastestConstructors[1].FastestLapAchievers.Count, actual[1].FastestLapAchievers.Count); 
            }
        }
    }
}
