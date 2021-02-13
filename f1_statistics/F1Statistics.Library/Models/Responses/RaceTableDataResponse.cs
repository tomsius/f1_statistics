using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class RaceTableDataResponse
    {
        public string season { get; set; }
        public string round { get; set; }
        public List<RacesDataResponse> Races { get; set; }
    }
}
