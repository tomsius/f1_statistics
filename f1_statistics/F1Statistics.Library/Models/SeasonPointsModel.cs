using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class SeasonPointsModel
    {
        public int Season { get; set; }
        public List<PointsModel> ScoredPoints { get; set; }
        public int TotalPoints
        {
            get 
            {
                return ScoredPoints.Sum(x => x.Points);
            }
        }

    }
}
