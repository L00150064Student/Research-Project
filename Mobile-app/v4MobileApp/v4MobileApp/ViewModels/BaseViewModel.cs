using CommunityToolkit.Mvvm.ComponentModel;

namespace v4MobileApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool _isBusy;

    [ObservableProperty]
    string _title;

    [ObservableProperty]
    string _selectedSensorMessageOutput;

    [ObservableProperty]
    string _dateCreatedAt;

    [ObservableProperty]
    int _scaledSensorValue;
    public bool IsNotBusy => !IsBusy;
}
