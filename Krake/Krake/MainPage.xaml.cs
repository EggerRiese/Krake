using Krake.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Krake
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (Preferences.Get("City", "") == "")
            {
                Title = "Standort wählen";
                NavigationPage.SetHasBackButton(this, false);
                Navigation.PushAsync(new SelectionPage(true));
            }
        }
    }
}
