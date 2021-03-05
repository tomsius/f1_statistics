using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class NationalityWinsModel
    {
        public string Nationality { get; set; }
        public List<string> Winners { get; set; }
        public int WinnersCount
        {
            get 
            { 
                return Winners.Count; 
            }
        }
    }
}
