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
    public class LeadingLapsControllerTests
    {
        private LeadingLapsController _controller;
        private Mock<ILeadingLapsService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<ILeadingLapsService>();

            _controller = new LeadingLapsController(_service.Object);
        }

        private List<LeadingLapsModel> GenerateLeadingLaps()
        {
            var leadingLaps = new List<LeadingLapsModel>
            {
                new LeadingLapsModel
                {
                    Name = "First",
                    LeadingLapCount = 2
                },
                new LeadingLapsModel
                {
                    Name = "Second",
                    LeadingLapCount = 1
                }
            };

            return leadingLaps;
        }

        [TestMethod]
        public void GetDriversLeadingLapsCount_ReturnAggregatedDriversLeadingLapsCountList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversLeadingLapsCount = GenerateLeadingLaps();
            _service.Setup((service) => service.AggregateDriversLeadingLapsCount(It.IsAny<OptionsModel>())).Returns(expectedDriversLeadingLapsCount);

            // Act
            var actual = _controller.GetDriversLeadingLapsCount(options);

            // Assert
            Assert.AreEqual(expectedDriversLeadingLapsCount.Count, actual.Count);

            for (int i = 0; i < expectedDriversLeadingLapsCount.Count; i++)
            {
                Assert.AreEqual(expectedDriversLeadingLapsCount[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapCount, actual[i].LeadingLapCount);
            }
        }

        [TestMethod]
        public void GetDriversLeadingLapsCount_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversLeadingLapsCount = new List<LeadingLapsModel>();
            _service.Setup((service) => service.AggregateDriversLeadingLapsCount(It.IsAny<OptionsModel>())).Returns(expectedDriversLeadingLapsCount);

            // Act
            var actual = _controller.GetDriversLeadingLapsCount(options);

            // Assert
            Assert.AreEqual(expectedDriversLeadingLapsCount.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsLeadingLapsCount_ReturnAggregatedConstructorsLeadingLapsCountList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsLeadingLapsCount = GenerateLeadingLaps();
            _service.Setup((service) => service.AggregateConstructorsLeadingLapsCount(It.IsAny<OptionsModel>())).Returns(expectedConstructorsLeadingLapsCount);

            // Act
            var actual = _controller.GetConstructorsLeadingLapsCount(options);

            // Assert
            Assert.AreEqual(expectedConstructorsLeadingLapsCount.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsLeadingLapsCount.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapCount, actual[i].LeadingLapCount);
            }
        }

        [TestMethod]
        public void GetConstructorsLeadingLapsCount_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsLeadingLapsCount = new List<LeadingLapsModel>();
            _service.Setup((service) => service.AggregateConstructorsLeadingLapsCount(It.IsAny<OptionsModel>())).Returns(expectedConstructorsLeadingLapsCount);

            // Act
            var actual = _controller.GetConstructorsLeadingLapsCount(options);

            // Assert
            Assert.AreEqual(expectedConstructorsLeadingLapsCount.Count, actual.Count);
        }
    }
}
