using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Models
{
    public class SamePodiumsModel
    {
        public List<string> PodiumFinishers { get; set; }
        public List<string> Circuits { get; set; }
        public int SamePodiumCount { get; set; }
    }
}
