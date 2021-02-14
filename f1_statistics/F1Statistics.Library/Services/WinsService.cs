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

        public List<AverageWinsModel> AggregateDriversAverageWins(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<AverageWinsModel> driversAverageWins;

            if (options.YearFrom != 0)
            {
                driversAverageWins = _aggregator.GetDriversAverageWins(options.YearFrom, options.YearTo);
            }
            else
            {
                driversAverageWins = _aggregator.GetDriversAverageWins(options.Season, options.Season);
            }

            driversAverageWins.Sort((x, y) => y.AverageWins.CompareTo(x.AverageWins));

            return driversAverageWins;
        }

        public List<AverageWinsModel> AggregateConstructorsAverageWins(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<AverageWinsModel> constructorsAverageWins;

            if (options.YearFrom != 0)
            {
                constructorsAverageWins = _aggregator.GetConstructorsAverageWins(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsAverageWins = _aggregator.GetConstructorsAverageWins(options.Season, options.Season);
            }

            constructorsAverageWins.Sort((x, y) => y.AverageWins.CompareTo(x.AverageWins));

            return constructorsAverageWins;
        }

        public List<CircuitWinsModel> AggregateCircuitsWinners(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<CircuitWinsModel> circuitsWinners;

            if (options.YearFrom != 0)
            {
                circuitsWinners = _aggregator.GetCircuitWinners(options.YearFrom, options.YearTo);
            }
            else
            {
                circuitsWinners = _aggregator.GetCircuitWinners(options.Season, options.Season);
            }

            circuitsWinners.ForEach(circuit => circuit.Winners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount)));

            return circuitsWinners;
        }

        public List<UniqueSeasonWinnersModel> AggregateUniqueSeasonDriverWinners(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<UniqueSeasonWinnersModel> uniqueSeasonWinners;

            if (options.YearFrom != 0)
            {
                uniqueSeasonWinners = _aggregator.GetUniqueSeasonDriverWinners(options.YearFrom, options.YearTo);
            }
            else
            {
                uniqueSeasonWinners = _aggregator.GetUniqueSeasonDriverWinners(options.Season, options.Season);
            }

            return uniqueSeasonWinners;
        }
    }
}
