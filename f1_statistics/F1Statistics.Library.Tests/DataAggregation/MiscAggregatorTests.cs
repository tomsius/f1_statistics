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
    public class MiscAggregatorTests
    {
        private MiscAggregator _aggregator;
        private Mock<IRacesDataAccess> _racesDataAccess;
        private Mock<IResultsDataAccess> _resultsDataAccess;
        private Mock<ILapsDataAccess> _lapsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _racesDataAccess = new Mock<IRacesDataAccess>();
            _resultsDataAccess = new Mock<IResultsDataAccess>();
            _lapsDataAccess = new Mock<ILapsDataAccess>();

            _aggregator = new MiscAggregator(_racesDataAccess.Object, _resultsDataAccess.Object, _lapsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        round = "1",
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "1",
                                    familyName = "FirstFamily", 
                                    givenName= "FirstName"
                                },
                                status = "Finished",
                                grid = "1",
                                position = "1",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "1"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "2",
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                status = "Electronics",
                                grid = "5",
                                position = "2",
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
                        round = "1",
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "1",
                                    familyName = "FirstFamily",
                                    givenName = "FirstName"
                                },
                                status = "+1",
                                grid = "1",
                                position = "1",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "1"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "2",
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                status = "Engine",
                                grid = "2",
                                position = "5",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "2"
                                }
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        round = "2",
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "2",
                                    familyName = "SecondFamily",
                                    givenName = "SecondName"
                                },
                                status = "+2",
                                grid = "5",
                                position = "1",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "1"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "1",
                                    familyName = "FirstFamily",
                                    givenName= "FirstName"
                                },
                                status = "Illness",
                                grid = "3",
                                position = "2",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "2"
                                }
                            }
                        }
                    }
                }
            };

            return racesList;
        }

        private List<List<LapsDataResponse>> GenerateLaps()
        {
            var lapsList = new List<List<LapsDataResponse>>
            {
                new List<LapsDataResponse>
                {
                    new LapsDataResponse
                    {
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "1"
                            }
                        }
                    }
                },
                new List<LapsDataResponse>
                {
                    new LapsDataResponse
                    {
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "2"
                            }
                        }
                    }
                },
                new List<LapsDataResponse>
                {
                    new LapsDataResponse
                    {
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "3"
                            }
                        }
                    }
                }
            };

            return lapsList;
        }

        [TestMethod]
        public void GetRaceCountPerSeason_ReturnAggregatedRaceCountPerSeasonList_IfThereAreAnyRaces()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSeasonRaces = new List<SeasonRacesModel> { new SeasonRacesModel { Season = 1, RaceCount = 1 }, new SeasonRacesModel { Season = 2, RaceCount = 2 } };
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(1)).Returns(GenerateRaces()[0].Count);
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(2)).Returns(GenerateRaces()[1].Count);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetRaceCountPerSeason(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);

                for (int i = 0; i < expectedSeasonRaces.Count; i++)
                {
                    Assert.AreEqual(expectedSeasonRaces[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedSeasonRaces[i].RaceCount, actual[i].RaceCount);
                }
            }
        }

        [TestMethod]
        public void GetRaceCountPerSeason_ReturnRaceCountPerSeasonWithRaceCountOf0List_IfThereAreNoRaces()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSeasonRaces = new List<SeasonRacesModel> { new SeasonRacesModel { Season = 1, RaceCount = 0 }, new SeasonRacesModel { Season = 2, RaceCount = 0 } };
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(1)).Returns(0);
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(2)).Returns(0);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetRaceCountPerSeason(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);

                for (int i = 0; i < expectedSeasonRaces.Count; i++)
                {
                    Assert.AreEqual(expectedSeasonRaces[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedSeasonRaces[i].RaceCount, actual[i].RaceCount);
                }
            }
        }

        [TestMethod]
        public void GetHatTricks_ReturnAggregatedHatTricksList_IfThereAreAnyDriversWithHatTricks()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedHatTricks = new List<HatTrickModel> { new HatTrickModel { Name = "FirstName FirstFamily", HatTrickCount = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetHatTricks(from, to);
                actual.Sort((x, y) => y.HatTrickCount.CompareTo(x.HatTrickCount));

                // Assert
                Assert.AreEqual(expectedHatTricks.Count, actual.Count);

                for (int i = 0; i < expectedHatTricks.Count; i++)
                {
                    Assert.AreEqual(expectedHatTricks[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedHatTricks[i].HatTrickCount, actual[i].HatTrickCount);
                }
            }
        }

        [TestMethod]
        public void GetHatTricks_ReturnRaceEmptyList_IfThereAreNoQualifyings()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedHatTricks = new List<HatTrickModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetHatTricks(from, to);

                // Assert
                Assert.AreEqual(expectedHatTricks.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetGrandSlams_ReturnAggregatedGrandSlamsList_IfThereAreAnyDriversWithGrandSlams()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedGrandslams = new List<GrandSlamModel> { new GrandSlamModel { Name = "FirstName FirstFamily", GrandSlamCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(1, 1)).Returns(GenerateLaps()[1]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(2, 1)).Returns(GenerateLaps()[0]);
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(2, 2)).Returns(GenerateLaps()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetGrandSlams(from, to);
                actual.Sort((x, y) => y.GrandSlamCount.CompareTo(x.GrandSlamCount));

                // Assert
                Assert.AreEqual(expectedGrandslams.Count, actual.Count);

                for (int i = 0; i < expectedGrandslams.Count; i++)
                {
                    Assert.AreEqual(expectedGrandslams[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedGrandslams[i].GrandSlamCount, actual[i].GrandSlamCount);
                }
            }
        }

        [TestMethod]
        public void GetGrandSlams_ReturnRaceEmptyList_IfThereAreNoQualifyings()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedGrandslams = new List<GrandSlamModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetGrandSlams(from, to);

                // Assert
                Assert.AreEqual(expectedGrandslams.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetNonFinishers_ReturnAggregatedNonFinishersList_IfThereAreAnyDriversWhoDidNotFinishRace()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedGrandslams = new List<DidNotFinishModel> { new DidNotFinishModel { Name = "FirstName FirstFamily", DidNotFinishCount = 1 }, new DidNotFinishModel { Name = "SecondName SecondFamily", DidNotFinishCount = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetNonFinishers(from, to);
                actual.Sort((x, y) => x.DidNotFinishCount.CompareTo(y.DidNotFinishCount));

                // Assert
                Assert.AreEqual(expectedGrandslams.Count, actual.Count);

                for (int i = 0; i < expectedGrandslams.Count; i++)
                {
                    Assert.AreEqual(expectedGrandslams[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedGrandslams[i].DidNotFinishCount, actual[i].DidNotFinishCount);
                }
            }
        }

        [TestMethod]
        public void GetNonFinishers_ReturnEmptyList_IfAllDriversFinishedEveryRace()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedGrandslams = new List<DidNotFinishModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetNonFinishers(from, to);

                // Assert
                Assert.AreEqual(expectedGrandslams.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetSeasonPositionChanges_ReturnAggregatedSeasonPositionChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSeasonPositionChanges = new List<SeasonPositionChangesModel>
            {
                new SeasonPositionChangesModel
                {
                    Season = 1,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "SecondName SecondFamily",
                            PositionChange = 3
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "FirstName FirstFamily",
                            PositionChange = 0
                        }
                    }
                },
                new SeasonPositionChangesModel
                {
                    Season = 2,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "FirstName FirstFamily",
                            PositionChange = 1
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "SecondName SecondFamily",
                            PositionChange = 1
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetSeasonPositionChanges(from, to);
                actual.ForEach(season => season.PositionChanges.Sort((x, y) => y.PositionChange.CompareTo(x.PositionChange)));
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);

                for (int i = 0; i < expectedSeasonPositionChanges.Count; i++)
                {
                    Assert.AreEqual(expectedSeasonPositionChanges[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges.Count, actual[i].PositionChanges.Count);

                    for (int j = 0; j < expectedSeasonPositionChanges[i].PositionChanges.Count; j++)
                    {
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].Name, actual[i].PositionChanges[j].Name);
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].PositionChange, actual[i].PositionChanges[j].PositionChange);
                    }
                }
            }
        }

        [TestMethod]
        public void GetSeasonPositionChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSeasonPositionChanges = new List<SeasonPositionChangesModel> { new SeasonPositionChangesModel { Season = 1 }, new SeasonPositionChangesModel { Season = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetSeasonPositionChanges(from, to);

                // Assert
                Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);
            }
        }
    }
}
