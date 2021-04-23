using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Helpers.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class PolesAggregator : IPolesAggregator
    {
        private readonly IQualifyingsDataAccess _qualifyingsDataAccess;
        private readonly INameHelper _nameHelper;
        private readonly ITimeHelper _timeHelper;

        public PolesAggregator(IQualifyingsDataAccess qualifyingsDataAccess, INameHelper nameHelper, ITimeHelper timeHelper)
        {
            _qualifyingsDataAccess = qualifyingsDataAccess;
            _nameHelper = nameHelper;
            _timeHelper = timeHelper;
        }

        public List<PolesModel> GetPoleSittersDrivers(int from, int to)
        {
            var driversPoles = new List<PolesModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var qualifyings = _qualifyingsDataAccess.GetQualifyingsFrom(year);

                foreach (var qualifying in qualifyings)
                {
                    var poleSitter = _nameHelper.GetDriverName(qualifying.QualifyingResults[0].Driver);
                    var circuitName = qualifying.Circuit.circuitName;

                    string poleSitterQualifyingTime;
                    string secondDriverQualifyingTime;
                    if (qualifying.QualifyingResults[0].Q3 != null && qualifying.QualifyingResults[1].Q3 != null)
                    {
                        poleSitterQualifyingTime = qualifying.QualifyingResults[0].Q3;
                        secondDriverQualifyingTime = qualifying.QualifyingResults[1].Q3;
                    }
                    else if (qualifying.QualifyingResults[0].Q2 != null && qualifying.QualifyingResults[1].Q2 != null)
                    {
                        poleSitterQualifyingTime = qualifying.QualifyingResults[0].Q2;
                        secondDriverQualifyingTime = qualifying.QualifyingResults[1].Q2;
                    }
                    else
                    {
                        poleSitterQualifyingTime = qualifying.QualifyingResults[0].Q1;
                        secondDriverQualifyingTime = qualifying.QualifyingResults[1].Q1;
                    }

                    var gapToSecond = _timeHelper.GetTimeDifference(poleSitterQualifyingTime, secondDriverQualifyingTime);
                    var newPoleInformationModel = new PoleInformationModel { CircuitName = circuitName, GapToSecond = gapToSecond };

                    lock (lockObject)
                    {
                        var newPolesByYearModel = new PolesByYearModel { Year = year, PoleInformation = new List<PoleInformationModel> { newPoleInformationModel } };

                        if (!driversPoles.Where(driver => driver.Name == poleSitter).Any())
                        {
                            var newPolesModel = new PolesModel { Name = poleSitter, PolesByYear = new List<PolesByYearModel> { newPolesByYearModel } };
                            driversPoles.Add(newPolesModel);
                        }
                        else
                        {
                            var driver = driversPoles.Where(driver => driver.Name == poleSitter).First();

                            if (!driver.PolesByYear.Where(model => model.Year == year).Any())
                            {
                                driver.PolesByYear.Add(newPolesByYearModel);
                            }
                            else
                            {
                                driver.PolesByYear.Where(model => model.Year == year).First().PoleInformation.Add(newPoleInformationModel);
                            }
                        } 
                    }
                }
            });

            return driversPoles;
        }

        public List<PolesModel> GetPoleSittersConstructors(int from, int to)
        {
            var constructorsPoles = new List<PolesModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var qualifyings = _qualifyingsDataAccess.GetQualifyingsFrom(year);

                foreach (var qualifying in qualifyings)
                {
                    var poleSitter = _nameHelper.GetConstructorName(qualifying.QualifyingResults[0].Constructor);
                    var circuitName = qualifying.Circuit.circuitName;

                    string poleSitterQualifyingTime;
                    string secondDriverQualifyingTime;
                    if (qualifying.QualifyingResults[0].Q3 != null && qualifying.QualifyingResults[1].Q3 != null)
                    {
                        poleSitterQualifyingTime = qualifying.QualifyingResults[0].Q3;
                        secondDriverQualifyingTime = qualifying.QualifyingResults[1].Q3;
                    }
                    else if (qualifying.QualifyingResults[0].Q2 != null && qualifying.QualifyingResults[1].Q2 != null)
                    {
                        poleSitterQualifyingTime = qualifying.QualifyingResults[0].Q2;
                        secondDriverQualifyingTime = qualifying.QualifyingResults[1].Q2;
                    }
                    else
                    {
                        poleSitterQualifyingTime = qualifying.QualifyingResults[0].Q1;
                        secondDriverQualifyingTime = qualifying.QualifyingResults[1].Q1;
                    }

                    var gapToSecond = _timeHelper.GetTimeDifference(poleSitterQualifyingTime, secondDriverQualifyingTime);
                    var newPoleInformationModel = new PoleInformationModel { CircuitName = circuitName, GapToSecond = gapToSecond };

                    lock (lockObject)
                    {
                        var newPolesByYearModel = new PolesByYearModel { Year = year, PoleInformation = new List<PoleInformationModel> { newPoleInformationModel } };

                        if (!constructorsPoles.Where(constructor => constructor.Name == poleSitter).Any())
                        {
                            var newPolesModel = new PolesModel { Name = poleSitter, PolesByYear = new List<PolesByYearModel> { newPolesByYearModel } };
                            constructorsPoles.Add(newPolesModel);
                        }
                        else
                        {
                            var constructor = constructorsPoles.Where(constructor => constructor.Name == poleSitter).First();

                            if (!constructor.PolesByYear.Where(model => model.Year == year).Any())
                            {
                                constructor.PolesByYear.Add(newPolesByYearModel);
                            }
                            else
                            {
                                constructor.PolesByYear.Where(model => model.Year == year).First().PoleInformation.Add(newPoleInformationModel);
                            }
                        } 
                    }
                }
            });

            return constructorsPoles;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersDrivers(int from, int to)
        {
            var uniquePoleSittersDrivers = new List<UniqueSeasonPoleSittersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year =>
            {
                var qualifyings = _qualifyingsDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, QualificationsCount = qualifyings.Count, PoleSitters = new List<string>() };

                foreach (var qualifying in qualifyings)
                {
                    var poleSitter = _nameHelper.GetDriverName(qualifying.QualifyingResults[0].Driver);

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonPoleSittersModel.PoleSitters.Where(winner => winner == poleSitter).Any())
                        {
                            newUniqueSeasonPoleSittersModel.PoleSitters.Add(poleSitter);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniquePoleSittersDrivers.Add(newUniqueSeasonPoleSittersModel); 
                }
            });

            return uniquePoleSittersDrivers;
        }

        public List<UniqueSeasonPoleSittersModel> GetUniquePoleSittersConstructors(int from, int to)
        {
            var uniquePoleSittersConstructors = new List<UniqueSeasonPoleSittersModel>(to - from + 1);
            var lockObject = new object();
            var lockAdd = new object();

            Parallel.For(from, to + 1, year => 
            {
                var qualifyings = _qualifyingsDataAccess.GetQualifyingsFrom(year);

                var newUniqueSeasonPoleSittersModel = new UniqueSeasonPoleSittersModel { Season = year, QualificationsCount = qualifyings.Count, PoleSitters = new List<string>() };

                foreach (var qualifying in qualifyings)
                {
                    var winnerName = _nameHelper.GetConstructorName(qualifying.QualifyingResults[0].Constructor);

                    lock (lockObject)
                    {
                        if (!newUniqueSeasonPoleSittersModel.PoleSitters.Where(winner => winner == winnerName).Any())
                        {
                            newUniqueSeasonPoleSittersModel.PoleSitters.Add(winnerName);
                        } 
                    }
                }

                lock (lockAdd)
                {
                    uniquePoleSittersConstructors.Add(newUniqueSeasonPoleSittersModel); 
                }
            });

            return uniquePoleSittersConstructors;
        }
    }
}
