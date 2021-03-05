using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface INationalitiesService
    {
        List<NationalityDriversModel> AggregateDriversNationalities(OptionsModel options);
        List<NationalityWinsModel> AggregateNationalitiesRaceWins(OptionsModel options);
        List<NationalityWinsModel> AggregateNationalitiesSeasonWins(OptionsModel options);
    }
}
