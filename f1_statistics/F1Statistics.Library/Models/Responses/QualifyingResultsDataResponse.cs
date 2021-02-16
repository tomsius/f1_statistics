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
    }
}
