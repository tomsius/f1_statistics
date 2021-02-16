using F1Statistics.Library.Models;
using System.Collections.Generic;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IPolesService
    {
        List<PolesModel> AggregatePoleSittersDrivers(OptionsModel options);
        List<PolesModel> AggregatePoleSittersConstructors(OptionsModel options);
        List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonPoleSittersDrivers(OptionsModel options);
        List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonPoleSittersConstructors(OptionsModel options);
        List<WinnersFromPoleModel> AggregateWinnersFromPole(OptionsModel options);
    }
}