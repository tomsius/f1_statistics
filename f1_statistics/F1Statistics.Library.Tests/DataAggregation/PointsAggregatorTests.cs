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
        private Mock<IStandingsDataAccess> _resultsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IStandingsDataAccess>();

            _aggregator = new PointsAggregator(_resultsDataAccess.Object);
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
                    Season = 1,
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
                    Season = 2,
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
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(GenerateDriverStandings()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(GenerateDriverStandings()[1]);

            // Act
            var actual = _aggregator.GetDriversPointsPerSeason(from, to);
            actual.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));
            actual.Sort((x, y) => x.Season.CompareTo(y.Season));

            // Assert
            Assert.AreEqual(expectedDriversPoints.Count, actual.Count);

            for (int i = 0; i < expectedDriversPoints.Count; i++)
            {
                Assert.AreEqual(expectedDriversPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriversPoints[i].TotalPoints, actual[i].TotalPoints);

                for (int j = 0; j < expectedDriversPoints[i].ScoredPoints.Count; j++)
                {
                    Assert.AreEqual(expectedDriversPoints[i].ScoredPoints[j].Name, actual[i].ScoredPoints[j].Name);
                    Assert.AreEqual(expectedDriversPoints[i].ScoredPoints[j].Points, actual[i].ScoredPoints[j].Points);
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
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(new List<DriverStandingsDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(new List<DriverStandingsDataResponse>());

            // Act
            var actual = _aggregator.GetDriversPointsPerSeason(from, to);

            // Assert
            Assert.AreEqual(expectedDriversPoints.Count, actual.Count);
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
                    Season = 1,
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
                    Season = 2,
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
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(GenerateConstructorStandings()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(GenerateConstructorStandings()[1]);

            // Act
            var actual = _aggregator.GetConstructorsPointsPerSeason(from, to);
            actual.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));
            actual.Sort((x, y) => x.Season.CompareTo(y.Season));

            // Assert
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
        public void GetConstructorsPointsPerSeason_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPoints = new List<PointsModel> { new PointsModel(), new PointsModel() };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(new List<ConstructorStandingsDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(new List<ConstructorStandingsDataResponse>());

            // Act
            var actual = _aggregator.GetConstructorsPointsPerSeason(from, to);

            // Assert
            Assert.AreEqual(expectedConstructorsPoints.Count, actual.Count);
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
                    Season = 1,
                    Winner = "FirstName FirstFamily",
                    Points = 100
                },
                new SeasonWinnersPointsModel
                {
                    Season = 2,
                    Winner = "FirstName FirstFamily",
                    Points = 200
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(GenerateDriverStandings()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(GenerateDriverStandings()[1]);

            // Act
            var actual = _aggregator.GetDriversWinnersPointsPerSeason(from, to);
            actual.Sort((x, y) => x.Season.CompareTo(y.Season));

            // Assert
            Assert.AreEqual(expectedDriversWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedDriversWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedDriversWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriversWinnersPoints[i].Winner, actual[i].Winner);
                Assert.AreEqual(expectedDriversWinnersPoints[i].Points, actual[i].Points);
            }
        }

        [TestMethod]
        public void GetDriversWinnersPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversWinnersPoints = new List<SeasonWinnersPointsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(1)).Returns(new List<DriverStandingsDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetDriverStandingsFrom(2)).Returns(new List<DriverStandingsDataResponse>());

            // Act
            var actual = _aggregator.GetDriversWinnersPointsPerSeason(from, to);

            // Assert
            Assert.AreEqual(expectedDriversWinnersPoints.Count, actual.Count);
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
                    Season = 1,
                    Winner = "First",
                    Points = 100
                },
                new SeasonWinnersPointsModel
                {
                    Season = 2,
                    Winner = "First",
                    Points = 200
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(GenerateConstructorStandings()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(GenerateConstructorStandings()[1]);

            // Act
            var actual = _aggregator.GetConstructorsWinnersPointsPerSeason(from, to);
            actual.Sort((x, y) => x.Season.CompareTo(y.Season));

            // Assert
            Assert.AreEqual(expectedConstructorsWinnersPoints.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsWinnersPoints.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Season, actual[i].Season);
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Winner, actual[i].Winner);
                Assert.AreEqual(expectedConstructorsWinnersPoints[i].Points, actual[i].Points);
            }
        }

        [TestMethod]
        public void GetConstructorsPointsPerSeason_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPoints = new List<SeasonWinnersPointsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(1)).Returns(new List<ConstructorStandingsDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetConstructorStandingsFrom(2)).Returns(new List<ConstructorStandingsDataResponse>());

            // Act
            var actual = _aggregator.GetConstructorsWinnersPointsPerSeason(from, to);

            // Assert
            Assert.AreEqual(expectedConstructorsPoints.Count, actual.Count);
        }
    }
}
