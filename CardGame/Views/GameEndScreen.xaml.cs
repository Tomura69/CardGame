using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CardGame.Views
{
    public partial class GameEndScreen : ContentPage
    {
        public GameEndScreen(string username, int score)
        {
            InitializeComponent();

            Name.Text = username;
            Score.Text = "Your score is: " + score;
        }
        void leaderboardButton(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Views.LeaderBoard());
        }
        void playAgainButton(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Views.StartingScreen());
        }
        void exitButton(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
