using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using CardGame;

using Xamarin.Forms;

namespace CardGame.Views
{
    public partial class StartingScreen : ContentPage
    {
        public StartingScreen()
        { 
            InitializeComponent();
        }

        private async void easyMode(System.Object sender, System.EventArgs e)
        {
            var username = Username.Text;
            await Navigation.PushAsync(new Views.GameScreen(8, username));
        }

        private async void mediumMode(System.Object sender, System.EventArgs e)
        {
            var username = Username.Text;
            await Navigation.PushAsync(new Views.GameScreen(12, username));
        }

        private async void hardMode(System.Object sender, System.EventArgs e)
        {
            var username = Username.Text;
            await Navigation.PushAsync(new Views.GameScreen(16, username));
        }
    }
}
