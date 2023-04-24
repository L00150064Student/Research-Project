using Newtonsoft.Json;
using v4MobileApp.Models;
using RestSharp;


namespace v4MobileApp.Services;

public class ThingSpeakService : IThingSpeakService
{
    RestClient client = new RestClient("https://api.thingspeak.com");
    List<SensorData> sensorDataList = new List<SensorData>();
    private const string API_KEY = "Y4IBOATVHIKUEU43";
    private const string CHANNEL_ID = "1954302";
    private const int NUMBER_OF_RESULTS = 3;

    // .Net HTTP client for READ request to ThingSpeak
    HttpClient _httpClient;
    public ThingSpeakService()
    {
        _httpClient = new HttpClient();
    }
    public class FeedWrapper
    {
        // the JSON response from ThingSpeak includes both metadata about the channel and an array of "feeds" which contains the actual sensor data.
        // In order to access the sensor data this Wrapper function will act as a container to accept the data
        public List<SensorData> feeds { get; set; }
    }
    public async Task<List<SensorData>> GetSensorDataAsync()
    {
        try
        {
            var request = new RestRequest("/channels/1954302/feeds.json", RestSharp.Method.Get);
            request.AddParameter("api_key", "Y4IBOATVHIKUEU43");
            request.AddParameter("results", 1);

            var response = await client.ExecuteAsync(request);

            if (response.ErrorException != null)
            {
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
}