using v4MobileApp.Models;

namespace v4MobileApp.Services;

public interface IThingSpeakService
{
    Task<List<SensorData>> GetSensorDataAsync();
}