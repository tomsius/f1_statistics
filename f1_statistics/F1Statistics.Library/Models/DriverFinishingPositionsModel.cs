using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class DriverFinishingPositionsModel
    {
        public string Name { get; set; }
        public List<FinishingPositionModel> FinishingPositions { get; set; }
    }
}
