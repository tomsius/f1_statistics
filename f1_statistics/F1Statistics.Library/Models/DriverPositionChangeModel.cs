using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class DriverPositionChangeModel
    {
        public string Name { get; set; }
        public int ChampionshipPosition { get; set; }
        public List<DriverPositionChangeInformationModel> DriverPositionChangeInformation { get; set; }
        public int TotalPositionChange 
        {
            get
            {
                return DriverPositionChangeInformation.Sum(model => model.RacePositionChange);
            }
        }
    }
}
