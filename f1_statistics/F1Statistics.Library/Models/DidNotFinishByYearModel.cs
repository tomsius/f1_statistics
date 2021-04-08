using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class DidNotFinishByYearModel
    {
        public int Year { get; set; }
        public List<DidNotFinishInformationModel> DidNotFinishInformation { get; set; }
        public int YearDidNotFinishCount
        {
            get
            {
                return DidNotFinishInformation.Count;
            }
        }
    }
}
