using F1Statistics.Library.DataAccess;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class ResultsDataAccessTests
    {
        private ResultsDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new ResultsDataAccess();
        }

        [TestMethod]
        public void GetResultsFrom_ReturnResultsList_IfYearIsValid()
        {
            // Arrange
            var year = 2020;
            var expected = new List<RacesDataResponse>
            {
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    Results = new List<ResultsDataResponse>
                    {
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse(),
                        new ResultsDataResponse()
                    }
                }
            };

            // Act
            var actual = _dataAccess.GetResultsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Results.Count, actual[i].Results.Count);
            }
        }

        [TestMethod]
        public void GetResultsFrom_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var expected = new List<RacesDataResponse>();

            // Act
            var actual = _dataAccess.GetResultsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}
