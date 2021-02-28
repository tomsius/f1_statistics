using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class ConstructorTableDataResponse
    {
        public string season { get; set; }
        public string round { get; set; }
        public string driverId { get; set; }
        public List<ConstructorDataResponse> Constructors { get; set; }
    }
}
