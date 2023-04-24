using Android.App;
using Android.Content.PM;
using Android.OS;

namespace v4MobileApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.SetStatusBarColor(Android.Graphics.Color.Blue);
        Window.SetNavigationBarColor(Android.Graphics.Color.Blue);

        base.OnCreate(savedInstanceState);
    }
}
