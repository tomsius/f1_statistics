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
                    Points = 200
                },
                new SeasonWinnersPointsModel
                {
                    Season = 2,
                    Winner = "Second",
                    Points = 400
                }
            };

            return winnersPoints;
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
            var expectedWinnersPoints = GenerateWinnersPoints();
            _service.Setup((service) => service.AggregateDriversWinnersPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedWinnersPoints);

            // Act
            var actual = _controller.GetDriversWinnersPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedWinnersPoints[i].Points, actual[i].Points);
                Assert.AreEqual(expectedWinnersPoints[i].Winner, actual[i].Winner);
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
            var expectedWinnersPoints = GenerateWinnersPoints();
            _service.Setup((service) => service.AggregateConstructorsWinnersPointsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedWinnersPoints);

            // Act
            var actual = _controller.GetConstructorsWinnersPointsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedWinnersPoints[i].Points, actual[i].Points);
                Assert.AreEqual(expectedWinnersPoints[i].Winner, actual[i].Winner);
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
    }
}
