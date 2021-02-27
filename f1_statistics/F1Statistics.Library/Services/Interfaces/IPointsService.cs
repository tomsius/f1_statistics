using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IPointsService
    {
        List<SeasonPointsModel> AggregateDriversPointsPerSeason(OptionsModel options);
        List<SeasonPointsModel> AggregateConstructorsPointsPerSeason(OptionsModel options);
        List<SeasonWinnersPointsModel> AggregateDriversWinnersPointsPerSeason(OptionsModel options);
        List<SeasonWinnersPointsModel> AggregateConstructorsWinnersPointsPerSeason(OptionsModel options);
    }
}
