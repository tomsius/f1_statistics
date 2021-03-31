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
    public class FastestLapsControllerTests
    {
        private FastestLapsController _controller;
        private Mock<IFastestLapsService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IFastestLapsService>();

            _controller = new FastestLapsController(_service.Object);
        }

        private List<FastestLapModel> GenerateFastestLappers()
        {
            var winners = new List<FastestLapModel>
            {
                new FastestLapModel
                {
                    Name = "First",
                    FastestLapsCount = 2
                },
                new FastestLapModel
                {
                    Name = "Second",
                    FastestLapsCount = 1
                }
            };

            return winners;
        }

        private List<UniqueSeasonFastestLapModel> GenerateUniqueSeasonFastestLappers()
        {
            var uniqueSeaosnWinners = new List<UniqueSeasonFastestLapModel>
            {
                new UniqueSeasonFastestLapModel
                {
                    Season = 2020,
                    FastestLapAchievers = new List<string>
                    {
                        "First",
                        "Second"
                    },
                    RacesCount = 1
                },
                new UniqueSeasonFastestLapModel
                {
                    Season = 2021,
                    FastestLapAchievers = new List<string>
                    {
                        "First",
                        "Second",
                        "Third"
                    },
                    RacesCount = 1
                }
            };

            return uniqueSeaosnWinners;
        }

        [TestMethod]
        public void GetDriversFastestLaps_ReturnAggregatedDriversFastestLappersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversFastestLappers = GenerateFastestLappers();
            _service.Setup((service) => service.AggregateDriversFastestLaps(It.IsAny<OptionsModel>())).Returns(expectedDriversFastestLappers);

            // Act
            var actual = _controller.GetDriversFastestLaps(options);

            // Assert
            Assert.AreEqual(expectedDriversFastestLappers.Count, actual.Count);

            for (int i = 0; i < expectedDriversFastestLappers.Count; i++)
            {
                Assert.AreEqual(expectedDriversFastestLappers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversFastestLappers[i].FastestLapsCount, actual[i].FastestLapsCount);
            }
        }

        [TestMethod]
        public void GetDriversFastestLaps_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<FastestLapModel>();
            _service.Setup((service) => service.AggregateDriversFastestLaps(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetDriversFastestLaps(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnAggregatedConstructorsFastestLappersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = GenerateFastestLappers();
            _service.Setup((service) => service.AggregateConstructorsFastestLaps(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsFastestLaps(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].FastestLapsCount, actual[i].FastestLapsCount);
            }
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<FastestLapModel>();
            _service.Setup((service) => service.AggregateConstructorsFastestLaps(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsFastestLaps(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueDriversFastestLapsPerSeason_ReturnAggregatedUniqueDriversFastestLappersPerSeasonList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = GenerateUniqueSeasonFastestLappers();
            _service.Setup((service) => service.AggregateUniqueDriversFastestLapsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueDriversFastestLapsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
                Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
            }
        }

        [TestMethod]
        public void GetUniqueDriversFastestLapsPerSeason_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = new List<UniqueSeasonFastestLapModel>();
            _service.Setup((service) => service.AggregateUniqueDriversFastestLapsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueDriversFastestLapsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLapsPerseason_ReturnAggregatedUniqueSeasonWinnersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = GenerateUniqueSeasonFastestLappers();
            _service.Setup((service) => service.AggregateUniqueConstructorsFastestLapsPerseason(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueConstructorsFastestLapsPerseason(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
                Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
            }
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLapsPerseason_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueWinners = new List<UniqueSeasonFastestLapModel>();
            _service.Setup((service) => service.AggregateUniqueConstructorsFastestLapsPerseason(It.IsAny<OptionsModel>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _controller.GetUniqueConstructorsFastestLapsPerseason(options);

            // Assert
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
        }
    }
}
