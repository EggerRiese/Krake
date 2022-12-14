using Krake.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krake.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailEventPage : ContentPage
    {
        DetailViewModel viewModel;
        private double _lastScroll = 0;

        public DetailEventPage(DetailViewModel vM)
        {
            InitializeComponent();
            BindingContext = this.viewModel = vM;

            if(viewModel.Ev.Entryfee == null || viewModel.Ev.Entryfee.ToLower() == "frei" || viewModel.Ev.Entryfee == "0" || viewModel.Ev.Entryfee == "")
            {
                EntryfeeLabel.Text = "Eintritt: frei";
            }
            else
            {
                EntryfeeLabel.Text = "Eintritt: " + viewModel.Ev.Entryfee;
            }
            TimeLabel.Text = viewModel.Ev.Time + " Uhr";
        }

        /*private void DescriptionButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel != null)
            {
               var l = viewModel.getLocation(viewModel.Ev.Description);
               Navigation.PushAsync(new DetailLocationPage(new DetailViewModel(null, l)));
            }
        }*/

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ParalaxScroll.Scrolled += OnParalaxScrolled;
        }


        protected override void OnDisappearing()
        {
            ParalaxScroll.Scrolled -= OnParalaxScrolled;
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