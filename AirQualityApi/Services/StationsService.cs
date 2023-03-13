using Microsoft.AspNetCore.Mvc;
using AirQualityApi.Models;
using AirQualityApi.Services;

namespace AirQualityApi.Services
{
    public class StationsService : IStationsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StationsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<List<Station>> GetAllStations()
        {
            var client = _httpClientFactory.CreateClient("stationsclient");
            var apiResponse = await client.GetFromJsonAsync<List<Station>>("station/findAll");

            return apiResponse;
        }

        public async Task<List<Sensor>> GetSensorDataById(int stationId)
        {
            var client = _httpClientFactory.CreateClient("stationsclient");
            var apiResponse = await client.GetFromJsonAsync<List<Sensor>>($"station/sensors/{stationId}");

            return apiResponse;
        }

        public async Task<List<StationWithParams>> GetStationAndSensorDataByStationName(string stationName)
        {

            var client = _httpClientFactory.CreateClient("stationsclient");
            var stationResponse = await client.GetFromJsonAsync<List<Station>>("station/findAll");

            List<Station> filteredStations = stationResponse.FindAll(s => s.StationName.Contains(stationName, StringComparison.InvariantCultureIgnoreCase)); 

            List<StationWithParams> stationsWithParams = new List<StationWithParams>();

            foreach (var station in filteredStations)
            {
                var sensorResponse = await client.GetFromJsonAsync<List<Sensor>>($"station/sensors/{station.Id}");

                List<Param> paramsList = new List<Param>();
                foreach (var sensor in sensorResponse)
                {
                    paramsList.Add(new Param
                    {
                        ParamName = sensor.Param.ParamName,
                        ParamFormula = sensor.Param.ParamFormula
                    });
                }

                stationsWithParams.Add(new StationWithParams
                {
                    StationName = station.StationName,
                    StationId = station.Id,
                    Param = paramsList
                });

            }
            return stationsWithParams;
        }
    }
}

