using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAccess
{
    public class DriversDataAccess : IDriversDataAccess
    {
        public string GetDriverName(string driverId)
        {
            var client = new RestClient($"http://ergast.com/api/f1/drivers/{driverId}.json");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            if (result.MRData.DriverTable.Drivers.Count == 0)
            {
                return "";
            }

            return $"{result.MRData.DriverTable.Drivers[0].givenName} {result.MRData.DriverTable.Drivers[0].familyName}";
        }

        public List<DriverDataResponse> GetDriversFrom(int year)
        {
            var client = new RestClient($"http://ergast.com/api/f1/{year}/drivers.json?limit=200");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<MRDataResponse>(response.Content);

            return result.MRData.DriverTable.Drivers;
        }
    }
}
