using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class StandingsTableDataResponse
    {
        public string season { get; set; }
        public List<StandingsListsDataResponse> StandingsLists { get; set; }
    }
}
