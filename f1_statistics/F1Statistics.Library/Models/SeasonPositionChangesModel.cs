using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class SeasonPositionChangesModel
    {
        public int Year { get; set; }
        public List<DriverPositionChangeModel> PositionChanges { get; set; }
    }
}
