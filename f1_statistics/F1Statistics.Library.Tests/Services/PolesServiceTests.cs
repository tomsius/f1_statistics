﻿using F1Statistics.Library.DataAggregation.Interfaces;
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

            _validator.Setup((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new PolesService(_validator.Object, _aggregator.Object);
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

        private List<WinnersFromPoleModel> GenerateWinnersFromPole()
        {
            var winners = new List<WinnersFromPoleModel>
            {
                new WinnersFromPoleModel
                {
                    Season = 1,
                    WinnersFromPole = new List<string> { "First", "Second" }
                },
                new WinnersFromPoleModel
                {
                    Season = 2,
                    WinnersFromPole = new List<string> { "First" }
                }
            };

            return winners;
        }

        [TestMethod]
        public void AggregatePoleSittersDrivers_ReturnAggregatedPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedPoleSittersDrivers = GeneratePoleSitters();
            expectedPoleSittersDrivers.Sort((x, y) => y.PoleCount.CompareTo(x.PoleCount));
            _aggregator.Setup((aggregator) => aggregator.GetPoleSittersDrivers(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePoleSitters());

            // Act
            var actual = _service.AggregatePoleSittersDrivers(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedPoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedPoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedPoleSittersDrivers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedPoleSittersDrivers[i].PoleCount, actual[i].PoleCount);
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedPoleSittersDrivers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregatePoleSittersConstructors_ReturnAggregatedPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedPoleSittersCoonstructors = GeneratePoleSitters();
            expectedPoleSittersCoonstructors.Sort((x, y) => y.PoleCount.CompareTo(x.PoleCount));
            _aggregator.Setup((aggregator) => aggregator.GetPoleSittersConstructors(It.IsAny<int>(), It.IsAny<int>())).Returns(GeneratePoleSitters());

            // Act
            var actual = _service.AggregatePoleSittersConstructors(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedPoleSittersCoonstructors.Count, actual.Count);

            for (int i = 0; i < expectedPoleSittersCoonstructors.Count; i++)
            {
                Assert.AreEqual(expectedPoleSittersCoonstructors[i].Name, actual[i].Name);
                Assert.AreEqual(expectedPoleSittersCoonstructors[i].PoleCount, actual[i].PoleCount);
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

            for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
            {
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateWinnersFromPole_ReturnAggregatedWinnersFromPoleList_IfThereAreAnyWinnersFromPole()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinnersFromPole = GenerateWinnersFromPole();
            _aggregator.Setup((aggregator) => aggregator.GetWinnersFromPole(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinnersFromPole());

            // Act
            var actual = _service.AggregateWinnersFromPole(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);

            for (int i = 0; i < expectedWinnersFromPole.Count; i++)
            {
                Assert.AreEqual(expectedWinnersFromPole[i].Season, actual[i].Season);
                Assert.AreEqual(expectedWinnersFromPole[i].WinsFromPoleCount, actual[i].WinsFromPoleCount);
            }
        }

        [TestMethod]
        public void AggregateWinnersFromPole_ReturnEmptyList_IfThereAreNoWinnersFromPole()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedWinnersFromPole = new List<WinnersFromPoleModel>();
            _aggregator.Setup((aggregator) => aggregator.GetWinnersFromPole(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinnersFromPole);

            // Act
            var actual = _service.AggregateWinnersFromPole(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);
        }
    }
}
