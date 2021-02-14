using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Tests.DataAggregation
{
    [TestClass]
    public class WinsAggregatorTests
    {
        private WinsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();

            _aggregator = new WinsAggregator(_resultsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse> { new RacesDataResponse { Results = new List<ResultsDataResponse> { new ResultsDataResponse { Driver = new DriverDataResponse { familyName = "FirstFamily", givenName= "FirstName" }, Constructor = new ConstructorDataResponse { name = "FirstConstructor"} } } } },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse { Results = new List<ResultsDataResponse> { new ResultsDataResponse { Driver = new DriverDataResponse { familyName = "FirstFamily", givenName = "FirstName" }, Constructor = new ConstructorDataResponse { name = "FirstConstructor" } } } },
                    new RacesDataResponse { Results = new List<ResultsDataResponse> { new ResultsDataResponse { Driver = new DriverDataResponse { familyName = "SecondFamily", givenName = "SecondName" }, Constructor = new ConstructorDataResponse { name = "SecondConstructor" } } } }
                }
            };

            return racesList;
        }

        [TestMethod]
        public void GetDriversWins_ReturnAggregatedDriversList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel> { new WinsModel { Name = "FirstName FirstFamily", WinCount = 2 }, new WinsModel { Name = "SecondName SecondFamily", WinCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetDriversWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void GetDriversWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetDriversWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnAggregatedConstructorsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel> { new WinsModel { Name = "FirstConstructor", WinCount = 2 }, new WinsModel { Name = "SecondConstructor", WinCount = 1 } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(2)).Returns(GenerateRaces()[1]);

            // Act
            var actual = _aggregator.GetConstructorsWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);

            for (int i = 0; i < expectedWinners.Count; i++)
            {
                Assert.AreEqual(expectedWinners[i].Name, actual[i].Name);
                Assert.AreEqual(expectedWinners[i].WinCount, actual[i].WinCount);
            }
        }

        [TestMethod]
        public void GetConstructorsWins_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<WinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetWinnersFrom(2)).Returns(new List<RacesDataResponse>());

            // Act
            var actual = _aggregator.GetConstructorsWins(from, to);

            // Assert
            Assert.AreEqual(expectedWinners.Count, actual.Count);
        }
    }
}
