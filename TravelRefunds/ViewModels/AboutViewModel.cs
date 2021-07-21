namespace TravelRefunds.ViewModels
{
    using System.Windows.Input;
    using MvvmHelpers;
    using TravelRefunds.Resources.Localization;
    using Xamarin.CommunityToolkit.Helpers;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class AboutViewModel : BaseViewModel
    {
        #region View LocalizedStrings
        public LocalizedString AboutPresentationTitle { get; } = new(() => AppStrings.AboutPresentationTitle);
        public LocalizedString AboutPresentationText { get; } = new(() => AppStrings.AboutPresentationText);
        #endregion

        public AboutViewModel()
        {
            Title = "About";
        }

        public ICommand OpenWebCommand { get; } = new Command(async () => await Browser.OpenAsync("https://github.com/Daniele-Tentoni/xamarin-travel-refunds"));
    }
}