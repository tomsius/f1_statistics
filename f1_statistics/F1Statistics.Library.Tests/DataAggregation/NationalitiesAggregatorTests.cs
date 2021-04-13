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
    public class NationalitiesAggregatorTests
    {
        private NationalitiesAggregator _aggregator;
        private Mock<IDriversDataAccess> _driversDataAccess;
        private Mock<IResultsDataAccess> _resultsDataAccess;
        private Mock<IStandingsDataAccess> _stadingsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _driversDataAccess = new Mock<IDriversDataAccess>();
            _resultsDataAccess = new Mock<IResultsDataAccess>();
            _stadingsDataAccess = new Mock<IStandingsDataAccess>();

            _aggregator = new NationalitiesAggregator(_driversDataAccess.Object, _resultsDataAccess.Object, _stadingsDataAccess.Object);
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
                                    givenName= "FirstName",
                                    nationality = "FirstNationality"
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
                                    givenName = "FirstName",
                                    nationality = "FirstNationality"
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
                                    familyName = "SecondFamily",
                                    givenName = "SecondName",
                                    nationality = "SecondNationality"
                                }
                            }
                        }
                    }
                }
            };

            return racesList;
        }

        private List<List<DriverDataResponse>> GenerateDrivers()
        {
            var drivers = new List<List<DriverDataResponse>>
            {
                new List<DriverDataResponse>
                {
                    new DriverDataResponse
                    {
                        nationality = "FirstNationality",
                        familyName = "FirstFamily",
                        givenName = "FirstName"
                    },
                    new DriverDataResponse
                    {
                        nationality = "FirstNationality",
                        familyName = "SecondFamily",
                        givenName = "SecondName"
                    },
                    new DriverDataResponse
                    {
                        nationality = "SecondNationality",
                        familyName = "ThirdFamily",
                        givenName = "ThirdName"
                    }
                },
                new List<DriverDataResponse>
                {
                    new DriverDataResponse
                    {
                        nationality = "FirstNationality",
                        familyName = "FirstFamily",
                        givenName = "FirstName"
                    },
                    new DriverDataResponse
                    {
                        nationality = "ThirdNationality",
                        familyName = "FourthFamily",
                        givenName = "FourthName"
                    }
                }
            };

            return drivers;
        }

        private List<List<DriverStandingsDataResponse>> GenerateStandings()
        {
            var standings = new List<List<DriverStandingsDataResponse>>
            {
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            nationality = "FirstNationality",
                            familyName = "FirstFamily",
                            givenName = "FirstName"
                        }
                    }
                },
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            nationality = "FirstNationality",
                            familyName = "FirstFamily",
                            givenName = "FirstName"
                        }
                    }
                },
                new List<DriverStandingsDataResponse>
                {
                    new DriverStandingsDataResponse
                    {
                        Driver = new DriverDataResponse
                        {
                            nationality = "SecondNationality",
                            familyName = "SecondFamily",
                            givenName = "SecondName"
                        }
                    }
                }
            };

            return standings;
        }

        [TestMethod]
        public void GetDriversNationalities_ReturnAggregatedNationalityDriversList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversNationalities = new List<NationalityDriversModel> 
            {
                new NationalityDriversModel 
                {
                    Nationality="FirstNationality", 
                    Drivers = new List<string> 
                    {
                        "FirstName FirstFamily", 
                        "SecondName SecondFamily" 
                    }
                },
                new NationalityDriversModel
                {
                    Nationality="SecondNationality",
                    Drivers = new List<string>
                    {
                        "ThirdName ThirdFamily"
                    }
                },
                new NationalityDriversModel
                {
                    Nationality="ThirdNationality",
                    Drivers = new List<string>
                    {
                        "FourthName FourthFamily",
                    }
                }
            };
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriversFrom(1)).Returns(GenerateDrivers()[0]);
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriversFrom(2)).Returns(GenerateDrivers()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetDriversNationalities(from, to);
                actual.Sort((x, y) => {
                    var cmp = y.DriversCount.CompareTo(x.DriversCount);

                    if (cmp == 0)
                    {
                        cmp = x.Nationality.CompareTo(y.Nationality);
                    }

                    return cmp;
                });

                // Assert
                Assert.AreEqual(expectedDriversNationalities.Count, actual.Count);

                for (int i = 0; i < expectedDriversNationalities.Count; i++)
                {
                    Assert.AreEqual(expectedDriversNationalities[i].Nationality, actual[i].Nationality);
                    Assert.AreEqual(expectedDriversNationalities[i].DriversCount, actual[i].DriversCount);

                    for (int j = 0; j < expectedDriversNationalities[j].DriversCount; j++)
                    {
                        Assert.AreEqual(expectedDriversNationalities[i].Drivers[j], actual[i].Drivers[j]);
                    }
                }
            }
        }

        [TestMethod]
        public void GetDriversNationalities_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedDriversNationalities = new List<NationalityDriversModel>();
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriversFrom(1)).Returns(new List<DriverDataResponse>());
            _driversDataAccess.Setup((driversDataAccess) => driversDataAccess.GetDriversFrom(2)).Returns(new List<DriverDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetDriversNationalities(from, to);

                // Assert
                Assert.AreEqual(expectedDriversNationalities.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetNationalitiesRaceWins_ReturnAggregatedNatonalityRaceWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedNationalitiesRaceWinners = new List<NationalityWinsModel> 
            {
                new NationalityWinsModel
                {
                    Nationality = "FirstNationality",
                    Winners = new List<string>
                    {
                        "FirstName FirstFamily",
                        "FirstName FirstFamily"
                    }
                },
                new NationalityWinsModel
                {
                    Nationality = "SecondNationality",
                    Winners = new List<string>
                    {
                        "SecondName SecondFamily"
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetNationalitiesRaceWins(from, to);
                actual.Sort((x, y) => {
                    var cmp = y.WinnersCount.CompareTo(x.WinnersCount);

                    if (cmp == 0)
                    {
                        cmp = x.Nationality.CompareTo(y.Nationality);
                    }

                    return cmp;
                });

                // Assert
                Assert.AreEqual(expectedNationalitiesRaceWinners.Count, actual.Count);

                for (int i = 0; i < expectedNationalitiesRaceWinners.Count; i++)
                {
                    Assert.AreEqual(expectedNationalitiesRaceWinners[i].Nationality, actual[i].Nationality);
                    Assert.AreEqual(expectedNationalitiesRaceWinners[i].WinnersCount, actual[i].WinnersCount);

                    for (int j = 0; j < expectedNationalitiesRaceWinners[j].WinnersCount; j++)
                    {
                        Assert.AreEqual(expectedNationalitiesRaceWinners[i].Winners[j], actual[i].Winners[j]);
                    }
                }
            }
        }

        [TestMethod]
        public void GetNationalitiesRaceWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedNationalitiesRaceWinners = new List<NationalityWinsModel>();
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetNationalitiesRaceWins(from, to);

                // Assert
                Assert.AreEqual(expectedNationalitiesRaceWinners.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetNationalitiesSeasonWins_ReturnAggregatedNationalitySeasonWinnersList_IfThereAreAnyDrivers()
        {
            // Arrange
            var from = 1;
            var to = 3;
            var expectedNationalitiesSeasonWinners = new List<NationalityWinsModel> 
            {
                new NationalityWinsModel
                {
                    Nationality = "FirstNationality",
                    Winners = new List<string>
                    {
                        "FirstName FirstFamily",
                        "FirstName FirstFamily"
                    }
                },
                new NationalityWinsModel
                {
                    Nationality = "SecondNationality",
                    Winners = new List<string>
                    {
                        "SecondName SecondFamily"
                    }
                }
            };
            _stadingsDataAccess.Setup((stadingsDataAccess) => stadingsDataAccess.GetDriverStandingsFrom(1)).Returns(GenerateStandings()[0]);
            _stadingsDataAccess.Setup((stadingsDataAccess) => stadingsDataAccess.GetDriverStandingsFrom(2)).Returns(GenerateStandings()[1]);
            _stadingsDataAccess.Setup((stadingsDataAccess) => stadingsDataAccess.GetDriverStandingsFrom(3)).Returns(GenerateStandings()[2]);

            for (int k = 0; k < 10000; k++)
            {
                // Act
                var actual = _aggregator.GetNationalitiesSeasonWins(from, to);
                actual.Sort((x, y) => {
                    var cmp = y.WinnersCount.CompareTo(x.WinnersCount);

                    if (cmp == 0)
                    {
                        cmp = x.Nationality.CompareTo(y.Nationality);
                    }

                    return cmp;
                });

                // Assert
                Assert.AreEqual(expectedNationalitiesSeasonWinners.Count, actual.Count);

                for (int i = 0; i < expectedNationalitiesSeasonWinners.Count; i++)
                {
                    Assert.AreEqual(expectedNationalitiesSeasonWinners[i].Nationality, actual[i].Nationality);
                    Assert.AreEqual(expectedNationalitiesSeasonWinners[i].WinnersCount, actual[i].WinnersCount);

                    for (int j = 0; j < expectedNationalitiesSeasonWinners[j].WinnersCount; j++)
                    {
                        Assert.AreEqual(expectedNationalitiesSeasonWinners[i].Winners[j], actual[i].Winners[j]);
                    }
                }
            }
        }

        [TestMethod]
        public void GetNationalitiesSeasonWins_ReturnEmptyList_IfThereAreNoDrivers()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedNationalitiesSeasonWinners = new List<NationalityWinsModel>();
            _stadingsDataAccess.Setup((stadingsDataAccess) => stadingsDataAccess.GetDriverStandingsFrom(1)).Returns(new List<DriverStandingsDataResponse>());
            _stadingsDataAccess.Setup((stadingsDataAccess) => stadingsDataAccess.GetDriverStandingsFrom(2)).Returns(new List<DriverStandingsDataResponse>());
            _stadingsDataAccess.Setup((stadingsDataAccess) => stadingsDataAccess.GetDriverStandingsFrom(3)).Returns(new List<DriverStandingsDataResponse>());

            for (int i = 0; i < 10000; i++)
            {
                // Act
                var actual = _aggregator.GetNationalitiesSeasonWins(from, to);

                // Assert
                Assert.AreEqual(expectedNationalitiesSeasonWinners.Count, actual.Count);
            }
        }
    }
}
