using System.Diagnostics;
using Lottie.Forms;
using TravelRefunds.ViewModels;
using Xamarin.Forms;

namespace TravelRefunds.Views
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
            BindingContext = new CalculatorViewModel();
        }

        void AnimatedTickerView_OnFinishedAnimation(object sender, System.EventArgs e)
        {
            var view = (AnimationView)sender;
            view.IsVisible = false;
            Debug.WriteLine("Animation ticker view finished.", "Lottie Animation");
        }
    }
}
