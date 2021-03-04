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
    public class MiscControllerTests
    {
        private MiscController _controller;
        private Mock<IMiscService> _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Mock<IMiscService>();

            _controller = new MiscController(_service.Object);
        }

        private List<SeasonRacesModel> GenerateSeasonRaces()
        {
            var sseasonRaces = new List<SeasonRacesModel>
            {
                new SeasonRacesModel
                {
                    Season = 1,
                    RaceCount = 10
                },
                new SeasonRacesModel
                {
                    Season = 2,
                    RaceCount = 20
                }
            };

            return sseasonRaces;
        }

        private List<HatTrickModel> GenerateHatTricks()
        {
            var hatTricks = new List<HatTrickModel>
            {
                new HatTrickModel
                {
                    Name = "First",
                    HatTrickCount = 5
                },
                new HatTrickModel
                {
                    Name = "Second",
                    HatTrickCount = 3
                }
            };

            return hatTricks;
        }

        private List<GrandSlamModel> GenerateGrandSlams()
        {
            var grandSlams = new List<GrandSlamModel>
            {
                new GrandSlamModel
                {
                    Name = "First",
                    GrandSlamCount = 5
                },
                new GrandSlamModel
                {
                    Name = "Second",
                    GrandSlamCount = 3
                }
            };

            return grandSlams;
        }

        private List<DidNotFinishModel> GenerateNonFinishers()
        {
            var nonFinishers = new List<DidNotFinishModel>
            {
                new DidNotFinishModel
                {
                    Name = "First",
                    DidNotFinishCount = 5
                },
                new DidNotFinishModel
                {
                    Name = "Second",
                    DidNotFinishCount = 3
                }
            };

            return nonFinishers;
        }

        private List<SeasonPositionChangesModel> GenerateSeasonPositionChanges()
        {
            var seasonPositionChanges = new List<SeasonPositionChangesModel>
            {
                new SeasonPositionChangesModel 
                { 
                    Season = 1,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "First",
                            PositionChange = 10
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "Second",
                            PositionChange = -10
                        }
                    }
                },
                new SeasonPositionChangesModel
                {
                    Season = 2,
                    PositionChanges = new List<DriverPositionChangeModel>
                    {
                        new DriverPositionChangeModel
                        {
                            Name = "First",
                            PositionChange = 20
                        },
                        new DriverPositionChangeModel
                        {
                            Name = "Second",
                            PositionChange = 0
                        }
                    }
                }
            };

            return seasonPositionChanges;
        }

        [TestMethod]
        public void GetRaceCountPerSeason_ReturnAggregatedRaceCountPerSeasonList_IfThereAreAnyRaces()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonRaces = GenerateSeasonRaces();
            _service.Setup((service) => service.AggregateRaceCountPerSeason(It.IsAny<OptionsModel>())).Returns(expectedSeasonRaces);

            // Act
            var actual = _controller.GetRaceCountPerSeason(options);

            // Assert
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);

            for (int i = 0; i < expectedSeasonRaces.Count; i++)
            {
                Assert.AreEqual(expectedSeasonRaces[i].Season, actual[i].Season);
                Assert.AreEqual(expectedSeasonRaces[i].RaceCount, actual[i].RaceCount);
            }
        }

        [TestMethod]
        public void GetRaceCountPerSeason_ReturnEmptyList_IfThereAreNoRaces()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonRaces = new List<SeasonRacesModel>();
            _service.Setup((service) => service.AggregateRaceCountPerSeason(It.IsAny<OptionsModel>())).Returns(expectedSeasonRaces);

            // Act
            var actual = _controller.GetRaceCountPerSeason(options);

            // Assert
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);
        }

        [TestMethod]
        public void GetHatTricks_ReturnAggregatedHatTricksList_IfThereAreAnyDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedHatTricks = GenerateHatTricks();
            _service.Setup((service) => service.AggregateHatTricks(It.IsAny<OptionsModel>())).Returns(expectedHatTricks);

            // Act
            var actual = _controller.GetHatTricks(options);

            // Assert
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);

            for (int i = 0; i < expectedHatTricks.Count; i++)
            {
                Assert.AreEqual(expectedHatTricks[i].Name, actual[i].Name);
                Assert.AreEqual(expectedHatTricks[i].HatTrickCount, actual[i].HatTrickCount);
            }
        }

        [TestMethod]
        public void GetHatTricks_ReturnEmptyList_IfThereAreNoDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedHatTricks = new List<HatTrickModel>();
            _service.Setup((service) => service.AggregateHatTricks(It.IsAny<OptionsModel>())).Returns(expectedHatTricks);

            // Act
            var actual = _controller.GetHatTricks(options);

            // Assert
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);
        }

        [TestMethod]
        public void GetGrandSlams_ReturnAggregatedGrandSlamsList_IfThereAreAnyDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedGrandSlams = GenerateGrandSlams();
            _service.Setup((service) => service.AggregateGrandSlams(It.IsAny<OptionsModel>())).Returns(expectedGrandSlams);

            // Act
            var actual = _controller.GetGrandSlams(options);

            // Assert
            Assert.AreEqual(expectedGrandSlams.Count, actual.Count);

            for (int i = 0; i < expectedGrandSlams.Count; i++)
            {
                Assert.AreEqual(expectedGrandSlams[i].Name, actual[i].Name);
                Assert.AreEqual(expectedGrandSlams[i].GrandSlamCount, actual[i].GrandSlamCount);
            }
        }

        [TestMethod]
        public void GetGrandSlams_ReturnEmptyList_IfThereAreNoDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedGrandSlams = new List<GrandSlamModel>();
            _service.Setup((service) => service.AggregateGrandSlams(It.IsAny<OptionsModel>())).Returns(expectedGrandSlams);

            // Act
            var actual = _controller.GetGrandSlams(options);

            // Assert
            Assert.AreEqual(expectedGrandSlams.Count, actual.Count);
        }

        [TestMethod]
        public void GetNonFinishers_ReturnAggregatedNonFinishersList_IfThereAreAnyDriversWhoDidNotFinishRace()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNonFinishgers = GenerateNonFinishers();
            _service.Setup((service) => service.AggregateNonFinishers(It.IsAny<OptionsModel>())).Returns(expectedNonFinishgers);

            // Act
            var actual = _controller.GetNonFinishers(options);

            // Assert
            Assert.AreEqual(expectedNonFinishgers.Count, actual.Count);

            for (int i = 0; i < expectedNonFinishgers.Count; i++)
            {
                Assert.AreEqual(expectedNonFinishgers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedNonFinishgers[i].DidNotFinishCount, actual[i].DidNotFinishCount);
            }
        }

        [TestMethod]
        public void GetNonFinishers_ReturnEmptyList_IfAllDriversFinishedEveryRace()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedNonFinishgers = new List<DidNotFinishModel>();
            _service.Setup((service) => service.AggregateNonFinishers(It.IsAny<OptionsModel>())).Returns(expectedNonFinishgers);

            // Act
            var actual = _controller.GetNonFinishers(options);

            // Assert
            Assert.AreEqual(expectedNonFinishgers.Count, actual.Count);
        }

        [TestMethod]
        public void GetPositionChanges_ReturnAggregatedSeasonPositionChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonPositionChanges = GenerateSeasonPositionChanges();
            _service.Setup((service) => service.AggregateSeasonPositionChanges(It.IsAny<OptionsModel>())).Returns(expectedSeasonPositionChanges);

            // Act
            var actual = _controller.GetSeasonPositionChanges(options);

            // Assert
            Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);

            for (int i = 0; i < expectedSeasonPositionChanges.Count; i++)
            {
                Assert.AreEqual(expectedSeasonPositionChanges[i].Season, actual[i].Season);
                Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges.Count, actual[i].PositionChanges.Count);

                for (int j = 0; j < expectedSeasonPositionChanges[i].PositionChanges.Count; j++)
                {
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].Name, actual[i].PositionChanges[j].Name);
                    Assert.AreEqual(expectedSeasonPositionChanges[i].PositionChanges[j].PositionChange, actual[i].PositionChanges[j].PositionChange);
                }
            }
        }

        [TestMethod]
        public void GetNonFinishers_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedSeasonPositionChanges = new List<SeasonPositionChangesModel>();
            _service.Setup((service) => service.AggregateSeasonPositionChanges(It.IsAny<OptionsModel>())).Returns(expectedSeasonPositionChanges);

            // Act
            var actual = _controller.GetSeasonPositionChanges(options);

            // Assert
            Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);
        }
    }
}
