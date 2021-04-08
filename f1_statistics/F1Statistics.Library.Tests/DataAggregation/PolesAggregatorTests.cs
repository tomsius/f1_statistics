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
    public class PolesAggregatorTests
    {
        private PolesAggregator _aggregator;
        private Mock<IQualifyingDataAccess> _qualifyingsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _qualifyingsDataAccess = new Mock<IQualifyingDataAccess>();

            _aggregator = new PolesAggregator(_qualifyingsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateQualifyings()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        round = "1",
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily", givenName= "FirstName"
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
                            }
                        },
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily", givenName= "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                }
                            },
                            new QualifyingResultsDataResponse
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
                            }
                        }
                    }
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        round = "1",
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily",
                                    givenName = "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                }
                            }
                        },
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "FirstFamily",
                                    givenName = "FirstName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "FirstConstructor"
                                }
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        round = "2",
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SecondFamily",
                                    givenName = "SecondName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                }
                            }
                        },
                        QualifyingResults = new List<QualifyingResultsDataResponse>
                        {
                            new QualifyingResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "SecondFamily",
                                    givenName = "SecondName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "SecondConstructor"
                                }
                            }
                        }
                    }
                }
            };

            return racesList;
        }

        [TestMethod]
        public void GetPoleSittersDrivers_ReturnAggregatedPoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedPoleSittersDrivers = new List<PolesModel> 
            {
                new PolesModel 
                {
                    Name = "FirstName FirstFamily", 
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 1
                        },
                        new PolesByYearModel
                        {
                            Year = 2
                        }
                    }
                },
                new PolesModel 
                {
                    Name = "SecondName SecondFamily",
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 2
                        }
                    }
                }
            };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersDrivers(from, to);
                actual.Sort((x, y) => y.TotalPoleCount.CompareTo(x.TotalPoleCount));
                actual.ForEach(model => model.PolesByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedPoleSittersDrivers.Count, actual.Count);

                for (int i = 0; i < expectedPoleSittersDrivers.Count; i++)
                {
                    Assert.AreEqual(expectedPoleSittersDrivers[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedPoleSittersDrivers[i].TotalPoleCount, actual[i].TotalPoleCount);
                    Assert.AreEqual(expectedPoleSittersDrivers[i].PolesByYear.Count, actual[i].PolesByYear.Count);

                    for (int j = 0; j < expectedPoleSittersDrivers[i].PolesByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedPoleSittersDrivers[i].PolesByYear[j].Year, actual[i].PolesByYear[j].Year);
                        Assert.AreEqual(expectedPoleSittersDrivers[i].PolesByYear[j].YearPoleCount, actual[i].PolesByYear[j].YearPoleCount);
                    }
                } 
            }
        }

        [TestMethod]
        public void GetPoleSittersDrivers_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedPoleSittersDrivers = new List<PolesModel>();
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersDrivers(from, to);

                // Assert
                Assert.AreEqual(expectedPoleSittersDrivers.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnAggregatedPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedPoleSittersConstructors = new List<PolesModel> 
            {
                new PolesModel 
                {
                    Name = "FirstConstructor",
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 1
                        },
                        new PolesByYearModel
                        {
                            Year = 2
                        }
                    }
                },
                new PolesModel 
                {
                    Name = "SecondConstructor",
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 2
                        }
                    }
                }
            };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersConstructors(from, to);
                actual.Sort((x, y) => y.TotalPoleCount.CompareTo(x.TotalPoleCount));
                actual.ForEach(model => model.PolesByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedPoleSittersConstructors.Count, actual.Count);

                for (int i = 0; i < expectedPoleSittersConstructors.Count; i++)
                {
                    Assert.AreEqual(expectedPoleSittersConstructors[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedPoleSittersConstructors[i].TotalPoleCount, actual[i].TotalPoleCount);
                } 
            }
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedPoleSittersConstructors = new List<PolesModel>();
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersConstructors(from, to);

                // Assert
                Assert.AreEqual(expectedPoleSittersConstructors.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetUniquePoleSittersDrivers_ReturnAggregatedUniquePoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel> { new UniqueSeasonPoleSittersModel { Season = 1, QualificationsCount = 1, PoleSitters = new List<string> { "FirstName FirstFamily" } }, new UniqueSeasonPoleSittersModel { Season = 2, QualificationsCount = 2, PoleSitters = new List<string> { "FirstName FirstFamily" , "SecondName SecondFamily" } } };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetUniquePoleSittersDrivers(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);

                for (int i = 0; i < expectedUniquePoleSittersDrivers.Count; i++)
                {
                    Assert.AreEqual(expectedUniquePoleSittersDrivers[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniquePoleSittersDrivers[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
                    Assert.AreEqual(expectedUniquePoleSittersDrivers[i].QualificationsCount, actual[i].QualificationsCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniquePoleSittersDrivers_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel> { new UniqueSeasonPoleSittersModel { PoleSitters = new List<string>() }, new UniqueSeasonPoleSittersModel { PoleSitters = new List<string>() } };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniquePoleSittersDrivers(from, to);

                // Assert
                Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[0].PoleSitters.Count, actual[0].PoleSitters.Count);
                Assert.AreEqual(expectedUniquePoleSittersDrivers[1].PoleSitters.Count, actual[1].PoleSitters.Count); 
            }
        }

        [TestMethod]
        public void GetUniquePoleSittersConstructors_ReturnAggregatedUniquePoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel> { new UniqueSeasonPoleSittersModel { Season = 1, QualificationsCount = 1, PoleSitters = new List<string> { "FirstName FirstFamily" } }, new UniqueSeasonPoleSittersModel { Season = 2, QualificationsCount = 2, PoleSitters = new List<string> { "FirstName FirstFamily", "SecondName SecondFamily" } } };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetUniquePoleSittersConstructors(from, to);
                actual.Sort((x, y) => x.Season.CompareTo(y.Season));

                // Assert
                Assert.AreEqual(expectedUniquePoleSittersConstructors.Count, actual.Count);

                for (int i = 0; i < expectedUniquePoleSittersConstructors.Count; i++)
                {
                    Assert.AreEqual(expectedUniquePoleSittersConstructors[i].Season, actual[i].Season);
                    Assert.AreEqual(expectedUniquePoleSittersConstructors[i].UniquePoleSittersCount, actual[i].UniquePoleSittersCount);
                    Assert.AreEqual(expectedUniquePoleSittersConstructors[i].QualificationsCount, actual[i].QualificationsCount);
                } 
            }
        }

        [TestMethod]
        public void GetUniquePoleSittersConstructors_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel> { new UniqueSeasonPoleSittersModel { PoleSitters = new List<string>() }, new UniqueSeasonPoleSittersModel { PoleSitters = new List<string>() } };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetUniquePoleSittersConstructors(from, to);

                // Assert
                Assert.AreEqual(expectedUniquePoleSittersConstructors.Count, actual.Count);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[0].PoleSitters.Count, actual[0].PoleSitters.Count);
                Assert.AreEqual(expectedUniquePoleSittersConstructors[1].PoleSitters.Count, actual[1].PoleSitters.Count); 
            }
        }
    }
}
