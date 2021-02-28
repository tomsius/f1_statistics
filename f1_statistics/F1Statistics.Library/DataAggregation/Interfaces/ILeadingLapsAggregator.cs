using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface ILeadingLapsAggregator
    {
        List<LeadingLapsModel> GetDriversLeadingLapsCount(int from, int to);
        List<LeadingLapsModel> GetConstructorsLeadingLapsCount(int from, int to);
    }
}
