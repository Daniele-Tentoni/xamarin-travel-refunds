namespace TravelRefunds.ViewModels
{
    using MvvmHelpers;
    using TravelRefunds.Resources.Localization;
    using Xamarin.CommunityToolkit.Helpers;

    public class ShellViewModel : BaseViewModel
    {
        #region LocalizedStrings
        public LocalizedString About { get; } = new(() => AppStrings.AboutPageTitle);
        public LocalizedString Calculator { get; } = new(() => AppStrings.Calculator);
        public LocalizedString History { get; } = new(() => AppStrings.History);
        #endregion
    }
}
