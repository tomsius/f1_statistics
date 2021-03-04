using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IMiscAggregator
    {
        List<SeasonRacesModel> GetRaceCountPerSeason(int from, int to);
        List<HatTrickModel> GetHatTricks(int from, int to);
        List<GrandSlamModel> GetGrandSlams(int from, int to);
        List<DidNotFinishModel> GetNonFinishers(int from, int to);
    }
}
