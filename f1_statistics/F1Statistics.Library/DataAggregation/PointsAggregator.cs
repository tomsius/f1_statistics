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

        private readonly IResultsDataAccess _resultsDataAccess;
        private readonly IStandingsDataAccess _standingsDataAccess;

        public PointsAggregator(IResultsDataAccess resultsDataAccess, IStandingsDataAccess standingsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
            _standingsDataAccess = standingsDataAccess;
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

                var racesCount = _resultsDataAccess.GetResultsFrom(year).Count;

                var winner = $"{standings[0].Driver.givenName} {standings[0].Driver.familyName}";
                var points = int.Parse(standings[0].points);

                var newSeasonWinnersPointsModel = new SeasonWinnersPointsModel { Season = year, Winner = winner, Points = points, RacesCount = racesCount };

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

                var racesCount = _resultsDataAccess.GetResultsFrom(year).Count;

                var winner = $"{standings[0].Constructor.name}";
                var points = int.Parse(standings[0].points);

                var newSeasonWinnersPointsModel = new SeasonWinnersPointsModel { Season = year, Winner = winner, Points = points, RacesCount = racesCount };

                lock (lockAdd)
                {
                    constructorsWinnersPoints.Add(newSeasonWinnersPointsModel); 
                }
            });

            return constructorsWinnersPoints;
        }

        public List<SeasonStandingsChangesModel> GetDriversPointsChanges(int from, int to)
        {
            var driversStandingsChanges = new List<SeasonStandingsChangesModel>(to - from + 1);
            var lockObject = new object();

            for (int year = from; year <= to; year++)
            {
                var newSeasonStandingsChangesModel = new SeasonStandingsChangesModel { Season = year, Standings = new List<StandingModel>() };

                var results = _resultsDataAccess.GetResultsFrom(year);
                var racesCount = results.Count;

                Parallel.For(1, racesCount + 1, round =>
                {
                    var standings = _standingsDataAccess.GetDriverStandingsFromRace(year, round);
                    var roundName = results[round - 1].raceName;

                    for (int i = 0; i < standings.Count; i++)
                    {
                        var driverName = $"{standings[i].Driver.givenName} {standings[i].Driver.familyName}";
                        var points = double.Parse(standings[i].points);
                        var position = i + 1;

                        lock (lockObject)
                        {
                            if (!newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == driverName).Any())
                            {
                                var newStandingModel = new StandingModel { Name = driverName, Rounds = new List<RoundModel>() };
                                newSeasonStandingsChangesModel.Standings.Add(newStandingModel);
                            }

                            var newRoundModel = new RoundModel { Round = round, RoundName = roundName, Points = points, Position = position };
                            newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == driverName).First().Rounds.Add(newRoundModel);
                        }
                    }
                });

                driversStandingsChanges.Add(newSeasonStandingsChangesModel);
            }

            return driversStandingsChanges;
        }

        public List<SeasonStandingsChangesModel> GetConstructorsPointsChanges(int from, int to)
        {
            var constructorsStandingsChanges = new List<SeasonStandingsChangesModel>(to - from + 1);
            var lockObject = new object();

            for (int year = from; year <= to; year++)
            {
                var newSeasonStandingsChangesModel = new SeasonStandingsChangesModel { Season = year, Standings = new List<StandingModel>() };

                var results = _resultsDataAccess.GetResultsFrom(year);
                var racesCount = results.Count;

                Parallel.For(1, racesCount + 1, round =>
                {
                    var standings = _standingsDataAccess.GetConstructorStandingsFromRace(year, round);
                    var roundName = results[round - 1].raceName;

                    for (int i = 0; i < standings.Count; i++)
                    {
                        var constructorName = $"{standings[i].Constructor.name}";
                        var points = double.Parse(standings[i].points);
                        var position = i + 1;

                        lock (lockObject)
                        {

                            if (!newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == constructorName).Any())
                            {
                                var newStandingModel = new StandingModel { Name = constructorName, Rounds = new List<RoundModel>() };
                                newSeasonStandingsChangesModel.Standings.Add(newStandingModel);
                            }

                            var newRoundModel = new RoundModel { Round = round, RoundName = roundName, Points = points, Position = position };
                            newSeasonStandingsChangesModel.Standings.Where(standings => standings.Name == constructorName).First().Rounds.Add(newRoundModel);
                        }
                    }
                });

                constructorsStandingsChanges.Add(newSeasonStandingsChangesModel);
            }

            return constructorsStandingsChanges;
        }
    }
}
