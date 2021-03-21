using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface ISeasonsService
    {
        List<SeasonModel> AggregateSeasonRaces(OptionsModel options);
    }
}
