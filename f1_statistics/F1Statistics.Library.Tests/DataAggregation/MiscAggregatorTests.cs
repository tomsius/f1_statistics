﻿using F1Statistics.Library.DataAccess.Interfaces;
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
        private Mock<IQualifyingDataAccess> _qualifyingsDataAccess;
        private Mock<ILapsDataAccess> _lapsDataAccess;
        private Mock<IStandingsDataAccess> _standingsDataAccess;
        private Mock<IDriversDataAccess> _driversDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _racesDataAccess = new Mock<IRacesDataAccess>();
            _resultsDataAccess = new Mock<IResultsDataAccess>();
            _qualifyingsDataAccess = new Mock<IQualifyingDataAccess>();
            _lapsDataAccess = new Mock<ILapsDataAccess>();
            _standingsDataAccess = new Mock<IStandingsDataAccess>();
            _driversDataAccess = new Mock<IDriversDataAccess>();

            _aggregator = new MiscAggregator(_racesDataAccess.Object, _resultsDataAccess.Object, _qualifyingsDataAccess.Object, _lapsDataAccess.Object, _standingsDataAccess.Object, _driversDataAccess.Object);
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
                                    driverId = "2",
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                status = "Electronics",
                                grid = "2",
                                position = "3",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "2"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
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
                                grid = "1",
                                position = "1",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "1"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
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
                                grid = "2",
                                position = "2",
                                FastestLap = new FastestLapDataResponse
                                {
                                    rank = "2"
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

        private List<List<RacesDataResponse>> GenerateQualifyings()
        {
            var qualifyings = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        round = "1",
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "1",
                                    familyName = "FirstFamily", 
                                    givenName= "FirstName"
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
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "1",
                                    familyName = "FirstFamily",
                                    givenName = "FirstName"
                                }
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        round = "2",
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    driverId = "3",
                                    familyName = "ThirdFamily",
                                    givenName = "ThirdName"
                                }
                            }
                        }
                    }
                }
            };

            return qualifyings;
        }

        private List<List<LapsDataResponse>> GenerateLaps()
        {
            var lapsList = new List<List<LapsDataResponse>>
            {
                new List<LapsDataResponse>
                {
                    new LapsDataResponse
                    {
                        number = "1",
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "1",
                                position = "1"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "3",
                                position = "2"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "4",
                                position = "3"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "2",
                                position = "4"
                            }
                        }
                    },
                    new LapsDataResponse
                    {
                        number = "2",
                        Timings = new List<TimingsDataResponse>
                        {
                            new TimingsDataResponse
                            {
                                driverId = "1",
                                position = "1"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "4",
                                position = "2"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "2",
                                position = "3"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "3",
                                position = "4"
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

        private List<List<DriverStandingsDataResponse>> GenerateDriversStandings()
        {
            var driversStandings = new List<List<DriverStandingsDataResponse>>
            {
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            familyName = "FirstFamily",
                            givenName= "FirstName"
                        },
                        points = "25"
                    },
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            familyName = "SecondFamily",
                            givenName= "SecondName"
                        },
                        points = "18"
                    }
                },
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            familyName = "FirstFamily",
                            givenName= "FirstName"
                        },
                        points = "25"
                    },
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            familyName = "SecondFamily",
                            givenName= "SecondName"
                        },
                        points = "15"
                    }
                },
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            familyName = "FirstFamily",
                            givenName= "FirstName"
                        },
                        points = "43"
                    },
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            familyName = "SecondFamily",
                            givenName= "SecondName"
                        },
                        points = "40"
                    }
                }
            };

            return driversStandings;
        }

        private List<List<ConstructorStandingsDataResponse>> GenerateConstructorsStandings()
        {
            var constructorsStandings = new List<List<ConstructorStandingsDataResponse>>
            {
                new List<ConstructorStandingsDataResponse>
                {
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "First"
                        },
                        points = "25"
                    },
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "Second"
                        },
                        points = "18"
                    }
                },
                new List<ConstructorStandingsDataResponse>
                {
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "First"
                        },
                        points = "25"
                    },
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "Second"
                        },
                        points = "15"
                    }
                },
                new List<ConstructorStandingsDataResponse>
                {
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "First"
                        },
                        points = "43"
                    },
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "Second"
                        },
                        points = "40"
                    }
                }
            };

            return constructorsStandings;
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
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

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
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

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
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);
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
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

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
            var expectedGrandslams = new List<DidNotFinishModel> { new DidNotFinishModel { Name = "SecondName SecondFamily", DidNotFinishCount = 2 }, new DidNotFinishModel { Name = "FirstName FirstFamily", DidNotFinishCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetNonFinishers(from, to);
                actual.Sort((x, y) => y.DidNotFinishCount.CompareTo(x.DidNotFinishCount));

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
                            Name = "FirstName FirstFamily",
                            PositionChange = 0
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "SecondName SecondFamily",
                            PositionChange = -1
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
                            PositionChange = 0
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "SecondName SecondFamily",
                            PositionChange = -3
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

        [TestMethod]
        public void GetConstructorsFrontRows_ReturnAggregatedConstructorsFrontRowsCountList_IfThereAreAnyConstructorsWithFrontRows()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsFrontRows = new List<FrontRowModel>
            {
                new FrontRowModel
                {
                    Name = "FirstConstructor",
                    FrontRowCount = 2
                },
                new FrontRowModel
                {
                    Name = "SecondConstructor",
                    FrontRowCount = 1
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsFrontRows(from, to);
                actual.Sort((x, y) => y.FrontRowCount.CompareTo(x.FrontRowCount));

                // Assert
                Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsFrontRows.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsFrontRows[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowCount, actual[i].FrontRowCount);
                }
            }
        }

        [TestMethod]
        public void GetConstructorsFrontRows_ReturnEmptyList_IfThereAreNoConstructorsWithFrontRows()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsFrontRows = new List<FrontRowModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsFrontRows(from, to);

                // Assert
                Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetDriversFinishingPositions_ReturnAggregatedDriversFinishingPositionsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversFinishingPositions = new List<DriverFinishingPositionsModel>
            {
                new DriverFinishingPositionsModel
                {
                    Name = "FirstName FirstFamily",
                    FinishingPositions = new List<FinishingPositionModel>
                    {
                        new FinishingPositionModel
                        {
                            FinishingPosition = 1,
                            Count = 2
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 2,
                            Count = 1
                        }
                    }
                },
                new DriverFinishingPositionsModel
                {
                    Name = "SecondName SecondFamily",
                    FinishingPositions = new List<FinishingPositionModel>
                    {
                        new FinishingPositionModel
                        {
                            FinishingPosition = 1,
                            Count = 1
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 3,
                            Count = 1
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 5,
                            Count = 1
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversFinishingPositions(from, to);
                actual.ForEach(driver => driver.FinishingPositions.Sort((x, y) => x.FinishingPosition.CompareTo(y.FinishingPosition)));
                actual.Sort((x, y) => x.Name.CompareTo(y.Name));

                // Assert
                Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);

                for (int i = 0; i < expectedDriversFinishingPositions.Count; i++)
                {
                    Assert.AreEqual(expectedDriversFinishingPositions[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions.Count, actual[i].FinishingPositions.Count);

                    for (int j = 0; j < expectedDriversFinishingPositions[i].FinishingPositions.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPosition, actual[i].FinishingPositions[j].FinishingPosition);
                        Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].Count, actual[i].FinishingPositions[j].Count);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversFinishingPositions_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversFinishingPositions = new List<DriverFinishingPositionsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversFinishingPositions(from, to);

                // Assert
                Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetDriversStandingsChanges_ReturnAggregatedDriversStandingsChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversStandingsChanges = new List<OLDSeasonStandingsChangesModel>
            {
                new OLDSeasonStandingsChangesModel
                {
                    Season = 1,
                    Rounds = new List<OLDRoundModel>
                    {
                        new OLDRoundModel
                        {
                            Round = 1,
                            Standings = new List<OLDStandingModel>
                            {
                                new OLDStandingModel
                                {
                                    Name = "FirstName FirstFamily",
                                    Position = 1,
                                    Points = 25
                                },
                                new OLDStandingModel
                                {
                                    Name = "SecondName SecondFamily",
                                    Position = 2,
                                    Points = 18
                                }
                            }
                        }
                    }
                },
                new OLDSeasonStandingsChangesModel
                {
                    Season = 2,
                    Rounds = new List<OLDRoundModel>
                    {
                        new OLDRoundModel
                        {
                            Round = 1,
                            Standings = new List<OLDStandingModel>
                            {
                                new OLDStandingModel
                                {
                                    Name = "FirstName FirstFamily",
                                    Position = 1,
                                    Points = 25
                                },
                                new OLDStandingModel
                                {
                                    Name = "SecondName SecondFamily",
                                    Position = 2,
                                    Points = 15
                                }
                            }
                        },
                        new OLDRoundModel
                        {
                            Round = 2,
                            Standings = new List<OLDStandingModel>
                            {
                                new OLDStandingModel
                                {
                                    Name = "FirstName FirstFamily",
                                    Position = 1,
                                    Points = 43
                                },
                                new OLDStandingModel
                                {
                                    Name = "SecondName SecondFamily",
                                    Position = 2,
                                    Points = 40
                                }
                            }
                        }
                    }
                }
            };
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(1)).Returns(GenerateRaces()[0].Count);
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(2)).Returns(GenerateRaces()[1].Count);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(1, 1)).Returns(GenerateDriversStandings()[0]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(2, 1)).Returns(GenerateDriversStandings()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(2, 2)).Returns(GenerateDriversStandings()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversStandingsChanges(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));
                actual.ForEach(model => model.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round)));

                // Assert
                Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);

                for (int i = 0; i < expectedDriversStandingsChanges.Count; i++)
                {
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds.Count, actual[i].Rounds.Count);

                    for (int j = 0; j < expectedDriversStandingsChanges[i].Rounds.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Round, actual[i].Rounds[j].Round);
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Standings.Count, actual[i].Rounds[j].Standings.Count);

                        for (int l = 0; l < expectedDriversStandingsChanges[i].Rounds[j].Standings.Count; l++)
                        {
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Standings[l].Name, actual[i].Rounds[j].Standings[l].Name);
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Standings[l].Position, actual[i].Rounds[j].Standings[l].Position);
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Standings[l].Points, actual[i].Rounds[j].Standings[l].Points);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversStandingsChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversStandingsChanges = new List<OLDSeasonStandingsChangesModel> { new OLDSeasonStandingsChangesModel { Season = 1 }, new OLDSeasonStandingsChangesModel { Season = 2 } };
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(1)).Returns(0);
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(2)).Returns(0);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversStandingsChanges(from, to);

                // Assert
                Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetConstructorsStandingsChanges_ReturnAggregatedConstructorsStandingsChangesList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsStandingsChanges = new List<OLDSeasonStandingsChangesModel>
            {
                new OLDSeasonStandingsChangesModel
                {
                    Season = 1,
                    Rounds = new List<OLDRoundModel>
                    {
                        new OLDRoundModel
                        {
                            Round = 1,
                            Standings = new List<OLDStandingModel>
                            {
                                new OLDStandingModel
                                {
                                    Name = "First",
                                    Position = 1,
                                    Points = 25
                                },
                                new OLDStandingModel
                                {
                                    Name = "Second",
                                    Position = 2,
                                    Points = 18
                                }
                            }
                        }
                    }
                },
                new OLDSeasonStandingsChangesModel
                {
                    Season = 2,
                    Rounds = new List<OLDRoundModel>
                    {
                        new OLDRoundModel
                        {
                            Round = 1,
                            Standings = new List<OLDStandingModel>
                            {
                                new OLDStandingModel
                                {
                                    Name = "First",
                                    Position = 1,
                                    Points = 25
                                },
                                new OLDStandingModel
                                {
                                    Name = "Second",
                                    Position = 2,
                                    Points = 15
                                }
                            }
                        },
                        new OLDRoundModel
                        {
                            Round = 2,
                            Standings = new List<OLDStandingModel>
                            {
                                new OLDStandingModel
                                {
                                    Name = "First",
                                    Position = 1,
                                    Points = 43
                                },
                                new OLDStandingModel
                                {
                                    Name = "Second",
                                    Position = 2,
                                    Points = 40
                                }
                            }
                        }
                    }
                }
            };
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(1)).Returns(GenerateRaces()[0].Count);
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(2)).Returns(GenerateRaces()[1].Count);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(1, 1)).Returns(GenerateConstructorsStandings()[0]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(2, 1)).Returns(GenerateConstructorsStandings()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(2, 2)).Returns(GenerateConstructorsStandings()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsStandingsChanges(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));
                actual.ForEach(model => model.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round)));

                // Assert
                Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsStandingsChanges.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds.Count, actual[i].Rounds.Count);

                    for (int j = 0; j < expectedConstructorsStandingsChanges[i].Rounds.Count; j++)
                    {
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Round, actual[i].Rounds[j].Round);
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Standings.Count, actual[i].Rounds[j].Standings.Count);

                        for (int l = 0; l < expectedConstructorsStandingsChanges[i].Rounds[j].Standings.Count; l++)
                        {
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Standings[l].Name, actual[i].Rounds[j].Standings[l].Name);
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Standings[l].Position, actual[i].Rounds[j].Standings[l].Position);
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Standings[l].Points, actual[i].Rounds[j].Standings[l].Points);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GetConstructorsStandingsChanges_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversFinishingPositions = new List<OLDSeasonStandingsChangesModel> { new OLDSeasonStandingsChangesModel { Season = 1 }, new OLDSeasonStandingsChangesModel { Season = 2 } };
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(1)).Returns(0);
            _racesDataAccess.Setup((racesDataAccess) => racesDataAccess.GetRacesCountFrom(2)).Returns(0);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsStandingsChanges(from, to);

                // Assert
                Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetDriversPositionChangesDuringRace_ReturnAggregatedDriversPositionChangesduringRaceList_IfThereAreAnyLapsInRace()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversPositionChangesDuringRace = new List<RacePositionChangesModel>
            {
                new RacePositionChangesModel
                {
                    LapNumber = 1,
                    DriversPositions = new List<DriverPositionModel>
                    {
                        new DriverPositionModel
                        {
                            Name = "First",
                            Position = 1
                        },
                        new DriverPositionModel
                        {
                            Name = "Third",
                            Position = 2
                        },
                        new DriverPositionModel
                        {
                            Name = "Forth",
                            Position = 3
                        },
                        new DriverPositionModel
                        {
                            Name = "Second",
                            Position = 4
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    LapNumber = 2,
                    DriversPositions = new List<DriverPositionModel>
                    {
                        new DriverPositionModel
                        {
                            Name = "First",
                            Position = 1
                        },
                        new DriverPositionModel
                        {
                            Name = "Forth",
                            Position = 2
                        },
                        new DriverPositionModel
                        {
                            Name = "Second",
                            Position = 3
                        },
                        new DriverPositionModel
                        {
                            Name = "Third",
                            Position = 4
                        }
                    }
                }
            };
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(1, 1)).Returns(GenerateLaps()[0]);
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriverName("1")).Returns("First");
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriverName("2")).Returns("Second");
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriverName("3")).Returns("Third");
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriverName("4")).Returns("Forth");

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversPositionChangesDuringRace(season, race);
                actual.Sort((x, y) => x.LapNumber.CompareTo(y.LapNumber));

                // Assert
                Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);

                for (int i = 0; i < expectedDriversPositionChangesDuringRace.Count; i++)
                {
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].LapNumber, actual[i].LapNumber);
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].DriversPositions.Count, actual[i].DriversPositions.Count);

                    for (int j = 0; j < expectedDriversPositionChangesDuringRace[i].DriversPositions.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].DriversPositions[j].Name, actual[i].DriversPositions[j].Name);
                        Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].DriversPositions[j].Position, actual[i].DriversPositions[j].Position);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversPositionChangesDuringRace_ReturnEmptyList_IfThereAreNoLapsInRace()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversFinishingPositions = new List<RacePositionChangesModel>();
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(season, race)).Returns(new List<LapsDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversPositionChangesDuringRace(season, race);

                // Assert
                Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
            }
        }
    }
}
