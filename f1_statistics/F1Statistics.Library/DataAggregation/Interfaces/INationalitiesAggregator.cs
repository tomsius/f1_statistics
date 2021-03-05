using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface INationalitiesAggregator
    {
        List<NationalityDriversModel> GetDriversNationalities(int from, int to);
        List<NationalityWinsModel> GetNationalitiesRaceWins(int from, int to);
        List<NationalityWinsModel> GetNationalitiesSeasonWins(int from, int to);
    }
}
