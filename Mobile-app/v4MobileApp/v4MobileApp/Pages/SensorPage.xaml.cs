using v4MobileApp.ViewModels;
namespace v4MobileApp.Pages;

public partial class SensorPage : ContentPage
{
    public SensorPage() { }
	public SensorPage(SensorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.GetSensorsCommand.Execute(viewModel);
    }

}