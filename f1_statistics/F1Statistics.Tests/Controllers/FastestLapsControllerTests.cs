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
                    FastestLapsByYear = new List<FastestLapsByYearModel>
                    {
                        new FastestLapsByYearModel
                        {
                            Year = 1,
                            FastestLapCount = 1
                        },
                        new FastestLapsByYearModel
                        {
                            Year = 2,
                            FastestLapCount = 2
                        }
                    }
                },
                new FastestLapModel
                {
                    Name = "Second",
                    FastestLapsByYear = new List<FastestLapsByYearModel>
                    {
                        new FastestLapsByYearModel
                        {
                            Year = 1,
                            FastestLapCount = 2
                        },
                        new FastestLapsByYearModel
                        {
                            Year = 2,
                            FastestLapCount = 1
                        }
                    }
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
                Assert.AreEqual(expectedDriversFastestLappers[i].FastestLapsByYear.Count, actual[i].FastestLapsByYear.Count);

                for (int j = 0; j < expectedDriversFastestLappers[i].FastestLapsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedDriversFastestLappers[i].FastestLapsByYear[j].Year, actual[i].FastestLapsByYear[j].Year);
                    Assert.AreEqual(expectedDriversFastestLappers[i].FastestLapsByYear[j].FastestLapCount, actual[i].FastestLapsByYear[j].FastestLapCount);
                }
            }
        }

        [TestMethod]
        public void GetDriversFastestLaps_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversFastestLappers = new List<FastestLapModel>();
            _service.Setup((service) => service.AggregateDriversFastestLaps(It.IsAny<OptionsModel>())).Returns(expectedDriversFastestLappers);

            // Act
            var actual = _controller.GetDriversFastestLaps(options);

            // Assert
            Assert.AreEqual(expectedDriversFastestLappers.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnAggregatedConstructorsFastestLappersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsFastestLappers = GenerateFastestLappers();
            _service.Setup((service) => service.AggregateConstructorsFastestLaps(It.IsAny<OptionsModel>())).Returns(expectedConstructorsFastestLappers);

            // Act
            var actual = _controller.GetConstructorsFastestLaps(options);

            // Assert
            Assert.AreEqual(expectedConstructorsFastestLappers.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsFastestLappers.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsFastestLappers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsFastestLappers[i].FastestLapsCount, actual[i].FastestLapsCount);
                Assert.AreEqual(expectedConstructorsFastestLappers[i].FastestLapsByYear.Count, actual[i].FastestLapsByYear.Count);

                for (int j = 0; j < expectedConstructorsFastestLappers[i].FastestLapsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsFastestLappers[i].FastestLapsByYear[j].Year, actual[i].FastestLapsByYear[j].Year);
                    Assert.AreEqual(expectedConstructorsFastestLappers[i].FastestLapsByYear[j].FastestLapCount, actual[i].FastestLapsByYear[j].FastestLapCount);
                }
            }
        }

        [TestMethod]
        public void GetConstructorsFastestLaps_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsFastestLappers = new List<FastestLapModel>();
            _service.Setup((service) => service.AggregateConstructorsFastestLaps(It.IsAny<OptionsModel>())).Returns(expectedConstructorsFastestLappers);

            // Act
            var actual = _controller.GetConstructorsFastestLaps(options);

            // Assert
            Assert.AreEqual(expectedConstructorsFastestLappers.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueDriversFastestLapsPerSeason_ReturnAggregatedUniqueDriversFastestLappersPerSeasonList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueFastestDrivers = GenerateUniqueSeasonFastestLappers();
            _service.Setup((service) => service.AggregateUniqueDriversFastestLapsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedUniqueFastestDrivers);

            // Act
            var actual = _controller.GetUniqueDriversFastestLapsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedUniqueFastestDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniqueFastestDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniqueFastestDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueFastestDrivers[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
                Assert.AreEqual(expectedUniqueFastestDrivers[i].RacesCount, actual[i].RacesCount);
            }
        }

        [TestMethod]
        public void GetUniqueDriversFastestLapsPerSeason_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueFastestDrivers = new List<UniqueSeasonFastestLapModel>();
            _service.Setup((service) => service.AggregateUniqueDriversFastestLapsPerSeason(It.IsAny<OptionsModel>())).Returns(expectedUniqueFastestDrivers);

            // Act
            var actual = _controller.GetUniqueDriversFastestLapsPerSeason(options);

            // Assert
            Assert.AreEqual(expectedUniqueFastestDrivers.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLapsPerseason_ReturnAggregatedUniqueSeasonWinnersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueFastestConstructors = GenerateUniqueSeasonFastestLappers();
            _service.Setup((service) => service.AggregateUniqueConstructorsFastestLapsPerseason(It.IsAny<OptionsModel>())).Returns(expectedUniqueFastestConstructors);

            // Act
            var actual = _controller.GetUniqueConstructorsFastestLapsPerseason(options);

            // Assert
            Assert.AreEqual(expectedUniqueFastestConstructors.Count, actual.Count);

            for (int i = 0; i < expectedUniqueFastestConstructors.Count; i++)
            {
                Assert.AreEqual(expectedUniqueFastestConstructors[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueFastestConstructors[i].UniqueFastestLapsCount, actual[i].UniqueFastestLapsCount);
                Assert.AreEqual(expectedUniqueFastestConstructors[i].RacesCount, actual[i].RacesCount);
            }
        }

        [TestMethod]
        public void GetUniqueConstructorsFastestLapsPerseason_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniqueFastestConstructors = new List<UniqueSeasonFastestLapModel>();
            _service.Setup((service) => service.AggregateUniqueConstructorsFastestLapsPerseason(It.IsAny<OptionsModel>())).Returns(expectedUniqueFastestConstructors);

            // Act
            var actual = _controller.GetUniqueConstructorsFastestLapsPerseason(options);

            // Assert
            Assert.AreEqual(expectedUniqueFastestConstructors.Count, actual.Count);
        }
    }
}
