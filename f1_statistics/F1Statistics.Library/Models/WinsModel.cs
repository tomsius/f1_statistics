using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class WinsModel
    {
        public string Name { get; set; }
        public List<WinsByYearModel> WinsByYear { get; set; }
        public int TotalWinCount
        {
            get 
            {
                return WinsByYear.Sum(model => model.YearWinCount); 
            }
        }
    }
}
