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
                    PoleCount = 2
                },
                new PolesModel
                {
                    Name = "Second",
                    PoleCount = 1
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
                    PoleSitters = new List<string>{ "First", "Second" }
                },
                new UniqueSeasonPoleSittersModel
                {
                    Season = 2,
                    PoleSitters = new List<string>{ "First" }
                }
            };

            return winners;
        }

        private List<WinsFromPoleModel> GenerateWinnersFromPole()
        {
            var winners = new List<WinsFromPoleModel>
            {
                new WinsFromPoleModel
                {
                    Season = 1,
                    WinnersFromPole = new List<string> { "First", "Second" }
                },
                new WinsFromPoleModel
                {
                    Season = 2,
                    WinnersFromPole = new List<string> { "First" }
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
            _service.Setup((service) => service.AggregateUniqueSeasonDriverPoleSitters(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetUniqueSeasonDriverPoleSitters(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonDriverPoleSitters_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>();
            _service.Setup((service) => service.AggregateUniqueSeasonDriverPoleSitters(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

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
            var expectedUniquePoleSittersDrivers = GenerateUniqueSeasonPoleSitters();
            _service.Setup((service) => service.AggregateUniqueSeasonConstructorPoleSitters(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetUniqueSeasonConstructorsPoleSitters(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
            }
        }

        [TestMethod]
        public void GetUniqueSeasonConstructorsPoleSitters_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>();
            _service.Setup((service) => service.AggregateUniqueSeasonConstructorPoleSitters(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetUniqueSeasonConstructorsPoleSitters(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnAggregatedWinnersFromPoleList_IfThereAreAnyWinnersFromPole()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersDrivers = GenerateWinnersFromPole();
            _service.Setup((service) => service.AggregateWinnersFromPole(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetWinnersFromPole(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].WinsFromPoleCount, actual[i].WinsFromPoleCount);
            }
        }

        [TestMethod]
        public void GetWinnersFromPole_ReturnEmptyList_IfThereAreNoWinnersFromPole()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedUniquePoleSittersDrivers = new List<WinsFromPoleModel>();
            _service.Setup((service) => service.AggregateWinnersFromPole(It.IsAny<OptionsModel>())).Returns(expectedUniquePoleSittersDrivers);

            // Act
            var actual = _controller.GetWinnersFromPole(options);

            // Assert
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
        }
    }
}
