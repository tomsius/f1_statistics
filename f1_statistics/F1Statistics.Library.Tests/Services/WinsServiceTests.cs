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

            _validator.Setup((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new WinsService(_validator.Object, _aggregator.Object);
        }

        private List<WinsModel> GenerateWinners()
        {
            var winners = new List<WinsModel>
            {
                new WinsModel
                {
                    Name = "First",
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 1,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 1,
                                    GapToSecond = 1.5
                                }
                            }
                        },
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 2,
                                    GapToSecond = 0.3
                                },
                                new WinInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 3,
                                    GapToSecond = 2.5
                                }
                            }
                        }
                    }
                },
                new WinsModel
                {
                    Name = "Second",
                    WinsByYear = new List<WinsByYearModel>
                    {
                        new WinsByYearModel
                        {
                            Year = 1,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "Second",
                                    GridPosition = 3,
                                    GapToSecond = 0.7
                                },
                                new WinInformationModel
                                {
                                    CircuitName = "Third",
                                    GridPosition = 4,
                                    GapToSecond = 0.8
                                }
                            }
                        },
                        new WinsByYearModel
                        {
                            Year = 2,
                            WinInformation = new List<WinInformationModel>
                            {
                                new WinInformationModel
                                {
                                    CircuitName = "First",
                                    GridPosition = 1,
                                    GapToSecond = 1
                                }
                            }
                        }
                    }
                }
            };

            return winners;
        }

        private List<AverageWinsModel> GenerateWinnersWithAverageWins()
        {
            var winners = new List<AverageWinsModel> 
            {
                new AverageWinsModel 
                {
                    Name = "Second", 
                    WinCount = 1, 
                    ParticipationCount = 10 
                },
                new AverageWinsModel 
                {
                    Name = "First",
                    WinCount = 2, 
                    ParticipationCount = 2 
                }
            };

            return winners;
        }

        private List<CircuitWinsModel> GenerateCircuitWinners()
        {
            var circuitWinners = new List<CircuitWinsModel>
            {
                new CircuitWinsModel
                {
                    Name = "FirstCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 2,
                            ParticipationsCount = 1
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "SecondCircuit",
                    Winners = new List<WinsAndParticipationsModel>
                    {
                        new WinsAndParticipationsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 1,
                            ParticipationsCount = 1
                        },
                        new WinsAndParticipationsModel
                        {
                            Name = "SecondDriver",
                            WinCount = 1,
                            ParticipationsCount = 1
                        }
                    }
                }
            };

            return circuitWinners;
        }

        private List<UniqueSeasonWinnersModel> GenerateUniqueSeasonWinners()
        {
            var uniqueSeaosnWinners = new List<UniqueSeasonWinnersModel>
            {
                new UniqueSeasonWinnersModel
                {
                    Season = 2020,
                    Winners = new List<string>
                    {
                        "First",
                        "Second"
                    }
                },
                new UniqueSeasonWinnersModel
                {
                    Season = 2021,
                    Winners = new List<string>
                    {
                        "First",
                        "Second",
                        "Third"
                    }
                }
            };

            return uniqueSeaosnWinners;
        }

        private List<WinnersFromPoleModel> GenerateWinnersFromPole()
        {
            var winners = new List<WinnersFromPoleModel>
            {
                new WinnersFromPoleModel
                {
                    Season = 1,
                    WinnersFromPole = new List<string> { "First", "Second" },
                    RacesCount = 1
                },
                new WinnersFromPoleModel
                {
                    Season = 2,
                    WinnersFromPole = new List<string> { "First" },
                    RacesCount = 2
                }
            };

            return winners;
        }

        private List<WinsByGridPositionModel> GenerateWinnersByGridPosition()
        {
            var winnersByGridPosition = new List<WinsByGridPositionModel>
            {
                new WinsByGridPositionModel
                {
                    GridPosition = 1,
                    WinInformation = new List<WinByGridInformationModel>
                    {
                        new WinByGridInformationModel
                        {
                            CircuitName = "First",
                            WinnerName = "FirstName FirstFamily"
                        },
                        new WinByGridInformationModel
                        {
                            CircuitName = "Second",
                            WinnerName = "FirstName FirstFamily"
                        }
                    }
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 2,
                    WinInformation = new List<WinByGridInformationModel>()
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 3,
                    WinInformation = new List<WinByGridInformationModel>
                    {
                        new WinByGridInformationModel
                        {
                            CircuitName = "First",
                            WinnerName = "SecondName SecondFamily"
                        },
                        new WinByGridInformationModel
                        {
                            CircuitName = "Second",
                            WinnerName = "SecondName SecondFamily"
                        }
                    }
                }
            };

            return winnersByGridPosition;
        }

        [TestMethod]
        public void AggregateDriversWins_ReturnSortedAggregatedWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversWinners = GenerateWinners();
            expectedDriversWinners.Sort((x, y) => y.TotalWinCount.CompareTo(x.TotalWinCount));
            expectedDriversWinners.ForEach(model => model.WinsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetDriversWins(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinners());

            // Act
            var actual = _service.AggregateDriversWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversWinners.Count, actual.Count);

            for (int i = 0; i < expectedDriversWinners.Count; i++)
            {
                Assert.AreEqual(expectedDriversWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversWinners[i].TotalWinCount, actual[i].TotalWinCount);
                Assert.AreEqual(expectedDriversWinners[i].WinsByYear.Count, actual[i].WinsByYear.Count);

                for (int j = 0; j < expectedDriversWinners[i].WinsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].Year, actual[i].WinsByYear[j].Year);
                    Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].YearWinCount, actual[i].WinsByYear[j].YearWinCount);
                    Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation.Count, actual[i].WinsByYear[j].WinInformation.Count);

                    for (int k = 0; k < expectedDriversWinners[i].WinsByYear[j].WinInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].CircuitName, actual[i].WinsByYear[j].WinInformation[k].CircuitName);
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].GapToSecond, actual[i].WinsByYear[j].WinInformation[k].GapToSecond);
                        Assert.AreEqual(expectedDriversWinners[i].WinsByYear[j].WinInformation[k].GridPosition, actual[i].WinsByYear[j].WinInformation[k].GridPosition);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversWinners = new List<WinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversWins(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversWinners);

            // Act
            var actual = _service.AggregateDriversWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsWins_ReturnSortedAggregatedWinnersList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorWinners = GenerateWinners();
            expectedConstructorWinners.Sort((x, y) => y.TotalWinCount.CompareTo(x.TotalWinCount));
            expectedConstructorWinners.ForEach(model => model.WinsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWins(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinners());

            // Act
            var actual = _service.AggregateConstructorsWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorWinners.Count, actual.Count);

            for (int i = 0; i < expectedConstructorWinners.Count; i++)
            {
                Assert.AreEqual(expectedConstructorWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorWinners[i].TotalWinCount, actual[i].TotalWinCount);
                Assert.AreEqual(expectedConstructorWinners[i].WinsByYear.Count, actual[i].WinsByYear.Count);

                for (int j = 0; j < expectedConstructorWinners[i].WinsByYear.Count; j++)
                {
                    Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].Year, actual[i].WinsByYear[j].Year);
                    Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].YearWinCount, actual[i].WinsByYear[j].YearWinCount);
                    Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation.Count, actual[i].WinsByYear[j].WinInformation.Count);

                    for (int k = 0; k < expectedConstructorWinners[i].WinsByYear[j].WinInformation.Count; k++)
                    {
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].CircuitName, actual[i].WinsByYear[j].WinInformation[k].CircuitName);
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].GapToSecond, actual[i].WinsByYear[j].WinInformation[k].GapToSecond);
                        Assert.AreEqual(expectedConstructorWinners[i].WinsByYear[j].WinInformation[k].GridPosition, actual[i].WinsByYear[j].WinInformation[k].GridPosition);
                    }
                }
            }
        }

        [TestMethod]
        public void AggregateConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorWinners = new List<WinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWins(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorWinners);

            // Act
            var actual = _service.AggregateConstructorsWins(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversWinPercent_ReturnSortedAggregatedWinnersWithWinPercentageList_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedDriversWinPercent = GenerateWinnersWithAverageWins();
            expectedDriversWinPercent.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetDriversWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinnersWithAverageWins());

            // Act
            var actual = _service.AggregateDriversWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversWinPercent.Count, actual.Count);

            for (int i = 0; i < expectedDriversWinPercent.Count; i++)
            {
                Assert.AreEqual(expectedDriversWinPercent[i].Name, actual[i].Name);
                Assert.AreEqual(expectedDriversWinPercent[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedDriversWinPercent[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedDriversWinPercent[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void AggregateDriversWinPercent_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedDriversWinPercent = new List<AverageWinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedDriversWinPercent);

            // Act
            var actual = _service.AggregateDriversWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedDriversWinPercent.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsWinPercent_ReturnSortedAggregatedWinnersWithWinPercentageList_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedConstructorsWinPercent = GenerateWinnersWithAverageWins();
            expectedConstructorsWinPercent.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinnersWithAverageWins());

            // Act
            var actual = _service.AggregateConstructorsWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsWinPercent.Count, actual.Count);

            for (int i = 0; i < expectedConstructorsWinPercent.Count; i++)
            {
                Assert.AreEqual(expectedConstructorsWinPercent[i].Name, actual[i].Name);
                Assert.AreEqual(expectedConstructorsWinPercent[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedConstructorsWinPercent[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedConstructorsWinPercent[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void AggregateConstructorsWinPercent_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedConstructorsWinPercent = new List<AverageWinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedConstructorsWinPercent);

            // Act
            var actual = _service.AggregateConstructorsWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedConstructorsWinPercent.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateCircuitsWinners_ReturnSortedAggregatedCircuitWinnersList_IfThereAreAnyCircuits()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedCircuitWinners = GenerateCircuitWinners();
            expectedCircuitWinners.ForEach(circuit => circuit.Winners = circuit.Winners.Where(winner => winner.WinCount > 0).ToList());
            expectedCircuitWinners.ForEach(circuit => circuit.Winners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount)));
            expectedCircuitWinners.Sort((x, y) => x.Name.CompareTo(y.Name));
            _aggregator.Setup((aggregator) => aggregator.GetCircuitWinners(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateCircuitWinners());

            // Act
            var actual = _service.AggregateCircuitsWinners(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedCircuitWinners.Count, actual.Count);

            for (int i = 0; i < expectedCircuitWinners.Count; i++)
            {
                Assert.AreEqual(expectedCircuitWinners[i].Name, actual[i].Name);

                for (int j = 0; j < expectedCircuitWinners[i].Winners.Count; j++)
                {
                    Assert.AreEqual(expectedCircuitWinners[i].Winners[j].Name, actual[i].Winners[j].Name);
                    Assert.AreEqual(expectedCircuitWinners[i].Winners[j].WinCount, actual[i].Winners[j].WinCount);
                }
            }
        }

        [TestMethod]
        public void AggregateCircuitsWinners_ReturnEmptyList_IfThereAreNoCircuits()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedCircuitWinners = new List<CircuitWinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetCircuitWinners(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedCircuitWinners);

            // Act
            var actual = _service.AggregateCircuitsWinners(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedCircuitWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateUniqueSeasonDriverWinners_ReturnAggregatedUniqueSeasonWinnersList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedUniqueWinners = GenerateUniqueSeasonWinners();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueSeasonDriverWinners(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _service.AggregateUniqueSeasonDriverWinners(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
            }
        }

        [TestMethod]
        public void AggregateUniqueSeasonDriverWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueSeasonDriverWinners(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _service.AggregateUniqueSeasonDriverWinners(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateUniqueSeasonConstructorWinners_ReturnAggregatedUniqueSeasonWinnersList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedUniqueWinners = GenerateUniqueSeasonWinners();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueSeasonConstructorWinners(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _service.AggregateUniqueSeasonConstructorWinners(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].RacesCount, actual[i].RacesCount);
                Assert.AreEqual(expectedUniqueWinners[i].UniqueWinnersCount, actual[i].UniqueWinnersCount);
            }
        }

        [TestMethod]
        public void AggregateUniqueSeasonConstructorWinners_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedUniqueWinners = new List<UniqueSeasonWinnersModel>();
            _aggregator.Setup((aggregator) => aggregator.GetUniqueSeasonConstructorWinners(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedUniqueWinners);

            // Act
            var actual = _service.AggregateUniqueSeasonConstructorWinners(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);
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
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);

            for (int i = 0; i < expectedWinnersFromPole.Count; i++)
            {
                Assert.AreEqual(expectedWinnersFromPole[i].Season, actual[i].Season);
                Assert.AreEqual(expectedWinnersFromPole[i].WinsFromPoleCount, actual[i].WinsFromPoleCount);
                Assert.AreEqual(expectedWinnersFromPole[i].RacesCount, actual[i].RacesCount);
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
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersFromPole.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateWinnersByGridPosition_ReturnAggregatedWinnersByGridPositionList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinnersByGridPosition = GenerateWinnersByGridPosition();
            _aggregator.Setup((aggregator) => aggregator.GetWinnersByGridPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinnersByGridPosition);

            // Act
            var actual = _service.AggregateWinnersByGridPosition(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);

            for (int i = 0; i < expectedWinnersByGridPosition.Count; i++)
            {
                Assert.AreEqual(expectedWinnersByGridPosition[i].GridPosition, actual[i].GridPosition);
                Assert.AreEqual(expectedWinnersByGridPosition[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation.Count, actual[i].WinInformation.Count);

                for (int j = 0; j < expectedWinnersByGridPosition[i].WinInformation.Count; j++)
                {
                    Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation[j].CircuitName, actual[i].WinInformation[j].CircuitName);
                    Assert.AreEqual(expectedWinnersByGridPosition[i].WinInformation[j].WinnerName, actual[i].WinInformation[j].WinnerName);
                }
            }
        }

        [TestMethod]
        public void AggregateWinnersByGridPosition_ReturnEmptyList_IfThereAreNoWinners()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedWinnersByGridPosition = new List<WinsByGridPositionModel>();
            _aggregator.Setup((aggregator) => aggregator.GetWinnersByGridPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinnersByGridPosition);

            // Act
            var actual = _service.AggregateWinnersByGridPosition(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);
        }
    }
}
