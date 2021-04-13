using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.Services
{
    [TestClass]
    public class SeasonsServiceTests
    {
        private SeasonsService _service;
        private Mock<IOptionsValidator> _validator;
        private Mock<ISeasonsAggregator> _aggregator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new Mock<IOptionsValidator>();
            _aggregator = new Mock<ISeasonsAggregator>();

            _validator.Setup((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>())).Verifiable();

            _service = new SeasonsService(_validator.Object, _aggregator.Object);
        }

        private List<SeasonModel> GenerateSeasons()
        {
            var seasons = new List<SeasonModel>
            {
                new SeasonModel
                {
                    Year = 1,
                    Races = new List<RaceModel>
                    {
                        new RaceModel
                        {
                            Round = 1,
                            RaceName = "FirstRace"
                        },
                        new RaceModel
                        {
                            Round = 2,
                            RaceName = "SecondRace"
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
                            Round = 3,
                            RaceName = "FourthRace"
                        },
                        new RaceModel
                        {
                            Round = 5,
                            RaceName = "FifthRace"
                        }
                    }
                }
            };

            return seasons;
        }

        [TestMethod]
        public void AggregateSeasonRaces_ReturnSortedAggregatedSeasonsList_IfThereAreAnySeasons()
        {
            // Arrange
            var options = new OptionsModel { YearFrom = 2000, YearTo = 2001 };
            var expectedSeasons = GenerateSeasons();
            expectedSeasons.Sort((x, y) => x.Year.CompareTo(y.Year));
            expectedSeasons.ForEach(season => season.Races.Sort((x, y) => x.Round.CompareTo(y.Round)));
            _aggregator.Setup((aggregator) => aggregator.GetSeasonRaces(It.IsAny<int>(), It.IsAny<int>())).Returns(GenerateSeasons());

            // Act
            var actual = _service.AggregateSeasonRaces(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
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

        [TestMethod]
        public void AggregateSeasonRaces_ReturnEmptyList_IfThereAreNoSeasons()
        {
            // Arrange
            var options = new OptionsModel { Season = 2000 };
            var expectedSeasons = new List<SeasonModel>();
            _aggregator.Setup((aggregator) => aggregator.GetSeasonRaces(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedSeasons);

            // Act
            var actual = _service.AggregateSeasonRaces(options);

            // Assert
            _validator.Verify((validator) => validator.NormalizeOptionsModel(It.IsAny<OptionsModel>()), Times.Once());
            Assert.AreEqual(expectedSeasons.Count, actual.Count);
        }
    }
}
