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
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapInformation = new List<LeadingLapInformationModel>
                            {
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "FirstCircuit"
                                },
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "SecondCircuit"
                                }
                            }
                        },
                        new LeadingLapsByYearModel
                        {
                            Year = 2,
                            LeadingLapInformation = new List<LeadingLapInformationModel>
                            {
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "ThirdCircuit"
                                },
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "ForthCircuit"
                                }
                            }
                        }
                    }
                },
                new LeadingLapsModel
                {
                    Name = "Second",
                    LeadingLapsByYear = new List<LeadingLapsByYearModel>
                    {
                        new LeadingLapsByYearModel
                        {
                            Year = 1,
                            LeadingLapInformation = new List<LeadingLapInformationModel>
                            {
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "ThirdCircuit"
                                },
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "ForthCircuit"
                                }
                            }
                        },
                        new LeadingLapsByYearModel
                        {
                            Year = 2,
                            LeadingLapInformation = new List<LeadingLapInformationModel>
                            {
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "FirstCircuit"
                                },
                                new LeadingLapInformationModel
                                {
                                    CircuitName = "SecondCircuit"
                                }
                            }
                        }
                    }
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
                Assert.AreEqual(expectedDriversLeadingLapsCount[i].TotalLeadingLapCount, actual[i].TotalLeadingLapCount);
                Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear.Count, actual[i].LeadingLapsByYear.Count);

                for (int j = 0; j < expectedDriversLeadingLapsCount[i].LeadingLapsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear[j].Year, actual[i].LeadingLapsByYear[j].Year);
                    Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear[j].YearLeadingLapCount, actual[i].LeadingLapsByYear[j].YearLeadingLapCount);
                    Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapInformation.Count, actual[i].LeadingLapsByYear[j].LeadingLapInformation.Count);

                    for (int k = 0; k < expectedDriversLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapInformation[k].CircuitName, actual[i].LeadingLapsByYear[j].LeadingLapInformation[k].CircuitName);
                    }
                }
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
                Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].TotalLeadingLapCount, actual[i].TotalLeadingLapCount);
                Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear.Count, actual[i].LeadingLapsByYear.Count);

                for (int j = 0; j < expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear[j].Year, actual[i].LeadingLapsByYear[j].Year);
                    Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear[j].YearLeadingLapCount, actual[i].LeadingLapsByYear[j].YearLeadingLapCount);
                    Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapInformation.Count, actual[i].LeadingLapsByYear[j].LeadingLapInformation.Count);

                    for (int k = 0; k < expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsLeadingLapsCount[i].LeadingLapsByYear[j].LeadingLapInformation[k].CircuitName, actual[i].LeadingLapsByYear[j].LeadingLapInformation[k].CircuitName);
                    }
                }
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
