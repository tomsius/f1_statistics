using F1Statistics.Library.Helpers;
using F1Statistics.Library.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.Helpers
{
    [TestClass]
    public class NameHelperTests
    {
        private NameHelper _helper;

        [TestInitialize]
        public void Setup()
        {
            _helper = new NameHelper();
        }

        [TestMethod]
        public void GetDriverName_ReturnDriversFullName_IfDriverHasFirstAndLastName()
        {
            // Arrange
            var driver = new DriverDataResponse { givenName = "Lewis", familyName = "Hamilton" };
            var expectedDriverName = "Lewis Hamilton";

            // Act
            var actual = _helper.GetDriverName(driver);

            // Assert
            Assert.AreEqual(expectedDriverName, actual);
        }

        [TestMethod]
        public void GetDriverName_ReturnDriversFirstName_IfDriverOnlyHasFirstName()
        {
            // Arrange
            var driver = new DriverDataResponse { givenName = "Lewis" };
            var expectedDriverName = "Lewis";

            // Act
            var actual = _helper.GetDriverName(driver);

            // Assert
            Assert.AreEqual(expectedDriverName, actual);
        }

        [TestMethod]
        public void GetDriverName_ReturnDriversLastName_IfDriverOnlyHasLastName()
        {
            // Arrange
            var driver = new DriverDataResponse { familyName = "Hamilton" };
            var expectedDriverName = "Hamilton";

            // Act
            var actual = _helper.GetDriverName(driver);

            // Assert
            Assert.AreEqual(expectedDriverName, actual);
        }

        [TestMethod]
        public void GetConstructorName_ReturnConstructorsName_IfConstructorHasName()
        {
            // Arrange
            var constructor = new ConstructorDataResponse { name = "Mercedes" };
            var expectedConstructorName = "Mercedes";

            // Act
            var actual = _helper.GetConstructorName(constructor);

            // Assert
            Assert.AreEqual(expectedConstructorName, actual);
        }

        [TestMethod]
        public void GetConstructorName_ReturnEmptyString_IfConstructorDoesNotHaveName()
        {
            // Arrange
            var constructor = new ConstructorDataResponse();
            var expectedConstructorName = "";

            // Act
            var actual = _helper.GetConstructorName(constructor);

            // Assert
            Assert.AreEqual(expectedConstructorName, actual);
        }
    }
}
