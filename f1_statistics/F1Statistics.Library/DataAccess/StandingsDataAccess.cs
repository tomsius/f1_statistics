using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess
{
    public class StandingsDataAccess : IStandingsDataAccess
    {
        public List<DriverStandingsDataResponse> GetDriverStandingsFrom(int year)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/driverStandings.json?limit=200");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            if (result.MRData.StandingsTable.StandingsLists.Count == 0)
            {
                return new List<DriverStandingsDataResponse>();
            }

            return result.MRData.StandingsTable.StandingsLists[0].DriverStandings;
        }

        public List<ConstructorStandingsDataResponse> GetConstructorStandingsFrom(int year)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/constructorStandings.json?limit=200");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            if (result.MRData.StandingsTable.StandingsLists.Count == 0)
            {
                return new List<ConstructorStandingsDataResponse>();
            }

            return result.MRData.StandingsTable.StandingsLists[0].ConstructorStandings;
        }

        public List<DriverStandingsDataResponse> GetDriverStandingsFromRace(int year, int round)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/{round}/driverStandings.json?limit=200");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            if (result.MRData.StandingsTable.StandingsLists.Count == 0)
            {
                return new List<DriverStandingsDataResponse>();
            }

            return result.MRData.StandingsTable.StandingsLists[0].DriverStandings;
        }

        public List<ConstructorStandingsDataResponse> GetConstructorStandingsFromRace(int year, int round)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/{round}/constructorStandings.json?limit=100");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            if (result.MRData.StandingsTable.StandingsLists.Count == 0)
            {
                return new List<ConstructorStandingsDataResponse>();
            }

            return result.MRData.StandingsTable.StandingsLists[0].ConstructorStandings;
        }
    }
}
