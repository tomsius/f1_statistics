using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IWinsAggregator
    {
        List<WinsModel> GetDriversWins(int from, int to);
        List<WinsModel> GetConstructorsWins(int yearFrom, int yearTo);
    }
}