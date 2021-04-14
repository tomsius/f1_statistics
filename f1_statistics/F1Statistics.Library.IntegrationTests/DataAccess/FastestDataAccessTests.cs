using F1Statistics.Library.DataAccess;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class FastestDataAccessTests
    {
        private FastestDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new FastestDataAccess();
        }

        [TestMethod]
        public void GetFastestDriversFrom_ReturnRacesList_IfYearIsValid()
        {
            // Arrange
            var year = 2020;
            var expected = new List<RacesDataResponse>
            {
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "norris"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mclaren"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "sainz"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mclaren"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "hamilton"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "max_verstappen"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "red_bull"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "hamilton"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "bottas"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "ricciardo"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "renault"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "hamilton"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "hamilton"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "bottas"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "max_verstappen"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "red_bull"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "hamilton"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "hamilton"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "norris"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mclaren"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "max_verstappen"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "red_bull"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "russell"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "mercedes"
                            }
                        }
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse
                        {
                            Driver = new DriverDataResponse
                            {
                                driverId = "ricciardo"
                            },
                            Constructor = new ConstructorDataResponse
                            {
                                constructorId = "renault"
                            }
                        }
                    }
                }
            };

            // Act
            var actual = _dataAccess.GetFastestDriversFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Results.Count, actual[i].Results.Count);

                for (int j = 0; j < expected[i].Results.Count; j++)
                {
                    Assert.AreEqual(expected[i].Results[j].Driver.driverId, actual[i].Results[j].Driver.driverId);
                    Assert.AreEqual(expected[i].Results[j].Constructor.constructorId, actual[i].Results[j].Constructor.constructorId);
                }
            }
        }

        [TestMethod]
        public void GetFastestDriversFrom_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var expected = new List<RacesDataResponse>();

            // Act
            var actual = _dataAccess.GetFastestDriversFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}
