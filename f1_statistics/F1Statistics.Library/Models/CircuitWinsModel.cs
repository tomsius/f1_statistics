using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class CircuitWinsModel
    {
        public string Name { get; set; }
        public List<WinsModel> Winners { get; set; }
    }
}
