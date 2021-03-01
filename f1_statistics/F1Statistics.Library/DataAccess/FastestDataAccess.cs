using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess
{
    public class FastestDataAccess : IFastestDataAccess
    {
        public List<RacesDataResponse> GetFastestDriversFrom(int year)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/fastest/1/results.json?limit=50");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            return result.MRData.RaceTable.Races;
        }
    }
}
