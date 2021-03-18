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
        private readonly IStandingsDataAccess _standingsDataAccess;

        public PointsAggregator(IStandingsDataAccess resultsDataAccess)
        {
            _standingsDataAccess = resultsDataAccess;
        }

        public List<SeasonPointsModel> GetDriversPointsPerSeason(int from, int to)
        {
            var driversPoints = new List<SeasonPointsModel>(to - from + 1);
            var lockAdd = new object();

            Parallel.For(from, to + 1, year => 
            {
                var standings = _standingsDataAccess.GetDriverStandingsFrom(year);

                var newSeasonPointsModel = new SeasonPointsModel { Season = year, ScoredPoints = new List<PointsModel>() };

                foreach (var standing in standings)
                {
                    var driverName = $"{standing.Driver.givenName} {standing.Driver.familyName}";
                    var driverScoredPoints = double.Parse(standing.points);

                    var newPointsModel = new PointsModel { Name = driverName, Points = driverScoredPoints };
                    newSeasonPointsModel.ScoredPoints.Add(newPointsModel);
                }

                lock (lockAdd)
                {
                    driversPoints.Add(newSeasonPointsModel); 
                }
            });

            return driversPoints;
        }

        public List<SeasonPointsModel> GetConstructorsPointsPerSeason(int from, int to)
        {
            var constructorsPoints = new List<SeasonPointsModel>(to - from + 1);
            var lockAdd = new object();

            Parallel.For(from, to + 1, year => 
            {
                var standings = _standingsDataAccess.GetConstructorStandingsFrom(year);

                var newSeasonPointsModel = new SeasonPointsModel { Season = year, ScoredPoints = new List<PointsModel>() };

                foreach (var standing in standings)
                {
                    var constructorName = $"{standing.Constructor.name}";
                    var constructorScoredPoints = double.Parse(standing.points);

                    var newPointsModel = new PointsModel { Name = constructorName, Points = constructorScoredPoints };
                    newSeasonPointsModel.ScoredPoints.Add(newPointsModel);
                }

                lock (lockAdd)
                {
                    constructorsPoints.Add(newSeasonPointsModel); 
                }
            });

            return constructorsPoints;
        }

        public List<SeasonWinnersPointsModel> GetDriversWinnersPointsPerSeason(int from, int to)
        {
            var driversWinnersPoints = new List<SeasonWinnersPointsModel>(to - from + 1);
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var standings = _standingsDataAccess.GetDriverStandingsFrom(year);

                if (standings.Count == 0)
                {
                    return;
                }

                var winner = $"{standings[0].Driver.givenName} {standings[0].Driver.familyName}";
                var points = int.Parse(standings[0].points);

                var newSeasonWinnersPointsModel = new SeasonWinnersPointsModel { Season = year, Winner = winner, Points = points };

                lock (lockAdd)
                {
                    driversWinnersPoints.Add(newSeasonWinnersPointsModel); 
                }
            });

            return driversWinnersPoints;
        }

        public List<SeasonWinnersPointsModel> GetConstructorsWinnersPointsPerSeason(int from, int to)
        {
            var constructorsWinnersPoints = new List<SeasonWinnersPointsModel>(to - from + 1);
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var standings = _standingsDataAccess.GetConstructorStandingsFrom(year);

                if (standings.Count == 0)
                {
                    return;
                }

                var winner = $"{standings[0].Constructor.name}";
                var points = int.Parse(standings[0].points);

                var newSeasonWinnersPointsModel = new SeasonWinnersPointsModel { Season = year, Winner = winner, Points = points };

                lock (lockAdd)
                {
                    constructorsWinnersPoints.Add(newSeasonWinnersPointsModel); 
                }
            });

            return constructorsWinnersPoints;
        }
    }
}
