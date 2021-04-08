using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class WinsByYearModel
    {
        public int Year { get; set; }
        public List<WinInformationModel> WinInformation { get; set; }
        public int YearWinCount
        {
            get
            {
                return WinInformation.Count;
            }
        }
    }
}
