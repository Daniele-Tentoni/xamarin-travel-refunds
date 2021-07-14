namespace TravelRefunds.ViewModels
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Lottie.Forms;
    using MvvmHelpers;
    using Refit;
    using TravelRefunds.Services;
    using Xamarin.Forms;

    [QueryProperty(nameof(From), nameof(From))]
    [QueryProperty(nameof(To), nameof(To))]
    public class CalculatorViewModel : BaseViewModel
    {
        private readonly TravelService travelService = DependencyService.Get<TravelService>();

        private string _from;
        public string From
        {
            get => _from;
            set => SetProperty(ref _from, value);
        }

        private string _to;
        public string To
        {
            get => _to;
            set => SetProperty(ref _to, value);
        }

        private string _result;
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        #region View LocalizedStrings
        public LocalizedString CalculateText { get; } = new(() => AppStrings.Calculate);
        public LocalizedString FromLocationPlaceholderText { get; } = new(() => AppStrings.FromLocation);
        #endregion

        public Command CalculateCommand { get; set; }

        public CalculatorViewModel()
        {
            Title = "Calculator";
            CalculateCommand = new Command<AnimationView>(async (animationView) => await ExecuteCalculate(animationView));
        }

        private async Task ExecuteCalculate(AnimationView animationView)
        {
            if (IsBusy) { return; }
            IsBusy = true;
            try
            {
                animationView.PlayAnimation();
                animationView.IsVisible = true;
                // https://api.geoapify.com/v1/routing?waypoints=44.136352,12.2422442|43.9098114,12.9131228&mode=drive&lang=it&apiKey=YOUR_API_KEY
                var res = await travelService.GetTravelAsync(From, To);
                Result = res;
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Accesso non autorizzato", "Rilevato accesso non autorizzato, verificare che le chiavi di accesso ai servizi web siano ancora valide.", "Bad..."));
                }
                else if (apiEx.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Destinazione non trovata", "Uno o più località non sono state trovate, verificare che i dati inseriti siano corretti.", "Bad..."));
                }
                else
                {
                    await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Errore HTTP sconosciuto", $"Rilevato errore non riconosciuto: {apiEx.StatusCode} {apiEx.ReasonPhrase}", "Bad..."));
                }
            }
            catch (Exception ex)
            {
                await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Errore sconosciuto", $"Rilevato errore non riconosciuto: {ex.Message}", "Bad..."));
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
