using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class FinishingPositionModel
    {
        public int FinishingPosition { get; set; }
        public List<FinishingPositionInformationModel> FinishingPositionInformation { get; set; }
        public int Count 
        {
            get
            {
                return FinishingPositionInformation.Count;
            }
        }
    }
}
