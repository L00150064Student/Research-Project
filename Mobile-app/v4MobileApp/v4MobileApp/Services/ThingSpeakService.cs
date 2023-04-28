using Newtonsoft.Json;
using RestSharp;

using v4MobileApp.Models;

namespace v4MobileApp.Services
{
    public class ThingSpeakService : IThingSpeakService
    {
        RestClient client;
        public ThingSpeakService()
        {
            client = new RestClient("https://api.thingspeak.com");
        }

        public async Task<List<SensorData>> GetSensorDataAsync()
        {
            try
            {
                // Check for network connectivity
                var networkAccess = Connectivity.NetworkAccess;
                if (networkAccess != NetworkAccess.Internet)
                {
                    throw new Exception("No internet connection available.");
                }

                var request = new RestRequest("/channels/1954302/feeds.json", RestSharp.Method.Get);
                request.AddParameter("api_key", "Y4IBOATVHIKUEU43");
                request.AddParameter("results", 1);
                var response = await client.ExecuteAsync(request);
                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Content: {response.Content}");

                if (!response.IsSuccessful)
                {
                    throw new Exception($"Error retrieving sensor data from ThingSpeak API: {response.ErrorMessage}");
                }

                var wrapper = JsonConvert.DeserializeObject<FeedWrapper>(response.Content);
                var data = wrapper.feeds;
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetSensorDataAsync: {ex.Message}");
                throw; // throw the exception
            }
        }

        public class FeedWrapper
        {
            public List<SensorData> feeds { get; set; }
        }
    }
}
