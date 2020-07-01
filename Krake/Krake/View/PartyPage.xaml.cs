using Krake.Object;
using Krake.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krake.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyPage : ContentPage
    {
        private PartyViewModel ViewModel { get; }
        public PartyPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new PartyViewModel();

            PartyListView.ItemsSource = ViewModel.Party;
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
