using F1Statistics.Library.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.Tests.Helpers
{
    [TestClass]
    public class TimeHelperTests
    {
        private TimeHelper _helper;

        [TestInitialize]
        public void Setup()
        {
            _helper = new TimeHelper();
        }

        [TestMethod]
        public void ConvertTimeToSeconds_ReturnTimeInSecondsOver60_IfTimeIsValid()
        {
            // Arrange
            var time = "1:2.555";
            var expectedTime = 62.555;

            // Act
            var actual = _helper.ConvertTimeToSeconds(time);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void ConvertTimeToSeconds_ReturnTimeInSecondsBelow60_IfTimeIsValid()
        {
            // Arrange
            var time = "2.555";
            var expectedTime = 2.555;

            // Act
            var actual = _helper.ConvertTimeToSeconds(time);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void ConvertTimeToSeconds_Return0Seconds_IfTimeIsNotValid()
        {
            // Arrange
            var time = "abc";
            var expectedTime = 0;

            // Act
            var actual = _helper.ConvertTimeToSeconds(time);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void GetTimeDifference_ReturnTimeDifferenceInSeconds_IfTimesAreValid()
        {
            // Arrange
            var time1 = "20.5";
            var time2 = "22.5";
            var expectedTime = 2;

            // Act
            var actual = _helper.GetTimeDifference(time1, time2);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void GetTimeDifference_Return0_IfTime1IsNotValid()
        {
            // Arrange
            var time1 = "abc";
            var time2 = "22.5";
            var expectedTime = 0;

            // Act
            var actual = _helper.GetTimeDifference(time1, time2);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void GetTimeDifference_Return0_IfTime2IsNotValid()
        {
            // Arrange
            var time1 = "20.5";
            var time2 = "abc";
            var expectedTime = 0;

            // Act
            var actual = _helper.GetTimeDifference(time1, time2);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void GetTimeDifference_Return0_IfTimesAreNotValid()
        {
            // Arrange
            var time1 = "abc";
            var time2 = "abc";
            var expectedTime = 0;

            // Act
            var actual = _helper.GetTimeDifference(time1, time2);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void ConvertGapFromStringToDouble_ReturnGapInSeconds_IfTimeIsValid()
        {
            // Arrange
            var time = "2.5";
            var expectedTime = 2.5;

            // Act
            var actual = _helper.ConvertGapFromStringToDouble(time);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void ConvertGapFromStringToDouble_ReturnGapInSeconds_IfTimeIsValidWithLetterSAtTheEnd()
        {
            // Arrange
            var time = "2.5s";
            var expectedTime = 2.5;

            // Act
            var actual = _helper.ConvertGapFromStringToDouble(time);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }

        [TestMethod]
        public void ConvertGapFromStringToDouble_Return0_IfTimeIsNotValid()
        {
            // Arrange
            var time = "abc";
            var expectedTime = 0;

            // Act
            var actual = _helper.ConvertGapFromStringToDouble(time);

            // Assert
            Assert.AreEqual(expectedTime, actual);
        }
    }
}
