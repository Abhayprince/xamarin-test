using App1.Logic;
using App1.Models;
using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenuesAsync(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (!(venueListView.SelectedItem is Venue selectedVenue))
            {
                DisplayAlert("Required", "Please select a venue", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(experienceEntry.Text))
            {
                DisplayAlert("Required", "Please write your experience", "OK");
                return;
            }
            var firstCategory = selectedVenue.categories.FirstOrDefault();

            var p = new Post
            {
                Experience = experienceEntry.Text,
                Address= selectedVenue.location?.address,
                CategoryId= firstCategory?.id,
                CategoryName=firstCategory?.name,
                Distance=selectedVenue.location.distance,
                Latitude=selectedVenue.location.lat,
                Longitude=selectedVenue.location.lng,
                VenueName=selectedVenue.name
            };

            using var con = new SQLiteConnection(App.DatabaseLocation);
            con.CreateTable<Post>();
            var rows = con.Insert(p);

            if (rows > 0)
            {
                //Insert successful
                DisplayAlert("Success", "Experience successfully inserted", "Ok");
            }
            else
                DisplayAlert("Failure", "Experience not saved", "Ok");
        }
    }
}