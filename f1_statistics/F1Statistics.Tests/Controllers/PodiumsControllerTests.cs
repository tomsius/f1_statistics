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
    public class PodiumsControllerTests
    {
        private PodiumsController _controller;
        private Mock<IPodiumsService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IPodiumsService>();

            _controller = new PodiumsController(_service.Object);
        }

        private List<PodiumsModel> GeneratePodiumAchievers()
        {
            var achievers = new List<PodiumsModel>
            {
                new PodiumsModel
                {
                    Name = "First",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            PodiumInformation = new List<PodiumInformationModel>
                            {
                                new PodiumInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 1,
                                    PodiumPosition = 1
                                },
                                new PodiumInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 2,
                                    PodiumPosition = 2
                                }
                            }
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            PodiumInformation = new List<PodiumInformationModel>
                            {
                                new PodiumInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 2,
                                    PodiumPosition = 2
                                },
                                new PodiumInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 1,
                                    PodiumPosition = 1
                                }
                            }
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "Second",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            PodiumInformation = new List<PodiumInformationModel>
                            {
                                new PodiumInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 2,
                                    PodiumPosition = 2
                                },
                                new PodiumInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 1,
                                    PodiumPosition = 1
                                }
                            }
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            PodiumInformation = new List<PodiumInformationModel>
                            {
                                new PodiumInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 1,
                                    PodiumPosition = 1
                                },
                                new PodiumInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 2,
                                    PodiumPosition = 2
                                }
                            }
                        }
                    }
                }
            };

            return achievers;
        }

        private List<SamePodiumsModel> GenerateSamePodiums()
        {
            var samePodiums = new List<SamePodiumsModel>
            {
                new SamePodiumsModel
                {
                    PodiumFinishers = new List<string>
                    {
                        "First",
                        "Second",
                        "Third"
                    },
                    SamePodiumCount = 3
                },
                new SamePodiumsModel
                {
                    PodiumFinishers = new List<string>
                    {
                        "Fourth",
                        "Fifth",
                        "Sixth"
                    },
                    SamePodiumCount = 1
                }
            };

            return samePodiums;
        }

        [TestMethod]
        public void GetDriversPodiums_ReturnAggregatedDriversPodiumsCountList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversPodiumsCount = GeneratePodiumAchievers();
            _service.Setup((service) => service.AggregateDriversPodiums(It.IsAny<OptionsModel>())).Returns(expectedDriversPodiumsCount);

            // Act
            var actual = _controller.GetDriversPodiums(options);

            // Assert
            Assert.AreEqual(expectedDriversPodiumsCount.Count, actual.Count);

            for (int i = 0; i < expectedDriversPodiumsCount.Count; i++)
            {
                Assert.AreEqual(expectedDriversPodiumsCount[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversPodiumsCount[i].TotalPodiumCount, actual[i].TotalPodiumCount);
                Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumsByYear.Count, actual[i].PodiumsByYear.Count);

                for (int j = 0; j < expectedDriversPodiumsCount[i].PodiumsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumsByYear[j].Year, actual[i].PodiumsByYear[j].Year);
                    Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumsByYear[j].YearPodiumCount, actual[i].PodiumsByYear[j].YearPodiumCount);
                    Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumsByYear[j].PodiumInformation.Count, actual[i].PodiumsByYear[j].PodiumInformation.Count);

                    for (int k = 0; k < expectedDriversPodiumsCount[i].PodiumsByYear[j].PodiumInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumsByYear[j].PodiumInformation[k].CircuitName, actual[i].PodiumsByYear[j].PodiumInformation[k].CircuitName);
                        Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumsByYear[j].PodiumInformation[k].GridPosition, actual[i].PodiumsByYear[j].PodiumInformation[k].GridPosition);
                        Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumsByYear[j].PodiumInformation[k].PodiumPosition, actual[i].PodiumsByYear[j].PodiumInformation[k].PodiumPosition);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversPodiums_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversPodiumsCount = new List<PodiumsModel>();
            _service.Setup((service) => service.AggregateDriversPodiums(It.IsAny<OptionsModel>())).Returns(expectedDriversPodiumsCount);

            // Act
            var actual = _controller.GetDriversPodiums(options);

            // Assert
            Assert.AreEqual(expectedDriversPodiumsCount.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsPodiums_ReturnAggregatedConstructorsPodiumsCountList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsPodiumsCount = GeneratePodiumAchievers();
            _service.Setup((service) => service.AggregateConstructorsPodiums(It.IsAny<OptionsModel>())).Returns(expectedConstructorsPodiumsCount);

            // Act
            var actual = _controller.GetConstructorsPodiums(options);

            // Assert
            Assert.AreEqual(expectedConstructorsPodiumsCount.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsPodiumsCount.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsPodiumsCount[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsPodiumsCount[i].TotalPodiumCount, actual[i].TotalPodiumCount);
                Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumsByYear.Count, actual[i].PodiumsByYear.Count);

                for (int j = 0; j < expectedConstructorsPodiumsCount[i].PodiumsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumsByYear[j].Year, actual[i].PodiumsByYear[j].Year);
                    Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumsByYear[j].YearPodiumCount, actual[i].PodiumsByYear[j].YearPodiumCount);
                    Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumsByYear[j].PodiumInformation.Count, actual[i].PodiumsByYear[j].PodiumInformation.Count);

                    for (int k = 0; k < expectedConstructorsPodiumsCount[i].PodiumsByYear[j].PodiumInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumsByYear[j].PodiumInformation[k].CircuitName, actual[i].PodiumsByYear[j].PodiumInformation[k].CircuitName);
                        Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumsByYear[j].PodiumInformation[k].GridPosition, actual[i].PodiumsByYear[j].PodiumInformation[k].GridPosition);
                        Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumsByYear[j].PodiumInformation[k].PodiumPosition, actual[i].PodiumsByYear[j].PodiumInformation[k].PodiumPosition);
                    }
                }
            }
        }

        [TestMethod]
        public void GetConstructorsPodiums_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<PodiumsModel>();
            _service.Setup((service) => service.AggregateConstructorsPodiums(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetConstructorsPodiums(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetSameDriverPodiums_ReturnAggregatedSameDriversPodiumsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSameDriversPodiums = GenerateSamePodiums();
            _service.Setup((service) => service.AggregateSameDriverPodiums(It.IsAny<OptionsModel>())).Returns(expectedSameDriversPodiums);

            // Act
            var actual = _controller.GetSameDriverPodiums(options);

            // Assert
            Assert.AreEqual(expectedSameDriversPodiums.Count, actual.Count);

            for (int i = 0; i < expectedSameDriversPodiums.Count; i++)
            {
                Assert.AreEqual(expectedSameDriversPodiums[i].SamePodiumCount, actual[i].SamePodiumCount);
                Assert.AreEqual(expectedSameDriversPodiums[i].PodiumFinishers.Count, actual[i].PodiumFinishers.Count);

                for (int j = 0; j < expectedSameDriversPodiums[i].PodiumFinishers.Count; j++)
                {
                    Assert.AreEqual(expectedSameDriversPodiums[i].PodiumFinishers[j], actual[i].PodiumFinishers[j]);
                }
            }
        }

        [TestMethod]
        public void GetSameDriverPodiums_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSameDriversPodiums = new List<SamePodiumsModel>();
            _service.Setup((service) => service.AggregateSameDriverPodiums(It.IsAny<OptionsModel>())).Returns(expectedSameDriversPodiums);

            // Act
            var actual = _controller.GetSameDriverPodiums(options);

            // Assert
            Assert.AreEqual(expectedSameDriversPodiums.Count, actual.Count);
        }

        [TestMethod]
        public void GetSameConstructorsPodiums_ReturnAggregatedSameConstructorsPodiumsCountList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSameConstructorsPodiums = GenerateSamePodiums();
            _service.Setup((service) => service.AggregateSameConstructorsPodiums(It.IsAny<OptionsModel>())).Returns(expectedSameConstructorsPodiums);

            // Act
            var actual = _controller.GetSameConstructorsPodiums(options);

            // Assert
            Assert.AreEqual(expectedSameConstructorsPodiums.Count, actual.Count);

            for (int i = 0; i < expectedSameConstructorsPodiums.Count; i++)
            {
                Assert.AreEqual(expectedSameConstructorsPodiums[i].SamePodiumCount, actual[i].SamePodiumCount);
                Assert.AreEqual(expectedSameConstructorsPodiums[i].PodiumFinishers.Count, actual[i].PodiumFinishers.Count);

                for (int j = 0; j < expectedSameConstructorsPodiums[i].PodiumFinishers.Count; j++)
                {
                    Assert.AreEqual(expectedSameConstructorsPodiums[i].PodiumFinishers[j], actual[i].PodiumFinishers[j]);
                }
            }
        }

        [TestMethod]
        public void GetSameConstructorsPodiums_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedWinners = new List<SamePodiumsModel>();
            _service.Setup((service) => service.AggregateSameConstructorsPodiums(It.IsAny<OptionsModel>())).Returns(expectedWinners);

            // Act
            var actual = _controller.GetSameConstructorsPodiums(options);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }
    }
}
