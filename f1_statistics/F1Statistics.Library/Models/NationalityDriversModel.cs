using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class NationalityDriversModel
    {
        public string Nationality { get; set; }
        public List<string> Drivers { get; set; }
        public int DriversCount
        {
            get
            {
                return Drivers.Count;
            }
        }
    }
}
