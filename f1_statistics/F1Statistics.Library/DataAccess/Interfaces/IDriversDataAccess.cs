using F1Statistics.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IDriversDataAccess
    {
        string GetDriverName(string leadingDriverId);
        List<DriverDataResponse> GetDriversFrom(int year);
    }
}
