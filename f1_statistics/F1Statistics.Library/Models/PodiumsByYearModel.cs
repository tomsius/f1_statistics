using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class PodiumsByYearModel
    {
        public int Year { get; set; }
        public List<PodiumInformationModel> PodiumInformation { get; set; }
        public int YearPodiumCount 
        {
            get
            {
                return PodiumInformation.Count;
            }
        }
    }
}
