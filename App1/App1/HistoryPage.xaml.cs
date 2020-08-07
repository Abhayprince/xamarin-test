using App1.Models;
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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Post>();    //Sqlite ignores this line if Table already exists
                var posts = con.Table<Post>().ToList();

                postListView.ItemsSource = posts;
            }
        }
    }
}