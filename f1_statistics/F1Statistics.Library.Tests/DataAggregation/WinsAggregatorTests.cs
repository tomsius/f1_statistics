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
                new List<RacesDataResponse> { new RacesDataResponse { Results = new List<ResultsDataResponse> { new ResultsDataResponse { Driver = new DriverDataResponse { familyName = "First",givenName="First"} } } } }, 
                new List<RacesDataResponse> 
                { 
                    new RacesDataResponse { Results = new List<ResultsDataResponse> { new ResultsDataResponse { Driver = new DriverDataResponse { familyName = "First", givenName = "First" } } } },
                    new RacesDataResponse { Results = new List<ResultsDataResponse> { new ResultsDataResponse { Driver = new DriverDataResponse { familyName = "Second", givenName = "Second" } } } } 
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
            var expectedWinners = new List<WinsModel> { new WinsModel { Name = "First First", WinCount = 2 }, new WinsModel { Name = "Second Second", WinCount = 1 } };
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
    }
}
