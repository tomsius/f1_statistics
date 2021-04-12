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
    public class PolesServiceTests
    {
        private PolesService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<IPolesAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<IPolesAggregator>();

            _validator.Setup((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new PolesService(_validator.Object, _aggregator.Object);
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
                                    CircuitName = "Forth",
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
            var uniquePoleSitters = new List<UniqueSeasonPoleSittersModel>
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

            return uniquePoleSitters;
        }

        [TestMethod]
        public void AggregatePoleSittersDrivers_ReturnAggregatedPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversPoleSitters = GeneratePoleSitters();
            expectedDriversPoleSitters.Sort((x, y) => y.TotalPoleCount.CompareTo(x.TotalPoleCount));
            expectedDriversPoleSitters.ForEach(model => model.PolesByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetPoleSittersDrivers(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePoleSitters());

            // Act
            var actual = _service.AggregatePoleSittersDrivers(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
        public void AggregatePoleSittersDrivers_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedPoleSittersDrivers = new List<PolesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetPoleSittersDrivers(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedPoleSittersDrivers);

            // Act
            var actual = _service.AggregatePoleSittersDrivers(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedPoleSittersDrivers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregatePoleSittersConstructors_ReturnAggregatedPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsPoleSitters = GeneratePoleSitters();
            expectedConstructorsPoleSitters.Sort((x, y) => y.TotalPoleCount.CompareTo(x.TotalPoleCount));
            expectedConstructorsPoleSitters.ForEach(model => model.PolesByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetPoleSittersConstructors(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePoleSitters());

            // Act
            var actual = _service.AggregatePoleSittersConstructors(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
        public void AggregatePoleSittersConstructors_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedPoleSittersCoonstructors = new List<PolesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetPoleSittersConstructors(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedPoleSittersCoonstructors);

            // Act
            var actual = _service.AggregatePoleSittersConstructors(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedPoleSittersCoonstructors.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateUniqueSeasonPoleSittersDrivers_ReturnAggregatedUniqueSeasonPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedUniquePoleSittersDrivers = GenerateUniqueSeasonPoleSitters();
            _aggregator.Setup((aggregator) => aggregator.GetUniquePoleSittersDrivers(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateUniqueSeasonPoleSitters());

            // Act
            var actual = _service.AggregateUniqueSeasonPoleSittersDrivers(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].QualificationsCount, actual[i].QualificationsCount);
            }
        }

        [TestMethod]
        public void AggregateUniqueSeasonPoleSittersDrivers_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>();
            _aggregator.Setup((aggregator) => aggregator.GetUniquePoleSittersDrivers(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _service.AggregateUniqueSeasonPoleSittersDrivers(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateUniqueSeasonPoleSittersConstructors_ReturnAggregatedUniqueSeasonPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedUniquePoleSittersDrivers = GenerateUniqueSeasonPoleSitters();
            _aggregator.Setup((aggregator) => aggregator.GetUniquePoleSittersConstructors(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateUniqueSeasonPoleSitters());

            // Act
            var actual = _service.AggregateUniqueSeasonPoleSittersConstructors(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].QualificationsCount, actual[i].QualificationsCount);
            }
        }

        [TestMethod]
        public void AggregateUniqueSeasonPoleSittersConstructors_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>();
            _aggregator.Setup((aggregator) => aggregator.GetUniquePoleSittersConstructors(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _service.AggregateUniqueSeasonPoleSittersConstructors(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
        }
    }
}
