using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class RacePositionChangesModel
    {
        public int LapNumber { get; set; }
        public List<DriverPositionModel> DriversPositions { get; set; }
    }
}
