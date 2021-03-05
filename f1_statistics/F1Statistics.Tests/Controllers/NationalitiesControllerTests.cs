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
    public class NationalitiesControllerTests
    {
        private NationalitiesController _controller;
        private Mock<INationalitiesService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<INationalitiesService>();

            _controller = new NationalitiesController(_service.Object);
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
        public void GetDriversNationalities_ReturnAggregatedNationalityDriversList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversNationalities = GenerateNationalityDrivers();
            _service.Setup((service) => service.AggregateDriversNationalities(It.IsAny<OptionsModel>())).Returns(expectedDriversNationalities);

            // Act
            var actual = _controller.GetDriversNationalities(options);

            // Assert
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
        public void GetDriversNationalities_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversNationalities = new List<NationalityDriversModel>();
            _service.Setup((service) => service.AggregateDriversNationalities(It.IsAny<OptionsModel>())).Returns(expectedDriversNationalities);

            // Act
            var actual = _controller.GetDriversNationalities(options);

            // Assert
            Assert.AreEqual(expectedDriversNationalities.Count, actual.Count);
        }

        [TestMethod]
        public void GetNationalitiesRaceWins_ReturnAggregatedNationalityRaceWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNationalitiesRaceWinners = GenerateNationalityWinners();
            _service.Setup((service) => service.AggregateNationalitiesRaceWins(It.IsAny<OptionsModel>())).Returns(expectedNationalitiesRaceWinners);

            // Act
            var actual = _controller.GetNationalitiesRaceWins(options);

            // Assert
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
        public void GetNationalitiesRaceWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNationalitiesRaceWinners = new List<NationalityWinsModel>();
            _service.Setup((service) => service.AggregateNationalitiesRaceWins(It.IsAny<OptionsModel>())).Returns(expectedNationalitiesRaceWinners);

            // Act
            var actual = _controller.GetNationalitiesRaceWins(options);

            // Assert
            Assert.AreEqual(expectedNationalitiesRaceWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetNationalitiesSeasonWins_ReturnAggregatedNationalitySeasonWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNationalitiesSeasonWinners = GenerateNationalityWinners();
            _service.Setup((service) => service.AggregateNationalitiesSeasonWins(It.IsAny<OptionsModel>())).Returns(expectedNationalitiesSeasonWinners);

            // Act
            var actual = _controller.GetNationalitiesSeasonWins(options);

            // Assert
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
        public void GetNationalitiesSeasonWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNationalitiesSeasonWinners = new List<NationalityWinsModel>();
            _service.Setup((service) => service.AggregateNationalitiesSeasonWins(It.IsAny<OptionsModel>())).Returns(expectedNationalitiesSeasonWinners);

            // Act
            var actual = _controller.GetNationalitiesSeasonWins(options);

            // Assert
            Assert.AreEqual(expectedNationalitiesSeasonWinners.Count, actual.Count);
        }
    }
}
