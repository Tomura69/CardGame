using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CardGame.Views
{
    public partial class GameScreen : ContentPage
    {
        public int CardNumber;
        public string Username;
        private bool isTimerRunning;
        private int timeLeft;
        private int scoreCount;
        private int cardCount = 0;
        private int ID;
        private int ID1;
        private int matches = 0;
        ImageButton btn1;
        public string[] imagesForFlipping = new string[30];

        public GameScreen(int cardNumber, string username)
        {
            Username = username;
            CardNumber = cardNumber;

            InitializeComponent();

            Init();

            Cards();
        }

        void Init()
        {
            ScoreCount.Text = "Score: " + scoreCount;
            isTimerRunning = true;
            timeLeft = 63;
            Timer(timeLeft);
        }

        public void Cards()
        {
            string mainURL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/";
            string pngEnding = ".png";
            string[] images = new string[30];
            string[] specificImages = new string[CardNumber];
            int[] randArray = new int[30];
            Random generator = new Random();
            int randomumber;

            for (int i = 0; i < randArray.Length; i++)
            {
                randomumber = generator.Next(1, 30);
                randArray[i] = randomumber;
                i++;
                randArray[i] = randomumber;
            }

            for (int i = 0; i < images.Length; i++)
            {
                images[i] = mainURL + randArray[i] + pngEnding;
            }

            for (int i = 0; i < CardNumber; i++)
            {
                specificImages[i] = images[i];
            }

            for (int i = specificImages.Length - 1; i >= 0; i--)
            {
                int randomIndex = generator.Next(0, i + 1);
                string temp = specificImages[i];
                specificImages[i] = specificImages[randomIndex];
                specificImages[randomIndex] = temp;
            }
            for (int i = 0; i < CardNumber; i++)
            {
                imagesForFlipping[i] = specificImages[i];
                var webImage = new ImageButton
                {
                    Source = ImageSource.FromUri(new Uri(specificImages[i])),
                    AutomationId = "" + i,
                    WidthRequest = 100,
                    HeightRequest = 100,
                };
                webImage.Clicked += OnImageButtonClicked;
                cards.Children.Add(webImage);
            }
        }

        bool Matcher(object sender)
        {
            if (cardCount == 0)
            {
                btn1 = (ImageButton)sender;
                btn1.IsEnabled = false;
                ID1 = int.Parse(btn1.AutomationId);
                btn1.Source = ImageSource.FromUri(new Uri(imagesForFlipping[ID1]));
                cardCount++;
                return false;
            }

            else if (cardCount == 1)
            {
                ImageButton btn = (ImageButton)sender;
                ID = int.Parse(btn.AutomationId);
                btn.Source = ImageSource.FromUri(new Uri(imagesForFlipping[ID]));
                if (imagesForFlipping[ID].ToString() == imagesForFlipping[ID1].ToString())
                {
                    matches++;
                    btn1.IsEnabled = false;
                    btn.IsEnabled = false;
                    btn1.IsVisible = false;
                    btn.IsVisible = false;
                    cardCount = 0;
                    ID = 0;
                    ID = 0;
                    return true;
                }

                else
                {
                    Task.Delay(1000).Wait();
                    btn1.IsEnabled = true;
                    cardCount = 0;
                    ID = 0;
                    ID1 = 0;
                    btn1.Source = ImageSource.FromUri(new Uri("https://png.pngtree.com/png-clipart/20190903/original/pngtree-chinese-style-black-brush-smoke-png-image_4428249.jpg"));
                    btn.Source = ImageSource.FromUri(new Uri("https://png.pngtree.com/png-clipart/20190903/original/pngtree-chinese-style-black-brush-smoke-png-image_4428249.jpg"));
                }
                return false;
            }

            else
            {
                return false;
            }
        }

        void OnImageButtonClicked(object sender, EventArgs e)
        { 
            if (CardNumber == 8)
            {
                if (Matcher(sender))
                {
                    scoreCount = scoreCount + 5;
                    ScoreCount.Text = "Score: " + scoreCount;
                    if (matches == (CardNumber/2))
                    {
                        scoreCount = scoreCount + timeLeft;
                        DataInsert();
                        Navigation.PushAsync(new Views.GameEndScreen(Username, scoreCount));
                    }
                }
            }

            else if (CardNumber == 12)
            {
                if (Matcher(sender))
                {
                    scoreCount = scoreCount + 5;
                    ScoreCount.Text = "Score: " + scoreCount;
                    if (matches == (CardNumber / 2))
                    {
                        scoreCount = scoreCount + timeLeft * 2;
                        DataInsert();
                        Navigation.PushAsync(new Views.GameEndScreen(Username, scoreCount));
                    }
                }
            }

            else
            {
                if (Matcher(sender))
                {
                    scoreCount = scoreCount + 5;
                    ScoreCount.Text = "Score: " + scoreCount;
                    if (matches == (CardNumber / 2))
                    {
                        scoreCount = scoreCount + timeLeft * 3;
                        DataInsert();
                        Navigation.PushAsync(new Views.GameEndScreen(Username, scoreCount));
                    }
                }
            }
        }


        public void Timer(int max)
        {
            int timeElapsed = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                timeElapsed++;
                TimeLeft.Text = "You have left: " + (max - timeElapsed);
                timeLeft = max - timeElapsed;
                if (timeLeft == 60)
                {
                    var images = cards.Children;
                    for(int i = 0; i < CardNumber; i++)
                    {
                        ImageButton btn = (ImageButton)images[i];
                        btn.Source = ImageSource.FromUri(new Uri("https://png.pngtree.com/png-clipart/20190903/original/pngtree-chinese-style-black-brush-smoke-png-image_4428249.jpg"));
                    }
                }
                if (timeElapsed < max)
                {
                    return isTimerRunning;
                }
                else
                {
                    DataInsert();
                    Navigation.PushAsync(new Views.GameEndScreen(Username, scoreCount));
                    return false;
                }
                // True = Repeat again, False = Stop the timer
            });
        }
        
        private async void pauseButton(System.Object sender, System.EventArgs e)
        {
            isTimerRunning = false;
            bool answer = await DisplayAlert("Paused", "Would you like to resume a game", "Yes", "No");
            
            if (answer == true)
            {
                isTimerRunning = true;
                Timer(timeLeft);
            }
            else
            {
                DataInsert();
                await Navigation.PushAsync(new Views.GameEndScreen(Username, scoreCount));
            }
        }
        async void DataInsert()
        {
            var player = new Player();
            player.username = Username;
            player.timeLeft = timeLeft;
            player.score = scoreCount;
            await App.Database.SavePlayerAsync(player);
        }
    }
}
