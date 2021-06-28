using System.ComponentModel;
using Xamarin.Forms;
using TravelRefunds.ViewModels;

namespace TravelRefunds.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}