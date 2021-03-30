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
            var winners = new List<WinsModel> 
            { 
                new WinsModel 
                {
                    Name = "Second",
                    WinCount = 1 
                },
                new WinsModel 
                { Name = "First",
                    WinCount = 2 
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
                    Winners = new List<WinsModel>
                    {
                        new WinsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 2
                        }
                    }
                },
                new CircuitWinsModel
                {
                    Name = "SecondCircuit",
                    Winners = new List<WinsModel>
                    {
                        new WinsModel
                        {
                            Name = "FirstDriver",
                            WinCount = 1
                        },
                        new WinsModel
                        {
                            Name = "SecondDriver",
                            WinCount = 1
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

        private List<WinsByGridPositionModel> GenerateWinnersByGridPosition()
        {
            var winnersByGridPosition = new List<WinsByGridPositionModel>
            {
                new WinsByGridPositionModel
                {
                    GridPosition = 1,
                    Winners = new List<string>
                    {
                        "First",
                        "Second"
                    }
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 2,
                    Winners = new List<string>()
                },
                new WinsByGridPositionModel
                {
                    GridPosition = 3,
                    Winners = new List<string>
                    {
                        "First",
                        "Third"
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
            var options = new OptionsModel { Season = 2000 };
            var expectedWinners = new List<WinsModel>();
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
            var options = new OptionsModel { Season = 2000 };
            var expectedWinners = new List<WinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWins(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinners);

            // Act
            var actual = _service.AggregateConstructorsWins(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateDriversWinPercent_ReturnSortedAggregatedWinnersListWithAverageWins_IfThereAreAnyDrivers()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinners = GenerateWinnersWithAverageWins();
            expectedWinners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetDriversWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinnersWithAverageWins());

            // Act
            var actual = _service.AggregateDriversWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedWinners[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedWinners[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void AggregateDriversWinPercent_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedWinners = new List<AverageWinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetDriversWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinners);

            // Act
            var actual = _service.AggregateDriversWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateConstructorsWinPercent_ReturnSortedAggregatedWinnersListWithAverageWins_IfThereAreAnyConstructors()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinners = GenerateWinnersWithAverageWins();
            expectedWinners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateWinnersWithAverageWins());

            // Act
            var actual = _service.AggregateConstructorsWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedWinners[i].ParticipationCount, actual[i].ParticipationCount);
                Assert.AreEqual(expectedWinners[i].AverageWins, actual[i].AverageWins);
            }
        }

        [TestMethod]
        public void AggregateConstructorsWinPercent_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedWinners = new List<AverageWinsModel>();
            _aggregator.Setup((aggregator) => aggregator.GetConstructorsWinPercent(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedWinners);

            // Act
            var actual = _service.AggregateConstructorsWinPercent(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void AggregateCircuitsWinners_ReturnSortedAggregatedCircuitWinnersList_IfThereAreAnyCircuits()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedCircuitWinners = GenerateCircuitWinners();
            expectedCircuitWinners.ForEach(circuit => circuit.Winners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount)));
            _aggregator.Setup((aggregator) => aggregator.GetCircuitWinners(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateCircuitWinners());

            // Act
            var actual = _service.AggregateCircuitsWinners(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].RaceCount, actual[i].RaceCount);
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedUniqueWinners.Count, actual.Count);

            for (int i = 0; i < expectedUniqueWinners.Count; i++)
            {
                Assert.AreEqual(expectedUniqueWinners[i].Season, actual[i].Season);
                Assert.AreEqual(expectedUniqueWinners[i].RaceCount, actual[i].RaceCount);
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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

        [TestMethod]
        public void AggregateWinnersByGridPosition_ReturnAggregatedWinnersByGridPositionList_IfThereAreAnyWinners()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedWinnersByGridPosition = GenerateWinnersByGridPosition();
            var mockReturn = GenerateWinnersByGridPosition();
            mockReturn.RemoveAt(1);
            _aggregator.Setup((aggregator) => aggregator.GetWinnersByGridPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(mockReturn);

            // Act
            var actual = _service.AggregateWinnersByGridPosition(options);

            // Assert
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);

            for (int i = 0; i < expectedWinnersByGridPosition.Count; i++)
            {
                Assert.AreEqual(expectedWinnersByGridPosition[i].GridPosition, actual[i].GridPosition);
                Assert.AreEqual(expectedWinnersByGridPosition[i].WinCount, actual[i].WinCount);
                Assert.AreEqual(expectedWinnersByGridPosition[i].Winners.Count, actual[i].Winners.Count);

                for (int j = 0; j < expectedWinnersByGridPosition[i].Winners.Count; j++)
                {
                    Assert.AreEqual(expectedWinnersByGridPosition[i].Winners[j], actual[i].Winners[j]);
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
            _validator.Verify((validator) => validator.ValidateOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedWinnersByGridPosition.Count, actual.Count);
        }
    }
}
