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
    public partial class BarPage : ContentPage
    {
        public BarViewModel ViewModel { get; set; }

        public BarPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new BarViewModel();

            BarListView.ItemsSource = ViewModel.Bar;
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
            var clickedEvent = e.Item as Event;
            if (clickedEvent == null)
                return;

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new DetailEventPage(new DetailViewModel(clickedEvent)));
        }
    }
}
