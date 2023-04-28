
using v4MobileApp.Services;
using v4MobileApp.Models;
using RestSharp;

namespace MobileAppTestSuite;

public class ThingSpeakServiceTests
{

    private ThingSpeakService thingSpeakService;

    public ThingSpeakServiceTests()
    {
        thingSpeakService = new ThingSpeakService();
    }
    
    [Fact]
    public void ThingSpeakService_f_CreatesRestClient()
    {
        // Arrange
        // New instande of ThingSpeakService class created in constructor

        // Act
        var thingSpeakService = new ThingSpeakService();

        // Assert
        Assert.NotNull(thingSpeakService);
    }

    [Fact]
    public async Task GetSensorDataAsync_ShouldReturnSensorData()
    {
        // Checks that the service returns a list of sensor data objects and that it's not empty

        // Arrange
        // New instande of ThingSpeakService class created in constructor

        // Act
        var sensorData = await thingSpeakService.GetSensorDataAsync();

        // Assert
        Assert.NotNull(sensorData);
        Assert.IsType<List<SensorData>>(sensorData);
        Assert.NotEmpty(sensorData);
    }

    [Fact]
    public async Task GetSensorDataAsync_ShouldThrowException_WhenApiResponseIsError()
    {
        // Arrange
        var service = new ThingSpeakService();

        // Act
        Func<Task> act = async () => await service.GetSensorDataAsync();

        // Assert
        var ex = await Assert.ThrowsAsync<Exception>(act);
        Assert.Contains("Error retrieving sensor data from ThingSpeak API:", ex.Message);
    }


}
