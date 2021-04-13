using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.Services
{
    [TestClass]
    public class LeadingLapsServiceTests
    {
        private LeadingLapsService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<ILeadingLapsAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<ILeadingLapsAggregator>();

            _validator.Setup((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new LeadingLapsService(_validator.Object, _aggregator.Object);
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
                                    CircuitName = "FourthCircuit"
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
                                    CircuitName = "FourthCircuit"
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
        public void AggregateDriversLeadingLapsCount_ReturnSortedAggregatedDriversLeadingLapsCountList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversLeadingLapsCount = GenerateLeadingLaps();
            expectedDriversLeadingLapsCount.Sort((x, y) => y.TotalLeadingLapCount.CompareTo(x.TotalLeadingLapCount));
            expectedDriversLeadingLapsCount.ForEach(model => model.LeadingLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetDriversLeadingLapsCount(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateLeadingLaps());

            // Act
            var actual = _service.AggregateDriversLeadingLapsCount(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
        public void AggregateDriversLeadingLapsCount_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversLeadingLapsCount = new List<LeadingLapsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversLeadingLapsCount(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversLeadingLapsCount);

            // Act
            var actual = _service.AggregateDriversLeadingLapsCount(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversLeadingLapsCount.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsLeadingLapsCount_ReturnSortedAggregatedConstructorsLeadingLapsCountList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsLeadingLapsCount = GenerateLeadingLaps();
            expectedConstructorsLeadingLapsCount.Sort((x, y) => y.TotalLeadingLapCount.CompareTo(x.TotalLeadingLapCount));
            expectedConstructorsLeadingLapsCount.ForEach(model => model.LeadingLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsLeadingLapsCount(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateLeadingLaps());

            // Act
            var actual = _service.AggregateConstructorsLeadingLapsCount(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
        public void AggregateConstructorsLeadingLapsCount_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsLeadingLapsCount = new List<LeadingLapsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsLeadingLapsCount(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsLeadingLapsCount);

            // Act
            var actual = _service.AggregateConstructorsLeadingLapsCount(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsLeadingLapsCount.Count, actual.Count);
        }
    }
}
