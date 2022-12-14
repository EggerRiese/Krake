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
    public partial class SettingPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public SettingPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Kontakt",
                "Standort",
                "Impressum",
                "Datenschutz"
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            else if(e.Item.ToString() == "Kontakt")
            {
                await Navigation.PushAsync(new ContactPage());
            }
            else if (e.Item.ToString() == "Standort")
            {
                await Navigation.PushAsync(new SelectionPage());
            }
            else if (e.Item.ToString() == "Impressum")
            {
                await Navigation.PushAsync(new ImprintPage());
            }
            else if (e.Item.ToString() == "Datenschutz")
            {
                await Navigation.PushAsync(new DataprotectionPage());
            }
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
