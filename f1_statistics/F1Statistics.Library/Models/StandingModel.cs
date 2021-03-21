using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class StandingModel
    {
        public string Name { get; set; }
        public List<RoundModel> Rounds { get; set; }
    }
}
