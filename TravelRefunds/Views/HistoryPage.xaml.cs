namespace TravelRefunds.Views
{
    using TravelRefunds.ViewModels;
    using Xamarin.Forms;

    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel _viewModel;

        public HistoryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HistoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            // _viewModel.LoadTravelQueriesCommand.Execute(null);
        }
    }
}
