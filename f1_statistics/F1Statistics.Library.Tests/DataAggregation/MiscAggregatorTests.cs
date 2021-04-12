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
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "FirstCircuit"
                        },
                        round = "1",
                        raceName = "FirstRace",
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
                                },
                                laps = "10"
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
                                },
                                laps = "1"
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
                            circuitName = "FirstCircuit"
                        },
                        round = "1",
                        raceName = "SecondRace",
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
                                },
                                laps = "50"
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
                                },
                                laps = "0"
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "SecondCircuit"
                        },
                        round = "2",
                        raceName = "ThirdRace",
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
                                },
                                laps = "15"
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
                                },
                                laps = "15"
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
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "FirstCircuit"
                        },
                        round = "1",
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                position = "1",
                                Driver = new DriverDataResponse
                                {
                                    driverId = "1",
                                    familyName = "FirstFamily", 
                                    givenName= "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "First"
                                }
                            },
                            new QualifyingResultsDataResponse
                            {
                                position = "2",
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "First"
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
                            circuitName = "FirstCircuit"
                        },
                        round = "1",
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                position = "1",
                                Driver = new DriverDataResponse
                                {
                                    driverId = "1",
                                    familyName = "FirstFamily",
                                    givenName = "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "First"
                                }
                            },
                            new QualifyingResultsDataResponse
                            {
                                position = "2",
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "First"
                                }
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "SecondCircuit"
                        },
                        round = "2",
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                position = "1",
                                Driver = new DriverDataResponse
                                {
                                    driverId = "3",
                                    familyName = "ThirdFamily",
                                    givenName = "ThirdName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "Second"
                                }
                            },
                            new QualifyingResultsDataResponse
                            {
                                position = "2",
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "Second"
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
                                position = "1",
                                time = "0:1.1"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "3",
                                position = "2",
                                time = "1:2.0"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "4",
                                position = "3",
                                time = "1:5"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "2",
                                position = "4",
                                time = "1:11"
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
                                position = "1",
                                time = "1:2.25"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "4",
                                position = "2",
                                time = "2:0.0"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "2",
                                position = "3",
                                time = "25.101"
                            },
                            new TimingsDataResponse
                            {
                                driverId = "3",
                                position = "4",
                                time = "1:55.05"
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
            var expectedNonFinishers = new List<DidNotFinishModel> 
            {
                new DidNotFinishModel 
                {
                    Name = "SecondName SecondFamily",
                    DidNotFinishByYear = new List<DidNotFinishByYearModel>
                    {
                        new DidNotFinishByYearModel
                        {
                            Year = 1,
                            DidNotFinishInformation = new List<DidNotFinishInformationModel>
                            {
                                new DidNotFinishInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    LapsCompleted = 1
                                }
                            }
                        },
                        new DidNotFinishByYearModel
                        {
                            Year = 2,
                            DidNotFinishInformation = new List<DidNotFinishInformationModel>
                            {
                                new DidNotFinishInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    LapsCompleted = 0
                                }
                            }
                        }
                    }
                },
                new DidNotFinishModel 
                {
                    Name = "FirstName FirstFamily",
                    DidNotFinishByYear = new List<DidNotFinishByYearModel>
                    {
                        new DidNotFinishByYearModel
                        {
                            Year = 2,
                            DidNotFinishInformation = new List<DidNotFinishInformationModel>
                            {
                                new DidNotFinishInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    LapsCompleted = 15
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetNonFinishers(from, to);
                actual.Sort((x, y) => y.TotalDidNotFinishCount.CompareTo(x.TotalDidNotFinishCount));
                actual.ForEach(model => model.DidNotFinishByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedNonFinishers.Count, actual.Count);

                for (int i = 0; i < expectedNonFinishers.Count; i++)
                {
                    Assert.AreEqual(expectedNonFinishers[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedNonFinishers[i].TotalDidNotFinishCount, actual[i].TotalDidNotFinishCount);
                    Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear.Count, actual[i].DidNotFinishByYear.Count);

                    for (int j = 0; j < expectedNonFinishers[i].DidNotFinishByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].Year, actual[i].DidNotFinishByYear[j].Year);
                        Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].YearDidNotFinishCount, actual[i].DidNotFinishByYear[j].YearDidNotFinishCount);
                        Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation.Count, actual[i].DidNotFinishByYear[j].DidNotFinishInformation.Count);

                        for (int k = 0; k < expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation.Count; k++)
                        {
                            Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation[k].CircuitName, actual[i].DidNotFinishByYear[j].DidNotFinishInformation[k].CircuitName);
                            Assert.AreEqual(expectedNonFinishers[i].DidNotFinishByYear[j].DidNotFinishInformation[k].LapsCompleted, actual[i].DidNotFinishByYear[j].DidNotFinishInformation[k].LapsCompleted);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GetNonFinishers_ReturnEmptyList_IfAllDriversFinishedEveryRace()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedNonFinishers = new List<DidNotFinishModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetNonFinishers(from, to);

                // Assert
                Assert.AreEqual(expectedNonFinishers.Count, actual.Count);
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
                    Year = 1,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "FirstName FirstFamily",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    RacePositionChange = 0
                                }
                            },
                            ChampionshipPosition = 1
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "SecondName SecondFamily",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    RacePositionChange = -1
                                }
                            },
                            ChampionshipPosition = 2
                        }
                    }
                },
                new SeasonPositionChangesModel
                {
                    Year = 2,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "FirstName FirstFamily",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    RacePositionChange = 0
                                },
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    RacePositionChange = 0
                                }
                            },
                            ChampionshipPosition = 1
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "SecondName SecondFamily",
                            DriverPositionChangeInformation = new List<DriverPositionChangeInformationModel>
                            {
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    RacePositionChange = -3
                                },
                                new DriverPositionChangeInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    RacePositionChange = 0
                                }
                            },
                            ChampionshipPosition = 2
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFrom(1)).Returns(GenerateDriversStandings()[0]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFrom(2)).Returns(GenerateDriversStandings()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetSeasonPositionChanges(from, to);
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));
                actual.ForEach(season => season.PositionChanges.Sort((x, y) => y.TotalPositionChange.CompareTo(x.TotalPositionChange)));
                actual.ForEach(season => season.PositionChanges.ForEach(information => information.DriverPositionChangeInformation.Sort((x, y) => x.CircuitName.CompareTo(y.CircuitName))));

                // Assert
                Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);

                for (int i = 0; i < expectedSeasonPositionChanges.Count; i++)
                {
                    Assert.AreEqual(expectedSeasonPositionChanges[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges.Count, actual[i].PositionChanges.Count);

                    for (int j = 0; j < expectedSeasonPositionChanges[i].PositionChanges.Count; j++)
                    {
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].Name, actual[i].PositionChanges[j].Name);
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].TotalPositionChange, actual[i].PositionChanges[j].TotalPositionChange);
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].ChampionshipPosition, actual[i].PositionChanges[j].ChampionshipPosition);
                        Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation.Count, actual[i].PositionChanges[j].DriverPositionChangeInformation.Count);

                        for (int k = 0; k < expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation.Count; k++)
                        {
                            Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation[k].CircuitName, actual[i].PositionChanges[j].DriverPositionChangeInformation[k].CircuitName);
                            Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].DriverPositionChangeInformation[k].RacePositionChange, actual[i].PositionChanges[j].DriverPositionChangeInformation[k].RacePositionChange);
                        }
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
            var expectedSeasonPositionChanges = new List<SeasonPositionChangesModel> { new SeasonPositionChangesModel { Year = 1 }, new SeasonPositionChangesModel { Year = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFrom(1)).Returns(new List<DriverStandingsDataResponse>());
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFrom(2)).Returns(new List<DriverStandingsDataResponse>());

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
                    Name = "First",
                    FrontRowInformation = new List<FrontRowInformationModel>
                    {
                        new FrontRowInformationModel
                        {
                            CircuitName = "FirstCircuit",
                            CircuitFrontRowCount = 2
                        }
                    }
                },
                new FrontRowModel
                {
                    Name = "Second",
                    FrontRowInformation = new List<FrontRowInformationModel>
                    {
                        new FrontRowInformationModel
                        {
                            CircuitName = "SecondCircuit",
                            CircuitFrontRowCount = 1
                        }
                    }
                }
            };
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetConstructorsFrontRows(from, to);
                actual.Sort((x, y) => y.TotalFrontRowCount.CompareTo(x.TotalFrontRowCount));

                // Assert
                Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsFrontRows.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsFrontRows[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedConstructorsFrontRows[i].TotalFrontRowCount, actual[i].TotalFrontRowCount);
                    Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowInformation.Count, actual[i].FrontRowInformation.Count);

                    for (int k = 0; k < expectedConstructorsFrontRows[i].FrontRowInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowInformation[k].CircuitName, actual[i].FrontRowInformation[k].CircuitName);
                        Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowInformation[k].CircuitFrontRowCount, actual[i].FrontRowInformation[k].CircuitFrontRowCount);
                    }
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
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingsDataAccess) => qualifyingsDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

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
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    FinishedRace = true
                                },
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    FinishedRace = true
                                }
                            }
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 2,
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    FinishedRace = false
                                }
                            }
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
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    FinishedRace = true
                                }
                            }
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 3,
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    FinishedRace = false
                                }
                            }
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 5,
                            FinishingPositionInformation = new List<FinishingPositionInformationModel>
                            {
                                new FinishingPositionInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    FinishedRace = false
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int t = 0; t < 10000; t++)
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
                        Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation.Count, actual[i].FinishingPositions[j].FinishingPositionInformation.Count);

                        for (int k = 0; k < expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation.Count; k++)
                        {
                            Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation[k].CircuitName, actual[i].FinishingPositions[j].FinishingPositionInformation[k].CircuitName);
                            Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPositionInformation[k].FinishedRace, actual[i].FinishingPositions[j].FinishingPositionInformation[k].FinishedRace);
                        }
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
            var expectedDriversStandingsChanges = new List<SeasonStandingsChangesModel>
            {
                new SeasonStandingsChangesModel
                {
                    Year = 1,
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "FirstName FirstFamily",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    RoundName = "FirstRace"
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "SecondName SecondFamily",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    RoundName = "FirstRace"
                                }
                            }
                        }
                    }
                },
                new SeasonStandingsChangesModel
                {
                    Year = 2,
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "FirstName FirstFamily",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    RoundName = "ThirdRace"
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "SecondName SecondFamily",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    RoundName = "ThirdRace"
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(1, 1)).Returns(GenerateDriversStandings()[0]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(2, 1)).Returns(GenerateDriversStandings()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(2, 2)).Returns(GenerateDriversStandings()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversStandingsChanges(from, to);
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));
                actual.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));

                // Assert
                Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);

                for (int i = 0; i < expectedDriversStandingsChanges.Count; i++)
                {
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                    for (int j = 0; j < expectedDriversStandingsChanges[i].Standings.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                        for (int l = 0; l < expectedDriversStandingsChanges[i].Standings[j].Rounds.Count; l++)
                        {
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[l].Round, actual[i].Standings[j].Rounds[l].Round);
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[l].Position, actual[i].Standings[j].Rounds[l].Position);
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[l].RoundName, actual[i].Standings[j].Rounds[l].RoundName);
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
            var expectedDriversStandingsChanges = new List<SeasonStandingsChangesModel> { new SeasonStandingsChangesModel { Year = 1 }, new SeasonStandingsChangesModel { Year = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

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
            var expectedConstructorsStandingsChanges = new List<SeasonStandingsChangesModel>
            {
                new SeasonStandingsChangesModel
                {
                    Year = 1,
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "First",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    RoundName = "FirstRace"
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "Second",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    RoundName = "FirstRace"
                                }
                            }
                        }
                    }
                },
                new SeasonStandingsChangesModel
                {
                    Year = 2,
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "First",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    RoundName = "ThirdRace"
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "Second",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    RoundName = "ThirdRace"
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(1, 1)).Returns(GenerateConstructorsStandings()[0]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(2, 1)).Returns(GenerateConstructorsStandings()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(2, 2)).Returns(GenerateConstructorsStandings()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsStandingsChanges(from, to);
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));
                actual.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));

                // Assert
                Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsStandingsChanges.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                    for (int j = 0; j < expectedConstructorsStandingsChanges[i].Standings.Count; j++)
                    {
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                        for (int l = 0; l < expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count; l++)
                        {
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[l].Round, actual[i].Standings[j].Rounds[l].Round);
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[l].Position, actual[i].Standings[j].Rounds[l].Position);
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[l].RoundName, actual[i].Standings[j].Rounds[l].RoundName);
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
            var expectedDriversFinishingPositions = new List<SeasonStandingsChangesModel> { new SeasonStandingsChangesModel { Year = 1 }, new SeasonStandingsChangesModel { Year = 2 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

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
                    Name = "First",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 1
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 1
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    Name = "Forth",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 3
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 2
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    Name = "Second",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 4
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 3
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    Name = "Third",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 2
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
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
                actual.Sort((x, y) => x.Name.CompareTo(y.Name));
                actual.ForEach(driver => driver.Laps.Sort((x, y) => x.LapNumber.CompareTo(y.LapNumber)));

                // Assert
                Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);

                for (int i = 0; i < expectedDriversPositionChangesDuringRace.Count; i++)
                {
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps.Count, actual[i].Laps.Count);

                    for (int j = 0; j < expectedDriversPositionChangesDuringRace[i].Laps.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps[j].LapNumber, actual[i].Laps[j].LapNumber);
                        Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps[j].Position, actual[i].Laps[j].Position);
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

        [TestMethod]
        public void GetDriversLapTimes_ReturnAggregatedDriversLapTimesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversLapTimes = new List<LapTimesModel>
            {
                new LapTimesModel
                {
                    Name = "First",
                    Timings = new List<double>
                    {
                        1.1,
                        62.25
                    }
                },
                new LapTimesModel
                {
                    Name = "Forth",
                    Timings = new List<double>
                    {
                        65,
                        120
                    }
                },
                new LapTimesModel
                {
                    Name = "Second",
                    Timings = new List<double>
                    {
                        25.101,
                        71
                    }
                },
                new LapTimesModel
                {
                    Name = "Third",
                    Timings = new List<double>
                    {
                        62,
                        115.05
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
                var actual = _aggregator.GetDriversLapTimes(season, race);
                actual.Sort((x, y) => x.Name.CompareTo(y.Name));
                actual.ForEach(model => model.Timings.Sort((x, y) => x.CompareTo(y)));

                // Assert
                Assert.AreEqual(expectedDriversLapTimes.Count, actual.Count);

                for (int i = 0; i < expectedDriversLapTimes.Count; i++)
                {
                    Assert.AreEqual(expectedDriversLapTimes[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversLapTimes[i].Timings.Count, actual[i].Timings.Count);

                    for (int j = 0; j < expectedDriversLapTimes[i].Timings.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversLapTimes[i].Timings[j], actual[i].Timings[j]);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversLapTimes_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversLapTimes = new List<LapTimesModel>();
            _lapsDataAccess.Setup((lapsDataAccess) => lapsDataAccess.GetLapsFrom(season, race)).Returns(new List<LapsDataResponse>());

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversLapTimes(season, race);

                // Assert
                Assert.AreEqual(expectedDriversLapTimes.Count, actual.Count);
            }
        }
    }
}
