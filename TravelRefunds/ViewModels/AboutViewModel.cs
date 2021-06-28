using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TravelRefunds.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/Daniele-Tentoni/xamarin-travel-refunds"));
        }

        public ICommand OpenWebCommand { get; }
    }
}