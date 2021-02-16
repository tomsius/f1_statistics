using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class WinsFromPoleModel
    {
        public int Season { get; set; }
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
