using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class FastestLapDataResponse
    {
        public string rank { get; set; }
        public string lap { get; set; }
        public TimeDataResponse Time { get; set; }
        public AverageSpeedDataResponse AverageSpeed { get; set; }
    }
}
