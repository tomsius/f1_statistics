using F1Statistics.Library.DataAccess;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class DriversDataAccessTests
    {
        private DriversDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new DriversDataAccess();
        }

        [TestMethod]
        public void GetDriverName_ReturnDriversName_IfDriverIdIsValid()
        {
            // Arrange
            var driverId = "vettel";
            var expected = "Sebastian Vettel";

            // Act
            var actual = _dataAccess.GetDriverName(driverId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDriverName_ReturnEmptyString_IfDriverIdIsNotValid()
        {
            // Arrange
            var driverId = "abc";
            var expected = "";

            // Act
            var actual = _dataAccess.GetDriverName(driverId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDriversFrom_ReturnDriversList_IfYearIsValid()
        {
            // Arrange
            var year = 2020;
            var expected = new List<DriverDataResponse> 
            {
                new DriverDataResponse
                {
                    driverId = "aitken"
                },
                new DriverDataResponse
                {
                    driverId = "albon"
                },
                new DriverDataResponse
                {
                    driverId = "bottas"
                },
                new DriverDataResponse
                {
                    driverId = "gasly"
                },
                new DriverDataResponse
                {
                    driverId = "giovinazzi"
                },
                new DriverDataResponse
                {
                    driverId = "grosjean"
                },
                new DriverDataResponse
                {
                    driverId = "hamilton"
                },
                new DriverDataResponse
                {
                    driverId = "hulkenberg"
                },
                new DriverDataResponse
                {
                    driverId = "kevin_magnussen"
                },
                new DriverDataResponse
                {
                    driverId = "kvyat"
                },
                new DriverDataResponse
                {
                    driverId = "latifi"
                },
                new DriverDataResponse
                {
                    driverId = "leclerc"
                },
                new DriverDataResponse
                {
                    driverId = "max_verstappen"
                },
                new DriverDataResponse
                {
                    driverId = "norris"
                },
                new DriverDataResponse
                {
                    driverId = "ocon"
                },
                new DriverDataResponse
                {
                    driverId = "perez"
                },
                new DriverDataResponse
                {
                    driverId = "pietro_fittipaldi"
                },
                new DriverDataResponse
                {
                    driverId = "raikkonen"
                },
                new DriverDataResponse
                {
                    driverId = "ricciardo"
                },
                new DriverDataResponse
                {
                    driverId = "russell"
                },
                new DriverDataResponse
                {
                    driverId = "sainz"
                },
                new DriverDataResponse
                {
                    driverId = "stroll"
                },
                new DriverDataResponse
                {
                    driverId = "vettel"
                }
            };

            // Act
            var actual = _dataAccess.GetDriversFrom(year);
            actual.Sort((x, y) => x.driverId.CompareTo(y.driverId));

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].driverId, actual[i].driverId);
            }
        }

        [TestMethod]
        public void GetDriversFrom_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var expected = new List<DriverDataResponse>();

            // Act
            var actual = _dataAccess.GetDriversFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].driverId, actual[i].driverId);
            }
        }
    }
}
