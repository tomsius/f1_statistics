using F1Statistics.Library.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class RacesDataAccessTests
    {
        private RacesDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new RacesDataAccess();
        }

        [TestMethod]
        public void GetRacesCountFrom_Return17_IfYearIs2020()
        {
            // Arrange
            var year = 2020;
            var expectedRacesCount = 17;

            // Act
            var actual = _dataAccess.GetRacesCountFrom(year);

            // Assert
            Assert.AreEqual(expectedRacesCount, actual);
        }

        [TestMethod]
        public void GetRacesCountFrom_Return0_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var expectedRacesCount = 0;

            // Act
            var actual = _dataAccess.GetRacesCountFrom(year);

            // Assert
            Assert.AreEqual(expectedRacesCount, actual);
        }
    }
}
