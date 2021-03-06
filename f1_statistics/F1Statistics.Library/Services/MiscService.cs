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

        public List<HatTrickModel> AggregateHatTricks(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            if (options.Season < 2004 && options.Season != 0)
            {
                options.Season = 2004;
            }
            else if (options.YearFrom < 2004 && options.YearFrom != 0)
            {
                options.YearFrom = 2004;

                if (options.YearTo < 2004)
                {
                    options.YearTo = 2004;
                }
            }

            List<HatTrickModel> hatTricks;

            if (options.YearFrom != 0)
            {
                hatTricks = _aggregator.GetHatTricks(options.YearFrom, options.YearTo);
            }
            else
            {
                hatTricks = _aggregator.GetHatTricks(options.Season, options.Season);
            }

            hatTricks.Sort((x, y) => y.HatTrickCount.CompareTo(x.HatTrickCount));

            return hatTricks;
        }

        public List<GrandSlamModel> AggregateGrandSlams(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            //TODO - iskelti i options validator
            if (options.Season < 2004 && options.Season != 0)
            {
                options.Season = 2004;
            }
            else if (options.YearFrom < 2004 && options.YearFrom != 0)
            {
                options.YearFrom = 2004;

                if (options.YearTo < 2004)
                {
                    options.YearTo = 2004;
                }
            }

            List<GrandSlamModel> hatTricks;

            if (options.YearFrom != 0)
            {
                hatTricks = _aggregator.GetGrandSlams(options.YearFrom, options.YearTo);
            }
            else
            {
                hatTricks = _aggregator.GetGrandSlams(options.Season, options.Season);
            }

            hatTricks.Sort((x, y) => y.GrandSlamCount.CompareTo(x.GrandSlamCount));

            return hatTricks;
        }

        public List<DidNotFinishModel> AggregateNonFinishers(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<DidNotFinishModel> nonFinishers;

            if (options.YearFrom != 0)
            {
                nonFinishers = _aggregator.GetNonFinishers(options.YearFrom, options.YearTo);
            }
            else
            {
                nonFinishers = _aggregator.GetNonFinishers(options.Season, options.Season);
            }

            nonFinishers.Sort((x, y) => x.DidNotFinishCount.CompareTo(y.DidNotFinishCount));

            return nonFinishers;
        }

        public List<SeasonPositionChangesModel> AggregateSeasonPositionChanges(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonPositionChangesModel> positionChanges;

            if (options.YearFrom != 0)
            {
                positionChanges = _aggregator.GetSeasonPositionChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                positionChanges = _aggregator.GetSeasonPositionChanges(options.Season, options.Season);
            }

            positionChanges.ForEach(season => season.PositionChanges.Sort((x, y) => y.PositionChange.CompareTo(x.PositionChange)));
            positionChanges.Sort((x, y) => x.Season.CompareTo(y.Season));

            return positionChanges;
        }

        public List<FrontRowModel> AggregateConstructorsFrontRows(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<FrontRowModel> constructorsWithFrontRow;

            if (options.YearFrom != 0)
            {
                constructorsWithFrontRow = _aggregator.GetConstructorsFrontRows(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsWithFrontRow = _aggregator.GetConstructorsFrontRows(options.Season, options.Season);
            }

            constructorsWithFrontRow.Sort((x, y) => y.FrontRowCount.CompareTo(x.FrontRowCount));

            return constructorsWithFrontRow;
        }
    }
}
