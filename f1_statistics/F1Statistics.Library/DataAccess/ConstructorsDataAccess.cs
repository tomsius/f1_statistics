using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess
{
    public class ConstructorsDataAccess : IConstructorsDataAccess
    {
        public string GetDriverConstructor(int year, int round, string leadingDriverId)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/{round}/drivers/{leadingDriverId}/constructors.json");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            return result.MRData.ConstructorTable.Constructors[0].name;
        }
    }
}
