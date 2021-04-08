using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CardGame.Views
{
    public partial class LeaderBoard : ContentPage
    {
        public LeaderBoard()
        {
            InitializeComponent();

            Data();
        }

        async void Data()
        {
            var data = await App.Database.GelAllPlayersAsync();
            foreach (var i in data)
            {
                var label = new Label
                {
                    Text = i.username + " " + i.timeLeft.ToString() + " " + i.score.ToString()
                };
                info.Children.Add(label);
            }
        }

        void exitButton(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
