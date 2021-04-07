using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class QualifyingResultsDataResponse
    {
        public string position { get; set; }
        public DriverDataResponse Driver { get; set; }
        public ConstructorDataResponse Constructor { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
    }
}
