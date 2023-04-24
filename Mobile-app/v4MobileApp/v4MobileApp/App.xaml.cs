namespace v4MobileApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new FlyoutMenuPage();
	}
}
