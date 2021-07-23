namespace TravelRefunds.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using MvvmHelpers;
    using TravelRefunds.Models;
    using TravelRefunds.Services;
    using TravelRefunds.Views;
    using Xamarin.Forms;

    public class HistoryViewModel : BaseViewModel
    {
        private readonly TravelService travelService = DependencyService.Get<TravelService>();
        private TravelQuery _selectedTravelQuery;

        public ObservableCollection<TravelQuery> TravelQueries { get; }
        public Command LoadTravelQueriesCommand { get; }
        public Command AddTravelQuerieCommand { get; }
        public Command<TravelQuery> TravelQueryTapped { get; }

        public HistoryViewModel()
        {
            Title = "History";
            TravelQueries = new ObservableCollection<TravelQuery>();
            LoadTravelQueriesCommand = new Command(async () => await ExecuteLoadTravelQueries());

            TravelQueryTapped = new Command<TravelQuery>(OnTravelQuerySelected);

            AddTravelQuerieCommand = new Command(OnAddItem);
        }

        private async Task ExecuteLoadTravelQueries()
        {
            IsBusy = true;

            try
            {
                TravelQueries.Clear();
                var queries = await travelService.GetTravelHistoryAsync();
                foreach (var query in queries)
                {
                    TravelQueries.Add(query);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(CalculatorPage)}");
        }

        public TravelQuery SelectedItem
        {
            get => _selectedTravelQuery;
            set
            {
                SetProperty(ref _selectedTravelQuery, value);
                OnTravelQuerySelected(value);
            }
        }

        async void OnTravelQuerySelected(TravelQuery item)
        {
            if (item == null) { return; }

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?from={item.From}&to={item.To}"); // {nameof(ItemDetailViewModel.ItemId)}={item.}");
        }
    }
}
