using F1Statistics.Library.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class ConstructorsDataAccessTests
    {
        private ConstructorsDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new ConstructorsDataAccess();
        }

        [TestMethod]
        public void GetConstructorByDriver_ReturnDriversConstructor_IfYearRoundAndDriverIdAreValid()
        {
            // Arrange
            var year = 2020;
            var round = 1;
            var driverId = "vettel";
            var expected = "Ferrari";

            // Act
            var actual = _dataAccess.GetConstructorByDriver(year, round, driverId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetConstructorByDriver_ReturnEmptyString_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var round = 1;
            var driverId = "vettel";
            var expected = "";

            // Act
            var actual = _dataAccess.GetConstructorByDriver(year, round, driverId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetConstructorByDriver_ReturnEmptyString_IfRoundIsNotValid()
        {
            // Arrange
            var year = 2020;
            var round = -1;
            var driverId = "vettel";
            var expected = "";

            // Act
            var actual = _dataAccess.GetConstructorByDriver(year, round, driverId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetConstructorByDriver_ReturnEmptyString_IfDriverIdIsNotValid()
        {
            // Arrange
            var year = 2020;
            var round = 1;
            var driverId = "abc";
            var expected = "";

            // Act
            var actual = _dataAccess.GetConstructorByDriver(year, round, driverId);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
