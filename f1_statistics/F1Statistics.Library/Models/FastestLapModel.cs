using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class FastestLapModel
    {
        public string Name { get; set; }
        public List<FastestLapsByYearModel> FastestLapsByYear { get; set; }
        public int FastestLapsCount
        {
            get
            {
                return FastestLapsByYear.Sum(model => model.FastestLapCount);
            }
        }
    }
}
