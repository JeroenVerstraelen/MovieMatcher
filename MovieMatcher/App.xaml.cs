using System;
using MovieMatcher.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieMatcher
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage2());
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
