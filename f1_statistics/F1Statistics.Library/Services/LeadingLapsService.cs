using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class LeadingLapsService : ILeadingLapsService
    {
        private readonly IOptionsValidator _validator;
        private readonly ILeadingLapsAggregator _aggregator;

        public LeadingLapsService(IOptionsValidator validator, ILeadingLapsAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<LeadingLapsModel> AggregateDriversLeadingLapsCount(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            List<LeadingLapsModel> driversLeadingLaps;

            if (options.YearFrom != 0)
            {
                driversLeadingLaps = _aggregator.GetDriversLeadingLapsCount(options.YearFrom, options.YearTo);
            }
            else
            {
                driversLeadingLaps = _aggregator.GetDriversLeadingLapsCount(options.Season, options.Season);
            }

            driversLeadingLaps.Sort((x, y) => y.TotalLeadingLapCount.CompareTo(x.TotalLeadingLapCount));
            driversLeadingLaps.ForEach(model => model.LeadingLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

            return driversLeadingLaps;
        }

        public List<LeadingLapsModel> AggregateConstructorsLeadingLapsCount(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            List<LeadingLapsModel> constructorsLeadingLaps;

            if (options.YearFrom != 0)
            {
                constructorsLeadingLaps = _aggregator.GetConstructorsLeadingLapsCount(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsLeadingLaps = _aggregator.GetConstructorsLeadingLapsCount(options.Season, options.Season);
            }

            constructorsLeadingLaps.Sort((x, y) => y.TotalLeadingLapCount.CompareTo(x.TotalLeadingLapCount));
            constructorsLeadingLaps.ForEach(model => model.LeadingLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

            return constructorsLeadingLaps;
        }
    }
}
