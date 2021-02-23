using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class ConstructorStandingsDataResponse
    {
        public string points { get; set; }
        public ConstructorDataResponse Constructor { get; set; }
    }
}
