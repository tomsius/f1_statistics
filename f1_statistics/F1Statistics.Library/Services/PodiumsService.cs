using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services
{
    public class PodiumsService : IPodiumsService
    {
        private readonly IOptionsValidator _validator;
        private readonly IPodiumsAggregator _aggregator;

        public PodiumsService(IOptionsValidator validator, IPodiumsAggregator aggregator)
        {
            _validator = validator;
            _aggregator = aggregator;
        }

        public List<PodiumsModel> AggregateDriversPodiums(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<PodiumsModel> driversPodiums;

            if (options.YearFrom != 0)
            {
                driversPodiums = _aggregator.GetDriversPodiums(options.YearFrom, options.YearTo);
            }
            else
            {
                driversPodiums = _aggregator.GetDriversPodiums(options.Season, options.Season);
            }

            driversPodiums.Sort((x, y) => y.PodiumCount.CompareTo(x.PodiumCount));

            return driversPodiums;
        }

        public List<PodiumsModel> AggregateConstructorsPodiums(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<PodiumsModel> constructorsPodiums;

            if (options.YearFrom != 0)
            {
                constructorsPodiums = _aggregator.GetConstructorsPodiums(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsPodiums = _aggregator.GetConstructorsPodiums(options.Season, options.Season);
            }

            constructorsPodiums.Sort((x, y) => y.PodiumCount.CompareTo(x.PodiumCount));

            return constructorsPodiums;
        }

        public List<SamePodiumsModel> AggregateSameDriverPodiums(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SamePodiumsModel> sameDriversPodiums;

            if (options.YearFrom != 0)
            {
                sameDriversPodiums = _aggregator.GetSameDriversPodiums(options.YearFrom, options.YearTo);
            }
            else
            {
                sameDriversPodiums = _aggregator.GetSameDriversPodiums(options.Season, options.Season);
            }

            sameDriversPodiums.ForEach(podium => podium.PodiumFinishers.Sort());

            sameDriversPodiums.Sort((x, y) => y.SamePodiumCount.CompareTo(x.SamePodiumCount));

            return sameDriversPodiums;
        }

        public List<SamePodiumsModel> AggregateSameConstructorsPodiums(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SamePodiumsModel> sameConstructorsPodiums;

            if (options.YearFrom != 0)
            {
                sameConstructorsPodiums = _aggregator.GetSameConstructorsPodiums(options.YearFrom, options.YearTo);
            }
            else
            {
                sameConstructorsPodiums = _aggregator.GetSameConstructorsPodiums(options.Season, options.Season);
            }

            sameConstructorsPodiums.ForEach(podium => podium.PodiumFinishers.Sort());

            sameConstructorsPodiums.Sort((x, y) => y.SamePodiumCount.CompareTo(x.SamePodiumCount));

            return sameConstructorsPodiums;
        }
    }
}
