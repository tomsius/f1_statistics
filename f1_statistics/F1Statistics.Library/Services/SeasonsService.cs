using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class SeasonsService : ISeasonsService
    {
        private readonly IOptionsValidator _validator;
        private readonly ISeasonsAggregator _aggregator;

        public SeasonsService(IOptionsValidator validator, ISeasonsAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<SeasonModel> AggregateSeasonRaces(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonModel> seasons;

            if (options.YearFrom != 0)
            {
                seasons = _aggregator.GetSeasonRaces(options.YearFrom, options.YearTo);
            }
            else
            {
                seasons = _aggregator.GetSeasonRaces(options.Season, options.Season);
            }

            seasons.Sort((x, y) => x.Season.CompareTo(y.Season));
            seasons.ForEach(season => season.Races.Sort((x, y) => x.Round.CompareTo(y.Round)));

            return seasons;
        }
    }
}
