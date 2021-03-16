using F1Statistics.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IStandingsDataAccess
    {
        List<DriverStandingsDataResponse> GetDriverStandingsFrom(int year);
        List<ConstructorStandingsDataResponse> GetConstructorStandingsFrom(int year);
        List<DriverStandingsDataResponse> GetDriverStandingsFromRace(int year, int round);
        List<ConstructorStandingsDataResponse> GetConstructorStandingsFromRace(int year, int round);
    }
}
