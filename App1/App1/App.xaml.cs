using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }
        public App(string databaseLocation) : this()
        {
            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
