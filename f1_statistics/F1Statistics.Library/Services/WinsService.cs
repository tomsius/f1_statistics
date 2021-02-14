using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class WinsService : IWinsService
    {
        private IOptionsValidator _validator;
        private IWinsAggregator _aggregator;

        public WinsService(IOptionsValidator validator, IWinsAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<WinsModel> AggregateDriversWins(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<WinsModel> driversWins;

            if (options.YearFrom != 0)
            {
                driversWins = _aggregator.GetDriversWins(options.YearFrom, options.YearTo);
            }
            else
            {
                driversWins = _aggregator.GetDriversWins(options.Season, options.Season);
            }

            driversWins.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));

            return driversWins;
        }

        public List<WinsModel> AggregateConstructorsWins(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<WinsModel> constructorsWins;

            if (options.YearFrom != 0)
            {
                constructorsWins = _aggregator.GetConstructorsWins(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsWins = _aggregator.GetConstructorsWins(options.Season, options.Season);
            }

            constructorsWins.Sort((x, y) => y.WinCount.CompareTo(x.WinCount));

            return constructorsWins;
        }
    }
}
