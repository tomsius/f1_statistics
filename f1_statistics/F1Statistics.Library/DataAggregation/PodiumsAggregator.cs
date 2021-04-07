using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Statistics.Library.DataAggregation
{
    public class PodiumsAggregator : IPodiumsAggregator
    {
        private readonly IResultsDataAccess _resultsDataAccess;

        public PodiumsAggregator(IResultsDataAccess resultsDataAccess)
        {
            _resultsDataAccess = resultsDataAccess;
        }

        public List<PodiumsModel> GetDriversPodiums(int from, int to)
        {
            var driversPodiums = new List<PodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var circuitName = race.Circuit.circuitName;

                    for (int i = 0; i < 3; i++)
                    {
                        var podiumFinisher = $"{race.Results[i].Driver.givenName} {race.Results[i].Driver.familyName}";
                        var gridPosition = int.Parse(race.Results[i].grid);
                        var podiumPosition = int.Parse(race.Results[i].position);
                        var newPodiumInformationModel = new PodiumInformationModel { CircuitName = circuitName, PodiumPosition = podiumPosition, GridPosition = gridPosition };

                        lock (lockObject)
                        {
                            var newPodiumsByYearModel = new PodiumsByYearModel { Year = year, PodiumCount = 1 };

                            if (!driversPodiums.Where(driver => driver.Name == podiumFinisher).Any())
                            {
                                var newPodiumsModel = new PodiumsModel { Name = podiumFinisher, PodiumsByYear = new List<PodiumsByYearModel> { newPodiumsByYearModel }, PodiumInformation = new List<PodiumInformationModel> { newPodiumInformationModel } };
                                driversPodiums.Add(newPodiumsModel);
                            }
                            else
                            {
                                var driver = driversPodiums.Where(driver => driver.Name == podiumFinisher).First();

                                if (!driver.PodiumsByYear.Where(model => model.Year == year).Any())
                                {
                                    driver.PodiumsByYear.Add(newPodiumsByYearModel);
                                }
                                else
                                {
                                    driver.PodiumsByYear.Where(model => model.Year == year).First().PodiumCount++;
                                }

                                driver.PodiumInformation.Add(newPodiumInformationModel);
                            }
                        } 
                    }
                }
            });

            return driversPodiums;
        }

        public List<PodiumsModel> GetConstructorsPodiums(int from, int to)
        {
            var constructorsPodiums = new List<PodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    var circuitName = race.Circuit.circuitName;

                    for (int i = 0; i < 3; i++)
                    {
                        var podiumFinisher = $"{race.Results[i].Constructor.name}";
                        var gridPosition = int.Parse(race.Results[i].grid);
                        var podiumPosition = int.Parse(race.Results[i].position);
                        var newPodiumInformationModel = new PodiumInformationModel { CircuitName = circuitName, PodiumPosition = podiumPosition, GridPosition = gridPosition };


                        lock (lockObject)
                        {
                            var newPodiumsByYearModel = new PodiumsByYearModel { Year = year, PodiumCount = 1 };
                            
                            if (!constructorsPodiums.Where(constructor => constructor.Name == podiumFinisher).Any())
                            {
                                var newPodiumsModel = new PodiumsModel { Name = podiumFinisher, PodiumsByYear = new List<PodiumsByYearModel> { newPodiumsByYearModel }, PodiumInformation = new List<PodiumInformationModel> { newPodiumInformationModel } };
                                constructorsPodiums.Add(newPodiumsModel);
                            }
                            else
                            {
                                var constructor = constructorsPodiums.Where(constructor => constructor.Name == podiumFinisher).First();

                                if (!constructor.PodiumsByYear.Where(model => model.Year == year).Any())
                                {
                                    constructor.PodiumsByYear.Add(newPodiumsByYearModel);
                                }
                                else
                                {
                                    constructor.PodiumsByYear.Where(model => model.Year == year).First().PodiumCount++;
                                }

                                constructor.PodiumInformation.Add(newPodiumInformationModel);
                            }
                        }
                    }
                }
            });

            return constructorsPodiums;
        }

        public List<SamePodiumsModel> GetSameDriversPodiums(int from, int to)
        {
            var sameDriversPodiums = new List<SamePodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    List<string> podiumFinishers = new List<string>(3);
                    var circuitName = race.Circuit.circuitName;

                    for (int i = 0; i < 3; i++)
                    {
                        var podiumFinisher = $"{race.Results[i].Driver.givenName} {race.Results[i].Driver.familyName}";
                        podiumFinishers.Add(podiumFinisher);
                    }

                    lock (lockObject)
                    {
                        if (IsUniquePodiumFinishers(sameDriversPodiums, podiumFinishers))
                        {
                            var newSamePodiumsModel = new SamePodiumsModel { PodiumFinishers = podiumFinishers, SamePodiumCount = 1, Circuits = new List<string> { circuitName } };
                            sameDriversPodiums.Add(newSamePodiumsModel);
                        }
                        else
                        {
                            FillSamePodiumModel(sameDriversPodiums, podiumFinishers, circuitName);
                        }
                    }
                }
            });

            return sameDriversPodiums;
        }

        private bool IsUniquePodiumFinishers(List<SamePodiumsModel> samePodiums, List<string> podiumFinishers)
        {
            foreach (var podium in samePodiums)
            {
                if (IsPodiumEqual(podium.PodiumFinishers, podiumFinishers))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsPodiumEqual(List<string> existingPodium, List<string> newPodium)
        {
            List<string> podium = new List<string>(existingPodium);

            foreach (var podiumFinisher in newPodium)
            {
                podium.Remove(podiumFinisher);
            }

            return podium.Count == 0;
        }

        private void FillSamePodiumModel(List<SamePodiumsModel> samePodiums, List<string> podiumFinishers, string circuitName)
        {
            foreach (var podium in samePodiums)
            {
                if (IsPodiumEqual(podium.PodiumFinishers, podiumFinishers))
                {
                    podium.SamePodiumCount++;
                    podium.Circuits.Add(circuitName);
                    break;
                }
            }
        }

        public List<SamePodiumsModel> GetSameConstructorsPodiums(int from, int to)
        {
            var sameConstructorsPodiums = new List<SamePodiumsModel>(to - from + 1);
            var lockObject = new object();

            Parallel.For(from, to + 1, year =>
            {
                var races = _resultsDataAccess.GetResultsFrom(year);

                foreach (var race in races)
                {
                    List<string> podiumFinishers = new List<string>(3);
                    var circuitName = race.Circuit.circuitName;

                    for (int i = 0; i < 3; i++)
                    {
                        var podiumFinisher = $"{race.Results[i].Constructor.name}";
                        podiumFinishers.Add(podiumFinisher);
                    }

                    lock (lockObject)
                    {
                        if (IsUniquePodiumFinishers(sameConstructorsPodiums, podiumFinishers))
                        {
                            var newSamePodiumsModel = new SamePodiumsModel { PodiumFinishers = podiumFinishers, SamePodiumCount = 1, Circuits = new List<string> { circuitName } };
                            sameConstructorsPodiums.Add(newSamePodiumsModel);
                        }
                        else
                        {
                            FillSamePodiumModel(sameConstructorsPodiums, podiumFinishers, circuitName);
                        }
                    }
                }
            });

            return sameConstructorsPodiums;
        }
    }
}
