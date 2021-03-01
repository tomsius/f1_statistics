using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class MiscService : IMiscService
    {
        private readonly IOptionsValidator _validator;
        private readonly IMiscAggregator _aggregator;

        public MiscService(IOptionsValidator validator, IMiscAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<SeasonRacesModel> AggregateRaceCountPerSeason(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonRacesModel> seasonRaces;

            if (options.YearFrom != 0)
            {
                seasonRaces = _aggregator.GetRaceCountPerSeason(options.YearFrom, options.YearTo);
            }
            else
            {
                seasonRaces = _aggregator.GetRaceCountPerSeason(options.Season, options.Season);
            }

            seasonRaces.Sort((x, y) => x.Season.CompareTo(y.Season));

            return seasonRaces;
        }
    }
}
