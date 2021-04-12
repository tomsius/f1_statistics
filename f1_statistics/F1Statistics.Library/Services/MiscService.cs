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
            _validator.NormalizeOptionsModel(options);

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
            _validator.NormalizeOptionsModel(options);

            if (!_validator.AreFastestLapYearsValid(options))
            {
                throw new ArgumentException("Duomenys prieinami nuo 2004 metų.");
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
            _validator.NormalizeOptionsModel(options);

            if (!_validator.AreFastestLapYearsValid(options))
            {
                throw new ArgumentException("Duomenys prieinami nuo 2004 metų.");
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
            _validator.NormalizeOptionsModel(options);

            List<DidNotFinishModel> nonFinishers;

            if (options.YearFrom != 0)
            {
                nonFinishers = _aggregator.GetNonFinishers(options.YearFrom, options.YearTo);
            }
            else
            {
                nonFinishers = _aggregator.GetNonFinishers(options.Season, options.Season);
            }

            nonFinishers.Sort((x, y) => y.TotalDidNotFinishCount.CompareTo(x.TotalDidNotFinishCount));
            nonFinishers.ForEach(model => model.DidNotFinishByYear.Sort((x, y) => x.Year.CompareTo(y.Year)));

            return nonFinishers;
        }

        public List<SeasonPositionChangesModel> AggregateSeasonPositionChanges(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            List<SeasonPositionChangesModel> positionChanges;

            if (options.YearFrom != 0)
            {
                positionChanges = _aggregator.GetSeasonPositionChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                positionChanges = _aggregator.GetSeasonPositionChanges(options.Season, options.Season);
            }

            positionChanges.ForEach(season => season.PositionChanges.Sort((x, y) => y.TotalPositionChange.CompareTo(x.TotalPositionChange)));
            positionChanges.Sort((x, y) => x.Year.CompareTo(y.Year));

            return positionChanges;
        }

        public List<FrontRowModel> AggregateConstructorsFrontRows(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            List<FrontRowModel> constructorsWithFrontRow;

            if (options.YearFrom != 0)
            {
                constructorsWithFrontRow = _aggregator.GetConstructorsFrontRows(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsWithFrontRow = _aggregator.GetConstructorsFrontRows(options.Season, options.Season);
            }

            constructorsWithFrontRow.Sort((x, y) => y.TotalFrontRowCount.CompareTo(x.TotalFrontRowCount));

            return constructorsWithFrontRow;
        }

        public List<DriverFinishingPositionsModel> AggregateDriversFinishingPositions(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

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

        private void FillMissingFinishingPositions(List<DriverFinishingPositionsModel> driversFinishingPositions)
        {
            for (int i = 0; i < driversFinishingPositions.Count; i++)
            {
                for (int j = 0; j < driversFinishingPositions[i].FinishingPositions.Count; j++)
                {
                    var expectedFinishingPosition = j + 1;
                    if (driversFinishingPositions[i].FinishingPositions[j].FinishingPosition != expectedFinishingPosition)
                    {
                        var missingFinishingPosition = new FinishingPositionModel { FinishingPosition = expectedFinishingPosition, FinishingPositionInformation = new List<FinishingPositionInformationModel>() };
                        driversFinishingPositions[i].FinishingPositions.Insert(j, missingFinishingPosition);
                    }
                }
            }
        }

        public List<SeasonStandingsChangesModel> AggregateDriversStandingsChanges(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            List<SeasonStandingsChangesModel> driversPositionChanges;

            if (options.YearFrom != 0)
            {
                driversPositionChanges = _aggregator.GetDriversStandingsChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                driversPositionChanges = _aggregator.GetDriversStandingsChanges(options.Season, options.Season);
            }

            driversPositionChanges.Sort((x, y) => x.Year.CompareTo(y.Year));
            driversPositionChanges.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));

            return driversPositionChanges;
        }

        public List<SeasonStandingsChangesModel> AggregateConstructorsStandingsChanges(OptionsModel options)
        {
            _validator.NormalizeOptionsModel(options);

            List<SeasonStandingsChangesModel> constructorsPositionChanges;

            if (options.YearFrom != 0)
            {
                constructorsPositionChanges = _aggregator.GetConstructorsStandingsChanges(options.YearFrom, options.YearTo);
            }
            else
            {
                constructorsPositionChanges = _aggregator.GetConstructorsStandingsChanges(options.Season, options.Season);
            }

            constructorsPositionChanges.Sort((x, y) => x.Year.CompareTo(y.Year));
            constructorsPositionChanges.ForEach(model => model.Standings.ForEach(standing => standing.Rounds.Sort((x, y) => x.Round.CompareTo(y.Round))));

            return constructorsPositionChanges;
        }

        public List<RacePositionChangesModel> AggregateDriversPositionChangesDuringRace(int season, int race)
        {
            if (!_validator.IsLapTimesSeasonValid(season))
            {
                throw new ArgumentException("Duomenys prieinami nuo 1996 metų.");
            }

            var driversPositionChangesDuringRace = _aggregator.GetDriversPositionChangesDuringRace(season, race);

            driversPositionChangesDuringRace.Sort((x, y) => x.Name.CompareTo(y.Name));
            driversPositionChangesDuringRace.ForEach(driver => driver.Laps.Sort((x, y) => x.LapNumber.CompareTo(y.LapNumber)));

            return driversPositionChangesDuringRace;
        }

        public List<LapTimesModel> AggregateDriversLapTimes(int season, int race)
        {
            if (!_validator.IsLapTimesSeasonValid(season))
            {
                throw new ArgumentException("Duomenys prieinami nuo 1996 metų.");
            }

            var driversLapTimes = _aggregator.GetDriversLapTimes(season, race);

            driversLapTimes.Sort((x, y) => x.Name.CompareTo(y.Name));
            driversLapTimes.ForEach(model => model.Timings.Sort((x, y) => x.CompareTo(y)));

            return driversLapTimes;
        }
    }
}
