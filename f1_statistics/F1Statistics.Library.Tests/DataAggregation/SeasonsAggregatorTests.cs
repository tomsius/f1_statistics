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
    public class SeasonsAggregatorTests
    {
        private SeasonsAggregator _aggregator;
        private Mock<IResultsDataAccess> _resultsDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _resultsDataAccess = new Mock<IResultsDataAccess>();

            _aggregator = new SeasonsAggregator(_resultsDataAccess.Object);
        }

        private List<List<RacesDataResponse>> GenerateRaces()
        {
            var racesList = new List<List<RacesDataResponse>>
            {
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        round = "1",
                        raceName = "First"
                    },
                    new RacesDataResponse
                    {
                        round = "2",
                        raceName = "Second"
                    }
                },
                new List<RacesDataResponse>
                {
                    new RacesDataResponse
                    {
                        round = "1",
                        raceName = "Third"
                    },
                    new RacesDataResponse
                    {
                        round = "2",
                        raceName = "Fourth"
                    }
                }
            };

            return racesList;
        }

        [TestMethod]
        public void GetSeasonRaces_ReturnAggregatedSeasonsList_IfThereAreAnySeasons()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSeasons = new List<SeasonModel>
            {
                new SeasonModel
                {
                    Year = 1,
                    Races = new List<RaceModel>
                    {
                        new RaceModel
                        {
                            Round = 1,
                            RaceName = "First"
                        },
                        new RaceModel
                        {
                            Round = 2,
                            RaceName = "Second"
                        }
                    }
                },
                new SeasonModel
                {
                    Year = 2,
                    Races = new List<RaceModel>
                    {
                        new RaceModel
                        {
                            Round = 1,
                            RaceName = "Third"
                        },
                        new RaceModel
                        {
                            Round = 2,
                            RaceName = "Fourth"
                        }
                    }
                }
            };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(GenerateRaces()[0]);
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(GenerateRaces()[1]);

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetSeasonRaces(from, to);
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));

                // Assert
                Assert.AreEqual(expectedSeasons.Count, actual.Count);

                for (int i = 0; i < expectedSeasons.Count; i++)
                {
                    Assert.AreEqual(expectedSeasons[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedSeasons[i].Races.Count, actual[i].Races.Count);

                    for (int j = 0; j < expectedSeasons[i].Races.Count; j++)
                    {
                        Assert.AreEqual(expectedSeasons[i].Races[j].Round, actual[i].Races[j].Round);
                        Assert.AreEqual(expectedSeasons[i].Races[j].RaceName, actual[i].Races[j].RaceName);
                    }
                }
            }
        }

        [TestMethod]
        public void GetSeasonRaces_ReturnEmptyList_IfThereAreNoSeasons()
        {
            // Arrange
            var from = 1;
            var to = 2;
            var expectedSeasons = new List<SeasonModel> { new SeasonModel { Year = 1, Races = new List<RaceModel>() }, new SeasonModel { Year = 2, Races = new List<RaceModel>() } };
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(1)).Returns(new List<RacesDataResponse>());
            _resultsDataAccess.Setup((resultsDataAccess) => resultsDataAccess.GetResultsFrom(2)).Returns(new List<RacesDataResponse>());

            for (int t = 0; t < 10000; t++)
            {
                // Act
                var actual = _aggregator.GetSeasonRaces(from, to);
                actual.Sort((x, y) => x.Year.CompareTo(y.Year));

                // Assert
                Assert.AreEqual(expectedSeasons.Count, actual.Count);

                for (int i = 0; i < expectedSeasons.Count; i++)
                {
                    Assert.AreEqual(expectedSeasons[i].Year, actual[i].Year);
                    Assert.AreEqual(expectedSeasons[i].Races.Count, actual[i].Races.Count);
                }
            }
        }
    }
}
