using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;
using System.IO;
using LeagueOfArcher.Classes;
using Xamarin.Forms;

namespace LeagueOfArcher.Classes
{
    public class SQLBase
    {
        public SQLiteConnection _dbconnection;
        readonly string connectionString = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //private string connectionString = DependencyService.Get<IFileHelper>().GetLocalFilePath("Ultimate.db");

        public SQLiteConnection ldbconnection
        {
            get { return this._dbconnection; }
        }

        public SQLBase(string db_name)
        {
            connectionString = DependencyService.Get<IFileHelper>().GetLocalFilePath(Path.Combine(connectionString, db_name));
            _dbconnection = new SQLiteConnection(connectionString);
            _dbconnection.CreateTable<Player>();
            _dbconnection.CreateTable<EloTable>();
        }

        //-------------------------------------------------------------- Dane Table ---------------------------------------------------------------------------

        public List<Player> GetPlayersList()
        {
            return _dbconnection.Query<Player>("select * from Player");
        }

        public List<string> GetPlayersNameList()
        {
            List<string> list = new List<string>();
            foreach (var item in GetPlayersList())
            {
                list.Add(item.name);
            }
            return list;
        }

        public Player GetPlayer(int id)
        {
            return _dbconnection.Table<Player>().FirstOrDefault(t => t.ID == id);
        }

        public Player GetPlayer(string name)
        {
            return _dbconnection.Table<Player>().FirstOrDefault(t => t.name == name);
        }

        public void AddPlayer(Player player)
        {
            _dbconnection.Insert(player);
        }

        public void DeletePlayer(int id)
        {
            _dbconnection.Delete<Player>(id);
        }

        public void DeletePlayer(Player player)
        {
            _dbconnection.Delete<Player>(player.ID);
        }

        public bool PlayerExist(string name)
        {
            if (_dbconnection.Table<Player>().FirstOrDefault(t => t.name == name) != null)
                return true;
            else
                return false;
        }

        //-------------------------------------------------------------- EloTable ---------------------------------------------------------------------------

        public List<EloTable> GetPlayersEloList()
        {
            return _dbconnection.Query<EloTable>("select * from EloTable");
        }

        public List<string> GetPlayersEloNameList()
        {
            List<string> list = new List<string>();
            foreach (var item in GetPlayersEloList())
            {
                list.Add(item.name);
            }
            return list;
        }

        public EloTable GetPlayerElo(int id)
        {
            return _dbconnection.Table<EloTable>().FirstOrDefault(t => t.ID == id);
        }

        public EloTable GetPlayerElo(string name)
        {
            return _dbconnection.Table<EloTable>().FirstOrDefault(t => t.name == name);
        }

        public void AddPlayerElo(EloTable player)
        {
            _dbconnection.Insert(player);
        }

        public void DeletePlayerElo(int id)
        {
            _dbconnection.Delete<EloTable>(id);
        }

        //-------------------------------------------------------------- AchivmentTable ---------------------------------------------------------------------------

        public List<AchivmentTable> GetPlayersAchivmentList()
        {
            return _dbconnection.Query<AchivmentTable>("select * from AchivmentTable");
        }

        public List<string> GetPlayersAchivmentNameList()
        {
            List<string> list = new List<string>();
            foreach (var item in GetPlayersEloList())
            {
                list.Add(item.name);
            }
            return list;
        }

        public AchivmentTable GetPlayerAchivment(int id)
        {
            return _dbconnection.Table<AchivmentTable>().FirstOrDefault(t => t.ID == id);
        }

        public AchivmentTable GetPlayerAchivment(string name)
        {
            return _dbconnection.Table<AchivmentTable>().FirstOrDefault(t => t.name == name);
        }

        public void AddPlayerAchivment(AchivmentTable player)
        {
            _dbconnection.Insert(player);
        }

        public void DeletePlayerAchivment(int id)
        {
            _dbconnection.Delete<AchivmentTable>(id);
        }

    }
}
