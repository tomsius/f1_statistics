using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class FastestLapsService : IFastestLapsService
    {
        private readonly IOptionsValidator _validator;
        private readonly IFastestLapsAggregator _aggregator;

        public FastestLapsService(IOptionsValidator validator, IFastestLapsAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<FastestLapModel> AggregateDriversFastestLaps(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            if (!_validator.AreFastestLapYearsValid(options))
            {
                throw new ArgumentException("Duomenys prieinami nuo 2004 metų.");
            }

            List<FastestLapModel> driversFastestLaps;

            if (options.YearFrom != 0)
            {
                driversFastestLaps = _aggregator.GetDriversFastestLaps(options.YearFrom, options.YearTo);
            }
            else
            {
                driversFastestLaps = _aggregator.GetDriversFastestLaps(options.Season, options.Season);
            }

            driversFastestLaps.Sort((x, y) => y.TotalFastestLapsCount.CompareTo(x.TotalFastestLapsCount));
            driversFastestLaps.ForEach(model => model.FastestLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

            return driversFastestLaps;
        }

        public List<FastestLapModel> AggregateConstructorsFastestLaps(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            if (!_validator.AreFastestLapYearsValid(options))
            {
                throw new ArgumentException("Duomenys prieinami nuo 2004 metų.");
            }

            List<FastestLapModel> constructorsFastestLaps;

            if (options.YearFrom != 0)
            {
                constructorsFastestLaps = _aggregator.GetConstructorsFastestLaps(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsFastestLaps = _aggregator.GetConstructorsFastestLaps(options.Season, options.Season);
            }

            constructorsFastestLaps.Sort((x, y) => y.TotalFastestLapsCount.CompareTo(x.TotalFastestLapsCount));
            constructorsFastestLaps.ForEach(model => model.FastestLapsByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

            return constructorsFastestLaps;
        }

        public List<UniqueSeasonFastestLapModel> AggregateUniqueDriversFastestLapsPerSeason(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            if (!_validator.AreFastestLapYearsValid(options))
            {
                throw new ArgumentException("Duomenys prieinami nuo 2004 metų.");
            }

            List<UniqueSeasonFastestLapModel> uniqueDriversFastestLaps;

            if (options.YearFrom != 0)
            {
                uniqueDriversFastestLaps = _aggregator.GetUniqueDriversFastestLaps(options.YearFrom, options.YearTo);
            }
            else
            {
                uniqueDriversFastestLaps = _aggregator.GetUniqueDriversFastestLaps(options.Season, options.Season);
            }

            uniqueDriversFastestLaps.Sort((x, y) => x.Season.CompareTo(y.Season));

            return uniqueDriversFastestLaps;
        }

        public List<UniqueSeasonFastestLapModel> AggregateUniqueConstructorsFastestLapsPerseason(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            if (!_validator.AreFastestLapYearsValid(options))
            {
                throw new ArgumentException("Duomenys prieinami nuo 2004 metų.");
            }

            List<UniqueSeasonFastestLapModel> uniqueConstructorsFastestLaps;

            if (options.YearFrom != 0)
            {
                uniqueConstructorsFastestLaps = _aggregator.GetUniqueConstructorsFastestLaps(options.YearFrom, options.YearTo);
            }
            else
            {
                uniqueConstructorsFastestLaps = _aggregator.GetUniqueConstructorsFastestLaps(options.Season, options.Season);
            }

            uniqueConstructorsFastestLaps.Sort((x, y) => x.Season.CompareTo(y.Season));

            return uniqueConstructorsFastestLaps;
        }
    }
}
