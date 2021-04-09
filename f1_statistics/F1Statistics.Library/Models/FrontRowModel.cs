using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class FrontRowModel
    {
        public string Name { get; set; }
        public List<FrontRowInformationModel> FrontRowInformation { get; set; }
        public int TotalFrontRowCount 
        {
            get
            {
                return FrontRowInformation.Sum(model => model.CircuitFrontRowCount);
            }
        }
    }
}
