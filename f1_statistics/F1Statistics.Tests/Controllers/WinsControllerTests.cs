using F1Statistics.Controllers;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Tests.Controllers
{
    [TestClass]
    public class WinsControllerTests
    {
        private WinsController _controller;
        private Mock<IWinsService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IWinsService>();

            _controller = new WinsController(_service.Object);
        }

        private List<WinsModel> GenerateWinners()
        {
            var winners = new List<WinsModel> 
            { 
                new WinsModel 
                { 
                    Name = "First",
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 1,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 1,
                                    GapToSecond = 1.5
                                }
                            }
                        },
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 2,
                                    GapToSecond = 0.3
                                },
                                new WinInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 3,
                                    GapToSecond = 2.5
                                }
                            }
                        }
                    }
                }, 
                new WinsModel 
                { 
                    Name = "Second",
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 1,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 3,
                                    GapToSecond = 0.7
                                },
                                new WinInformationModel
                                {
                                    CircuitName = "Third",
                                    GridPosition = 4,
                                    GapToSecond = 0.8
                                }
                            }
                        },
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 1,
                                    GapToSecond = 1
                                }
                            }
                        }
                    }
                } 
            };

            return winners;
        }

        private List<AverageWinsModel> GenerateWinnersWithAverageWins()
        {
            var winners = new List<AverageWinsModel> 
            {
                new AverageWinsModel 
                {
                    Name = "First", 
                    WinCount = 2, 
                    ParticipationCount = 4 
                },
                new AverageWinsModel
                {
                    Name = "Second", 
                    WinCount = 1,
                    ParticipationCount = 1 
                }
            };

            return winners;
        }

        private List<CircuitWinsModel> GenerateCircuitWinners()
        {
            var circuitWinners = new List<CircuitWinsModel>
            {
                new CircuitWinsModel
                {
                    Name = "FirstCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 2,
                            ParticipationsCount = 1
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "SecondCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 1,
                            ParticipationsCount = 1
                        },
                        new WinsAndParticipationsModel
                        {
                            Name = "SecondDriver",
                            WinCount = 1,
                            ParticipationsCount = 1
                        }
                    }
                }
            };

            return circuitWinners;
        }

        private List<UniqueSeasonWinnersModel> GenerateUniqueSeasonWinners()
        {
            var uniqueSeaosnWinners = new List<UniqueSeasonWinnersModel>
            {
                new UniqueSeasonWinnersModel
                {
                    Season = 2020,
                    Winners = new List<string>
                    {
                        "First",
                        "Second"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2021,
                    Winners = new List<string>
                    {
                        "First",
                        "Second",
                        "Third"
                    },
                    RacesCount = 2
                }
            };

            return uniqueSeaosnWinners;
        }

        private List<WinnersFromPoleModel> GenerateWinnersFromPole()
        {
            var winners = new List<WinnersFromPoleModel>
            {
                new WinnersFromPoleModel
                {
                    Season = 1,
                    WinnersFromPole = new List<string> { "First", "Second" }
                },
                new WinnersFromPoleModel
                {
                    Season = 2,
                    WinnersFromPole = new List<string> { "First" }
                }
            };

            return winners;
        }

        private List<WinsByGridPositionModel> GenerateWinnersByGridPosition()
        {
            var winnersByGridPosition = new List<WinsByGridPositionModel>
            {
                new WinsByGridPositionModel
                {
                    GridPosition = 1,
                    WinInformation = new List<WinByGridInformationModel>
                    {
                        new WinByGridInformationModel
                        {
                            CircuitName = "First",
                            WinnerName = "FirstName FirstFamily"
                        },
                        new WinByGridInformationModel
                        {
                            CircuitName = "Second",
                            WinnerName = "FirstName FirstFamily"
                        }
                    }
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 2,
                    WinInformation = new List<WinByGridInformationModel>()
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 3,
                    WinInformation = new List<WinByGridInformationModel>
                    {
                        new WinByGridInformationModel
                        {
                            CircuitName = "First",
                            WinnerName = "SecondName SecondFamily"
                        },
                        new WinByGridInformationModel
                        {
                            CircuitName = "Second",
                            WinnerName = "SecondName SecondFamily"
                        }
                    }
                }
            };

            return winnersByGridPosition;
        }

        [TestMethod]
        public void GetDriversWins_ReturnAggregatedWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversWinners = GenerateWinners();
            _service.Setup((service) => service.AggregateDriversWins(It.IsAny<OptionsModel>())).Returns(expectedDriversWinners);

            // Act
            var actual = _controller.GetDriversWins(options);

            // Assert
            Assert.AreEqual(expectedDriversWinners.Count, actual.Count);

            for (int i = 0; i < expectedDriversWinners.Count; i++)
            {
                Assert.AreEqual(expectedDriversWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversWinners[i].TotalWinCount, actual[i].TotalWinCount);
                Assert.AreEqual(expectedDriversWinners[i].WinsByYear.Count, actual[i].WinsByYear.Count);

                for (int j = 0; j < expectedDriversWinners[i].WinsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].Year, actual[i].WinsByYear[j].Year);
                    Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].YearWinCount, actual[i].WinsByYear[j].YearWinCount);
                    Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation.Count, actual[i].WinsByYear[j].WinInformation.Count);

                    for (int k = 0; k < expectedDriversWinners[i].WinsByYear[j].WinInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].CircuitName, actual[i].WinsByYear[j].WinInformation[k].CircuitName);
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].GapToSecond, actual[i].WinsByYear[j].WinInformation[k].GapToSecond);
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].GridPosition, actual[i].WinsByYear[j].WinInformation[k].GridPosition);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversWinners = new List<WinsModel>();
            _service.Setup((service) => service.AggregateDriversWins(It.IsAny<OptionsModel>())).Returns(expectedDriversWinners);

            // Act
            var actual = _controller.GetDriversWins(options);

            // Assert
            Assert.AreEqual(expectedDriversWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnAggregatedWinnersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorWinners = GenerateWinners();
            _service.Setup((service) => service.AggregateConstructorsWins(It.IsAny<OptionsModel>())).Returns(expectedConstructorWinners);

            // Act
            var actual = _controller.GetConstructorsWins(options);

            // Assert
            Assert.AreEqual(expectedConstructorWinners.Count, actual.Count);

            for (int i = 0; i < expectedConstructorWinners.Count; i++)
            {
                Assert.AreEqual(expectedConstructorWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorWinners[i].TotalWinCount, actual[i].TotalWinCount);
                Assert.AreEqual(expectedConstructorWinners[i].WinsByYear.Count, actual[i].WinsByYear.Count);

                for (int j = 0; j < expectedConstructorWinners[i].WinsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].Year, actual[i].WinsByYear[j].Year);
                    Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].YearWinCount, actual[i].WinsByYear[j].YearWinCount);
                    Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation.Count, actual[i].WinsByYear[j].WinInformation.Count);

                    for (int k = 0; k < expectedConstructorWinners[i].WinsByYear[j].WinInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].CircuitName, actual[i].WinsByYear[j].WinInformation[k].CircuitName);
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].GapToSecond, actual[i].WinsByYear[j].WinInformation[k].GapToSecond);
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].GridPosition, actual[i].WinsByYear[j].WinInformation[k].GridPosition);
                    }
                }
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorWinners = new List<WinsModel>();
            _service.Setup((service) => service.AggregateConstructorsWins(It.IsAny<OptionsModel>())).Returns(expectedConstructorWinners);

            // Act
            var actual = _controller.GetConstructorsWins(options);

            // Assert
            Assert.AreEqual(expectedConstructorWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnAggregatedWinnersWithWinPercentageList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversWinPercent = GenerateWinnersWithAverageWins();
            _service.Setup((service) => service.AggregateDriversWinPercent(It.IsAny<OptionsModel>())).Returns(expectedDriversWinPercent);

            // Act
            var actual = _controller.GetDriversWinPercent(options);

            // Assert
            Assert.AreEqual(expectedDriversWinPercent.Count, actual.Count);

            for (int i = 0; i < expectedDriversWinPercent.Count; i++)
            {
                Assert.AreEqual(expectedDriversWinPercent[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversWinPercent[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedDriversWinPercent[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedDriversWinPercent[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversWinPercent = new List<AverageWinsModel>();
            _service.Setup((service) => service.AggregateDriversWinPercent(It.IsAny<OptionsModel>())).Returns(expectedDriversWinPercent);

            // Act
            var actual = _controller.GetDriversWinPercent(options);

            // Assert
            Assert.AreEqual(expectedDriversWinPercent.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnAggregatedWinnersWithWinPercentageList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorWinPercent = GenerateWinnersWithAverageWins();
            _service.Setup((service) => service.AggregateConstructorsWinPercent(It.IsAny<OptionsModel>())).Returns(expectedConstructorWinPercent);

            // Act
            var actual = _controller.GetConstructorsWinPercent(options);

            // Assert
            Assert.AreEqual(expectedConstructorWinPercent.Count, actual.Count);

            for (int i = 0; i < expectedConstructorWinPercent.Count; i++)
            {
                Assert.AreEqual(expectedConstructorWinPercent[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorWinPercent[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedConstructorWinPercent[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedConstructorWinPercent[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorWinPercent = new List<AverageWinsModel>();
            _service.Setup((service) => service.AggregateConstructorsWinPercent(It.IsAny<OptionsModel>())).Returns(expectedConstructorWinPercent);

            // Act
            var actual = _controller.GetConstructorsWinPercent(options);

            // Assert
            Assert.AreEqual(expectedConstructorWinPercent.Count, actual.Count);
        }

        [TestMethod]
        public void GetCircuitWinners_ReturnAggregatedCircuitWinnersList_IfThereAreAnyCircuits()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedCircuitWinners = GenerateCircuitWinners();
            _service.Setup((service) => service.AggregateCircuitsWinners(It.IsAny<OptionsModel>())).Returns(expectedCircuitWinners);

            // Act
            var actual = _controller.GetCircuitWinners(options);

            // Assert
            Assert.AreEqual(expectedCircuitWinners.Count, actual.Count);

            for (int i = 0; i < expectedCircuitWinners.Count; i++)
            {
                Assert.AreEqual(expectedCircuitWinners[i].Name, actual[i].Name);

                for (int j = 0; j < expectedCircuitWinners[i].Winners.Count; j++)
                {
                    Assert.AreEqual(expectedCircuitWinners[i].Winners[j].Name, actual[i].Winners[j].Name);
                    Assert.AreEqual(expectedCircuitWinners[i].Winners[j].WinCount, actual[i].Winners[j].WinCount);
                }
            }
        }

        [TestMethod]
        public void GetCircuitWinners_ReturnEmptyList_IfThereAreNoCircuits()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<CircuitWinsModel>();
            _service.Setup((service) => service.AggregateCircuitsWinners(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetCircuitWinners(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueSeasonDriverWinners_ReturnAggregatedUniqueSeasonWinnersList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = GenerateUniqueSeasonWinners();
            _service.Setup((service) => service.AggregateUniqueSeasonDriverWinners(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueSeasonDriverWinners(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverWinners_ReturnEmptyList_IfThereAreNoCircuits()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>();
            _service.Setup((service) => service.AggregateUniqueSeasonDriverWinners(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueSeasonDriverWinners(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorWinners_ReturnAggregatedUniqueSeasonWinnersList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = GenerateUniqueSeasonWinners();
            _service.Setup((service) => service.AggregateUniqueSeasonConstructorWinners(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueSeasonConstructorWinners(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorWinners_ReturnEmptyList_IfThereAreNoCircuits()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>();
            _service.Setup((service) => service.AggregateUniqueSeasonConstructorWinners(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueSeasonConstructorWinners(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnAggregatedWinnersFromPoleList_IfThereAreAnyWinnersFromPole()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersFromPole = GenerateWinnersFromPole();
            _service.Setup((service) => service.AggregateWinnersFromPole(It.IsAny<OptionsModel>())).Returns(expectedWinnersFromPole);

            // Act
            var actual = _controller.GetWinnersFromPole(options);

            // Assert
            Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);

            for (int i = 0; i < expectedWinnersFromPole.Count; i++)
            {
                Assert.AreEqual(expectedWinnersFromPole[i].Season, actual[i].Season);
                Assert.AreEqual(expectedWinnersFromPole[i].WinsFromPoleCount, actual[i].WinsFromPoleCount);
            }
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnEmptyList_IfThereAreNoWinnersFromPole()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersFromPole = new List<WinnersFromPoleModel>();
            _service.Setup((service) => service.AggregateWinnersFromPole(It.IsAny<OptionsModel>())).Returns(expectedWinnersFromPole);

            // Act
            var actual = _controller.GetWinnersFromPole(options);

            // Assert
            Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);
        }

        [TestMethod]
        public void GetWinnersByGridPosition_ReturnAggregatedWinnersByStartingGridPositionList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersByGridPosition = GenerateWinnersByGridPosition();
            _service.Setup((service) => service.AggregateWinnersByGridPosition(It.IsAny<OptionsModel>())).Returns(expectedWinnersByGridPosition);

            // Act
            var actual = _controller.GetWinnersByGridPosition(options);

            // Assert
            Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);

            for (int i = 0; i < expectedWinnersByGridPosition.Count; i++)
            {
                Assert.AreEqual(expectedWinnersByGridPosition[i].GridPosition, actual[i].GridPosition);
                Assert.AreEqual(expectedWinnersByGridPosition[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation.Count, actual[i].WinInformation.Count);

                for (int j = 0; j < expectedWinnersByGridPosition[i].WinInformation.Count; j++)
                {
                    Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation[j].CircuitName, actual[i].WinInformation[j].CircuitName);
                    Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation[j].WinnerName, actual[i].WinInformation[j].WinnerName);
                }
            }
        }

        [TestMethod]
        public void GetWinnersByGridPosition_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersByGridPosition = new List<WinsByGridPositionModel>();
            _service.Setup((service) => service.AggregateWinnersByGridPosition(It.IsAny<OptionsModel>())).Returns(expectedWinnersByGridPosition);

            // Act
            var actual = _controller.GetWinnersByGridPosition(options);

            // Assert
            Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);
        }
    }
}
