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
    public class SeasonsControllerTests
    {
        private SeasonsController _controller;
        private Mock<ISeasonsService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<ISeasonsService>();

            _controller = new SeasonsController(_service.Object);
        }

        private List<SeasonModel> GenerateSeasons()
        {
            var seasons = new List<SeasonModel>
            {
                new SeasonModel
                {
                    Year = 1,
                    Races = new List<RaceModel>
                    {
                        new RaceModel
                        {
                            Round = 1,
                            RaceName = "FirstRace"
                        },
                        new RaceModel
                        {
                            Round = 2,
                            RaceName = "SecondRace"
                        }
                    }
                },
                new SeasonModel
                {
                    Year = 2,
                    Races = new List<RaceModel>
                    {
                        new RaceModel
                        {
                            Round = 3,
                            RaceName = "FourthRace"
                        },
                        new RaceModel
                        {
                            Round = 5,
                            RaceName = "FifthRace"
                        }
                    }
                }
            };

            return seasons;
        }

        [TestMethod]
        public void GetSeasonRaces_ReturnAggregatedSeasonList_IfThereAreAnySeasons()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasons = GenerateSeasons();
            _service.Setup((service) => service.AggregateSeasonRaces(It.IsAny<OptionsModel>())).Returns(expectedSeasons);

            // Act
            var actual = _controller.GetSeasonRaces(options);

            // Assert
            Assert.AreEqual(expectedSeasons.Count, actual.Count);

            for (int i = 0; i < expectedSeasons.Count; i++)
            {
                Assert.AreEqual(expectedSeasons[i].Year, actual[i].Year);
                Assert.AreEqual(expectedSeasons[i].Races.Count, actual[i].Races.Count);

                for (int j = 0; j < expectedSeasons[i].Races.Count; j++)
                {
                    Assert.AreEqual(expectedSeasons[i].Races[j].Round, actual[i].Races[j].Round);
                    Assert.AreEqual(expectedSeasons[i].Races[j].RaceName, actual[i].Races[j].RaceName);
                }
            }
        }

        [TestMethod]
        public void GetSeasonRaces_ReturnEmptyList_IfThereAreNoSeasons()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasons = new List<SeasonModel>();
            _service.Setup((service) => service.AggregateSeasonRaces(It.IsAny<OptionsModel>())).Returns(expectedSeasons);

            // Act
            var actual = _controller.GetSeasonRaces(options);

            // Assert
            Assert.AreEqual(expectedSeasons.Count, actual.Count);
        }
    }
}
