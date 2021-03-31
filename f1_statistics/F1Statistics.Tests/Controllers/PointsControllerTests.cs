using F1Statistics.Controllers;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Tests.Controllers
{
    [TestClass]
    public class PointsControllerTests
    {
        private PointsController _controller;
        private Mock<IPointsService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IPointsService>();

            _controller = new PointsController(_service.Object);
        }

        private List<SeasonPointsModel> GeneratePointsScorers()
        {
            var pointsScorers = new List<SeasonPointsModel>
            {
                new SeasonPointsModel
                {
                    Season = 1,
                    ScoredPoints = new List<PointsModel>
                    { 
                        new PointsModel 
                        {
                            Name = "First",
                            Points = 200
                        },
                        new PointsModel
                        {
                            Name = "Second",
                            Points = 100
                        }
                    }
                },
                new SeasonPointsModel
                {
                    Season = 2,
                    ScoredPoints = new List<PointsModel>
                    {
                        new PointsModel
                        {
                            Name = "First",
                            Points = 400
                        },
                        new PointsModel
                        {
                            Name = "Second",
                            Points = 300
                        }
                    }
                }
            };

            return pointsScorers;
        }

        private List<SeasonWinnersPointsModel> GenerateWinnersPoints()
        {
            var winnersPoints = new List<SeasonWinnersPointsModel>
            {
                new SeasonWinnersPointsModel
                {
                    Season = 1,
                    Winner = "First",
                    Points = 200,
                    RacesCount = 1
                },
                new SeasonWinnersPointsModel
                {
                    Season = 2,
                    Winner = "Second",
                    Points = 400,
                    RacesCount = 1
                }
            };

            return winnersPoints;
        }

        private List<SeasonStandingsChangesModel> GenerateSeasonPointsChanges()
        {
            var seasonStandings = new List<SeasonStandingsChangesModel>
            {
                new SeasonStandingsChangesModel
                {
                    Season = 1,
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
                                    Position = 2,
                                    Points = 18
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    Points = 33
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
                                    Position = 1,
                                    Points = 25
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    Points = 32
                                }
                            }
                        }
                    }
                },
                new SeasonStandingsChangesModel
                {
                    Season = 2,
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
                                    Points = 25
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    Points = 50
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
                                    Points = 18
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    Points = 32
                                }
                            }
                        }
                    }
                }
            };

            return seasonStandings;
        }

        [TestMethod]
        public void GetDriversPointsPerSeason_ReturnAggregatedDriversPointsPerSeasonList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedPointsScorers = GeneratePointsScorers();
            _service.Setup((service) => service.AggregateDriversPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedPointsScorers);

            // Act
            var actual = _controller.GetDriversPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedPointsScorers.Count, actual.Count);

            for (int i = 0; i < expectedPointsScorers.Count; i++)
            {
                Assert.AreEqual(expectedPointsScorers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedPointsScorers[i].ScoredPoints.Count, actual[i].ScoredPoints.Count);

                for (int j = 0; j < expectedPointsScorers[i].ScoredPoints.Count; j++)
                {
                    Assert.AreEqual(expectedPointsScorers[i].ScoredPoints[j].Name, actual[i].ScoredPoints[j].Name);
                    Assert.AreEqual(expectedPointsScorers[i].ScoredPoints[j].Points, actual[i].ScoredPoints[j].Points);
                }

                Assert.AreEqual(expectedPointsScorers[i].TotalPoints, actual[i].TotalPoints);
            }
        }

        [TestMethod]
        public void GetDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<SeasonPointsModel>();
            _service.Setup((service) => service.AggregateDriversPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetDriversPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsPointsPerSeason_ReturnAggregatedConstructorsPointsPerSeasonList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedPointsScorers = GeneratePointsScorers();
            _service.Setup((service) => service.AggregateConstructorsPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedPointsScorers);

            // Act
            var actual = _controller.GetConstructorsPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedPointsScorers.Count, actual.Count);

            for (int i = 0; i < expectedPointsScorers.Count; i++)
            {
                Assert.AreEqual(expectedPointsScorers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedPointsScorers[i].ScoredPoints.Count, actual[i].ScoredPoints.Count);

                for (int j = 0; j < expectedPointsScorers[i].ScoredPoints.Count; j++)
                {
                    Assert.AreEqual(expectedPointsScorers[i].ScoredPoints[j].Name, actual[i].ScoredPoints[j].Name);
                    Assert.AreEqual(expectedPointsScorers[i].ScoredPoints[j].Points, actual[i].ScoredPoints[j].Points);
                }

                Assert.AreEqual(expectedPointsScorers[i].TotalPoints, actual[i].TotalPoints);
            }
        }

        [TestMethod]
        public void GetConstructorsPointsPerSeason_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<SeasonPointsModel>();
            _service.Setup((service) => service.AggregateConstructorsPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversWinnersPointsPerSeason_ReturnAggregatedDriversWinnersPointsPerSeasonList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriverWinnersPoints = GenerateWinnersPoints();
            _service.Setup((service) => service.AggregateDriversWinnersPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedDriverWinnersPoints);

            // Act
            var actual = _controller.GetDriversWinnersPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedDriverWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedDriverWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedDriverWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriverWinnersPoints[i].Points, actual[i].Points);
                Assert.AreEqual(expectedDriverWinnersPoints[i].Winner, actual[i].Winner);
                Assert.AreEqual(expectedDriverWinnersPoints[i].RacesCount, actual[i].RacesCount);
            }
        }

        [TestMethod]
        public void GetDriversWinnersPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersPoints = new List<SeasonWinnersPointsModel>();
            _service.Setup((service) => service.AggregateDriversWinnersPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedWinnersPoints);

            // Act
            var actual = _controller.GetDriversWinnersPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedWinnersPoints.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsWinnersPointsPerSeason_ReturnAggregatedConstructorsWinnersPointsPerSeasonList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsWinnersPoints = GenerateWinnersPoints();
            _service.Setup((service) => service.AggregateConstructorsWinnersPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedConstructorsWinnersPoints);

            // Act
            var actual = _controller.GetConstructorsWinnersPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedConstructorsWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Points, actual[i].Points);
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Winner, actual[i].Winner);
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].RacesCount, actual[i].RacesCount);
            }
        }

        [TestMethod]
        public void GetConstructorsWinnersPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersPoints = new List<SeasonWinnersPointsModel>();
            _service.Setup((service) => service.AggregateConstructorsWinnersPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedWinnersPoints);

            // Act
            var actual = _controller.GetConstructorsWinnersPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedWinnersPoints.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversPointsChanges_ReturnAggregatedDriversPointsChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversStandingsChanges = GenerateSeasonPointsChanges();
            _service.Setup((service) => service.AggregateDriversPointsChanges(It.IsAny<OptionsModel>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _controller.GetDriversPointsChanges(options);

            // Assert
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedDriversStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedDriversStandingsChanges[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriversStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                for (int j = 0; j < expectedDriversStandingsChanges[i].Standings.Count; j++)
                {
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                    for (int k = 0; k < expectedDriversStandingsChanges[i].Standings[j].Rounds.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[k].Round, actual[i].Standings[j].Rounds[k].Round);
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[k].Points, actual[i].Standings[j].Rounds[k].Points);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversPointsChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversStandingsChanges = new List<SeasonStandingsChangesModel>();
            _service.Setup((service) => service.AggregateDriversPointsChanges(It.IsAny<OptionsModel>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _controller.GetDriversPointsChanges(options);

            // Assert
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsPointsChanges_ReturnAggregatedConstructorsPointsChangesList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsStandingsChanges = GenerateSeasonPointsChanges();
            _service.Setup((service) => service.AggregateConstructorsPointsChanges(It.IsAny<OptionsModel>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _controller.GetConstructorsPointsChanges(options);

            // Assert
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Season, actual[i].Season);
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                for (int j = 0; j < expectedConstructorsStandingsChanges[i].Standings.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                    for (int k = 0; k < expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[k].Round, actual[i].Standings[j].Rounds[k].Round);
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[k].Points, actual[i].Standings[j].Rounds[k].Points);
                    }
                }
            }
        }

        [TestMethod]
        public void GetConstructorsPointsChanges_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsStandingsChanges = new List<SeasonStandingsChangesModel>();
            _service.Setup((service) => service.AggregateConstructorsPointsChanges(It.IsAny<OptionsModel>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _controller.GetConstructorsPointsChanges(options);

            // Assert
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);
        }
    }
}
