using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class PolesService : IPolesService
    {
        private IOptionsValidator _validator;
        private IPolesAggregator _aggregator;

        public PolesService(IOptionsValidator validator, IPolesAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<PolesModel> AggregatePoleSittersDrivers(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<PolesModel> driversPoles;

            if (options.YearFrom != 0)
            {
                driversPoles = _aggregator.GetPoleSittersDrivers(options.YearFrom, options.YearTo);
            }
            else
            {
                driversPoles = _aggregator.GetPoleSittersDrivers(options.Season, options.Season);
            }

            driversPoles.Sort((x, y) => y.PoleCount.CompareTo(x.PoleCount));

            return driversPoles;
        }

        public List<PolesModel> AggregatePoleSittersConstructors(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<PolesModel> constructorPoles;

            if (options.YearFrom != 0)
            {
                constructorPoles = _aggregator.GetPoleSittersConstructors(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorPoles = _aggregator.GetPoleSittersConstructors(options.Season, options.Season);
            }

            constructorPoles.Sort((x, y) => y.PoleCount.CompareTo(x.PoleCount));

            return constructorPoles;
        }

        public List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonPoleSittersDrivers(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<UniqueSeasonPoleSittersModel> uniqueSeasonPoleSitters;

            if (options.YearFrom != 0)
            {
                uniqueSeasonPoleSitters = _aggregator.GetUniquePoleSittersDrivers(options.YearFrom, options.YearTo);
            }
            else
            {
                uniqueSeasonPoleSitters = _aggregator.GetUniquePoleSittersDrivers(options.Season, options.Season);
            }

            return uniqueSeasonPoleSitters;
        }

        public List<UniqueSeasonPoleSittersModel> AggregateUniqueSeasonPoleSittersConstructors(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<UniqueSeasonPoleSittersModel> uniqueSeasonPoleSitters;

            if (options.YearFrom != 0)
            {
                uniqueSeasonPoleSitters = _aggregator.GetUniquePoleSittersConstructors(options.YearFrom, options.YearTo);
            }
            else
            {
                uniqueSeasonPoleSitters = _aggregator.GetUniquePoleSittersConstructors(options.Season, options.Season);
            }

            return uniqueSeasonPoleSitters;
        }

        public List<WinsFromPoleModel> AggregateWinnersFromPole(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<WinsFromPoleModel> winsFromPole;

            if (options.YearFrom != 0)
            {
                winsFromPole = _aggregator.GetWinCountFromPole(options.YearFrom, options.YearTo);
            }
            else
            {
                winsFromPole = _aggregator.GetWinCountFromPole(options.Season, options.Season);
            }

            return winsFromPole;
        }
    }
}
