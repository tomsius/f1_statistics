using F1Statistics.Library.Models.Responses;
using System.Collections.Generic;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IQualifyingsDataAccess
    {
        List<RacesDataResponse> GetQualifyingsFrom(int year);
    }
}