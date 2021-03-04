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
    public class MiscServiceTests
    {
        private MiscService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<IMiscAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<IMiscAggregator>();

            _validator.Setup((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new MiscService(_validator.Object, _aggregator.Object);
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
        public void AggregateRaceCountPerSeason_ReturnSortedAggregatedRaceCountPerSeasonList_IfThereAreAnyRaces()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedSeasonRaces = GenerateSeasonRaces();
            expectedSeasonRaces.Sort((x, y) => x.Season.CompareTo(y.Season));
            _aggregator.Setup((aggregator) => aggregator.GetRaceCountPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonRaces());

            // Act
            var actual = _service.AggregateRaceCountPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);

            for (int i = 0; i < expectedSeasonRaces.Count; i++)
            {
                Assert.AreEqual(expectedSeasonRaces[i].Season, actual[i].Season);
                Assert.AreEqual(expectedSeasonRaces[i].RaceCount, actual[i].RaceCount);
            }
        }

        [TestMethod]
        public void AggregateRaceCountPerSeason_ReturnEmptyList_IfThereAreNoRaces()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedSeasonRaces = new List<SeasonRacesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetRaceCountPerSeason(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedSeasonRaces);

            // Act
            var actual = _service.AggregateRaceCountPerSeason(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasonRaces.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateHatTricks_ReturnSortedAggregatedHatTricksList_IfThereAreAnyDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedHatTricks = GenerateHatTricks();
            expectedHatTricks.Sort((x, y) => y.HatTrickCount.CompareTo(x.HatTrickCount));
            _aggregator.Setup((aggregator) => aggregator.GetHatTricks(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateHatTricks());

            // Act
            var actual = _service.AggregateHatTricks(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);

            for (int i = 0; i < expectedHatTricks.Count; i++)
            {
                Assert.AreEqual(expectedHatTricks[i].Name, actual[i].Name);
                Assert.AreEqual(expectedHatTricks[i].HatTrickCount, actual[i].HatTrickCount);
            }
        }

        [TestMethod]
        public void AggregateHatTricks_ReturnEmptyList_IfThereAreNoDriversWithHatTricks()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedHatTricks = new List<HatTrickModel>();
            _aggregator.Setup((aggregator) => aggregator.GetHatTricks(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedHatTricks);

            // Act
            var actual = _service.AggregateHatTricks(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedHatTricks.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateGrandSlams_ReturnSortedAggregatedGrandSlamsList_IfThereAreAnyDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedGrandslams = GenerateGrandSlams();
            expectedGrandslams.Sort((x, y) => y.GrandSlamCount.CompareTo(x.GrandSlamCount));
            _aggregator.Setup((aggregator) => aggregator.GetGrandSlams(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateGrandSlams());

            // Act
            var actual = _service.AggregateGrandSlams(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedGrandslams.Count, actual.Count);

            for (int i = 0; i < expectedGrandslams.Count; i++)
            {
                Assert.AreEqual(expectedGrandslams[i].Name, actual[i].Name);
                Assert.AreEqual(expectedGrandslams[i].GrandSlamCount, actual[i].GrandSlamCount);
            }
        }

        [TestMethod]
        public void AggregateGrandSlams_ReturnEmptyList_IfThereAreNoDriversWithGrandSlams()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedGrandSlams = new List<GrandSlamModel>();
            _aggregator.Setup((aggregator) => aggregator.GetGrandSlams(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedGrandSlams);

            // Act
            var actual = _service.AggregateGrandSlams(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedGrandSlams.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateNonFinishers_ReturnSortedAggregatedNonFinishersList_IfThereAreAnyDriversWhoDidNotFinishRace()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedNonFinishers = GenerateNonFinishers();
            expectedNonFinishers.Sort((x, y) => x.DidNotFinishCount.CompareTo(y.DidNotFinishCount));
            _aggregator.Setup((aggregator) => aggregator.GetNonFinishers(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateNonFinishers());

            // Act
            var actual = _service.AggregateNonFinishers(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNonFinishers.Count, actual.Count);

            for (int i = 0; i < expectedNonFinishers.Count; i++)
            {
                Assert.AreEqual(expectedNonFinishers[i].Name, actual[i].Name);
                Assert.AreEqual(expectedNonFinishers[i].DidNotFinishCount, actual[i].DidNotFinishCount);
            }
        }

        [TestMethod]
        public void AggregateNonFinisherss_ReturnEmptyList_IfAllDriversFinishedEveryRace()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedNonFinishers = new List<DidNotFinishModel>();
            _aggregator.Setup((aggregator) => aggregator.GetNonFinishers(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedNonFinishers);

            // Act
            var actual = _service.AggregateNonFinishers(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedNonFinishers.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateSeasonPositionChanges_ReturnSortedAggregatedSeasonPositionChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedSeasonPositionChanges = GenerateSeasonPositionChanges();
            expectedSeasonPositionChanges.ForEach(season => season.PositionChanges.Sort((x, y) => y.PositionChange.CompareTo(x.PositionChange)));
            expectedSeasonPositionChanges.Sort((x, y) => x.Season.CompareTo(y.Season));
            _aggregator.Setup((aggregator) => aggregator.GetSeasonPositionChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonPositionChanges());

            // Act
            var actual = _service.AggregateSeasonPositionChanges(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
        public void AggregateSeasonPositionChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedSeasonPositionChanges = new List<SeasonPositionChangesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetSeasonPositionChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedSeasonPositionChanges);

            // Act
            var actual = _service.AggregateSeasonPositionChanges(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasonPositionChanges.Count, actual.Count);
        }
    }
}
