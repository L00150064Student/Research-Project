using Newtonsoft.Json;
using v4MobileApp.Models;
using RestSharp;


namespace v4MobileApp.Services;

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
            var request = new RestRequest("/channels/1954302/feeds.json", RestSharp.Method.Get);
            request.AddParameter("api_key", "Y4IBOATVHIKUEU43");
            request.AddParameter("results", 1);
            var response = await client.ExecuteAsync(request);

            if (response.ErrorException != null){
                throw new Exception($"Error retrieving sensor data from ThingSpeak API: {response.ErrorMessage}", response.ErrorException);
            }

            var wrapper = JsonConvert.DeserializeObject<FeedWrapper>(response.Content);
            var data = wrapper.feeds; // from the wrapper function
            return data;
        }
        catch (Exception ex)
        {
            // Handle the exception by displaying error message 
            Console.WriteLine($"Exception in GetSensorDataAsync: {ex.Message}");
            throw; // throw the exception
        }
    }
    public class FeedWrapper
    {
        // the JSON response from ThingSpeak includes both metadata about the channel and an array of "feeds" which contains the actual sensor data.
        // In order to access the sensor data this Wrapper function will act as a container to accept the data
        public List<SensorData> feeds { get; set; }
    }
}