using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class WinsByGridPositionModel
    {
        public int GridPosition { get; set; }
        public List<string> Winners { get; set; }
        public int WinCount 
        {
            get 
            {
                return Winners.Count;
            }
        }
    }
}
