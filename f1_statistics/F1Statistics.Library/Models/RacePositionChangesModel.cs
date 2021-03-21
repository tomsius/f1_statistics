using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class RacePositionChangesModel
    {
        public string Name { get; set; }
        public List<LapPositionModel> Laps { get; set; }
    }
}
