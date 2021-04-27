using F1Statistics.Library.Models;
using F1Statistics.Library.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

            _configuration.Setup((configuration) => configuration.GetSection("DefaultYearFrom").Value).Returns("2000");
            _configuration.Setup((configuration) => configuration.GetSection("DefaultYearTo").Value).Returns("2010");
            _configuration.Setup((configuration) => configuration.GetSection("DefaultSeason").Value).Returns("2020");

            Mock<ILogger<OptionsValidator>> logger = new Mock<ILogger<OptionsValidator>>();
            logger.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Callback(new InvocationAction(invocation =>
                {
                    var logLevel = (LogLevel)invocation.Arguments[0];
                    var eventId = (EventId)invocation.Arguments[1];
                    var state = invocation.Arguments[2];
                    var exception = (Exception)invocation.Arguments[3];
                    var formatter = invocation.Arguments[4];

                    var invokeMethod = formatter.GetType().GetMethod("Invoke");
                    var logMessage = (string)invokeMethod?.Invoke(formatter, new[] { state, exception });
                }));

            _validator = new OptionsValidator(_configuration.Object, logger.Object);
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
            _validator.NormalizeOptionsModel(optionsModel);

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
            _validator.NormalizeOptionsModel(optionsModel);

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
            _validator.NormalizeOptionsModel(optionsModel);

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
            _validator.NormalizeOptionsModel(optionsModel);

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
            _validator.NormalizeOptionsModel(optionsModel);

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
            _validator.NormalizeOptionsModel(optionsModel);

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
            _validator.NormalizeOptionsModel(optionsModel);

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
            _validator.NormalizeOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Once());
        }

        [TestMethod]
        public void ValidateOptionsModel_ReturnCorrectedOptionsModel_IfSeasonAndYearsAreGiven()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 2030, YearFrom = 2000, YearTo = 2010 };
            var expectedYearFrom = 2000;
            var expectedYearTo = 2010;
            var expectedSeason = 0;

            // Act
            _validator.NormalizeOptionsModel(optionsModel);

            // Assert
            Assert.AreEqual(expectedYearFrom, optionsModel.YearFrom);
            Assert.AreEqual(expectedYearTo, optionsModel.YearTo);
            Assert.AreEqual(expectedSeason, optionsModel.Season);
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
            _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
        }

        [TestMethod]
        public void ValidateOptionsModel_ThrowsException_IfDefaultYearFromIsNotAnInteger()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 0, YearTo = 0 };
            var expectedMessage = "DefaultYearFrom setting has to be an integer value.";
            _configuration.Setup((configuration) => configuration.GetSection("DefaultYearFrom").Value).Returns("abc");

            try
            {
                // Act
                _validator.NormalizeOptionsModel(optionsModel);

                // Assert
                Assert.Fail("Exception needs to be raised.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
                _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(1));
                _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
                _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
            }
        }

        [TestMethod]
        public void ValidateOptionsModel_ThrowsException_IfDefaultYearToIsNotAnInteger()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 2000, YearTo = 20000 };
            var expectedMessage = "DefaultYearTo setting has to be an integer value.";
            _configuration.Setup((configuration) => configuration.GetSection("DefaultYearTo").Value).Returns("abc");

            try
            {
                // Act
                _validator.NormalizeOptionsModel(optionsModel);

                // Assert
                Assert.Fail("Exception needs to be raised.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
                _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
                _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(1));
                _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(0));
            }
        }

        [TestMethod]
        public void ValidateOptionsModel_ThrowsException_IfDefaultSeasonIsNotAnInteger()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 20000, YearFrom = 0, YearTo = 0 };
            var expectedMessage = "DefaultSeason setting has to be an integer value.";
            _configuration.Setup((configuration) => configuration.GetSection("DefaultSeason").Value).Returns("abc");

            try
            {
                // Act
                _validator.NormalizeOptionsModel(optionsModel);

                // Assert
                Assert.Fail("Exception needs to be raised.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
                _configuration.Verify((configuration) => configuration.GetSection("DefaultYearFrom").Value, Times.Exactly(0));
                _configuration.Verify((configuration) => configuration.GetSection("DefaultYearTo").Value, Times.Exactly(0));
                _configuration.Verify((configuration) => configuration.GetSection("DefaultSeason").Value, Times.Exactly(1));
            }
        }

        [TestMethod]
        public void IsLapTimesSeasonValid_ReturnTrue_IfSeasonIsMoreThan1996()
        {
            // Arrange
            var season = 1997;
            var expected = true;

            // Act
            var actual = _validator.IsLapTimesSeasonValid(season);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsLapTimesSeasonValid_ReturnTrue_IfSeasonIsEqualTo1996()
        {
            // Arrange
            var season = 1996;
            var expected = true;

            // Act
            var actual = _validator.IsLapTimesSeasonValid(season);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsLapTimesSeasonValid_ReturnFalse_IfSeasonIsLessThan1996()
        {
            // Arrange
            var season = 1995;
            var expected = false;

            // Act
            var actual = _validator.IsLapTimesSeasonValid(season);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnTrue_IfSeasonIsMoreThan2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 2005, YearFrom = 0, YearTo = 0 };
            var expected = true;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnTrue_IfSeasonIsEqualTo2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 2004, YearFrom = 0, YearTo = 0 };
            var expected = true;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnFalse_IfSeasonIsLessThan2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 2003, YearFrom = 0, YearTo = 0 };
            var expected = false;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnTrue_IfYearFromIsMoreThan2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 2005, YearTo = 0 };
            var expected = true;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnTrue_IfYearFromIsEqualTo2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 2004, YearTo = 0 };
            var expected = true;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnFalse_IfYearFromIsLessThan2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 2003, YearTo = 0 };
            var expected = false;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnTrue_IfYearToIsMoreThan2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 0, YearTo = 2005 };
            var expected = true;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnTrue_IfYearToIsEqualTo2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 0, YearTo = 2004 };
            var expected = true;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AreFastestLapYearsValid_ReturnFalse_IfYearToIsLessThan2004()
        {
            // Arrange
            var optionsModel = new OptionsModel { Season = 0, YearFrom = 0, YearTo = 2003 };
            var expected = false;

            // Act
            var actual = _validator.AreFastestLapYearsValid(optionsModel);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
