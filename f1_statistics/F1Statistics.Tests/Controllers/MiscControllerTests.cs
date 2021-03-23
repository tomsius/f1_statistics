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
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "First",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    Points = 18
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    Points = 33
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "Second",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    Points = 25
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    Points = 32
                                }
                            }
                        }
                    }
                },
                new SeasonStandingsChangesModel
                {
                    Season = 2,
                    Standings = new List<StandingModel>
                    {
                        new StandingModel
                        {
                            Name = "First",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 1,
                                    Points = 25
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 1,
                                    Points = 50
                                }
                            }
                        },
                        new StandingModel
                        {
                            Name = "Second",
                            Rounds = new List<RoundModel>
                            {
                                new RoundModel
                                {
                                    Round = 1,
                                    Position = 2,
                                    Points = 18
                                },
                                new RoundModel
                                {
                                    Round = 2,
                                    Position = 2,
                                    Points = 32
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
                    Name = "First",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 1
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 1
                        },
                        new LapPositionModel
                        {
                            LapNumber = 3,
                            Position = 1
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    Name = "Second",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 2
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 3
                        },
                        new LapPositionModel
                        {
                            LapNumber = 3,
                            Position = 2
                        }
                    }
                },
                new RacePositionChangesModel
                {
                    Name = "Third",
                    Laps = new List<LapPositionModel>
                    {
                        new LapPositionModel
                        {
                            LapNumber = 1,
                            Position = 3
                        },
                        new LapPositionModel
                        {
                            LapNumber = 2,
                            Position = 2
                        },
                        new LapPositionModel
                        {
                            LapNumber = 3,
                            Position = 3
                        }
                    }
                }
            };

            return positionChangesDuringRace;
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

        [TestMethod]
        public void GetConstructorsFrontRows_ReturnAggregatedConstructorsfrontRowsCountList_IfThereAreAnyConstructorWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsFrontRows = GenerateConstructorsFrontRows();
            _service.Setup((service) => service.AggregateConstructorsFrontRows(It.IsAny<OptionsModel>())).Returns(expectedConstructorsFrontRows);

            // Act
            var actual = _controller.GetConstructorsFrontRows(options);

            // Assert
            Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsFrontRows.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsFrontRows[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsFrontRows[i].FrontRowCount, actual[i].FrontRowCount);
            }
        }

        [TestMethod]
        public void GetConstructorsFrontRows_ReturnEmptyList_IfThereAreNoConstructorsWithFrontRows()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsFrontRows = new List<FrontRowModel>();
            _service.Setup((service) => service.AggregateConstructorsFrontRows(It.IsAny<OptionsModel>())).Returns(expectedConstructorsFrontRows);

            // Act
            var actual = _controller.GetConstructorsFrontRows(options);

            // Assert
            Assert.AreEqual(expectedConstructorsFrontRows.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversFinishingPositions_ReturnAggregatedDriversFinishingPositionsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversFinishingPositions = GenerateDriversFinishingPositions();
            _service.Setup((service) => service.AggregateDriversFinishingPositions(It.IsAny<OptionsModel>())).Returns(expectedDriversFinishingPositions);

            // Act
            var actual = _controller.GetDriversFinishingPositions(options);

            // Assert
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
        public void GetDriversFinishingPositions_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversFinishingPositions = new List<DriverFinishingPositionsModel>();
            _service.Setup((service) => service.AggregateDriversFinishingPositions(It.IsAny<OptionsModel>())).Returns(expectedDriversFinishingPositions);

            // Act
            var actual = _controller.GetDriversFinishingPositions(options);

            // Assert
            Assert.AreEqual(expectedDriversFinishingPositions.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversStandingsChanges_ReturnAggregatedDriversStandingsChangesList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversStandingsChanges = GenerateSeasonStandingsChanges();
            _service.Setup((service) => service.AggregateDriversStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _controller.GetDriversStandingsChanges(options);

            // Assert
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedDriversStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedDriversStandingsChanges[i].Season, actual[i].Season);
                Assert.AreEqual(expectedDriversStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                for (int j = 0; j < expectedDriversStandingsChanges[i].Standings.Count; j++)
                {
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                    Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                    for (int k = 0; k < expectedDriversStandingsChanges[i].Standings[j].Rounds.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[k].Round, actual[i].Standings[j].Rounds[k].Round);
                        Assert.AreEqual(expectedDriversStandingsChanges[i].Standings[j].Rounds[k].Position, actual[i].Standings[j].Rounds[k].Position);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversStandingsChanges_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedDriversStandingsChanges = new List<SeasonStandingsChangesModel>();
            _service.Setup((service) => service.AggregateDriversStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedDriversStandingsChanges);

            // Act
            var actual = _controller.GetDriversStandingsChanges(options);

            // Assert
            Assert.AreEqual(expectedDriversStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsStandingsChanges_ReturnAggregatedConstructorsStandingsChangesList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsStandingsChanges = GenerateSeasonStandingsChanges();
            _service.Setup((service) => service.AggregateConstructorsStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _controller.GetConstructorsStandingsChanges(options);

            // Assert
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsStandingsChanges.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Season, actual[i].Season);
                Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings.Count, actual[i].Standings.Count);

                for (int j = 0; j < expectedConstructorsStandingsChanges[i].Standings.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Name, actual[i].Standings[j].Name);
                    Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count, actual[i].Standings[j].Rounds.Count);

                    for (int k = 0; k < expectedConstructorsStandingsChanges[i].Standings[j].Rounds.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[k].Round, actual[i].Standings[j].Rounds[k].Round);
                        Assert.AreEqual(expectedConstructorsStandingsChanges[i].Standings[j].Rounds[k].Position, actual[i].Standings[j].Rounds[k].Position);
                    }
                }
            }
        }

        [TestMethod]
        public void GetConstructorsStandingsChanges_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel();
            var expectedConstructorsStandingsChanges = new List<SeasonStandingsChangesModel>();
            _service.Setup((service) => service.AggregateConstructorsStandingsChanges(It.IsAny<OptionsModel>())).Returns(expectedConstructorsStandingsChanges);

            // Act
            var actual = _controller.GetConstructorsStandingsChanges(options);

            // Assert
            Assert.AreEqual(expectedConstructorsStandingsChanges.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriversPositionChangesDuringRace_ReturnAggregatedDriversPositionChangesDuringRaceList_IfThereAreAnyDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversPositionChangesDuringRace = GenerateDriversPositionChangesDuringRace();
            _service.Setup((service) => service.AggregateDriversPositionChangesDuringRace(season, race)).Returns(expectedDriversPositionChangesDuringRace);

            // Act
            var actual = _controller.GetDriversPositionChangesDuringRace(season, race);

            // Assert
            Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);

            for(int i = 0; i < expectedDriversPositionChangesDuringRace.Count; i++)
            {
                Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps.Count, actual[i].Laps.Count);

                for (int j = 0; j < expectedDriversPositionChangesDuringRace[i].Laps.Count; j++)
                {
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps[j].LapNumber, actual[i].Laps[j].LapNumber);
                    Assert.AreEqual(expectedDriversPositionChangesDuringRace[i].Laps[j].Position, actual[i].Laps[j].Position);
                }
            }
        }

        [TestMethod]
        public void GetDriversPositionChangesDuringRace_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var season = 1;
            var race = 1;
            var expectedDriversPositionChangesDuringRace = new List<RacePositionChangesModel>();
            _service.Setup((service) => service.AggregateDriversPositionChangesDuringRace(season, race)).Returns(expectedDriversPositionChangesDuringRace);

            // Act
            var actual = _controller.GetDriversPositionChangesDuringRace(season, race);

            // Assert
            Assert.AreEqual(expectedDriversPositionChangesDuringRace.Count, actual.Count);
        }
    }
}
