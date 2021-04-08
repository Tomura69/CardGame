using System;
using SQLite;

namespace CardGame.Views
{
    public class Player
    {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            public string username { get; set; }
            public int score { get; set; }
            public int timeLeft { get; set; }
    }
}
