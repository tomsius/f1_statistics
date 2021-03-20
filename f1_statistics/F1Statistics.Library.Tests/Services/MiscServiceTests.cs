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

        private List<FrontRowModel> GenerateConstructorsFrontRows()
        {
            var constructorFrontRows = new List<FrontRowModel>
            {
                new FrontRowModel
                {
                    Name = "First",
                    FrontRowCount = 3
                },
                new FrontRowModel
                {
                    Name = "Second",
                    FrontRowCount = 1
                }
            };

            return constructorFrontRows;
        }

        private List<DriverFinishingPositionsModel> GenerateDriversFinishingPositions()
        {
            var driversfinishingPositions = new List<DriverFinishingPositionsModel>()
            {
                new DriverFinishingPositionsModel
                {
                    Name = "First",
                    FinishingPositions = new List<FinishingPositionModel>
                    {
                        new FinishingPositionModel
                        {
                            FinishingPosition = 1,
                            Count = 10
                        },
                        new FinishingPositionModel
                        {
                            FinishingPosition = 2,
                            Count = 5
                        }
                    }
                },
                new DriverFinishingPositionsModel
                {
                    Name = "Second",
                    FinishingPositions = new List<FinishingPositionModel>
                    {
                        new FinishingPositionModel
                        {
                            FinishingPosition = 1,
                            Count = 2
                        }
                    }
                }
            };

            return driversfinishingPositions;
        }

        private List<SeasonStandingsChangesModel> GenerateSeasonStandingsChanges()
        {
            var seasonStandings = new List<SeasonStandingsChangesModel>
            {
                new SeasonStandingsChangesModel
                {
                    Season = 1,
                    Rounds = new List<RoundModel>
                    {
                        new RoundModel
                        {
                            Round = 1,
                            Standings = new List<StandingModel>
                            {
                                new StandingModel
                                {
                                    Name = "First",
                                    Position = 1
                                },
                                new StandingModel
                                {
                                    Name = "Second",
                                    Position = 2
                                }
                            }
                        },
                        new RoundModel
                        {
                            Round = 2,
                            Standings = new List<StandingModel>
                            {
                                new StandingModel
                                {
                                    Name = "Second",
                                    Position = 1
                                },
                                new StandingModel
                                {
                                    Name = "First",
                                    Position = 2
                                }
                            }
                        }
                    }
                },
                new SeasonStandingsChangesModel
                {
                    Season = 2,
                    Rounds = new List<RoundModel>
                    {
                        new RoundModel
                        {
                            Round = 1,
                            Standings = new List<StandingModel>
                            {
                                new StandingModel
                                {
                                    Name = "Second",
                                    Position = 1
                                },
                                new StandingModel
                                {
                                    Name = "First",
                                    Position = 2
                                }
                            }
                        },
                        new RoundModel
                        {
                            Round = 2,
                            Standings = new List<StandingModel>
                            {
                                new StandingModel
                                {
                                    Name = "First",
                                    Position = 1
                                },
                                new StandingModel
                                {
                                    Name = "Second",
                                    Position = 2
                                }
                            }
                        }
                    }
                }
            };

            return seasonStandings;
        }

        private List<RacePositionChangesModel> GenerateDriversPositionChangesDuringRace()
        {
            var positionChangesDuringRace = new List<RacePositionChangesModel>
            {
                new RacePositionChangesModel
                {
                    LapNumber = 1,
                    DriversPositions = new List<DriverPositionModel>
                    {
                        new DriverPositionModel
                        {
                            Name = "First",
                            Position = 1
                        },
                        new DriverPositionModel
                        {
                            Name = "Second",
                            Position = 2
                        },
                        new DriverPositionModel
                        {
                            Name = "Third",
                            Position = 3
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    LapNumber = 2,
                    DriversPositions = new List<DriverPositionModel>
                    {
                        new DriverPositionModel
                        {
                            Name = "Second",
                            Position = 1
                        },
                        new DriverPositionModel
                        {
                            Name = "First",
                            Position = 2
                        },
                        new DriverPositionModel
                        {
                            Name = "Third",
                            Position = 3
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    LapNumber = 2,
                    DriversPositions = new List<DriverPositionModel>
                    {
                        new DriverPositionModel
                        {
                            Name = "Third",
                            Position = 1
                        },
                        new DriverPositionModel
                        {
                            Name = "Second",
                            Position = 2
                        },
                        new DriverPositionModel
                        {
                            Name = "First",
                            Position = 3
                        }
                    }
                }
            };

            return positionChangesDuringRace;
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
            expectedNonFinishers.Sort((x, y) => y.DidNotFinishCount.CompareTo(x.DidNotFinishCount));
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

        [TestMethod]
        public void AggregateConstructorsFrontRows_ReturnSortedAggregatedConstructorsFrontRowsCountList_IfThereAreAnyConstructorsWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsFrontRows = GenerateConstructorsFrontRows();
            expectedConstructorsFrontRows.Sort((x, y) => y.FrontRowCount.CompareTo(x.FrontRowCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsFrontRows(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateConstructorsFrontRows());

            // Act
            var actual = _service.AggregateConstructorsFrontRows(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsFrontRows.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsFrontRows[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowCount, actual[i].FrontRowCount);
            }
        }

        [TestMethod]
        public void AggregateConstructorsFrontRows_ReturnEmptyList_IfThereAreNoConstructorsWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsFrontRows = new List<FrontRowModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsFrontRows(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsFrontRows);

            // Act
            var actual = _service.AggregateConstructorsFrontRows(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversFinishingPositions_ReturnSortedAggregatedDriversFinishingPositionsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversFinishingPositions = GenerateDriversFinishingPositions();
            expectedDriversFinishingPositions.ForEach(driver => driver.FinishingPositions.Sort((x, y) => x.FinishingPosition.CompareTo(y.FinishingPosition)));
            expectedDriversFinishingPositions.Sort((x, y) => x.Name.CompareTo(y.Name));
            _aggregator.Setup((aggregator) => aggregator.GetDriversFinishingPositions(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateDriversFinishingPositions());

            // Act
            var actual = _service.AggregateDriversFinishingPositions(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);

            for (int i = 0; i < expectedDriversFinishingPositions.Count; i++)
            {
                Assert.AreEqual(expectedDriversFinishingPositions[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions.Count, actual[i].FinishingPositions.Count);

                for (int j = 0; j < expectedDriversFinishingPositions[i].FinishingPositions.Count; j++)
                {
                    Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].FinishingPosition, actual[i].FinishingPositions[j].FinishingPosition);
                    Assert.AreEqual(expectedDriversFinishingPositions[i].FinishingPositions[j].Count, actual[i].FinishingPositions[j].Count);
                }
            }
        }

        [TestMethod]
        public void AggregateDriversFinishingPositions_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversFinishingPositions = new List<DriverFinishingPositionsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversFinishingPositions(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversFinishingPositions);

            // Act
            var actual = _service.AggregateDriversFinishingPositions(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversStandingsChanges_ReturnSortedAggregatedDriversStandingsChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversStandingsChanges = GenerateSeasonStandingsChanges();
            expectedDriversStandingsChanges.Sort((x, y) => x.Season.CompareTo(y.Season));
            expectedDriversStandingsChanges.ForEach(model => model.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round)));
            _aggregator.Setup((aggregator) => aggregator.GetDriversStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonStandingsChanges());

            // Act
            var actual = _service.AggregateDriversStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedDriversStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedDriversStandingsChanges[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds.Count, actual[i].Rounds.Count);

                for (int j = 0; j < expectedDriversStandingsChanges[i].Rounds.Count; j++)
                {
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Round, actual[i].Rounds[j].Round);
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Standings.Count, actual[i].Rounds[j].Standings.Count);

                    for (int k = 0; k < expectedDriversStandingsChanges[i].Rounds[j].Standings.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Standings[k].Name, actual[i].Rounds[j].Standings[k].Name);
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Rounds[j].Standings[k].Position, actual[i].Rounds[j].Standings[k].Position);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateDriversStandingsChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversStandingsChanges = new List<SeasonStandingsChangesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _service.AggregateDriversStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsStandingsChanges_ReturnSortedAggregatedConstructorsStandingsChangesList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsStandingsChanges = GenerateSeasonStandingsChanges();
            expectedConstructorsStandingsChanges.Sort((x, y) => x.Season.CompareTo(y.Season));
            expectedConstructorsStandingsChanges.ForEach(model => model.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round)));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasonStandingsChanges());

            // Act
            var actual = _service.AggregateConstructorsStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Season, actual[i].Season);
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds.Count, actual[i].Rounds.Count);

                for (int j = 0; j < expectedConstructorsStandingsChanges[i].Rounds.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Round, actual[i].Rounds[j].Round);
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Standings.Count, actual[i].Rounds[j].Standings.Count);

                    for (int k = 0; k < expectedConstructorsStandingsChanges[i].Rounds[j].Standings.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Standings[k].Name, actual[i].Rounds[j].Standings[k].Name);
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Rounds[j].Standings[k].Position, actual[i].Rounds[j].Standings[k].Position);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateConstructorsStandingsChanges_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsStandingsChanges = new List<SeasonStandingsChangesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsStandingsChanges(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _service.AggregateConstructorsStandingsChanges(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversPositionChangesDuringRace_ReturnSortedAggregatedDriversPositionChangesDuringRaceList_IfThereAreAnyDrivers()
        {
            // Arrange
            var season = 2020;
            var race = 1;
            var expectedDriversPositionChangesDuringRace = GenerateDriversPositionChangesDuringRace();
            expectedDriversPositionChangesDuringRace.Sort((x, y) => x.LapNumber.CompareTo(y.LapNumber));
            _aggregator.Setup((aggregator) => aggregator.GetDriversPositionChangesDuringRace(season, race)).Returns(GenerateDriversPositionChangesDuringRace());

            // Act
            var actual = _service.AggregateDriversPositionChangesDuringRace(season, race);

            // Assert
            Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);

            for (int i = 0; i < expectedDriversPositionChangesDuringRace.Count; i++)
            {
                Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].LapNumber, actual[i].LapNumber);
                Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].DriversPositions.Count, actual[i].DriversPositions.Count);

                for (int j = 0; j < expectedDriversPositionChangesDuringRace[i].DriversPositions.Count; j++)
                {
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].DriversPositions[j].Name, actual[i].DriversPositions[j].Name);
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].DriversPositions[j].Position, actual[i].DriversPositions[j].Position);
                }
            }
        }

        [TestMethod]
        public void AggregateDriversPositionChangesDuringRace_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var defaultYear = 1996;
            var expectedDriversPositionChangesDuringRace = new List<RacePositionChangesModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversPositionChangesDuringRace(defaultYear, race)).Returns(expectedDriversPositionChangesDuringRace);

            // Act
            var actual = _service.AggregateDriversPositionChangesDuringRace(season, race);

            // Assert
            Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);
        }
    }
}
