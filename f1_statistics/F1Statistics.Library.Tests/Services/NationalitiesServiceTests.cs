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
    public class NationalitiesServiceTests
    {
        private NationalitiesService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<INationalitiesAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<INationalitiesAggregator>();

            _validator.Setup((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new NationalitiesService(_validator.Object, _aggregator.Object);
        }

        private List<NationalityDriversModel> GenerateNationalityDrivers()
        {
            var nationalityDrivers = new List<NationalityDriversModel>
            {
                new NationalityDriversModel
                {
                    Nationality = "FirstNationality",
                    Drivers = new List<string>
                    {
                        "FirstDriver",
                        "SecondDriver"
                    }
                },
                new NationalityDriversModel
                {
                    Nationality = "SecondNationality",
                    Drivers = new List<string>
                    {
                        "ThirdDriver"
                    }
                }
            };

            return nationalityDrivers;
        }

        private List<NationalityWinsModel> GenerateNationalityWinners()
        {
            var nationalityDrivers = new List<NationalityWinsModel>
            {
                new NationalityWinsModel
                {
                    Nationality = "FirstNationality",
                    Winners = new List<string>
                    {
                        "FirstDriver",
                        "SecondDriver"
                    }
                },
                new NationalityWinsModel
                {
                    Nationality = "SecondNationality",
                    Winners = new List<string>
                    {
                        "ThirdDriver"
                    }
                }
            };

            return nationalityDrivers;
        }

        [TestMethod]
        public void AggregateDriversNationalities_ReturnSortedAggregatedNationalityDriversList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversNationalities = GenerateNationalityDrivers();
            //expectedDriversNationalities.Sort((x, y) => y.DriversCount.CompareTo(x.DriversCount));
            expectedDriversNationalities.OrderBy(nationality => nationality.DriversCount).ThenBy(nationality => nationality.Nationality);
            _aggregator.Setup((aggregator) => aggregator.GetDriversNationalities(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateNationalityDrivers());

            // Act
            var actual = _service.AggregateDriversNationalities(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversNationalities.Count, actual.Count);

            for (int i = 0; i < expectedDriversNationalities.Count; i++)
            {
                Assert.AreEqual(expectedDriversNationalities[i].Nationality, actual[i].Nationality);
                Assert.AreEqual(expectedDriversNationalities[i].DriversCount, actual[i].DriversCount);

                for (int j = 0; j < expectedDriversNationalities[j].DriversCount; j++)
                {
                    Assert.AreEqual(expectedDriversNationalities[i].Drivers[j], actual[i].Drivers[j]);
                }
            }
        }

        [TestMethod]
        public void AggregateDriversNationalitiess_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversNationalities = new List<NationalityDriversModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversNationalities(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversNationalities);

            // Act
            var actual = _service.AggregateDriversNationalities(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversNationalities.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateNationalitiesRaceWins_ReturnSortedAggregatedNationalityRaceWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedNationalitiesRaceWinners = GenerateNationalityWinners();
            //expectedNationalitiesRaceWinners.Sort((x, y) => y.WinnersCount.CompareTo(x.WinnersCount));
            expectedNationalitiesRaceWinners.OrderBy(nationality => nationality.WinnersCount).ThenBy(nationality => nationality.Nationality);
            _aggregator.Setup((aggregator) => aggregator.GetNationalitiesRaceWins(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateNationalityWinners());

            // Act
            var actual = _service.AggregateNationalitiesRaceWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNationalitiesRaceWinners.Count, actual.Count);

            for (int i = 0; i < expectedNationalitiesRaceWinners.Count; i++)
            {
                Assert.AreEqual(expectedNationalitiesRaceWinners[i].Nationality, actual[i].Nationality);
                Assert.AreEqual(expectedNationalitiesRaceWinners[i].WinnersCount, actual[i].WinnersCount);

                for (int j = 0; j < expectedNationalitiesRaceWinners[j].WinnersCount; j++)
                {
                    Assert.AreEqual(expectedNationalitiesRaceWinners[i].Winners[j], actual[i].Winners[j]);
                }
            }
        }

        [TestMethod]
        public void AggregateNationalitiesRaceWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedNationalitiesRaceWinners = new List<NationalityWinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetNationalitiesRaceWins(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedNationalitiesRaceWinners);

            // Act
            var actual = _service.AggregateNationalitiesRaceWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNationalitiesRaceWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateNationalitiesSeasonWins_ReturnSortedAggregatedNationalityseasonWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedNationalitiesSeasonWinners = GenerateNationalityWinners();
            //expectedNationalitiesSeasonWinners.Sort((x, y) => y.WinnersCount.CompareTo(x.WinnersCount));
            expectedNationalitiesSeasonWinners.OrderBy(nationality => nationality.WinnersCount).ThenBy(nationality => nationality.Nationality);
            _aggregator.Setup((aggregator) => aggregator.GetNationalitiesSeasonWins(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateNationalityWinners());

            // Act
            var actual = _service.AggregateNationalitiesSeasonWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNationalitiesSeasonWinners.Count, actual.Count);

            for (int i = 0; i < expectedNationalitiesSeasonWinners.Count; i++)
            {
                Assert.AreEqual(expectedNationalitiesSeasonWinners[i].Nationality, actual[i].Nationality);
                Assert.AreEqual(expectedNationalitiesSeasonWinners[i].WinnersCount, actual[i].WinnersCount);

                for (int j = 0; j < expectedNationalitiesSeasonWinners[j].WinnersCount; j++)
                {
                    Assert.AreEqual(expectedNationalitiesSeasonWinners[i].Winners[j], actual[i].Winners[j]);
                }
            }
        }

        [TestMethod]
        public void AggregateNationalitiesSeasonWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedNationalitiesSeasonWinners = new List<NationalityWinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetNationalitiesSeasonWins(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedNationalitiesSeasonWinners);

            // Act
            var actual = _service.AggregateNationalitiesSeasonWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNationalitiesSeasonWinners.Count, actual.Count);
        }
    }
}
