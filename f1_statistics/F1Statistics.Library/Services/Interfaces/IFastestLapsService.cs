using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IFastestLapsService
    {
        List<FastestLapModel> AggregateDriversFastestLaps(OptionsModel options);
        List<FastestLapModel> AggregateConstructorsFastestLaps(OptionsModel options);
        List<UniqueSeasonFastestLapModel> AggregateUniqueDriversFastestLapsPerSeason(OptionsModel options);
        List<UniqueSeasonFastestLapModel> AggregateUniqueConstructorsFastestLapsPerseason(OptionsModel options);
    }
}
