using F1Statistics.Library.DataAccess;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.IntegrationTests.DataAccess
{
    [TestClass]
    public class QualifyingDataAccessTests
    {
        private QualifyingDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new QualifyingDataAccess();
        }

        [TestMethod]
        public void GetQualifyingsFrom_ReturnQualifyingsList_IfYearIsValid()
        {
            // Arrange
            var year = 2020;
            var expected = new List<RacesDataResponse>
            {
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                },
                new RacesDataResponse
                {
                    QualifyingResults = new List<QualifyingResultsDataResponse>
                    {
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse(),
                        new QualifyingResultsDataResponse()
                    }
                }
            };

            // Act
            var actual = _dataAccess.GetQualifyingsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].QualifyingResults.Count, actual[i].QualifyingResults.Count);
            }
        }

        [TestMethod]
        public void GetQualifyingsFrom_ReturnEmptyList_IfYearIsNotValid()
        {
            // Arrange
            var year = 1949;
            var expected = new List<RacesDataResponse>();

            // Act
            var actual = _dataAccess.GetQualifyingsFrom(year);

            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}
