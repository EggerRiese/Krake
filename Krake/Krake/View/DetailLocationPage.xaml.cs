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
            
            //ParalaxScroll.Scrolled += ScrollView_Scrolled;  
            
        }

        private void EventsFromLocation_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(ItemsListView.IsVisible == false)
            {
                ItemsListView.IsVisible = true;
            }
            ItemsListView.HeightRequest = 55 * viewModel.eventsFromLocationCount + 55 * viewModel.eventsFromLocation.Count;
        }

        protected override void OnDisappearing()
        {
            //ParalaxScroll.Scrolled -= ScrollView_Scrolled;
        }

        private void OnParalaxScrolled(object sender, ScrolledEventArgs e)
        {
            double fade = 1;
            double scale = 1;
            double translation = 0;
            double lastPosition = 0;

            if (_lastScroll < e.ScrollY)
            {
                translation = 0 - ((e.ScrollY / 2));
                fade = 0.8;
                scale = 1.2;
                if (translation <= -100)
                {
                    translation = -100;
                    fade = 0.8;
                    scale = 1.2;
                }
            }
            else
            {
                translation = 0 + ((e.ScrollY / 2));
                if (translation > 5)
                {
                    translation = 0;
                    fade = 1;
                    scale = 1;
                }
            }

            ParalaxScroll.TranslateTo(ParalaxScroll.TranslationX, translation);
            //EventImage.FadeTo(fade, 500);
            EventImage.ScaleTo(scale, 500, Easing.BounceOut);
            
            _lastScroll = e.ScrollY;
        }

        private const int ScrollMinLimit = 0;
        private const int ScrollMaxLimit = 190;

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            var val = ReMap(e.ScrollY, ScrollMinLimit, ScrollMaxLimit, 1, 0);

            this.EventImage.Scale = val;
            this.EventImage.Opacity = val;
        }

        public static double ReMap(double oldValue, double oldMin, double oldMax, double newMin, double newMax)
        {
            return (((oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin)) + newMin;
        }
    }
}