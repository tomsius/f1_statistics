using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class SeasonStandingsChangesModel
    {
        public int Year { get; set; }
        public List<StandingModel> Standings { get; set; }
    }
}
