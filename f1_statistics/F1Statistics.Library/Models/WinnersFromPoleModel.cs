using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class WinnersFromPoleModel
    {
        public int Season { get; set; }
        public int RacesCount { get; set; }
        public List<string> WinnersFromPole { get; set; }
        public int WinsFromPoleCount 
        {
            get
            {
                return WinnersFromPole.Count;
            }
        }
    }
}
