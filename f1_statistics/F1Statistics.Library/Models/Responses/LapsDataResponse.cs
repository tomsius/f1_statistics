using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models.Responses
{
    public class LapsDataResponse
    {
        public string number { get; set; }
        public List<TimingsDataResponse> Timings { get; set; }
    }
}
