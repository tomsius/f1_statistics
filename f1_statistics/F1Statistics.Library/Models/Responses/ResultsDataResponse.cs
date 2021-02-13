using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class ResultsDataResponse
    {
        public string number { get; set; }
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public DriverDataResponse Driver { get; set; }
        public ConstructorDataResponse Constructor { get; set; }
        public string grid { get; set; }
        public string laps { get; set; }
        public string status { get; set; }
        public TimeDataResponse Time { get; set; }
        public FastestLapDataResponse FastestLap { get; set; }
    }
}
