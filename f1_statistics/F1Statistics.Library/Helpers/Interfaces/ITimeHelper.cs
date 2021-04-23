using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Helpers.Interfaces
{
    public interface ITimeHelper
    {
        double ConvertGapFromStringToDouble(string time);
        double ConvertTimeToSeconds(string time);
        double GetTimeDifference(string firstTime, string secondTime);
    }
}
