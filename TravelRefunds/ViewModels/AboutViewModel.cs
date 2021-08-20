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
        public LocalizedString AboutPageTitle { get; } = new(() => AppStrings.AboutPageTitle);
        public LocalizedString AboutPresentationTitle { get; } = new(() => AppStrings.AboutPresentationTitle);
        public LocalizedString AboutPresentationText { get; } = new(() => AppStrings.AboutPresentationText);
        public LocalizedString LearnMore { get; } = new(() => AppStrings.LearnMore);
        public LocalizedString SignalABug { get; } = new(() => AppStrings.SignalABug);
        public LocalizedString WhatImplemented { get; } = new(() => AppStrings.WhatImplemented);
        public LocalizedString AwesomeIcons { get; } = new(() => AppStrings.AwesomeIcons);
        public LocalizedString TravelApiDesc { get; } = new(() => AppStrings.TravelApiDesc);
        public LocalizedString MonkeyBarrelsDesc { get; } = new(() => AppStrings.MonkeyBarrelsDesc);
        public LocalizedString LottieDesc { get; } = new(() => AppStrings.LottieDesc);
        #endregion

        public AboutViewModel()
        {
            Title = AboutPageTitle.Localized;
        }

        public ICommand OpenWebCommand { get; } = new Command<string>(async (arg) => await Browser.OpenAsync(arg));
    }
}