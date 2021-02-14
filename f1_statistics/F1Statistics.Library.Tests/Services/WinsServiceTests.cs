using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Tests.Services
{
    [TestClass]
    public class WinsServiceTests
    {
        private WinsService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<IWinsAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<IWinsAggregator>();

            _validator.Setup((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new WinsService(_validator.Object, _aggregator.Object);
        }

        private List<WinsModel> GenerateWinners()
        {
            var winners = new List<WinsModel> { new WinsModel { Name = "Second", WinCount = 1 }, new WinsModel { Name = "First", WinCount = 2 } };

            return winners;
        }

        [TestMethod]
        public void AggregateDriversWins_ReturnSortedAggregatedWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinners = GenerateWinners();
            expectedWinners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetDriversWins(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinners());

            // Act
            var actual = _service.AggregateDriversWins(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void AggregateDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinners = new List<WinsModel>();
            expectedWinners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetDriversWins(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinners);

            // Act
            var actual = _service.AggregateDriversWins(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsWins_ReturnSortedAggregatedWinnersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinners = GenerateWinners();
            expectedWinners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWins(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinners());

            // Act
            var actual = _service.AggregateConstructorsWins(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void AggregateConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinners = new List<WinsModel>();
            expectedWinners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWins(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinners);

            // Act
            var actual = _service.AggregateConstructorsWins(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }
    }
}
