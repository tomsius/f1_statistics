using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class DriverStandingsDataResponse
    {
        public string points { get; set; }
        public DriverDataResponse Driver { get; set; }
    }
}
