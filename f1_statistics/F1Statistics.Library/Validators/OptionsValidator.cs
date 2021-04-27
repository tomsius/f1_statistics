using F1Statistics.Library.Models;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Validators
{
    public class OptionsValidator : IOptionsValidator
    {
        private const int FIRST_SEASON = 1950;
        private const int MINIMUM_FASTEST_LAPS_DATA_YEAR = 2004;
        private const int MINIMUM_LAP_TIMES_DATA_YEAR = 1996;

        private IConfiguration _configuration;
        private ILogger _logger;

        public OptionsValidator(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void NormalizeOptionsModel(OptionsModel options)
        {
            if (AreBothYearsGiven(options) && IsSeasonGiven(options))
            {
                options.Season = 0;
            }

            if (AreYearsGiven(options))
            {
                CheckYears(options);
            }
            else
            {
                CheckSeason(options);
            }
        }

        private bool AreBothYearsGiven(OptionsModel options)
        {
            return IsYearValid(options.YearFrom) && IsYearValid(options.YearTo);
        }

        private bool IsYearValid(int year)
        {
            return year != 0;
        }

        private bool IsSeasonGiven(OptionsModel options)
        {
            return IsYearValid(options.Season);
        }

        private bool AreYearsGiven(OptionsModel options)
        {
            return !IsYearValid(options.Season);
        }

        private void CheckYears(OptionsModel options)
        {
            if (!AreBothYearsGiven(options))
            {
                FillMissingYear(options);
            }

            SwapYearsIfNeeded(options);
            SetDefaultYearsIfNeeded(options);
        }

        private void FillMissingYear(OptionsModel options)
        {
            if (IsYearMissing(options.YearFrom))
            {
                options.YearFrom = GetThisYear();
            }
            else
            {
                options.YearTo = GetThisYear();
            }
        }

        private bool IsYearMissing(int year)
        {
            return !IsYearValid(year);
        }

        private int GetThisYear()
        {
            return DateTime.Now.Year;
        }

        private void SwapYearsIfNeeded(OptionsModel options)
        {
            if (!IsCorrectOrder(options))
            {
                SwapYears(options);
            }
        }

        private bool IsCorrectOrder(OptionsModel options)
        {
            return options.YearFrom < options.YearTo;
        }

        private void SwapYears(OptionsModel options)
        {
            int temporaryYear = options.YearFrom;
            options.YearFrom = options.YearTo;
            options.YearTo = temporaryYear;
        }

        private void SetDefaultYearsIfNeeded(OptionsModel options)
        {
            if (IsYearTooSmall(options.YearFrom))
            {
                options.YearFrom = GetDefaultYearFrom();
            }

            if (IsYearTooBigOrInvalid(options.YearTo))
            {
                options.YearTo = GetDefaultYearTo();
            }

            SwapYearsIfNeeded(options);
        }

        private bool IsYearTooSmall(int year)
        {
            return year < FIRST_SEASON;
        }

        private int GetDefaultYearFrom()
        {
            if (int.TryParse(_configuration.GetSection("DefaultYearFrom").Value, out int defaultYearFrom))
            {
                return defaultYearFrom;
            }
            else
            {
                _logger.LogCritical("DefaultYearFrom nėra sveikas skaičius.");
                throw new ArgumentException("DefaultYearFrom setting has to be an integer value.");
            }
        }

        private bool IsYearTooBigOrInvalid(int year)
        {
            return year > GetThisYear() || IsYearTooSmall(year);
        }

        private int GetDefaultYearTo()
        {
            if (int.TryParse(_configuration.GetSection("DefaultYearTo").Value, out int defaultYearTo))
            {

                return defaultYearTo;
            }
            else
            {
                _logger.LogCritical("DefaultYearTo nėra sveikas skaičius.");
                throw new ArgumentException("DefaultYearTo setting has to be an integer value.");
            }
        }

        private void CheckSeason(OptionsModel options)
        {
            if (!IsSeasonValid(options))
            {
                options.Season = GetDefaultSeason(options);
            }
        }

        private bool IsSeasonValid(OptionsModel options)
        {
            return options.Season >= FIRST_SEASON && options.Season <= GetThisYear();
        }

        private int GetDefaultSeason(OptionsModel options)
        {
            if (int.TryParse(_configuration.GetSection("DefaultSeason").Value, out int defaultSeason))
            {
                return defaultSeason; 
            }
            else
            {
                _logger.LogCritical("DefaultSeason nėra sveikas skaičius.");
                throw new ArgumentException("DefaultSeason setting has to be an integer value.");
            }
        }

        public bool AreFastestLapYearsValid(OptionsModel options)
        {
            if (options.Season < MINIMUM_FASTEST_LAPS_DATA_YEAR && options.Season > 0)
            {
                return false;
            }
            else if (options.YearFrom < MINIMUM_FASTEST_LAPS_DATA_YEAR && options.YearFrom > 0)
            {
                return false;
            }
            else if (options.YearTo < MINIMUM_FASTEST_LAPS_DATA_YEAR && options.YearTo > 0)
            {
                return false;
            }

            return true;
        }

        public bool IsLapTimesSeasonValid(int season)
        {
            return season >= MINIMUM_LAP_TIMES_DATA_YEAR;
        }
    }
}
