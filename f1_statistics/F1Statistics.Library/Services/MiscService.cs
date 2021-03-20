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

            nonFinishers.Sort((x, y) => y.DidNotFinishCount.CompareTo(x.DidNotFinishCount));

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

        public List<DriverFinishingPositionsModel> AggregateDriversFinishingPositions(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<DriverFinishingPositionsModel> driversFinishingPositions;

            if (options.YearFrom != 0)
            {
                driversFinishingPositions = _aggregator.GetDriversFinishingPositions(options.YearFrom, options.YearTo);
            }
            else
            {
                driversFinishingPositions = _aggregator.GetDriversFinishingPositions(options.Season, options.Season);
            }

            driversFinishingPositions.ForEach(driver => driver.FinishingPositions.Sort((x, y) => x.FinishingPosition.CompareTo(y.FinishingPosition)));
            driversFinishingPositions.Sort((x, y) => x.Name.CompareTo(y.Name));

            FillMissingFinishingPositions(driversFinishingPositions);

            return driversFinishingPositions;
        }

        //TODO - iskelti kaip extension
        private void FillMissingFinishingPositions(List<DriverFinishingPositionsModel> driversFinishingPositions)
        {
            for (int i = 0; i < driversFinishingPositions.Count; i++)
            {
                for (int j = 0; j < driversFinishingPositions[i].FinishingPositions.Count; j++)
                {
                    var expectedFinishingPosition = j + 1;
                    if (driversFinishingPositions[i].FinishingPositions[j].FinishingPosition != expectedFinishingPosition)
                    {
                        var missingFinishingPosition = new FinishingPositionModel { FinishingPosition = expectedFinishingPosition, Count = 0 };
                        driversFinishingPositions[i].FinishingPositions.Insert(j, missingFinishingPosition);
                    }
                }
            }
        }

        public List<SeasonStandingsChangesModel> AggregateDriversStandingsChanges(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonStandingsChangesModel> driversPositionChanges;

            if (options.YearFrom != 0)
            {
                driversPositionChanges = _aggregator.GetDriversStandingsChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                driversPositionChanges = _aggregator.GetDriversStandingsChanges(options.Season, options.Season);
            }

            driversPositionChanges.Sort((x, y) => x.Season.CompareTo(y.Season));
            driversPositionChanges.ForEach(model => model.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round)));

            return driversPositionChanges;
        }

        public List<SeasonStandingsChangesModel> AggregateConstructorsStandingsChanges(OptionsModel options)
        {
            _validator.ValidateOptionsModel(options);

            List<SeasonStandingsChangesModel> constructorsPositionChanges;

            if (options.YearFrom != 0)
            {
                constructorsPositionChanges = _aggregator.GetConstructorsStandingsChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsPositionChanges = _aggregator.GetConstructorsStandingsChanges(options.Season, options.Season);
            }

            //TODO - galbut iskelti rikiavimus i atskira klase kaip extension
            constructorsPositionChanges.Sort((x, y) => x.Season.CompareTo(y.Season));
            constructorsPositionChanges.ForEach(model => model.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round)));

            return constructorsPositionChanges;
        }

        // TODO - iskelti kaip konstanta, naudoti default metus is settings, galbut is vis iskelti i atskira validatoriu visus tikrinimus
        public List<RacePositionChangesModel> AggregateDriversPositionChangesDuringRace(int season, int race)
        {
            if (season < 1996)
            {
                season = 1996;
            }

            var driversPositionChangesDuringRace = _aggregator.GetDriversPositionChangesDuringRace(season, race);

            driversPositionChangesDuringRace.Sort((x, y) => x.LapNumber.CompareTo(y.LapNumber));

            return driversPositionChangesDuringRace;
        }
    }
}
