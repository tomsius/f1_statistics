using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class NationalitiesService : INationalitiesService
    {
        private readonly IOptionsValidator _validator;
        private readonly INationalitiesAggregator _aggregator;

        public NationalitiesService(IOptionsValidator validator, INationalitiesAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<NationalityDriversModel> AggregateDriversNationalities(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<NationalityDriversModel> driversNationalities;

            if (options.YearFrom != 0)
            {
                driversNationalities = _aggregator.GetDriversNationalities(options.YearFrom, options.YearTo);
            }
            else
            {
                driversNationalities = _aggregator.GetDriversNationalities(options.Season, options.Season);
            }

            driversNationalities.Sort((x, y) => y.DriversCount.CompareTo(x.DriversCount));

            return driversNationalities;
        }

        public List<NationalityWinsModel> AggregateNationalitiesRaceWins(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<NationalityWinsModel> nationalitiesRaceWins;

            if (options.YearFrom != 0)
            {
                nationalitiesRaceWins = _aggregator.GetNationalitiesRaceWins(options.YearFrom, options.YearTo);
            }
            else
            {
                nationalitiesRaceWins = _aggregator.GetNationalitiesRaceWins(options.Season, options.Season);
            }

            nationalitiesRaceWins.Sort((x, y) => y.WinnersCount.CompareTo(x.WinnersCount));

            return nationalitiesRaceWins;
        }

        public List<NationalityWinsModel> AggregateNationalitiesSeasonWins(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<NationalityWinsModel> nationalitiesSeasonWins;

            if (options.YearFrom != 0)
            {
                nationalitiesSeasonWins = _aggregator.GetNationalitiesSeasonWins(options.YearFrom, options.YearTo);
            }
            else
            {
                nationalitiesSeasonWins = _aggregator.GetNationalitiesSeasonWins(options.Season, options.Season);
            }

            nationalitiesSeasonWins.Sort((x, y) => y.WinnersCount.CompareTo(x.WinnersCount));

            return nationalitiesSeasonWins;
        }
    }
}
