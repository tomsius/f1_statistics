using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation;
using F1Statistics.Library.Models;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.DataAggregation
{
    [TestClass]
    public class PodiumsAggregatorTests
    {
        private PodiumsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();

            _aggregator = new PodiumsAggregator(_resultsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily", 
                                    givenName= "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "ThirdFamily",
                                    givenName= "ThirdName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "ThirdConstructor"
                                }
                            }
                        }
                    }
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily", 
                                    givenName= "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SecondFamily",
                                    givenName= "SecondName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "ThirdFamily",
                                    givenName= "ThirdName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "ThirdConstructor"
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
                                    familyName = "ForthFamily", 
                                    givenName= "ForthName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "ForthConstructor"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FifthFamily",
                                    givenName= "FifthName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FifthConstructor"
                                }
                            },
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SixthFamily",
                                    givenName= "SixthName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SixthConstructor"
                                }
                            }
                        }
                    }
                }
            };

            return racesList;
        }

        [TestMethod]
        public void GetDriversPodiums_ReturnAggregatedDriversPodiumsCountList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversPodiumsCount = new List<PodiumsModel> 
            {
                new PodiumsModel 
                {
                    Name = "FirstName FirstFamily", 
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 1
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    } 
                },
                new PodiumsModel 
                {
                    Name = "SecondName SecondFamily",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 1
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "ThirdName ThirdFamily",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 1
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "ForthName ForthFamily",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "FifthName FifthFamily",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "SixthName SixthFamily",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversPodiums(from, to);
                actual.Sort((x, y) => y.TotalPodiumCount.CompareTo(x.TotalPodiumCount));

                // Assert
                Assert.AreEqual(expectedDriversPodiumsCount.Count, actual.Count);

                for (int i = 0; i < expectedDriversPodiumsCount.Count; i++)
                {
                    Assert.AreEqual(expectedDriversPodiumsCount[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversPodiumsCount[i].TotalPodiumCount, actual[i].TotalPodiumCount);
                } 
            }
        }

        [TestMethod]
        public void GetDriversPodiums_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversPodiumsCount = new List<PodiumsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversPodiums(from, to);

                // Assert
                Assert.AreEqual(expectedDriversPodiumsCount.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetConstructorsPodiums_ReturnAggregatedConstructorsPodiumsCountList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPodiumsCount = new List<PodiumsModel> 
            {
                new PodiumsModel
                {
                    Name = "FirstConstructor",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 1
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "SecondConstructor",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 1
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "ThirdConstructor",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 1,
                            YearPodiumCount = 1
                        },
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "ForthConstructor",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "FifthConstructor",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                },
                new PodiumsModel
                {
                    Name = "SixthConstructor",
                    PodiumsByYear = new List<PodiumsByYearModel>
                    {
                        new PodiumsByYearModel
                        {
                            Year = 2,
                            YearPodiumCount = 1
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetConstructorsPodiums(from, to);
                actual.Sort((x, y) => y.TotalPodiumCount.CompareTo(x.TotalPodiumCount));

                // Assert
                Assert.AreEqual(expectedConstructorsPodiumsCount.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsPodiumsCount.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsPodiumsCount[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedConstructorsPodiumsCount[i].TotalPodiumCount, actual[i].TotalPodiumCount);
                } 
            }
        }

        [TestMethod]
        public void GetConstructorsPodiums_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<PodiumsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetConstructorsPodiums(from, to);

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetSameDriversPodiums_ReturnAggregatedSameDriversPodiumsList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSameDriversPodiums = new List<SamePodiumsModel>
            {
                new SamePodiumsModel
                {
                    PodiumFinishers = new List<string>
                    {
                        "FirstName FirstFamily",
                        "SecondName SecondFamily",
                        "ThirdName ThirdFamily"
                    },
                    SamePodiumCount = 2
                },
                new SamePodiumsModel
                {
                    PodiumFinishers = new List<string>
                    {
                        "FifthName FifthFamily",
                        "ForthName ForthFamily",
                        "SixthName SixthFamily"
                    },
                    SamePodiumCount = 1
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetSameDriversPodiums(from, to);
                actual.ForEach(podium => podium.PodiumFinishers.Sort());
                actual.Sort((x, y) => y.SamePodiumCount.CompareTo(x.SamePodiumCount));

                // Assert
                Assert.AreEqual(expectedSameDriversPodiums.Count, actual.Count);

                for (int i = 0; i < expectedSameDriversPodiums.Count; i++)
                {
                    Assert.AreEqual(expectedSameDriversPodiums[i].SamePodiumCount, actual[i].SamePodiumCount);
                    Assert.AreEqual(expectedSameDriversPodiums[i].PodiumFinishers.Count, actual[i].PodiumFinishers.Count);

                    for (int j = 0; j < expectedSameDriversPodiums[i].PodiumFinishers.Count; j++)
                    {
                        Assert.AreEqual(expectedSameDriversPodiums[i].PodiumFinishers[j], actual[i].PodiumFinishers[j]);
                    }
                } 
            }
        }

        [TestMethod]
        public void GetSameDriversPodiums_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSameDriversPodiums = new List<SamePodiumsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetSameDriversPodiums(from, to);

                // Assert
                Assert.AreEqual(expectedSameDriversPodiums.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetSameConstructorsPodiums_ReturnAggregatedSameConstructorsPodiumsList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSameConstructorsPodiums = new List<SamePodiumsModel>
            {
                new SamePodiumsModel
                {
                    PodiumFinishers = new List<string>
                    {
                        "FirstConstructor",
                        "SecondConstructor",
                        "ThirdConstructor"
                    },
                    SamePodiumCount = 2
                },
                new SamePodiumsModel
                {
                    PodiumFinishers = new List<string>
                    {
                        "FifthConstructor",
                        "ForthConstructor",
                        "SixthConstructor"
                    },
                    SamePodiumCount = 1
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetSameConstructorsPodiums(from, to);
                actual.ForEach(podium => podium.PodiumFinishers.Sort());
                actual.Sort((x, y) => y.SamePodiumCount.CompareTo(x.SamePodiumCount));

                // Assert
                Assert.AreEqual(expectedSameConstructorsPodiums.Count, actual.Count);

                for (int i = 0; i < expectedSameConstructorsPodiums.Count; i++)
                {
                    Assert.AreEqual(expectedSameConstructorsPodiums[i].SamePodiumCount, actual[i].SamePodiumCount);
                    Assert.AreEqual(expectedSameConstructorsPodiums[i].PodiumFinishers.Count, actual[i].PodiumFinishers.Count);

                    for (int j = 0; j < expectedSameConstructorsPodiums[i].PodiumFinishers.Count; j++)
                    {
                        Assert.AreEqual(expectedSameConstructorsPodiums[i].PodiumFinishers[j], actual[i].PodiumFinishers[j]);
                    }
                } 
            }
        }

        [TestMethod]
        public void GetSameConstructorsPodiums_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedWinners = new List<SamePodiumsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetSameConstructorsPodiums(from, to);

                // Assert
                Assert.AreEqual(expectedWinners.Count, actual.Count); 
            }
        }
    }
}
