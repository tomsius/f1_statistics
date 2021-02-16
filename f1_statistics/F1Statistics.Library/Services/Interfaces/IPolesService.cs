using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IPolesService
    {
        List<PolesModel> AggregatePoleSittersDrivers(OptionsModel options);
        List<PolesModel> AggregatePoleSittersConstructors(OptionsModel options);
        List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonDriverPoleSitters(OptionsModel options);
        List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonConstructorPoleSitters(OptionsModel options);
        List<WinsFromPoleModel> AggregateWinnersFromPole(OptionsModel options);
    }
}