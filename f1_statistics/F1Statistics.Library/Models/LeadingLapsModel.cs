using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class LeadingLapsModel
    {
        public string Name { get; set; }
        public List<LeadingLapsByYearModel> LeadingLapsByYear { get; set; }
        public int LeadingLapCount
        {
            get
            {
                return LeadingLapsByYear.Sum(model => model.LeadingLapCount);
            }
        }
    }
}
