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
                    Winners = new List<WinsModel>
                    {
                        new WinsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 2
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "SecondCircuit",
                    Winners = new List<WinsModel>
                    {
                        new WinsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 1
                        },
                        new WinsModel
                        {
                            Name = "SecondDriver",
                            WinCount = 1
                        }
                    }
                }
            };

            return circuitWinners;
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
        public void GetDriversAverageWins_ReturnAggregatedWinnersListWithAverageWins_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersWithAverageWins = GenerateWinnersWithAverageWins();
            _service.Setup((service) => service.AggregateDriversAverageWins(It.IsAny<OptionsModel>())).Returns(expectedWinnersWithAverageWins);

            // Act
            var actual = _controller.GetDriversAverageWins(options);

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
        public void GetDriversAverageWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<AverageWinsModel>();
            _service.Setup((service) => service.AggregateDriversAverageWins(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetDriversAverageWins(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsAverageWins_ReturnAggregatedWinnersListWithAverageWins_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinnersWithAverageWins = GenerateWinnersWithAverageWins();
            _service.Setup((service) => service.AggregateConstructorsAverageWins(It.IsAny<OptionsModel>())).Returns(expectedWinnersWithAverageWins);

            // Act
            var actual = _controller.GetConstructorsAverageWins(options);

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
        public void GetConstructorsAverageWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<AverageWinsModel>();
            _service.Setup((service) => service.AggregateConstructorsAverageWins(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsAverageWins(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetCircuitWinners_ReturnAggregatedCircuitWinnersListWithAverageWins_IfThereAreAnyCircuits()
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
    }
}
