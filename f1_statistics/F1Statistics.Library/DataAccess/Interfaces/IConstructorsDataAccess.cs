using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IConstructorsDataAccess
    {
        string GetDriverConstructor(int year, int round, string leadingDriverId);
    }
}
