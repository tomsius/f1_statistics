using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class LeadingLapsByYearModel
    {
        public int Year { get; set; }
        public List<LeadingLapInformationModel> LeadingLapInformation { get; set; }
        public int YearLeadingLapCount
        {
            get
            {
                return LeadingLapInformation.Count;
            }
        }
    }
}
