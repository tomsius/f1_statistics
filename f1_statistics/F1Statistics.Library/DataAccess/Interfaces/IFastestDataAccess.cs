using F1Statistics.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IFastestDataAccess
    {
        List<RacesDataResponse> GetFastestDriversFrom(int year);
    }
}
