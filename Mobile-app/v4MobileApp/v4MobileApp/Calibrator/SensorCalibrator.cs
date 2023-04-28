
namespace v4MobileApp.Calibrator;

public static class SensorCalibrator
{
    public static int GetScaledTemperatureValue(float sensorValue)
    {
        int scaledValue = 0;
        int value = Convert.ToInt32(sensorValue);

        // Scale the value to a range between 1-10
        if (value < 0)
            scaledValue = 1;
        else if (value > 40)
            scaledValue = 10;
        else
            scaledValue = value / 4;

        return scaledValue;
    }

    public static int GetScaledHumidityValue(float sensorValue)
    {
        // humidity should fall between 40-60, so include a range of 30-70 to reflect values of discomfort
        int scaledValue = 0;
        int min = 30;
        int max = 70;
        int value = Convert.ToInt32(sensorValue);

        // Scale the value to a range between 1-10
        if (value < min)
            scaledValue = 1;
        else if (value > max)
            scaledValue = 10;
        else
            scaledValue = (value - min) / 4; // minus 30 to put range as 0-30... then divide by 3 to get a scale from 1-10

        return scaledValue;
    }

    public static int GetScaledAirQualityValue(float sensorValue)
    {
        int min = 200;
        int max = 1200;
        int value = Convert.ToInt32(sensorValue);

        // Scale the value to a range between 1-10
        if (value < min)
            return 1;
        else if (value > max)
            return 10;
        else
            return (value - min) / 100;
    }

    public static int GetScaledPressureValue(float sensorValue)
    {
        // air quality should fall between 400-1000, so include a range of 200-1200 to reflect values of discomfort
        int scaledValue = 0;
        int min = 950;
        int max = 1050;
        int value = Convert.ToInt32(sensorValue);

        // Scale the value to a range between 1-10
        if (value < min)
            scaledValue = 1;
        else if (value > max)
            scaledValue = 10;
        else
            scaledValue = (value - min) / 10; // minus 950 to put range as 0-100 for easier calculation... then divide by 10 to get a scale from 1-10

        return scaledValue;
    }

    public static int GetScaledSoundValue(float sensorValue)
    {
        int scaledValue = 0;
        int min = 100;
        int max = 150;
        int value = Convert.ToInt32(sensorValue);

        // Scale the value to a range between 1-10
        if (value < min)
            scaledValue = 1;
        else if (value > max)
            scaledValue = 10;
        else
            scaledValue = (value - min) / 5; // minus 100 to put range as 0-50 for easier calculation... then divide by 5 to get a scale from 1-10

        return scaledValue;
    }

    public static int GetScaledLightValue(float sensorValue)
    {
        float max = 511;
        // max - sensor value (to get reverse value range) then / max to get % value, then * 10 to get range 1-10
        float scaledValue = (max - sensorValue) / max * 10;

        return Convert.ToInt32(scaledValue);
    }
}
