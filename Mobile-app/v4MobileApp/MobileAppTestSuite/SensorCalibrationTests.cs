using v4MobileApp.Calibrator;
namespace MobileAppTestSuite;

public class SensorCalibrationTests
{
    // unit tests for validation of calibrator functions
    [Fact]
    public void CalibrateTemperature_ValidFloatValue_ReturnScaledValue()
    {
        // Arrange
        int expected = 5;
        float tempValue = 20.0f;
        // Act
        int result = SensorCalibrator.GetScaledTemperatureValue(tempValue);
        // Assert - should return a scaled value of temperature by dividing by 4
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalibrateHumidity_ValidFloatValue_ReturnScaledValue()
    {
        // Arrange
        int expected = 5;
        float humidityValue = 50.0f;
        // Act
        int result = SensorCalibrator.GetScaledHumidityValue(humidityValue);
        // Assert - should return a scaled value of temperature by dividing by 10
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalibrateAirQuality_ValidFloatValue_ReturnScaledValue()
    {
        // Arrange
        int expected = 5;
        float airQualityValue = 700.0f;
        // Act
        int result = SensorCalibrator.GetScaledAirQualityValue(airQualityValue);
        // Assert - should return a scaled value of temperature by dividing by 120
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalibratePressure_ValidFloatValue_ReturnScaledValue()
    {
        // Arrange
        int expected = 5;
        float pressureValue = 1000.0f;
        // Act
        int result = SensorCalibrator.GetScaledPressureValue(pressureValue);
        // Assert - should return a scaled value of temperature by dividing by 200
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalibrateSound_ValidFloatValue_ReturnScaledValue()
    {
        // Arrange
        int expected = 5;
        float soundValue = 125.0f;
        // Act
        int result = SensorCalibrator.GetScaledSoundValue(soundValue);
        // Assert - 
        Assert.Equal(expected, result); // minus 100 to put range as 0-50 for easier calculation... then divide by 5 to get a scale from 1-10
    }

    [Fact]
    public void TestGetSCalibrateLight_ValidFloatValue_ReturnScaledValuecaledLightValue()
    {
        // Arrange
        int expected = 5;
        float pressureValue = 256.0f;
        // Act
        int result = SensorCalibrator.GetScaledLightValue(pressureValue);
        // Assert - should return a scaled value of temperature by dividing by 200
        Assert.Equal(expected, result); // max(511) - sensor value (to get reverse value range) then / max to get % value, then * 10 to get range 1-10
    }
}