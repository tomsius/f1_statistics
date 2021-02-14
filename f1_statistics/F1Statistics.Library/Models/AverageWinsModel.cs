using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class AverageWinsModel
    {
        public string Name { get; set; }
        public int WinCount { get; set; }
        public int ParticipationCount { get; set; }
        public double AverageWins 
        {
            get
            {
                return Math.Round((double)WinCount / ParticipationCount * 100, 2);
            }
        }
    }
}
