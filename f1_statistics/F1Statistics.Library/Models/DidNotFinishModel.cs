using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class DidNotFinishModel
    {
        public string Name { get; set; }
        public List<DidNotFinishByYearModel> DidNotFinishByYear { get; set; }
        public int TotalDidNotFinishCount
        {
            get
            {
                return DidNotFinishByYear.Sum(model => model.YearDidNotFinishCount);
            }
        }
    }
}
