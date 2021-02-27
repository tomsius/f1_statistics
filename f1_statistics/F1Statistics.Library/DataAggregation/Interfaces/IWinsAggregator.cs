using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IWinsAggregator
    {
        List<WinsModel> GetDriversWins(int from, int to);
        List<WinsModel> GetConstructorsWins(int from, int to);
        List<AverageWinsModel> GetDriversWinAverage(int from, int to);
        List<AverageWinsModel> GetConstructorsWinAverage(int from, int to);
        List<CircuitWinsModel> GetCircuitWinners(int from, int to);
        List<UniqueSeasonWinnersModel> GetUniqueSeasonDriverWinners(int from, int to);
        List<UniqueSeasonWinnersModel> GetUniqueSeasonConstructorWinners(int from, int to);
    }
}