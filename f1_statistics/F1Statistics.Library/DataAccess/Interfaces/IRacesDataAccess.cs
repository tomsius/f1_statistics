using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess.Interfaces
{
    public interface IRacesDataAccess
    {
        int GetRacesCountFrom(int year);
    }
}
