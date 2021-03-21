using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface ISeasonsAggregator
    {
        List<SeasonModel> GetSeasonRaces(int from, int to);
    }
}
