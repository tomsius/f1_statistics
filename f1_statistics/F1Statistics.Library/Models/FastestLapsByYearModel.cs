using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class FastestLapsByYearModel
    {
        public int Year { get; set; }
        public List<FastestLapInformationModel> FastestLapInformation { get; set; }
        public int YearFastestLapCount 
        { 
            get
            {
                return FastestLapInformation.Count;
            }
        }
    }
}
