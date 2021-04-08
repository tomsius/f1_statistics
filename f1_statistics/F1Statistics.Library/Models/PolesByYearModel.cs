using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class PolesByYearModel
    {
        public int Year { get; set; }
        public List<PoleInformationModel> PoleInformation { get; set; }
        public int YearPoleCount 
        {
            get 
            {
                return PoleInformation.Count;
            }
        }
    }
}
