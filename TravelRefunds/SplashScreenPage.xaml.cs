using Xamarin.Forms;

namespace TravelRefunds
{
    public partial class SplashScreenPage : ContentPage
    {
        public SplashScreenPage()
        {
            InitializeComponent();
        }

        async void AnimationView_OnFinishedAnimation(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//CalculatorPage");
        }
    }
}
