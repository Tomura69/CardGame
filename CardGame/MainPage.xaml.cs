using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void playButton(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Views.StartingScreen());
        }
        private async void leaderBoardButton(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Views.LeaderBoard());
        }
        void exitButton(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
