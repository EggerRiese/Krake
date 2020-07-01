using Krake.CustomRenderer;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krake
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TransparentNavigation(new MainPage());
            NavigationPage.SetHasNavigationBar(MainPage, false);
            NavigationPage.SetBackButtonTitle(MainPage, "");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
