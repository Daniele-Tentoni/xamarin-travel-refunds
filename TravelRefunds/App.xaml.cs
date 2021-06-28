using Xamarin.Forms;
using TravelRefunds.Services;
using MonkeyCache.SQLite;
using Xamarin.Essentials;
using System.Diagnostics;
using TravelRefunds.Helpers;

namespace TravelRefunds
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Barrel.ApplicationId = "io.danieletentoni.travelrefunds";
            Barrel.Current.EmptyExpired(); // Clean history.

            DependencyService.Register<TravelService>();
            DependencyService.Register<MockDataStore>();

            try
            {
                AppActions.OnAppAction += AppActions_OnAppAction;
            }
            catch (FeatureNotSupportedException ex)
            {
                Debug.WriteLine($"{nameof(AppActions)} Exception: {ex}");
            }

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            try
            {
                await AppActions.SetAsync(
                    new AppAction("app_info", "About", "Get application info", FontAwesomeIcons.Calculator),
                    new AppAction("calculator", "Calculator", icon: FontAwesomeIcons.Calculator));
            }
            catch (FeatureNotSupportedException ex)
            {
                Debug.WriteLine($"App Actions not supported: {ex.Message}");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void AppActions_OnAppAction(object sender, AppActionEventArgs e)
        {
            // Don't handle events fired for old application instances
            // and cleanup the old instance's event handler
            if (Application.Current != this && Application.Current is App app)
            {
                AppActions.OnAppAction -= app.AppActions_OnAppAction;
                return;
            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                var route = e.AppAction.Id switch
                {
                    "calculator" => "CalculatorPage",
                    "app_info" => "AboutPage",
                    _ => "HistoryPage"
                };

                if (route != null)
                {
                    // await Application.Current.MainPage.Navigation.PopToRootAsync();
                    await Shell.Current.GoToAsync($"//{route}");
                }
            });
        }
    }
}
