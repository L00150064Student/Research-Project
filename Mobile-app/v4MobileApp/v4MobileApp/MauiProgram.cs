using Microsoft.Extensions.Logging;
using v4MobileApp.Services;
using v4MobileApp.ViewModels;
using v4MobileApp.Models;
using v4MobileApp.Pages;

namespace v4MobileApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        // Register the services
        builder.Services.AddTransient<IThingSpeakService, ThingSpeakService>();

        // Register the view model with the dependency injection container
        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddTransient<SensorViewModel>();

        // Register the Sensor page
        builder.Services.AddTransient<SensorPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
