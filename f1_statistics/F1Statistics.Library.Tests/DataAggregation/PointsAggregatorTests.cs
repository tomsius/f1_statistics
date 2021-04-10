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
    public class PointsAggregatorTests
    {
        private PointsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;
        private Mock<IStandingsDataAccess> _standingsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();
            _standingsDataAccess = new Mock<IStandingsDataAccess>();

            _aggregator = new PointsAggregator(_resultsDataAccess.Object, _standingsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        raceName = "FirstRace"
                    }
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        raceName = "SecondRace"
                    },
                    new RacesDataResponse
                    {
                        raceName = "ThirdRace"
                    }
                }
            };

            return racesList;
        }

        private List<List<DriverStandingsDataResponse>> GenerateDriverStandings()
        {
            var standings = new List<List<DriverStandingsDataResponse>> 
            {
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            givenName = "FirstName",
                            familyName = "FirstFamily"
                        },
                        points = "100"
                    },
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            givenName = "SecondName",
                            familyName = "SecondFamily"
                        },
                        points = "50"
                    } 
                },
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            givenName = "FirstName",
                            familyName = "FirstFamily"
                        },
                        points = "200"
                    },
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            givenName = "SecondName",
                            familyName = "SecondFamily"
                        },
                        points = "100"
                    }
                }
            };

            return standings;
        }

        private List<List<ConstructorStandingsDataResponse>> GenerateConstructorStandings()
        {
            var standings = new List<List<ConstructorStandingsDataResponse>> 
            {
                new List<ConstructorStandingsDataResponse>
                {
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "First"
                        },
                        points = "100"
                    },
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "Second"
                        },
                        points = "50"
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
                        points = "200"
                    },
                    new ConstructorStandingsDataResponse
                    {
                        Constructor = new ConstructorDataResponse
                        {
                            name = "Second"
                        },
                        points = "100"
                    }
                }
            };

            return standings;
        }

        private List<List<DriverStandingsDataResponse>> GenerateDriversStandingsAfterEachRace()
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

        private List<List<ConstructorStandingsDataResponse>> GenerateConstructorsStandingsAfterEachRace()
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
        public void GetDriversPointsPerSeason_ReturnAggregatedDriversPointsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversPoints = new List<SeasonPointsModel>
            {
                new SeasonPointsModel
                {
                    Year = 1,
                    ScoredPoints = new List<PointsModel>
                    {
                        new PointsModel
                        {
                            Name= "FirstName FirstFamily",
                            Points = 100
                        },
                        new PointsModel
                        {
                            Name = "SecondName SecondFamily",
                            Points = 50
                        }
                    }
                },
                new SeasonPointsModel
                {
                    Year = 2,
                    ScoredPoints = new List<PointsModel>
                    {
                        new PointsModel
                        {
                            Name= "FirstName FirstFamily",
                            Points = 200
                        },
                        new PointsModel
                        {
                            Name = "SecondName SecondFamily",
                            Points = 100
                        }
                    }
                }
            };
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(GenerateDriverStandings()[0]);
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(GenerateDriverStandings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversPointsPerSeason(from, to);
                actual.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));

                // Assert
                Assert.AreEqual(expectedDriversPoints.Count, actual.Count);

                for (int i = 0; i < expectedDriversPoints.Count; i++)
                {
                    Assert.AreEqual(expectedDriversPoints[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedDriversPoints[i].TotalPoints, actual[i].TotalPoints);

                    for (int j = 0; j < expectedDriversPoints[i].ScoredPoints.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversPoints[i].ScoredPoints[j].Name, actual[i].ScoredPoints[j].Name);
                        Assert.AreEqual(expectedDriversPoints[i].ScoredPoints[j].Points, actual[i].ScoredPoints[j].Points);
                    }
                } 
            }
        }

        [TestMethod]
        public void GetDriversPointsPerSeason_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversPoints = new List<PointsModel> { new PointsModel(), new PointsModel() };
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(new List<DriverStandingsDataResponse>());
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(new List<DriverStandingsDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversPointsPerSeason(from, to);

                // Assert
                Assert.AreEqual(expectedDriversPoints.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsPointsPerSeason_ReturnAggregatedConstructorsPointsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPoints = new List<SeasonPointsModel>
            {
                new SeasonPointsModel
                {
                    Year = 1,
                    ScoredPoints = new List<PointsModel>
                    {
                        new PointsModel
                        {
                            Name= "First",
                            Points = 100
                        },
                        new PointsModel
                        {
                            Name = "Second",
                            Points = 50
                        }
                    }
                },
                new SeasonPointsModel
                {
                    Year = 2,
                    ScoredPoints = new List<PointsModel>
                    {
                        new PointsModel
                        {
                            Name= "First",
                            Points = 200
                        },
                        new PointsModel
                        {
                            Name = "Second",
                            Points = 100
                        }
                    }
                }
            };
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(GenerateConstructorStandings()[0]);
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(GenerateConstructorStandings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsPointsPerSeason(from, to);
                actual.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));

                // Assert
                Assert.AreEqual(expectedConstructorsPoints.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsPoints.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsPoints[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedConstructorsPoints[i].TotalPoints, actual[i].TotalPoints);

                    for (int j = 0; j < expectedConstructorsPoints[i].ScoredPoints.Count; j++)
                    {
                        Assert.AreEqual(expectedConstructorsPoints[i].ScoredPoints[j].Name, actual[i].ScoredPoints[j].Name);
                        Assert.AreEqual(expectedConstructorsPoints[i].ScoredPoints[j].Points, actual[i].ScoredPoints[j].Points);
                    }
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsPointsPerSeason_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPoints = new List<PointsModel> { new PointsModel(), new PointsModel() };
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(new List<ConstructorStandingsDataResponse>());
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(new List<ConstructorStandingsDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsPointsPerSeason(from, to);

                // Assert
                Assert.AreEqual(expectedConstructorsPoints.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetDriversWinnersPointsPerSeason_ReturnAggregatedDriversWinnersPointsList_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversWinnersPoints = new List<SeasonWinnersPointsModel>
            {
                new SeasonWinnersPointsModel
                {
                    Year = 1,
                    Winner = "FirstName FirstFamily",
                    Points = 100,
                    RacesCount = 1
                },
                new SeasonWinnersPointsModel
                {
                    Year = 2,
                    Winner = "FirstName FirstFamily",
                    Points = 200,
                    RacesCount = 2
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(GenerateDriverStandings()[0]);
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(GenerateDriverStandings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversWinnersPointsPerSeason(from, to);
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));

                // Assert
                Assert.AreEqual(expectedDriversWinnersPoints.Count, actual.Count);

                for (int i = 0; i < expectedDriversWinnersPoints.Count; i++)
                {
                    Assert.AreEqual(expectedDriversWinnersPoints[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedDriversWinnersPoints[i].Winner, actual[i].Winner);
                    Assert.AreEqual(expectedDriversWinnersPoints[i].Points, actual[i].Points);
                    Assert.AreEqual(expectedDriversWinnersPoints[i].RacesCount, actual[i].RacesCount);
                } 
            }
        }

        [TestMethod]
        public void GetDriversWinnersPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversWinnersPoints = new List<SeasonWinnersPointsModel>();
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(new List<DriverStandingsDataResponse>());
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(new List<DriverStandingsDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversWinnersPointsPerSeason(from, to);

                // Assert
                Assert.AreEqual(expectedDriversWinnersPoints.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsPointsPerSeason_ReturnAggregatedConstructorsPointsList_IfThereAreAnyWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsWinnersPoints = new List<SeasonWinnersPointsModel>
            {
                new SeasonWinnersPointsModel
                {
                    Year = 1,
                    Winner = "First",
                    Points = 100,
                    RacesCount = 1
                },
                new SeasonWinnersPointsModel
                {
                    Year = 2,
                    Winner = "First",
                    Points = 200,
                    RacesCount = 2
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(GenerateConstructorStandings()[0]);
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(GenerateConstructorStandings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWinnersPointsPerSeason(from, to);
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));

                // Assert
                Assert.AreEqual(expectedConstructorsWinnersPoints.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsWinnersPoints.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsWinnersPoints[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedConstructorsWinnersPoints[i].Winner, actual[i].Winner);
                    Assert.AreEqual(expectedConstructorsWinnersPoints[i].Points, actual[i].Points);
                    Assert.AreEqual(expectedConstructorsWinnersPoints[i].RacesCount, actual[i].RacesCount);
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPoints = new List<SeasonWinnersPointsModel>();
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(new List<ConstructorStandingsDataResponse>());
            _standingsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(new List<ConstructorStandingsDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsWinnersPointsPerSeason(from, to);

                // Assert
                Assert.AreEqual(expectedConstructorsPoints.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetDriversPointsChanges_ReturnAggregatedDriversPointsList_IfThereAreAnyDrivers()
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
                                    Points = 25,
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
                                    Points = 18,
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
                                    Points = 25,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Points = 43,
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
                                    Points = 15,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Points = 40,
                                    RoundName = "ThirdRace"
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(1, 1)).Returns(GenerateDriversStandingsAfterEachRace()[0]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(2, 1)).Returns(GenerateDriversStandingsAfterEachRace()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetDriverStandingsFromRace(2, 2)).Returns(GenerateDriversStandingsAfterEachRace()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversPointsChanges(from, to);
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
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[l].Points, actual[i].Standings[j].Rounds[l].Points);
                            Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[l].RoundName, actual[i].Standings[j].Rounds[l].RoundName);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversPointsChanges_ReturnEmptyList_IfThereAreNoDrivers()
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
                var actual = _aggregator.GetDriversPointsChanges(from, to);

                // Assert
                Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetConstructorsPointsChanges_ReturnAggregatedConstructorsPointsList_IfThereAreAnyConstructors()
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
                                    Points = 25,
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
                                    Points = 18,
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
                                    Points = 25,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Points = 43,
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
                                    Points = 15,
                                    RoundName = "SecondRace"
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Points = 40,
                                    RoundName = "ThirdRace"
                                }
                            }
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(1, 1)).Returns(GenerateConstructorsStandingsAfterEachRace()[0]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(2, 1)).Returns(GenerateConstructorsStandingsAfterEachRace()[1]);
            _standingsDataAccess.Setup((standingsDataAccess) => standingsDataAccess.GetConstructorStandingsFromRace(2, 2)).Returns(GenerateConstructorsStandingsAfterEachRace()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsPointsChanges(from, to);
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
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[l].Points, actual[i].Standings[j].Rounds[l].Points);
                            Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[l].RoundName, actual[i].Standings[j].Rounds[l].RoundName);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GetConstructorsPointsChanges_ReturnEmptyList_IfThereAreNoConstructors()
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
                var actual = _aggregator.GetConstructorsPointsChanges(from, to);

                // Assert
                Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
            }
        }
    }
}
