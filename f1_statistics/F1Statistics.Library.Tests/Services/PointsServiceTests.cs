using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.Services
{
    [TestClass]
    public class PointsServiceTests
    {
        private PointsService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<IPointsAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<IPointsAggregator>();

            _validator.Setup((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new PointsService(_validator.Object, _aggregator.Object);
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
        public void AggregateDriversPointsPerSeason_ReturnSortedAggregatedDriversPointsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriverPoints = GeneratePointsScorers();
            expectedDriverPoints.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));
            expectedDriverPoints.Sort((x, y) => x.Season.CompareTo(y.Season));
            _aggregator.Setup((aggregator) => aggregator.GetDriversPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePointsScorers());

            // Act
            var actual = _service.AggregateDriversPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriverPoints.Count, actual.Count);

            for (int i = 0; i < expectedDriverPoints.Count; i++)
            {
                Assert.AreEqual(expectedDriverPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriverPoints[i].TotalPoints, actual[i].TotalPoints);

                for (int j = 0; j < expectedDriverPoints[i].ScoredPoints.Count; j++)
                {
                    Assert.AreEqual(expectedDriverPoints[i].ScoredPoints[j].Name, actual[i].ScoredPoints[j].Name);
                    Assert.AreEqual(expectedDriverPoints[i].ScoredPoints[j].Points, actual[i].ScoredPoints[j].Points);
                }
            }
        }

        [TestMethod]
        public void AggregateDriversPointsPerSeason_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriverPoints = new List<SeasonPointsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriverPoints);

            // Act
            var actual = _service.AggregateDriversPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriverPoints.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsPointsPerSeason_ReturnSortedAggregatedConstructorsPointsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsPoints = GeneratePointsScorers();
            expectedConstructorsPoints.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));
            expectedConstructorsPoints.Sort((x, y) => x.Season.CompareTo(y.Season));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePointsScorers());

            // Act
            var actual = _service.AggregateConstructorsPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsPoints.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsPoints.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedConstructorsPoints[i].TotalPoints, actual[i].TotalPoints);

                for (int j = 0; j < expectedConstructorsPoints[i].ScoredPoints.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsPoints[i].ScoredPoints[j].Name, actual[i].ScoredPoints[j].Name);
                    Assert.AreEqual(expectedConstructorsPoints[i].ScoredPoints[j].Points, actual[i].ScoredPoints[j].Points);
                }
            }
        }

        [TestMethod]
        public void AggregateConstructorsPointsPerSeason_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsPoints = new List<SeasonPointsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsPoints);

            // Act
            var actual = _service.AggregateConstructorsPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsPoints.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversWinnersPointsPerSeason_ReturnSortedAggregatedDriversWinnersPointsList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriverWinnersPoints = GenerateWinnersPoints();
            expectedDriverWinnersPoints.Sort((x, y) => x.Season.CompareTo(y.Season));
            _aggregator.Setup((aggregator) => aggregator.GetDriversWinnersPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinnersPoints());

            // Act
            var actual = _service.AggregateDriversWinnersPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriverWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedDriverWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedDriverWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriverWinnersPoints[i].Points, actual[i].Points);
                Assert.AreEqual(expectedDriverWinnersPoints[i].Winner, actual[i].Winner);
            }
        }

        [TestMethod]
        public void AggregateDriversWinnersPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriverWinnersPoints = new List<SeasonWinnersPointsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversWinnersPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriverWinnersPoints);

            // Act
            var actual = _service.AggregateDriversWinnersPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriverWinnersPoints.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsWinnersPointsPerSeason_ReturnSortedAggregatedConstructorsWinnersPointsList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsWinnersPoints = GenerateWinnersPoints();
            expectedConstructorsWinnersPoints.Sort((x, y) => x.Season.CompareTo(y.Season));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWinnersPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinnersPoints());

            // Act
            var actual = _service.AggregateConstructorsWinnersPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Points, actual[i].Points);
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Winner, actual[i].Winner);
            }
        }

        [TestMethod]
        public void AggregateConstructorsWinnersPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsWinnersPoints = new List<SeasonWinnersPointsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWinnersPointsPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsWinnersPoints);

            // Act
            var actual = _service.AggregateConstructorsWinnersPointsPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsWinnersPoints.Count, actual.Count);
        }
    }
}
