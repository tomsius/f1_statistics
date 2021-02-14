using F1Statistics.Library.Models.Responses;
using System.Collections.Generic;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IResultsDataAccess
    {
        List<RacesDataResponse> GetRacesFrom(int year);
    }
}