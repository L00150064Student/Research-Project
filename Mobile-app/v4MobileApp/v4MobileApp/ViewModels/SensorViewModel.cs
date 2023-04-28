using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using v4MobileApp.Services;
using v4MobileApp.Models;
using v4MobileApp.Calibrator;

namespace v4MobileApp.ViewModels;

public partial class SensorViewModel : BaseViewModel
{
    IThingSpeakService _thingspeakService;
    //IThingSpeakService _dataService;

    public ObservableCollection<SensorData> Sensors { get; set; } = new ObservableCollection<SensorData>();
    private string _selectedSensorMessageOutput;
    private int _sliderValue;

    [ObservableProperty]
    bool isRefreshing;

    public SensorViewModel(IThingSpeakService thingspeakService)
    {
        _thingspeakService = thingspeakService;
        //_dataService = dataService;
    }

    [RelayCommand]
    public async Task GetSensorsAsync()
    {
        // if we are busy just return
        if (IsBusy)
            return;

        Debug.WriteLine("getting sensor list");
        try
        {
            IsBusy = true;
            var sensors = await _thingspeakService.GetSensorDataAsync();

            if (sensors != null && sensors.Count > 0)
            {
                Sensors.Clear(); //remove previous data and get most recent values

                foreach (var item in sensors)
                {
                    Sensors.Add(item); // Add the item to the collection
                }

                UpdateSliderValue(); // Update the slider value based on the current title
            }
            else
            {
                // Display an alert if the data is empty
                await Shell.Current.DisplayAlert("Warning", "Sensor data is empty", "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            if (ex.Message == "No internet connection available.")
            {
                // if service detects no internet connection a catch is made and message displayed on sensorpage
                SelectedSensorMessageOutput = ex.Message;
            }
            else
            {
                // Display an alert if an exception is raised
                await Shell.Current.DisplayAlert("Error", "Unable to get sensor information.", "OK");
            }
        }
        finally
        {
            // Finish/clean up code in here
            IsBusy = false;
        }
    }



    public void UpdateSliderValue()
    {
        //SelectedSensorMessageOutput = "";
        //Title = "";
        //ScaledSensorValue = 0;
        // Get the title of the Shell item selected and assign it to observable property Title
        Title = Shell.Current.CurrentItem.CurrentItem.Title;

        var item = Sensors.LastOrDefault(); // get the last SensorData object in the collection

        // use switch statement to show the field information based on the Title selected
        // each field will call a static class SensorCalibrator to scale the values to a user friendly 1-10
        if (item != null) // make sure the collection is not empty
        {
            switch (Title)
            {
                case "Temperature":
                    // round to 1 decimal place and convert to string for observable property SelectedSensorValue
                    _selectedSensorMessageOutput = Math.Round(item.field1, 1).ToString();
                    _sliderValue = SensorCalibrator.GetScaledTemperatureValue(item.field1);
                    break;
                case "Humidity":
                    _selectedSensorMessageOutput = Math.Round(Convert.ToDouble(item.field2), 1).ToString();
                    _sliderValue = SensorCalibrator.GetScaledHumidityValue(item.field2);
                    break;
                case "Pressure":
                    _selectedSensorMessageOutput = Math.Round(Convert.ToDouble(item.field3), 1).ToString();
                    _sliderValue = SensorCalibrator.GetScaledPressureValue(item.field3);
                    break;
                case "Sound":
                    //_selectedSensorMessageOutput = Math.Round(Convert.ToDouble(item.field4), 1).ToString();
                    if (item.field8 == 1) // can only be 0 or 1 : 1 indicating noisy
                        _selectedSensorMessageOutput = "Noise disturbance detected - high noise levels detected.";
                    else
                        _selectedSensorMessageOutput = "No excessive noise detected.";
                    _sliderValue = SensorCalibrator.GetScaledSoundValue(item.field4);
                    break;
                case "Air_Quality":
                    _selectedSensorMessageOutput = Math.Round(Convert.ToDouble(item.field5), 1).ToString();
                    _sliderValue = SensorCalibrator.GetScaledAirQualityValue(item.field5);
                    break;
                case "Light":
                    //_selectedSensorMessageOutput = Math.Round(Convert.ToDouble(item.field6), 1).ToString();
                    if (item.field7 == 1) // can only be 0 or 1 : 1 indicating light disturbance
                        _selectedSensorMessageOutput = "Light disturbance detected - possible flickering bulb";
                    else
                        _selectedSensorMessageOutput = "No light disturbance detected.";
                    _sliderValue = SensorCalibrator.GetScaledLightValue(item.field6);
                    break;
            }
            
            SelectedSensorMessageOutput = _selectedSensorMessageOutput;
            ScaledSensorValue = _sliderValue;
            DateCreatedAt = item.created_at.ToString();
        }
    }

}
