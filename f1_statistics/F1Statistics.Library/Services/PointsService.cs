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
        private IOptionsValidator _validator;
        private IPointsAggregator _aggregator;

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

            return constructorsPoints;
        }
    }
}
