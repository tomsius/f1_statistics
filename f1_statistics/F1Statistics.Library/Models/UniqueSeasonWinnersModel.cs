using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class UniqueSeasonWinnersModel
    {
        public int Season { get; set; }
        public int RaceCount { get; set; }
        public List<string> Winners { get; set; }
        public int UniqueWinnersCount 
        {
            get
            {
                return Winners.Count;
            }
        }
    }
}
