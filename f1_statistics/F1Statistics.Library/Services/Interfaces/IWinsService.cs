using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IWinsService
    {
        List<WinsModel> AggregateDriversWins(OptionsModel options);
        List<WinsModel> AggregateConstructorsWins(OptionsModel options);
        List<AverageWinsModel> AggregateDriversWinPercent(OptionsModel options);
        List<AverageWinsModel> AggregateConstructorsWinPercent(OptionsModel options);
        List<CircuitWinsModel> AggregateCircuitsWinners(OptionsModel options);
        List<UniqueSeasonWinnersModel> AggregateUniqueSeasonDriverWinners(OptionsModel options);
        List<UniqueSeasonWinnersModel> AggregateUniqueSeasonConstructorWinners(OptionsModel options);
        List<WinnersFromPoleModel> AggregateWinnersFromPole(OptionsModel options);
        List<WinsByGridPositionModel> AggregateWinnersByGridPosition(OptionsModel options);
    }
}