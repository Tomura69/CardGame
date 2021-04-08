using System;
using Xamarin.Forms;
using CardGame.Views;
using System.IO;
using Xamarin.Forms.Xaml;

namespace CardGame
{
    public partial class App : Application
    {
        static Database database;

        // Create the database connection as a singleton.
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    string path = Path.Combine(documentsPath, "Notes.db3");
                    database = new Database(path);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
