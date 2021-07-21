using Xamarin.UITest;

namespace TravelRefunds.UITests
{
    public class AppInitializer
    {
        /*
         * Is a good idea to read some articles before start: https://trailheadtechnology.com/using-the-xamarin-repl-tool/?utm_campaign=Weekly%2BXamarin&utm_medium=email&utm_source=Weekly_Xamarin_312
         */
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