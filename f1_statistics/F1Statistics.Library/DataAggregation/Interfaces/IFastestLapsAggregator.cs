using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IFastestLapsAggregator
    {
        List<FastestLapModel> GetDriversFastestLaps(int from, int to);
        List<FastestLapModel> GetConstructorsFastestLaps(int from, int to);
        List<UniqueSeasonFastestLapModel> GetUniqueDriversFastestLaps(int from, int to);
        List<UniqueSeasonFastestLapModel> GetUniqueConstructorsFastestLaps(int from, int to);
    }
}
