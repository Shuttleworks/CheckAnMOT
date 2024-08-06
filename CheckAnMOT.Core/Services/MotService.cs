
using CheckAnMOT.Core.Models;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;

namespace CheckAnMOT.Core.Services
{
    public class MotService : IMotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey = "";
        private readonly string _endPoint = "?registration=";

        public MotService(HttpClient httpClient) {
            _httpClient = httpClient;
            _apiUrl = GetAPIUrl();
            _apiKey = GetAPIKey();
        
        }

        public string GetAPIUrl()
        {
            string APIUrl = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests";
            return APIUrl;
        }

        public string GetAPIKey()
        {
            string APIKey = "";
            return APIKey;
        }

        public async Task<ResultDTO> GetVehicleMot(string numberplate)
        {
            ResultDTO output = new ResultDTO();

            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_apiUrl))
            {
                output.StatusCode = HttpStatusCode.ServiceUnavailable;
                output.ErrorMessage = "Connection settings unavailable";

                return output;
            }

            if(_httpClient != null)
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);

                var uri = $"{_apiUrl}{_endPoint}{numberplate}";
                var response = await _httpClient.GetAsync(uri);

                if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return await GetErrorInfo(response);
                }

                if (response.IsSuccessStatusCode)
                {
                    return await GetVehicleData(response);
                }

            } 
                
            output.StatusCode = HttpStatusCode.ServiceUnavailable;
            output.ErrorMessage = "No HttpClient available";

            return output;
            
        }

        private async Task<ResultDTO> GetVehicleData(HttpResponseMessage response)
        {
            ResultDTO output = new ResultDTO();

            output.StatusCode = response.StatusCode;

            List<Vehicle>? vehicles = await response.Content.ReadFromJsonAsync<List<Vehicle>>(); //Gotcha, the results are returned within an array.

            if (vehicles != null && vehicles.Count > 0)
            {
                Vehicle theVehicle = vehicles.First();

                output.Registration = theVehicle.Registration ?? "";
                output.Make = theVehicle.Make ?? "";
                output.Model = theVehicle.Model ?? "";
                output.Colour = theVehicle.PrimaryColour ?? "";

                var cultureInfo = new CultureInfo("en-GB");

                if (theVehicle?.MotTests.Count > 0)
                {
                    var latestMOT = theVehicle.MotTests.First();

                    if (!String.IsNullOrEmpty(latestMOT.ExpiryDate))
                    {
                        output.MotExpiryDate = DateTime.Parse(latestMOT.ExpiryDate, cultureInfo);
                    }

                    int fails = theVehicle.MotTests.Where(x => x.TestResult == "FAILED").Count();
                    output.NumFailedMots = fails;

                }
                else
                {
                    //in the case of a car under 3 years with no MOT history, the mot expiry appears in the outer json object
                    if (theVehicle?.MotTestExpiryDate != null)
                    {
                        output.MotExpiryDate = DateTime.Parse(theVehicle.MotTestExpiryDate, cultureInfo);
                    }

                }
                return output;
            } else
            {
                output.ErrorMessage = "Unable to retrieve vehicle data";
                return output;
            }
        }

        private async Task<ResultDTO> GetErrorInfo(HttpResponseMessage response)
        {
            ResultDTO output = new ResultDTO();
            output.StatusCode = response.StatusCode;
            ApiError? apiError = await response.Content.ReadFromJsonAsync<ApiError>();

            if (apiError != null)
            {
                output.ErrorMessage = apiError.ErrorMessage;
            } else
            {
                output.ErrorMessage = "An unknown error occured";
            }

            return output;
        }
    }
}
