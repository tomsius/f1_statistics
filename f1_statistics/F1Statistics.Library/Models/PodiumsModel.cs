using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class PodiumsModel
    {
        public string Name { get; set; }
        public List<PodiumsByYearModel> PodiumsByYear { get; set; }
        public int TotalPodiumCount
        {
            get
            {
                return PodiumsByYear.Sum(model => model.YearPodiumCount);
            }
        }
    }
}
