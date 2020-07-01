using Krake.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krake.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionPage : ContentPage
    {
        public SelectionViewModel ViewModel { get; set; }
        private bool firstTime;

        public SelectionPage(bool firstT = false)
        {
            InitializeComponent();

            firstTime = firstT;

            BindingContext = ViewModel = new SelectionViewModel();

            MyListView.ItemsSource = ViewModel.CityList;

            
                Title = "Standort: " + Preferences.Get("City", "");
            
            
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null || e.Item.ToString() == Preferences.Get("City", ""))
            {

                ((ListView)sender).SelectedItem = null;
                return;
            }

            Preferences.Set("City", e.Item.ToString());
            Title = "Standort: " + Preferences.Get("City", "");
            await DisplayAlert("Standort wurde geändert", Preferences.Get("City", "") + " ist jetzt die aktive Stadt.", "OK");
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            if (firstTime)
            {
                await Navigation.PopAsync();
            }
            else
            {
                this.ViewModel.SetReload();
            }

            
        }
    }
}