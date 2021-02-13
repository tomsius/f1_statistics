using F1Statistics.Library.Models;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Validators
{
    public class OptionsValidator : IOptionsValidator
    {
        private const int FIRST_SEASON = 1950;

        private IConfiguration _configuration;

        public OptionsValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ValidateOptionsModel(OptionsModel options)
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

        private bool AreYearsGiven(OptionsModel options)
        {
            return !IsYearValid(options.Season);
        }

        private bool IsYearValid(int year)
        {
            return year != 0;
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

        private bool AreBothYearsGiven(OptionsModel options)
        {
            return IsYearValid(options.YearFrom) && IsYearValid(options.YearTo);
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
                options.YearFrom = SetDefaultYearFrom();
            }

            if (IsYearTooBigOrInvalid(options.YearTo))
            {
                options.YearTo = SetDefaultYearTo();
            }

            SwapYearsIfNeeded(options);
        }

        private bool IsYearTooSmall(int year)
        {
            return year < FIRST_SEASON;
        }

        private int SetDefaultYearFrom()
        {
            if (int.TryParse(_configuration.GetSection("DefaultYearFrom").Value, out int defaultYearFrom))
            {
                return defaultYearFrom;
            }
            else
            {
                throw new ArgumentException("DefaultYearFrom setting has to be an integer value.");
            }
        }

        private bool IsYearTooBigOrInvalid(int year)
        {
            return year > GetThisYear() || IsYearTooSmall(year);
        }

        private int SetDefaultYearTo()
        {
            if (int.TryParse(_configuration.GetSection("DefaultYearTo").Value, out int defaultYearTo))
            {
                return defaultYearTo;
            }
            else
            {
                throw new ArgumentException("DefaultYearTo setting has to be an integer value.");
            }
        }

        private bool IsSeasonGiven(OptionsModel options)
        {
            return IsYearValid(options.Season);
        }

        private void CheckSeason(OptionsModel options)
        {
            if (!IsSeasonValid(options))
            {
                SetDefaultSeason(options);
            }
        }

        private bool IsSeasonValid(OptionsModel options)
        {
            return options.Season >= FIRST_SEASON && options.Season <= GetThisYear();
        }

        private void SetDefaultSeason(OptionsModel options)
        {
            if (int.TryParse(_configuration.GetSection("DefaultSeason").Value, out int defaultSeason))
            {
                options.Season = defaultSeason; 
            }
            else
            {
                throw new ArgumentException("DefaultSeason setting has to be an integer value.");
            }
        }
    }
}
