using F1Statistics.Library.Models;
using F1Statistics.Library.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Tests.Validators
{
    [TestClass]
    public class OptionsValidatorTests
    {
        private OptionsValidator _validator;
        private Mock<IConfiguration> _configuration;

        [TestInitialize]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();

            //var defaultYearFromSection = new Mock<IConfigurationSection>();
            //defaultYearFromSection.Setup((configuration) => configuration.Value).Returns(2000);
            //var defaultYearToSection = new Mock<IConfigurationSection>();
            //defaultYearFromSection.Setup((configuration) => configuration.Value).Returns(2010);
            //var defaultSeasonSection = new Mock<IConfigurationSection>();
            //defaultYearFromSection.Setup((configuration) => configuration.Value).Returns(2020);

            //_configuration.Setup((configuration) => configuration.GetSection("DefaultYearFrom")).Returns(defaultYearFromSection.Object);
            //_configuration.Setup((configuration) => configuration.GetSection("DefaultYearTo")).Returns(defaultYearToSection.Object);
            //_configuration.Setup((configuration) => configuration.GetSection("DefaultSeason")).Returns(defaultSeasonSection.Object);

            _configuration.Setup((configuration) => configuration.GetSection("DefaultYearFrom").Value).Returns("2000");
            _configuration.Setup((configuration) => configuration.GetSection("DefaultYearTo").Value).Returns("2010");
            _configuration.Setup((configuration) => configuration.GetSection("DefaultSeason").Value).Returns("2020");

            _validator = new OptionsValidator(_configuration.Object);
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnOptionsModel_IfYearsFromAndYearsToAreCorrectlySet()
        {
            // Arrange
            var optionsModel = new OptionsModel { YearFrom = 2000, YearTo = 2010 };
            var expectedYearFrom = 2000;
            var expectedYearTo = 2010;
            var expectedSeason = 0;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnOptionsModel_IfSeasonCorrectlySet()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 2000 };
            var expectedYearFrom = 0;
            var expectedYearTo = 0;
            var expectedSeason = 2000;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnCorrectedOptionsModel_IfYearsFromIsBiggerThanYearsTo()
        {
            // Arrange
            var optionsModel = new OptionsModel { YearFrom = 2010, YearTo = 2000 };
            var expectedYearFrom = 2000;
            var expectedYearTo = 2010;
            var expectedSeason = 0;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnCorrectedOptionsModel_IfOnlyYearsFromIsGiven()
        {
            // Arrange
            var optionsModel = new OptionsModel { YearFrom = 2000 };
            var expectedYearFrom = 2000;
            var expectedYearTo = DateTime.Now.Year;
            var expectedSeason = 0;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnCorrectedOptionsModel_IfOnlyYearsToIsGiven()
        {
            // Arrange
            var optionsModel = new OptionsModel { YearTo = 2010 };
            var expectedYearFrom = 2010;
            var expectedYearTo = DateTime.Now.Year;
            var expectedSeason = 0;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnCorrectedOptionsModel_IfInvalidYearsAreGiven()
        {
            // Arrange
            var optionsModel = new OptionsModel { YearFrom = 1900, YearTo = 2030 };
            var expectedYearFrom = 2000;
            var expectedYearTo = 2010;
            var expectedSeason = 0;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Once());
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Once());
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnCorrectedOptionsModel_IfSeasonIsSmallerThanSupposedToBe()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 1900 };
            var expectedYearFrom = 0;
            var expectedYearTo = 0;
            var expectedSeason = 2020;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Once());
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnCorrectedOptionsModel_IfSeasonIsBiggerThanSupposedToBe()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 2030 };
            var expectedYearFrom = 0;
            var expectedYearTo = 0;
            var expectedSeason = 2020;

            // Act
            _validator.ValidateOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Once());
        }
    }
}
