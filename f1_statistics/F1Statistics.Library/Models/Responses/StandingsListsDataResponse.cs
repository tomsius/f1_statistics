using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class StandingsListsDataResponse
    {
        public string season { get; set; }
        public string round { get; set; }
        public List<DriverStandingsDataResponse> DriverStandings { get; set; }
        public List<ConstructorStandingsDataResponse> ConstructorStandings { get; set; }
    }
}
