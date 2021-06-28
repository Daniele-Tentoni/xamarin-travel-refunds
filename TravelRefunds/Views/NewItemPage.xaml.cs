using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TravelRefunds.Models;
using TravelRefunds.ViewModels;

namespace TravelRefunds.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new object(); // new NewItemViewModel();
        }
    }
}