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

        private List<WinsModel> GenerateResult()
        {
            var winners = new List<WinsModel> { new WinsModel { Name = "First", WinCount = 2 }, new WinsModel { Name = "Second", WinCount = 1 } };

            return winners;
        }

        [TestMethod]
        public void GetDriversWins_ReturnAggregatedDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = GenerateResult();
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
    }
}
