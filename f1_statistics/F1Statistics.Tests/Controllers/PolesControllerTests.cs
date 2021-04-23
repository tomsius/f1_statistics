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
            var poleSitters = new List<PolesModel>
            {
                new PolesModel
                {
                    Name = "First",
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 1,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "First",
                                    GapToSecond = 1.5
                                }
                            }
                        },
                        new PolesByYearModel
                        {
                            Year = 2,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "Second",
                                    GapToSecond = 1
                                }
                            }
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
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "Third",
                                    GapToSecond = 0.8
                                }
                            }
                        },
                        new PolesByYearModel
                        {
                            Year = 2,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "Fourth",
                                    GapToSecond = 2
                                }
                            }
                        }
                    }
                }
            };

            return poleSitters;
        }

        private List<UniqueSeasonPoleSittersModel> GenerateUniqueSeasonPoleSitters()
        {
            var uniquePolesitters = new List<UniqueSeasonPoleSittersModel>
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

            return uniquePolesitters;
        }

        [TestMethod]
        public void GetPoleSittersDrivers_ReturnAggregatedPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversPoleSitters = GeneratePoleSitters();
            _service.Setup((service) => service.AggregatePoleSittersDrivers(It.IsAny<OptionsModel>())).Returns(expectedDriversPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersDrivers(options);

            // Assert
            Assert.AreEqual(expectedDriversPoleSitters.Count, actual.Count);

            for (int i = 0; i < expectedDriversPoleSitters.Count; i++)
            {
                Assert.AreEqual(expectedDriversPoleSitters[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversPoleSitters[i].TotalPoleCount, actual[i].TotalPoleCount);
                Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear.Count, actual[i].PolesByYear.Count);

                for (int j = 0; j < expectedDriversPoleSitters[i].PolesByYear.Count; j++)
                {
                    Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].Year, actual[i].PolesByYear[j].Year);
                    Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].YearPoleCount, actual[i].PolesByYear[j].YearPoleCount);
                    Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation.Count, actual[i].PolesByYear[j].PoleInformation.Count);

                    for (int k = 0; k < expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation[k].CircuitName, actual[i].PolesByYear[j].PoleInformation[k].CircuitName);
                        Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation[k].GapToSecond, actual[i].PolesByYear[j].PoleInformation[k].GapToSecond);
                    }
                }
            }
        }

        [TestMethod]
        public void GetPoleSittersDrivers_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversPoleSitters = new List<PolesModel>();
            _service.Setup((service) => service.AggregatePoleSittersDrivers(It.IsAny<OptionsModel>())).Returns(expectedDriversPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersDrivers(options);

            // Assert
            Assert.AreEqual(expectedDriversPoleSitters.Count, actual.Count);
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnAggregatedPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsPoleSitters = GeneratePoleSitters();
            _service.Setup((service) => service.AggregatePoleSittersConstructors(It.IsAny<OptionsModel>())).Returns(expectedConstructorsPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersConstructors(options);

            // Assert
            Assert.AreEqual(expectedConstructorsPoleSitters.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsPoleSitters.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsPoleSitters[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsPoleSitters[i].TotalPoleCount, actual[i].TotalPoleCount);
                Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear.Count, actual[i].PolesByYear.Count);

                for (int j = 0; j < expectedConstructorsPoleSitters[i].PolesByYear.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].Year, actual[i].PolesByYear[j].Year);
                    Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].YearPoleCount, actual[i].PolesByYear[j].YearPoleCount);
                    Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation.Count, actual[i].PolesByYear[j].PoleInformation.Count);

                    for (int k = 0; k < expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation[k].CircuitName, actual[i].PolesByYear[j].PoleInformation[k].CircuitName);
                        Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation[k].GapToSecond, actual[i].PolesByYear[j].PoleInformation[k].GapToSecond);
                    }
                }
            }
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsPoleSitters = new List<PolesModel>();
            _service.Setup((service) => service.AggregatePoleSittersConstructors(It.IsAny<OptionsModel>())).Returns(expectedConstructorsPoleSitters);

            // Act
            var actual = _controller.GetPoleSittersConstructors(options);

            // Assert
            Assert.AreEqual(expectedConstructorsPoleSitters.Count, actual.Count);
        }

        [TestMethod]
        public void GetUniqueSeasonDriverPoleSitters_ReturnAggregatedUniqueSeasonPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersDrivers = GenerateUniqueSeasonPoleSitters();
            _service.Setup((service) => service.AggregateUniqueSeasonPoleSittersDrivers(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetUniqueSeasonDriversPoleSitters(options);

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
            var actual = _controller.GetUniqueSeasonDriversPoleSitters(options);

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
