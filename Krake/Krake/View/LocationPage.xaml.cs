using Krake.Object;
using Krake.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krake.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage
    {
        public LocationViewModel ViewModel { get; set; }

        public LocationPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new LocationViewModel();
        }

        protected override void OnAppearing()
        {
            if (ViewModel.GetReload())
            {
                ViewModel.Reload();
            }
            
            base.OnAppearing();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clickedLocation = e.Item as Location;
            if (clickedLocation == null)
                return;

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new DetailLocationPage(new DetailViewModel(null, clickedLocation)));
        }
    }
}
