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
                    PodiumCount = 10
                },
                new PodiumsModel
                {
                    Name = "Second",
                    PodiumCount = 5
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
                Assert.AreEqual(expectedDriversPodiumsCount[i].PodiumCount, actual[i].PodiumCount);
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
                Assert.AreEqual(expectedConstructorsPodiumsCount[i].PodiumCount, actual[i].PodiumCount);
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
