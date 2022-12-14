using Krake.Object;
using Krake.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krake.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailLocationPage : ContentPage
    {
        DetailViewModel viewModel;
        private double _lastScroll = 0;

        public DetailLocationPage(DetailViewModel vM)
        {
            InitializeComponent();
            BindingContext = this.viewModel = vM;

            viewModel.eventsFromLocation.CollectionChanged += EventsFromLocation_CollectionChanged;
            
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var clickedEvent = ItemsListView.SelectedItem as Event;
            //var clickedEvent = e.Item as Event;
            if (clickedEvent == null)
                return;

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new DetailEventPage(new DetailViewModel(clickedEvent)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            ParalaxScroll.Scrolled += OnParalaxScrolled;  
            
        }

        protected override void OnDisappearing()
        {
            ParalaxScroll.Scrolled -= OnParalaxScrolled;
        }

        private void EventsFromLocation_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(ItemsListView.IsVisible == false)
            {
                ItemsListView.IsVisible = true;
            }
            ItemsListView.HeightRequest = 55 * viewModel.eventsFromLocationCount + 55 * viewModel.eventsFromLocation.Count;
        }

        private void OnParalaxScrolled(object sender, ScrolledEventArgs e)
        {
            double scale = 1;
            double translation = 0;

            if (_lastScroll < e.ScrollY)
            {
                translation = 0 - ((e.ScrollY / 2));
                scale = ((translation * -2) / 1000) + 1;
                if (translation <= -100)
                {
                    translation = -100;
                    scale = 1.2;
                }
            }
            else
            {
                translation = 0 + ((e.ScrollY / 2));
                if (translation > 5)
                {
                    translation = 0;
                    scale = ((translation * -2) / 1000) + 1;
                }
            }

            ParalaxScroll.TranslateTo(ParalaxScroll.TranslationX, translation);
            EventImage.ScaleTo(scale, 250, Easing.Linear);
            
            _lastScroll -= e.ScrollY;
        }
    }
}