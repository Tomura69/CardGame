using System.IO;
using System.Data.SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace CardGame.Views
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            //System.Diagnostics.Debug.WriteLine("Paleido");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Player>().Wait();
        }

        public Task<List<Player>> GetNotesAsync()
        {
            //Get all Players.
            return database.Table<Player>().ToListAsync();
        }

        public Task<Player> GetPlayerAsync(string id)
        {
            // Get a specific Player.
            return database.Table<Player>()
                            .Where(i => i.username == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<Player>> GelAllPlayersAsync()
        {
            return database.QueryAsync<Player>("SELECT * FROM [Player] ORDER BY [score] DESC");
        }

        public Task<int> SavePlayerAsync(Player player)
        {
            if (player.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(player);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(player);
            }
        }

        public Task<int> DeleteNoteAsync(Player note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
