using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IMiscService
    {
        List<SeasonRacesModel> AggregateRaceCountPerSeason(OptionsModel options);
        List<HatTrickModel> AggregateHatTricks(OptionsModel options);
        List<GrandSlamModel> AggregateGrandSlams(OptionsModel options);
    }
}
