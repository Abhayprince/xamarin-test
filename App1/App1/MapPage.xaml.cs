using App1.Models;
using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(new TimeSpan(0, 0, 0), 100);

            var position = await locator.GetPositionAsync();

            var center = new Position(position.Latitude, position.Longitude);
            var mapSpan = new MapSpan(center, 2, 2);
            locationMap.MoveToRegion(mapSpan);

            using var con = new SQLiteConnection(App.DatabaseLocation);
            con.CreateTable<Post>();
            var posts = con.Table<Post>().ToList();
            DisplayInMap(posts);
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var p in posts)
            {
                try
                {
                    var position = new Position(p.Latitude, p.Longitude);
                    var pin = new Pin
                    {
                        Position = position,
                        Address = p.Address,
                        Label = p.VenueName,
                        Type = PinType.SavedPin
                    };
                    locationMap.Pins.Add(pin);
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;
            await locator.StopListeningAsync();
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var center = new Position(e.Position.Latitude, e.Position.Longitude);
            var mapSpan = new MapSpan(center, 2, 2);
            locationMap.MoveToRegion(mapSpan);
        }
    }
}