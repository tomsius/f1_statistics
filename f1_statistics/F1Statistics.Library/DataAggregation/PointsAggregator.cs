using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

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
            var driversPoints = new List<SeasonPointsModel>(to - from + 1);

            Parallel.For(from, to + 1, year => 
            {
                var standings = _standingsDataAccess.GetDriverStandingsFrom(year);

                var newSeasonPointsModel = new SeasonPointsModel { Season = year, ScoredPoints = new List<PointsModel>() };

                foreach (var standing in standings)
                {
                    var driverName = $"{standing.Driver.givenName} {standing.Driver.familyName}";
                    var driverScoredPoints = int.Parse(standing.points);

                    var newPointsModel = new PointsModel { Name = driverName, Points = driverScoredPoints };
                    newSeasonPointsModel.ScoredPoints.Add(newPointsModel);
                }

                driversPoints.Add(newSeasonPointsModel);
            });

            return driversPoints;
        }

        public List<SeasonPointsModel> GetConstructorsPointsPerSeason(int from, int to)
        {
            var constructorsPoints = new List<SeasonPointsModel>(to - from + 1);

            Parallel.For(from, to + 1, year => 
            {
                var standings = _standingsDataAccess.GetConstructorStandingsFrom(year);

                var newSeasonPointsModel = new SeasonPointsModel { Season = year, ScoredPoints = new List<PointsModel>() };

                foreach (var standing in standings)
                {
                    var constructorName = $"{standing.Constructor.name}";
                    var constructorScoredPoints = int.Parse(standing.points);

                    var newPointsModel = new PointsModel { Name = constructorName, Points = constructorScoredPoints };
                    newSeasonPointsModel.ScoredPoints.Add(newPointsModel);
                }

                constructorsPoints.Add(newSeasonPointsModel);
            });

            return constructorsPoints;
        }
    }
}
