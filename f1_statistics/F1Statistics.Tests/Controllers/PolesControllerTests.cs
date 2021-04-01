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
    public class PolesControllerTests
    {
        private PolesController _controller;
        private Mock<IPolesService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IPolesService>();

            _controller = new PolesController(_service.Object);
        }

        private List<PolesModel> GeneratePoleSitters()
        {
            var winners = new List<PolesModel>
            {
                new PolesModel
                {
                    Name = "First",
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 1,
                            PoleCount = 1
                        },
                        new PolesByYearModel
                        {
                            Year = 2,
                            PoleCount = 2
                        }
                    }
                },
                new PolesModel
                {
                    Name = "Second",
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 1,
                            PoleCount = 2
                        },
                        new PolesByYearModel
                        {
                            Year = 2,
                            PoleCount = 1
                        }
                    }
                }
            };

            return winners;
        }

        private List<UniqueSeasonPoleSittersModel> GenerateUniqueSeasonPoleSitters()
        {
            var winners = new List<UniqueSeasonPoleSittersModel>
            {
                new UniqueSeasonPoleSittersModel
                {
                    Season = 1,
                    PoleSitters = new List<string>{ "First", "Second" },
                    QualificationsCount = 1
                },
                new UniqueSeasonPoleSittersModel
                {
                    Season = 2,
                    PoleSitters = new List<string>{ "First" },
                    QualificationsCount = 1
                }
            };

            return winners;
        }

        [TestMethod]
        public void GetPoleSittersDrivers_ReturnAggregatedPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedPoleSitters = GeneratePoleSitters();
            _service.Setup((service) => service.AggregatePoleSittersDrivers(It.IsAny<OptionsModel>())).Returns(expectedPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersDrivers(options);

            // Assert
            Assert.AreEqual(expectedPoleSitters.Count, actual.Count);

            for (int i = 0; i < expectedPoleSitters.Count; i++)
            {
                Assert.AreEqual(expectedPoleSitters[i].Name, actual[i].Name);
                Assert.AreEqual(expectedPoleSitters[i].PoleCount, actual[i].PoleCount);
                Assert.AreEqual(expectedPoleSitters[i].PolesByYear.Count, actual[i].PolesByYear.Count);

                for (int j = 0; j < expectedPoleSitters[i].PolesByYear.Count; j++)
                {
                    Assert.AreEqual(expectedPoleSitters[i].PolesByYear[j].Year, actual[i].PolesByYear[j].Year);
                    Assert.AreEqual(expectedPoleSitters[i].PolesByYear[j].PoleCount, actual[i].PolesByYear[j].PoleCount);
                }
            }
        }

        [TestMethod]
        public void GetPoleSittersDrivers_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedPoleSitters = new List<PolesModel>();
            _service.Setup((service) => service.AggregatePoleSittersDrivers(It.IsAny<OptionsModel>())).Returns(expectedPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersDrivers(options);

            // Assert
            Assert.AreEqual(expectedPoleSitters.Count, actual.Count);
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnAggregatedPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedPoleSitters = GeneratePoleSitters();
            _service.Setup((service) => service.AggregatePoleSittersConstructors(It.IsAny<OptionsModel>())).Returns(expectedPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersConstructors(options);

            // Assert
            Assert.AreEqual(expectedPoleSitters.Count, actual.Count);

            for (int i = 0; i < expectedPoleSitters.Count; i++)
            {
                Assert.AreEqual(expectedPoleSitters[i].Name, actual[i].Name);
                Assert.AreEqual(expectedPoleSitters[i].PoleCount, actual[i].PoleCount);
                Assert.AreEqual(expectedPoleSitters[i].PolesByYear.Count, actual[i].PolesByYear.Count);

                for (int j = 0; j < expectedPoleSitters[i].PolesByYear.Count; j++)
                {
                    Assert.AreEqual(expectedPoleSitters[i].PolesByYear[j].Year, actual[i].PolesByYear[j].Year);
                    Assert.AreEqual(expectedPoleSitters[i].PolesByYear[j].PoleCount, actual[i].PolesByYear[j].PoleCount);
                }
            }
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedPoleSitters = new List<PolesModel>();
            _service.Setup((service) => service.AggregatePoleSittersConstructors(It.IsAny<OptionsModel>())).Returns(expectedPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersConstructors(options);

            // Assert
            Assert.AreEqual(expectedPoleSitters.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueSeasonDriverPoleSitters_ReturnAggregatedUniqueSeasonPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersDrivers = GenerateUniqueSeasonPoleSitters();
            _service.Setup((service) => service.AggregateUniqueSeasonPoleSittersDrivers(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetUniqueSeasonDriverPoleSitters(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].QualificationsCount, actual[i].QualificationsCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverPoleSitters_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>();
            _service.Setup((service) => service.AggregateUniqueSeasonPoleSittersDrivers(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetUniqueSeasonDriverPoleSitters(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorsPoleSitters_ReturnAggregatedUniqueSeasonPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersConstructors = GenerateUniqueSeasonPoleSitters();
            _service.Setup((service) => service.AggregateUniqueSeasonPoleSittersConstructors(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersConstructors);

            // Act
            var actual = _controller.GetUniqueSeasonConstructorsPoleSitters(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersConstructors.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersConstructors.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersConstructors[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[i].QualificationsCount, actual[i].QualificationsCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorsPoleSitters_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel>();
            _service.Setup((service) => service.AggregateUniqueSeasonPoleSittersConstructors(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersConstructors);

            // Act
            var actual = _controller.GetUniqueSeasonConstructorsPoleSitters(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersConstructors.Count, actual.Count);
        }
    }
}
