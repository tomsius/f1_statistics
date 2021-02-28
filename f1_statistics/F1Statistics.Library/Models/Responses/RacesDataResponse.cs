using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class RacesDataResponse
    {
        public string season { get; set; }
        public string round { get; set; }
        public string url { get; set; }
        public string raceName { get; set; }
        public CircuitDataResponse Circuit { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public List<ResultsDataResponse> Results { get; set; }
        public List<QualifyingResultsDataResponse> QualifyingResults { get; set; }
        public List<LapsDataResponse> Laps { get; set; }
    }
}
