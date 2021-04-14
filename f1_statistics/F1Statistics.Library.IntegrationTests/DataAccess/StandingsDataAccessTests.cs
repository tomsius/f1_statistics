using F1Statistics.Library.DataAccess;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class StandingsDataAccessTests
    {
        private StandingsDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new StandingsDataAccess();
        }

        [TestMethod]
        public void GetDriverStandingsFrom_ReturnDriversStandingsList_IfYearIsValid()
        {
            // Arrange
            var year = 2020;
            var expected = new List<DriverStandingsDataResponse> 
            {
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "hamilton"
                    },
                    points = "347"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "bottas"
                    },
                    points = "223"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "max_verstappen"
                    },
                    points = "214"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "perez"
                    },
                    points = "125"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "ricciardo"
                    },
                    points = "119"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "sainz"
                    },
                    points = "105"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "albon"
                    },
                    points = "105"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "leclerc"
                    },
                    points = "98"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "norris"
                    },
                    points = "97"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "gasly"
                    },
                    points = "75"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "stroll"
                    },
                    points = "75"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "ocon"
                    },
                    points = "62"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "vettel"
                    },
                    points = "33"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "kvyat"
                    },
                    points = "32"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "hulkenberg"
                    },
                    points = "10"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "raikkonen"
                    },
                    points = "4"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "giovinazzi"
                    },
                    points = "4"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "russell"
                    },
                    points = "3"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "grosjean"
                    },
                    points = "2"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "kevin_magnussen"
                    },
                    points = "1"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "latifi"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "aitken"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "pietro_fittipaldi"
                    },
                    points = "0"
                }
            };

            // Act
            var actual = _dataAccess.GetDriverStandingsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Driver.driverId, actual[i].Driver.driverId);
                Assert.AreEqual(expected[i].points, actual[i].points);
            }
        }

        [TestMethod]
        public void GetDriverStandingsFrom_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var expected = new List<DriverStandingsDataResponse>();

            // Act
            var actual = _dataAccess.GetDriverStandingsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorStandingsFrom_ReturnDriversStandingsList_IfYearIsValid()
        {
            // Arrange
            var year = 2020;
            var expected = new List<ConstructorStandingsDataResponse>
            {
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "mercedes"
                    },
                    points = "573"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "red_bull"
                    },
                    points = "319"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "mclaren"
                    },
                    points = "202"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "racing_point"
                    },
                    points = "195"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "renault"
                    },
                    points = "181"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "ferrari"
                    },
                    points = "131"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "alphatauri"
                    },
                    points = "107"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "alfa"
                    },
                    points = "8"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "haas"
                    },
                    points = "3"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "williams"
                    },
                    points = "0"
                }

            };

            // Act
            var actual = _dataAccess.GetConstructorStandingsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Constructor.constructorId, actual[i].Constructor.constructorId);
                Assert.AreEqual(expected[i].points, actual[i].points);
            }
        }

        [TestMethod]
        public void GetConstructorStandingsFrom_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var expected = new List<ConstructorStandingsDataResponse>();

            // Act
            var actual = _dataAccess.GetConstructorStandingsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriverStandingsFromRace_ReturnDriversStandingsAfterRaceList_IfYearAndRoundAreValid()
        {
            // Arrange
            var year = 2020;
            var round = 1;
            var expected = new List<DriverStandingsDataResponse>
            {
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "bottas"
                    },
                    points = "25"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "leclerc"
                    },
                    points = "18"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "norris"
                    },
                    points = "16"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "hamilton"
                    },
                    points = "12"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "sainz"
                    },
                    points = "10"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "perez"
                    },
                    points = "8"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "gasly"
                    },
                    points = "6"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "ocon"
                    },
                    points = "4"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "giovinazzi"
                    },
                    points = "2"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "vettel"
                    },
                    points = "1"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "latifi"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "kvyat"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "albon"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "raikkonen"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "russell"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "grosjean"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "kevin_magnussen"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "stroll"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "ricciardo"
                    },
                    points = "0"
                },
                new DriverStandingsDataResponse
                {
                    Driver = new DriverDataResponse
                    {
                        driverId = "max_verstappen"
                    },
                    points = "0"
                }
            };

            // Act
            var actual = _dataAccess.GetDriverStandingsFromRace(year, round);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Driver.driverId, actual[i].Driver.driverId);
                Assert.AreEqual(expected[i].points, actual[i].points);
            }
        }

        [TestMethod]
        public void GetDriverStandingsFromRace_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var round = 1;
            var expected = new List<DriverStandingsDataResponse>();

            // Act
            var actual = _dataAccess.GetDriverStandingsFromRace(year, round);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void GetDriverStandingsFromRace_ReturnEmptyList_IfRoundIsNotValid()
        {
            // Arrange
            var year = 2020;
            var round = -1;
            var expected = new List<DriverStandingsDataResponse>();

            // Act
            var actual = _dataAccess.GetDriverStandingsFromRace(year, round);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorStandingsFromRace_ReturnDriversStandingsFromRaceList_IfYearAndRoundAreValid()
        {
            // Arrange
            var year = 2020;
            var round = 1;
            var expected = new List<ConstructorStandingsDataResponse>
            {
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "mercedes"
                    },
                    points = "37"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "mclaren"
                    },
                    points = "26"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "ferrari"
                    },
                    points = "19"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "racing_point"
                    },
                    points = "8"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "alphatauri"
                    },
                    points = "6"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "renault"
                    },
                    points = "4"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "alfa"
                    },
                    points = "2"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "williams"
                    },
                    points = "0"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "red_bull"
                    },
                    points = "0"
                },
                new ConstructorStandingsDataResponse
                {
                    Constructor = new ConstructorDataResponse
                    {
                        constructorId = "haas"
                    },
                    points = "0"
                }
            };

            // Act
            var actual = _dataAccess.GetConstructorStandingsFromRace(year, round);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Constructor.constructorId, actual[i].Constructor.constructorId);
                Assert.AreEqual(expected[i].points, actual[i].points);
            }
        }

        [TestMethod]
        public void GetConstructorStandingsFromRace_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var round = 1;
            var expected = new List<ConstructorStandingsDataResponse>();

            // Act
            var actual = _dataAccess.GetConstructorStandingsFromRace(year, round);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public void GetConstructorStandingsFromRace_ReturnEmptyList_IfRoundIsNotValid()
        {
            // Arrange
            var year = 2020;
            var round = -1;
            var expected = new List<ConstructorStandingsDataResponse>();

            // Act
            var actual = _dataAccess.GetConstructorStandingsFromRace(year, round);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}
