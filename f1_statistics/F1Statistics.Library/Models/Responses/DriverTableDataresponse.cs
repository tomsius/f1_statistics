using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class DriverTableDataresponse
    {
        public string driverId { get; set; }
        public List<DriverDataResponse> Drivers { get; set; }
    }
}
