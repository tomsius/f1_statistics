using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IPolesAggregator
    {
        List<PolesModel> GetDriversPoles(int from, int to);
        List<PolesModel> GetConstructorsPoles(int from, int to);
        List<UniqueSeasonPoleCountModel> GetUniqueDriverPoleSitters(int from, int to);
        List<UniqueSeasonPoleCountModel> GetUniqueConstructorPoleSitters(int from, int to);
        List<WinsFromPoleModel> GetWinCountFromPole(int from, int to);
    }
}