namespace TravelRefunds
{
    using Xamarin.Forms;
    using TravelRefunds.Services;
    using MonkeyCache.SQLite;
    using Xamarin.Essentials;
    using System.Diagnostics;
    using TravelRefunds.Helpers;
    using System;
    using TravelRefunds.Views;
    using Xamarin.CommunityToolkit.Extensions;
    using System.Linq;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Barrel.ApplicationId = "io.danieletentoni.travelrefunds";
            Barrel.Current.EmptyExpired(); // Clean history.

            DependencyService.Register<TravelService>();
            // DependencyService.Register<MockDataStore>();

            try
            {
                AppActions.OnAppAction += AppActions_OnAppAction;
            }
            catch (FeatureNotSupportedException ex)
            {
                Debug.WriteLine($"{nameof(AppActions)} Exception: {ex}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(AppActions)} Generic Exception while hooking App Actions: {ex}", "App Actions");
            }

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            try
            {
                await AppActions.SetAsync(
                    new AppAction("calculate_from", "Calculate From", "Calculate distance from where you are now.", FontAwesomeIcons.Calculator),
                    new AppAction("calculate_to", "Calculate To", "Calculate distance to where you are now.", FontAwesomeIcons.Calculator),
                    new AppAction("app_info", "About", "Get application info", FontAwesomeIcons.Calculator));
            }
            catch (FeatureNotSupportedException ex)
            {
                Debug.WriteLine($"App Actions not supported: {ex.Message}", "App Actions");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(AppActions)} Generic Exception while generating App Actions: {ex}", "App Actions");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        async void AppActions_OnAppAction(object sender, AppActionEventArgs e)
        {
            // Don't handle events fired for old application instances
            // and cleanup the old instance's event handler
            if (Current != this && Current is App app)
            {
                AppActions.OnAppAction -= app.AppActions_OnAppAction;
                return;
            }

            if (e.AppAction.Id.Equals("app_info"))
            {
                await Device.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync($"//{nameof(AboutPage)}"));
                return;
            }

            string route = null;
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        var geocodeAddress =
                            $"AdminArea:       {placemark.AdminArea}\n" +
                            $"CountryCode:     {placemark.CountryCode}\n" +
                            $"CountryName:     {placemark.CountryName}\n" +
                            $"FeatureName:     {placemark.FeatureName}\n" +
                            $"Locality:        {placemark.Locality}\n" +
                            $"PostalCode:      {placemark.PostalCode}\n" +
                            $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                            $"SubLocality:     {placemark.SubLocality}\n" +
                            $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                            $"Thoroughfare:    {placemark.Thoroughfare}\n";

                        Console.WriteLine(geocodeAddress);
                    }

                    // TODO: Concat location.
                    route = e.AppAction.Id switch
                    {
                        "calculate_from" => "CalculatorPage?From=Cesena",
                        "calculate_to" => "CalculatorPage?To=Pesaro",
                        _ => "HistoryPage"
                    };
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Device.InvokeOnMainThreadAsync(async () => await Current.MainPage.DisplayToastAsync("Geolocation feature not supported, limited functionalities."));
                Debug.WriteLine($"Geolocation not supported: {fnsEx.Message}", "Xamarin.Essentials.Geolocation");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Device.InvokeOnMainThreadAsync(async () => await Current.MainPage.DisplayToastAsync("Geolocation feature not enabled, limited functionalities."));
                Debug.WriteLine($"Geolocation feature not enabled: {fneEx.Message}", "Xamarin.Essentials.Geolocation");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Device.InvokeOnMainThreadAsync(async () => await Current.MainPage.DisplayToastAsync("Geolocation permission rejected, limited functionalities."));
                Debug.WriteLine($"Geolocation permission rejected: {pEx.Message}", "Xamarin.Essentials.Geolocation");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Device.InvokeOnMainThreadAsync(async () => await Current.MainPage.DisplayToastAsync($"Geolocation generic error: {ex.Message}, limited functionalities."));
                Debug.WriteLine($"Geolocation generic error: {ex.Message}", "Xamarin.Essentials.Geolocation");
            }
            finally
            {
                route ??= "HistoryPage";
            }

            if (route != null)
            {
                // await Application.Current.MainPage.Navigation.PopToRootAsync();
                await Device.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync($"//{route}"));
            }
        }
    }
}
