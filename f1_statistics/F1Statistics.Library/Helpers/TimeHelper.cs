using F1Statistics.Library.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Helpers
{
    public class TimeHelper : ITimeHelper
    {
        public double ConvertTimeToSeconds(string time)
        {
            int minutes = 0;
            double seconds = 0;

            if (time.Contains(':'))
            {
                var splitMinutesFromRest = time.Split(':');

                int.TryParse(splitMinutesFromRest[0], out minutes);
                double.TryParse(splitMinutesFromRest[1], out seconds);
            }
            else
            {
                double.TryParse(time, out seconds);
            }

            var timeInSeconds = Math.Round(minutes * 60 + seconds, 3);

            return timeInSeconds;
        }

        public double GetTimeDifference(string firstTime, string secondTime)
        {
            var firstTimeInSeconds = ConvertTimeToSeconds(firstTime);
            var secondTimeInSeconds = ConvertTimeToSeconds(secondTime);

            if (firstTimeInSeconds == 0 || secondTimeInSeconds == 0)
            {
                return 0;
            }
            else
            {
                var gap = Math.Round(secondTimeInSeconds - firstTimeInSeconds, 3);

                return gap;
            }
        }

        public double ConvertGapFromStringToDouble(string time)
        {
            if (time[time.Length - 1] == 's')
            {
                time = time.Substring(0, time.Length - 1);
            }

            var isValid = double.TryParse(time, out double gap);

            if (isValid)
            {
                return gap;
            }
            else
            {
                return 0;
            }
        }
    }
}
