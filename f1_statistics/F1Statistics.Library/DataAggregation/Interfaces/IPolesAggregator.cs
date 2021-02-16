using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IPolesAggregator
    {
        List<PolesModel> GetPoleSittersDrivers(int from, int to);
        List<PolesModel> GetPoleSittersConstructors(int from, int to);
        List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersDrivers(int from, int to);
        List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersConstructors(int from, int to);
        List<WinsFromPoleModel> GetWinCountFromPole(int from, int to);
    }
}