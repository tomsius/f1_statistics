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
                    WinCount = 2 
                }, 
                new WinsModel 
                { 
                    Name = "Second", 
                    WinCount = 1 
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
                    Winners = new List<string>
                    {
                        "First",
                        "Second"
                    }
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 2,
                    Winners = new List<string>()
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 3,
                    Winners = new List<string>
                    {
                        "First",
                        "Third"
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
            var expectedWinners = GenerateWinners();
            _service.Setup((service) => service.AggregateDriversWins(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetDriversWins(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void GetDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<WinsModel>();
            _service.Setup((service) => service.AggregateDriversWins(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetDriversWins(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnAggregatedWinnersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = GenerateWinners();
            _service.Setup((service) => service.AggregateConstructorsWins(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsWins(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<WinsModel>();
            _service.Setup((service) => service.AggregateConstructorsWins(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsWins(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnAggregatedWinnersListWithAverageWins_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersWithAverageWins = GenerateWinnersWithAverageWins();
            _service.Setup((service) => service.AggregateDriversWinPercent(It.IsAny<OptionsModel>())).Returns(expectedWinnersWithAverageWins);

            // Act
            var actual = _controller.GetDriversWinPercent(options);

            // Assert
            Assert.AreEqual(expectedWinnersWithAverageWins.Count, actual.Count);

            for (int i = 0; i < expectedWinnersWithAverageWins.Count; i++)
            {
                Assert.AreEqual(expectedWinnersWithAverageWins[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinnersWithAverageWins[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedWinnersWithAverageWins[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedWinnersWithAverageWins[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void GetDriversWinPercent_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<AverageWinsModel>();
            _service.Setup((service) => service.AggregateDriversWinPercent(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetDriversWinPercent(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnAggregatedWinnersListWithAverageWins_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersWithAverageWins = GenerateWinnersWithAverageWins();
            _service.Setup((service) => service.AggregateConstructorsWinPercent(It.IsAny<OptionsModel>())).Returns(expectedWinnersWithAverageWins);

            // Act
            var actual = _controller.GetConstructorsWinPercent(options);

            // Assert
            Assert.AreEqual(expectedWinnersWithAverageWins.Count, actual.Count);

            for (int i = 0; i < expectedWinnersWithAverageWins.Count; i++)
            {
                Assert.AreEqual(expectedWinnersWithAverageWins[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinnersWithAverageWins[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedWinnersWithAverageWins[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedWinnersWithAverageWins[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void GetConstructorsWinPercent_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<AverageWinsModel>();
            _service.Setup((service) => service.AggregateConstructorsWinPercent(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsWinPercent(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
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
                Assert.AreEqual(expectedWinnersByGridPosition[i].Winners.Count, actual[i].Winners.Count);

                for (int j = 0; j < expectedWinnersByGridPosition[i].Winners.Count; j++)
                {
                    Assert.AreEqual(expectedWinnersByGridPosition[i].Winners[j], actual[i].Winners[j]);
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
