using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class UniqueSeasonFastestLapModel
    {
        public int Season { get; set; }
        public int RacesCount { get; set; }
        public List<string> FastestLapAchievers { get; set; }
        public int UniqueFastestLapsCount
        {
            get
            {
                return FastestLapAchievers.Count;
            }
        }
    }
}
