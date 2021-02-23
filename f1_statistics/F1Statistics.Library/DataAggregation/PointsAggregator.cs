using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace F1Statistics.Library.DataAggregation
{
    public class PointsAggregator : IPointsAggregator
    {
        private IStandingsDataAccess _standingsDataAccess;

        public PointsAggregator(IStandingsDataAccess resultsDataAccess)
        {
            _standingsDataAccess = resultsDataAccess;
        }

        public List<SeasonPointsModel> GetDriversPointsPerSeason(int from, int to)
        {
            var driversPoints = new List<SeasonPointsModel>();

            for (int year = from; year <= to; year++)
            {
                var standings = _standingsDataAccess.GetDriverStandingsFrom(year);

                var newSeasonPointsModel = new SeasonPointsModel { Season = year, ScoredPoints = new List<PointsModel>() };
                driversPoints.Add(newSeasonPointsModel);

                foreach (var standing in standings)
                {
                    var driverName = $"{standing.Driver.givenName} {standing.Driver.familyName}";
                    var driverScoredPoints = int.Parse(standing.points);

                    var newPointsModel = new PointsModel { Name = driverName, Points = driverScoredPoints };
                    driversPoints[year - from].ScoredPoints.Add(newPointsModel);
                }
            }

            return driversPoints;
        }

        public List<SeasonPointsModel> GetConstructorsPointsPerSeason(int from, int to)
        {
            var constructorsPoints = new List<SeasonPointsModel>();

            for (int year = from; year <= to; year++)
            {
                var standings = _standingsDataAccess.GetConstructorStandingsFrom(year);

                var newSeasonPointsModel = new SeasonPointsModel { Season = year, ScoredPoints = new List<PointsModel>() };
                constructorsPoints.Add(newSeasonPointsModel);

                foreach (var standing in standings)
                {
                    var constructorName = $"{standing.Constructor.name}";
                    var constructorScoredPoints = int.Parse(standing.points);

                    var newPointsModel = new PointsModel { Name = constructorName, Points = constructorScoredPoints };
                    constructorsPoints[year - from].ScoredPoints.Add(newPointsModel);
                }
            }

            return constructorsPoints;
        }
    }
}
