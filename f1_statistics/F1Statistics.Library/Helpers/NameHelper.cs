using F1Statistics.Library.Helpers.Interfaces;
using F1Statistics.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Helpers
{
    public class NameHelper : INameHelper
    {
        public string GetDriverName(DriverDataResponse driver)
        {
            var driverName = $"{driver.givenName} {driver.familyName}";
            var trimmedDriverName = driverName.Trim();

            return trimmedDriverName;
        }

        public string GetConstructorName(ConstructorDataResponse constructor)
        {
            var constructorName = $"{constructor.name}";
            var trimmedConstructorName = constructorName.Trim();

            return trimmedConstructorName;
        }
    }
}
