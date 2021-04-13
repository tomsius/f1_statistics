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
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "FirstCircuit"
                        },
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
                                },
                                Q3 = "1:03.555"
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
                                },
                                Q3 = "1:04.755"
                            }
                        }
                    }
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        round = "1",
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "FirstCircuit"
                        },
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
                                },
                                Q2 = "59.754"
                            },
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
                                },
                                Q2 = "1:2.754"
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        round = "2",
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "SecondCircuit"
                        },
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
                                },
                                Q1 = "2:20.755"
                            },
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
                                },
                                Q1 = "2:22.731"
                            }
                        }
                    },
                    new RacesDataResponse
                    {
                        round = "3",
                        Circuit = new CircuitDataResponse
                        {
                            circuitName = "ThirdCircuit"
                        },
                        Results = new List<ResultsDataResponse>
                        {
                            new ResultsDataResponse
                            {
                                Driver = new DriverDataResponse
                                {
                                    familyName = "ThirdFamily",
                                    givenName = "ThirdName"
                                },
                                Constructor = new ConstructorDataResponse
                                {
                                    name = "ThirdConstructor"
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
                                },
                                Q3 = "2:20.755"
                            },
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
                                },
                                Q3 = "2:22.731"
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
            var expectedDriversPoleSitters = new List<PolesModel> 
            {
                new PolesModel 
                {
                    Name = "FirstName FirstFamily", 
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 1,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 1.2
                                }
                            }
                        },
                        new PolesByYearModel
                        {
                            Year = 2,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 3
                                }
                            }
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
                            Year = 2,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    GapToSecond = 1.976
                                },
                                new PoleInformationModel
                                {
                                    CircuitName = "ThirdCircuit",
                                    GapToSecond = 1.976
                                }
                            }
                        }
                    }
                }
            };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersDrivers(from, to);
                actual.Sort((x, y) => y.TotalPoleCount.CompareTo(x.TotalPoleCount));
                actual.ForEach(model => model.PolesByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedDriversPoleSitters.Count, actual.Count);

                for (int i = 0; i < expectedDriversPoleSitters.Count; i++)
                {
                    Assert.AreEqual(expectedDriversPoleSitters[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedDriversPoleSitters[i].TotalPoleCount, actual[i].TotalPoleCount);
                    Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear.Count, actual[i].PolesByYear.Count);

                    for (int j = 0; j < expectedDriversPoleSitters[i].PolesByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].Year, actual[i].PolesByYear[j].Year);
                        Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].YearPoleCount, actual[i].PolesByYear[j].YearPoleCount);
                        Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation.Count, actual[i].PolesByYear[j].PoleInformation.Count);

                        for (int k = 0; k < expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation.Count; k++)
                        {
                            Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation[k].CircuitName, actual[i].PolesByYear[j].PoleInformation[k].CircuitName);
                            Assert.AreEqual(expectedDriversPoleSitters[i].PolesByYear[j].PoleInformation[k].GapToSecond, actual[i].PolesByYear[j].PoleInformation[k].GapToSecond);
                        }
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
            var expectedDriversPoleSitters = new List<PolesModel>();
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersDrivers(from, to);

                // Assert
                Assert.AreEqual(expectedDriversPoleSitters.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnAggregatedPoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPoleSitters = new List<PolesModel> 
            {
                new PolesModel 
                {
                    Name = "FirstConstructor",
                    PolesByYear = new List<PolesByYearModel>
                    {
                        new PolesByYearModel
                        {
                            Year = 1,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 1.2
                                }
                            }
                        },
                        new PolesByYearModel
                        {
                            Year = 2,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "FirstCircuit",
                                    GapToSecond = 3
                                }
                            }
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
                            Year = 2,
                            PoleInformation = new List<PoleInformationModel>
                            {
                                new PoleInformationModel
                                {
                                    CircuitName = "SecondCircuit",
                                    GapToSecond = 1.976
                                },
                                new PoleInformationModel
                                {
                                    CircuitName = "ThirdCircuit",
                                    GapToSecond = 1.976
                                }
                            }
                        }
                    }
                }
            };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersConstructors(from, to);
                actual.Sort((x, y) => y.TotalPoleCount.CompareTo(x.TotalPoleCount));
                actual.ForEach(model => model.PolesByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

                // Assert
                Assert.AreEqual(expectedConstructorsPoleSitters.Count, actual.Count);

                for (int i = 0; i < expectedConstructorsPoleSitters.Count; i++)
                {
                    Assert.AreEqual(expectedConstructorsPoleSitters[i].Name, actual[i].Name);
                    Assert.AreEqual(expectedConstructorsPoleSitters[i].TotalPoleCount, actual[i].TotalPoleCount);
                    Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear.Count, actual[i].PolesByYear.Count);

                    for (int j = 0; j < expectedConstructorsPoleSitters[i].PolesByYear.Count; j++)
                    {
                        Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].Year, actual[i].PolesByYear[j].Year);
                        Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].YearPoleCount, actual[i].PolesByYear[j].YearPoleCount);
                        Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation.Count, actual[i].PolesByYear[j].PoleInformation.Count);

                        for (int k = 0; k < expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation.Count; k++)
                        {
                            Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation[k].CircuitName, actual[i].PolesByYear[j].PoleInformation[k].CircuitName);
                            Assert.AreEqual(expectedConstructorsPoleSitters[i].PolesByYear[j].PoleInformation[k].GapToSecond, actual[i].PolesByYear[j].PoleInformation[k].GapToSecond);
                        }
                    }
                } 
            }
        }

        [TestMethod]
        public void GetPoleSittersConstructors_ReturnEmptyList_IfThereAreNoConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedConstructorsPoleSitters = new List<PolesModel>();
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(new List<RacesDataResponse>());
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetPoleSittersConstructors(from, to);

                // Assert
                Assert.AreEqual(expectedConstructorsPoleSitters.Count, actual.Count); 
            }
        }

        [TestMethod]
        public void GetUniquePoleSittersDrivers_ReturnAggregatedUniquePoleSittersDriverList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel> 
            {
                new UniqueSeasonPoleSittersModel 
                {
                    Season = 1, 
                    QualificationsCount = 1, 
                    PoleSitters = new List<string> 
                    {
                        "FirstName FirstFamily" 
                    }
                },
                new UniqueSeasonPoleSittersModel 
                {
                    Season = 2, 
                    QualificationsCount = 3, 
                    PoleSitters = new List<string> 
                    {
                        "FirstName FirstFamily",
                        "SecondName SecondFamily"
                    }
                }
            };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int t = 0; t < 10000; t++)
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

                    for (int j = 0; j < expectedUniquePoleSittersDrivers[0].PoleSitters.Count; j++)
                    {

                        Assert.AreEqual(expectedUniquePoleSittersDrivers[0].PoleSitters[j], actual[0].PoleSitters[j]);
                    }
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

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetUniquePoleSittersDrivers(from, to);

                // Assert
                Assert.AreEqual(expectedUniquePoleSittersDrivers.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetUniquePoleSittersConstructors_ReturnAggregatedUniquePoleSittersConstructorList_IfThereAreAnyConstructors()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedUniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel> 
            {
                new UniqueSeasonPoleSittersModel 
                {
                    Season = 1, 
                    QualificationsCount = 1, 
                    PoleSitters = new List<string> 
                    {
                        "FirstConstructor" 
                    }
                },
                new UniqueSeasonPoleSittersModel 
                {
                    Season = 2, 
                    QualificationsCount = 3,
                    PoleSitters = new List<string> 
                    {
                        "FirstConstructor", 
                        "SecondConstructor"
                    }
                }
            };
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(1)).Returns(GenerateQualifyings()[0]);
            _qualifyingsDataAccess.Setup((qualifyingDataAccess) => qualifyingDataAccess.GetQualifyingsFrom(2)).Returns(GenerateQualifyings()[1]);

            for (int t = 0; t < 10000; t++)
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

                    for (int j = 0; j < expectedUniquePoleSittersConstructors[i].PoleSitters.Count; j++)
                    {

                        Assert.AreEqual(expectedUniquePoleSittersConstructors[i].PoleSitters[j], actual[i].PoleSitters[j]);
                    }
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
            }
        }
    }
}
