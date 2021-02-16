using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IPolesService
    {
        List<PolesModel> AggregateDriversPoles(OptionsModel options);
        List<PolesModel> AggregateConstructorsPoles(OptionsModel options);
        List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonDriverPoleSitters(OptionsModel options);
        List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonConstructorPoleSitters(OptionsModel options);
        List<WinsFromPoleModel> AggregateWinnersFromPole(OptionsModel options);
    }
}