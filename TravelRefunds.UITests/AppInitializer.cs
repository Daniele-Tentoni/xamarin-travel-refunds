using Xamarin.UITest;

namespace TravelRefunds.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("io.github.danieletentoni.travel_refunds").EnableLocalScreenshots().StartApp();
            }

            return ConfigureApp.iOS.InstalledApp("io.github.danieletentoni.travel_refunds").EnableLocalScreenshots().StartApp();
        }
    }
}