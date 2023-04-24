using v4MobileApp.Models;

namespace v4MobileApp.Services;

public interface IThingSpeakService
{
    public Task<List<SensorData>> GetSensorDataAsync();

    public class FeedWrapper { }
}

