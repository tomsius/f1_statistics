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
        List<DidNotFinishModel> AggregateNonFinishers(OptionsModel options);
        List<SeasonPositionChangesModel> AggregateSeasonPositionChanges(OptionsModel options);
        List<FrontRowModel> AggregateConstructorsFrontRows(OptionsModel options);
        List<DriverFinishingPositionsModel> AggregateDriversFinishingPositions(OptionsModel options);
        List<SeasonStandingsChangesModel> AggregateDriversStandingsChanges(OptionsModel options);
        List<SeasonStandingsChangesModel> AggregateConstructorsStandingsChanges(OptionsModel options);
        List<RacePositionChangesModel> AggregateDriversPositionChangesDuringRace(int season, int race);
    }
}
