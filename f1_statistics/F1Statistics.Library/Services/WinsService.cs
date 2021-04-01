using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class WinsService : IWinsService
    {
        private readonly IOptionsValidator _validator;
        private readonly IWinsAggregator _aggregator;

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
            driversWins.ForEach(model => model.WinsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

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
            constructorsWins.ForEach(model => model.WinsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

            return constructorsWins;
        }

        public List<AverageWinsModel> AggregateDriversWinPercent(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<AverageWinsModel> driversAverageWins;

            if (options.YearFrom != 0)
            {
                driversAverageWins = _aggregator.GetDriversWinPercent(options.YearFrom, options.YearTo);
            }
            else
            {
                driversAverageWins = _aggregator.GetDriversWinPercent(options.Season, options.Season);
            }

            driversAverageWins.Sort((x, y) => y.AverageWins.CompareTo(x.AverageWins));

            return driversAverageWins;
        }

        public List<AverageWinsModel> AggregateConstructorsWinPercent(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<AverageWinsModel> constructorsAverageWins;

            if (options.YearFrom != 0)
            {
                constructorsAverageWins = _aggregator.GetConstructorsWinPercent(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsAverageWins = _aggregator.GetConstructorsWinPercent(options.Season, options.Season);
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

            circuitsWinners.ForEach(circuit => circuit.Winners = circuit.Winners.Where(winner => winner.WinCount > 0).ToList());

            circuitsWinners.ForEach(circuit => circuit.Winners.Sort((x, y) => y.WinCount.CompareTo(x.WinCount)));
            circuitsWinners.Sort((x, y) => x.Name.CompareTo(y.Name));

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

            uniqueSeasonWinners.Sort((x, y) => x.Season.CompareTo(y.Season));

            return uniqueSeasonWinners;
        }

        public List<UniqueSeasonWinnersModel> AggregateUniqueSeasonConstructorWinners(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<UniqueSeasonWinnersModel> uniqueSeasonWinners;

            if (options.YearFrom != 0)
            {
                uniqueSeasonWinners = _aggregator.GetUniqueSeasonConstructorWinners(options.YearFrom, options.YearTo);
            }
            else
            {
                uniqueSeasonWinners = _aggregator.GetUniqueSeasonConstructorWinners(options.Season, options.Season);
            }

            uniqueSeasonWinners.Sort((x, y) => x.Season.CompareTo(y.Season));

            return uniqueSeasonWinners;
        }

        public List<WinnersFromPoleModel> AggregateWinnersFromPole(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<WinnersFromPoleModel> winsFromPole;

            if (options.YearFrom != 0)
            {
                winsFromPole = _aggregator.GetWinnersFromPole(options.YearFrom, options.YearTo);
            }
            else
            {
                winsFromPole = _aggregator.GetWinnersFromPole(options.Season, options.Season);
            }

            winsFromPole.Sort((x, y) => x.Season.CompareTo(y.Season));

            return winsFromPole;
        }

        public List<WinsByGridPositionModel> AggregateWinnersByGridPosition(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<WinsByGridPositionModel> winnersByGridPosition;

            if (options.YearFrom != 0)
            {
                winnersByGridPosition = _aggregator.GetWinnersByGridPosition(options.YearFrom, options.YearTo);
            }
            else
            {
                winnersByGridPosition = _aggregator.GetWinnersByGridPosition(options.Season, options.Season);
            }

            winnersByGridPosition.Sort((x, y) => x.GridPosition.CompareTo(y.GridPosition));

            FillMissingGridPositions(winnersByGridPosition);

            return winnersByGridPosition;
        }

        //TODO - iskelti kaip extension
        private void FillMissingGridPositions(List<WinsByGridPositionModel> winnersByGridPosition)
        {
            for (int i = 0; i < winnersByGridPosition.Count; i++)
            {
                var expectedGridPosition = i + 1;
                if (winnersByGridPosition[i].GridPosition != expectedGridPosition)
                {
                    var missingGrid = new WinsByGridPositionModel { GridPosition = expectedGridPosition, Winners = new List<string>() };
                    winnersByGridPosition.Insert(i, missingGrid);
                }
            }
        }
    }
}
