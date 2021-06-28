using TravelRefunds.ViewModels;
using Xamarin.Forms;

namespace TravelRefunds.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }
    }
}