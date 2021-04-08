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
    public class PodiumsServiceTests
    {
        private PodiumsService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<IPodiumsAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<IPodiumsAggregator>();

            _validator.Setup((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new PodiumsService(_validator.Object, _aggregator.Object);
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
                            YearPodiumCount = 1
                        },
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 2
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
                            YearPodiumCount = 2
                        },
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 1
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
        public void AggregateDriversPodiums_ReturnSortedAggregatedDriversPodiumsCountList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversPodiumsCount = GeneratePodiumAchievers();
            expectedDriversPodiumsCount.Sort((x, y) => y.TotalPodiumCount.CompareTo(x.TotalPodiumCount));
            expectedDriversPodiumsCount.ForEach(model => model.PodiumsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetDriversPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePodiumAchievers());

            // Act
            var actual = _service.AggregateDriversPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
                }
            }
        }

        [TestMethod]
        public void AggregateDriversPodiums_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversPodiumsCount = new List<PodiumsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversPodiumsCount);

            // Act
            var actual = _service.AggregateDriversPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversPodiumsCount.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsPodiums_ReturnSortedAggregatedConstructorsPodiumsCountList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsPodiumsCount = GeneratePodiumAchievers();
            expectedConstructorsPodiumsCount.Sort((x, y) => y.TotalPodiumCount.CompareTo(x.TotalPodiumCount));
            expectedConstructorsPodiumsCount.ForEach(model => model.PodiumsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePodiumAchievers());

            // Act
            var actual = _service.AggregateConstructorsPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
                }
            }
        }

        [TestMethod]
        public void AggregateConstructorsPodiums_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsPodiumsCount = new List<PodiumsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsPodiumsCount);

            // Act
            var actual = _service.AggregateConstructorsPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsPodiumsCount.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateSameDriverPodiums_ReturnSortedAggregatedSameDriversPodiumsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedSameDriversPodiums = GenerateSamePodiums();
            expectedSameDriversPodiums.ForEach(podium => podium.PodiumFinishers.Sort());
            expectedSameDriversPodiums.Sort((x, y) => y.SamePodiumCount.CompareTo(x.SamePodiumCount));
            _aggregator.Setup((aggregator) => aggregator.GetSameDriversPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSamePodiums());

            // Act
            var actual = _service.AggregateSameDriverPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
        public void AggregateSameDriverPodiums_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedSameDriversPodiums = new List<SamePodiumsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetSameDriversPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedSameDriversPodiums);

            // Act
            var actual = _service.AggregateSameDriverPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSameDriversPodiums.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateSameConstructorsPodiums_ReturnSortedAggregatedSameConstructorsPodiumsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedSameConstructorsPodiums = GenerateSamePodiums();
            expectedSameConstructorsPodiums.ForEach(podium => podium.PodiumFinishers.Sort());
            expectedSameConstructorsPodiums.Sort((x, y) => y.SamePodiumCount.CompareTo(x.SamePodiumCount));
            _aggregator.Setup((aggregator) => aggregator.GetSameConstructorsPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSamePodiums());

            // Act
            var actual = _service.AggregateSameConstructorsPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
        public void AggregateSameConstructorsPodiums_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsPodiums = new List<SamePodiumsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetSameConstructorsPodiums(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsPodiums);

            // Act
            var actual = _service.AggregateSameConstructorsPodiums(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsPodiums.Count, actual.Count);
        }
    }
}
