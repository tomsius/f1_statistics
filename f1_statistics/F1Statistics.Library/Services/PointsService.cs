using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class PointsService : IPointsService
    {
        private readonly IOptionsValidator _validator;
        private readonly IPointsAggregator _aggregator;

        public PointsService(IOptionsValidator validator, IPointsAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<SeasonPointsModel> AggregateDriversPointsPerSeason(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonPointsModel> driversPoints;

            if (options.YearFrom != 0)
            {
                driversPoints = _aggregator.GetDriversPointsPerSeason(options.YearFrom, options.YearTo);
            }
            else
            {
                driversPoints = _aggregator.GetDriversPointsPerSeason(options.Season, options.Season);
            }

            driversPoints.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));

            driversPoints.Sort((x, y) => x.Year.CompareTo(y.Year));

            return driversPoints;
        }

        public List<SeasonPointsModel> AggregateConstructorsPointsPerSeason(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonPointsModel> constructorsPoints;

            if (options.YearFrom != 0)
            {
                constructorsPoints = _aggregator.GetConstructorsPointsPerSeason(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsPoints = _aggregator.GetConstructorsPointsPerSeason(options.Season, options.Season);
            }

            constructorsPoints.ForEach(season => season.ScoredPoints.Sort((x, y) => y.Points.CompareTo(x.Points)));

            constructorsPoints.Sort((x, y) => x.Year.CompareTo(y.Year));

            return constructorsPoints;
        }

        public List<SeasonWinnersPointsModel> AggregateDriversWinnersPointsPerSeason(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonWinnersPointsModel> driversWinnersPoints;

            if (options.YearFrom != 0)
            {
                driversWinnersPoints = _aggregator.GetDriversWinnersPointsPerSeason(options.YearFrom, options.YearTo);
            }
            else
            {
                driversWinnersPoints = _aggregator.GetDriversWinnersPointsPerSeason(options.Season, options.Season);
            }

            driversWinnersPoints.Sort((x, y) => x.Year.CompareTo(y.Year));

            return driversWinnersPoints;
        }

        public List<SeasonWinnersPointsModel> AggregateConstructorsWinnersPointsPerSeason(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonWinnersPointsModel> constructorsWinnersPoints;

            if (options.YearFrom != 0)
            {
                constructorsWinnersPoints = _aggregator.GetConstructorsWinnersPointsPerSeason(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsWinnersPoints = _aggregator.GetConstructorsWinnersPointsPerSeason(options.Season, options.Season);
            }

            constructorsWinnersPoints.Sort((x, y) => x.Year.CompareTo(y.Year));

            return constructorsWinnersPoints;
        }

        public List<SeasonStandingsChangesModel> AggregateDriversPointsChanges(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonStandingsChangesModel> driversPositionChanges;

            if (options.YearFrom != 0)
            {
                driversPositionChanges = _aggregator.GetDriversPointsChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                driversPositionChanges = _aggregator.GetDriversPointsChanges(options.Season, options.Season);
            }

            driversPositionChanges.Sort((x, y) => x.Year.CompareTo(y.Year));
            driversPositionChanges.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));

            return driversPositionChanges;
        }

        public List<SeasonStandingsChangesModel> AggregateConstructorsPointsChanges(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonStandingsChangesModel> constructorsPositionChanges;

            if (options.YearFrom != 0)
            {
                constructorsPositionChanges = _aggregator.GetConstructorsPointsChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsPositionChanges = _aggregator.GetConstructorsPointsChanges(options.Season, options.Season);
            }

            constructorsPositionChanges.Sort((x, y) => x.Year.CompareTo(y.Year));
            constructorsPositionChanges.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));

            return constructorsPositionChanges;
        }
    }
}
