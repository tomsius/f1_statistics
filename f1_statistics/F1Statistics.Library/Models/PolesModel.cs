using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class PolesModel
    {
        public string Name { get; set; }
        public List<PolesByYearModel> PolesByYear { get; set; }
        public List<PoleInformationModel> PoleInformation { get; set; }
        public int PoleCount
        {
            get
            {
                return PolesByYear.Sum(model => model.PoleCount);
            }
        }
    }
}
