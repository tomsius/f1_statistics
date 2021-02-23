using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IPointsAggregator
    {
        List<SeasonPointsModel> GetDriversPointsPerSeason(int from, int to);
        List<SeasonPointsModel> GetConstructorsPointsPerSeason(int from, int to);
    }
}
