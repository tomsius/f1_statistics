using F1Statistics.Library.DataAccess;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class LapsDataAccessTests
    {
        private LapsDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new LapsDataAccess();
        }

        [TestMethod]
        public void GetLapsFrom_ReturnLapsList_IfYearAndRoundAreValid()
        {
            // Arrange
            var year = 2020;
            var round = 1;
            var expectedLapCount = 71;

            // Act
            var actual = _dataAccess.GetLapsFrom(year, round);

            // Assert
            Assert.AreEqual(expectedLapCount, actual.Count);
        }

        [TestMethod]
        public void GetLapsFrom_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var round = 1;
            var expectedLapCount = 0;

            // Act
            var actual = _dataAccess.GetLapsFrom(year, round);

            // Assert
            Assert.AreEqual(expectedLapCount, actual.Count);
        }

        [TestMethod]
        public void GetLapsFrom_ReturnEmptyList_IfRoundIsNotValid()
        {
            // Arrange
            var year = 2020;
            var round = -1;
            var expectedLapCount = 0;

            // Act
            var actual = _dataAccess.GetLapsFrom(year, round);

            // Assert
            Assert.AreEqual(expectedLapCount, actual.Count);
        }
    }
}
