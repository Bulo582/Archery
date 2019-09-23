using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using SQLite;

namespace LeagueOfArcher.Classes
{
    public class Players
    {
        public ObservableCollection<Player> playerObservable = new ObservableCollection<Player>();
        public ObservableCollection<EloTable> eloObservable = new ObservableCollection<EloTable>();
        public SQLBase dbase;
        int place = 1;

        public Players () { }

        public Players(SQLBase db)
        {
            this.dbase = db;
        }

        public void Collection ()
        {
            playerObservable.Clear();
            var query = (from c in dbase._dbconnection.Table<Player>() orderby c.elo descending select c);
            foreach (var wiersz in query)
            {
                playerObservable.Add(new PlayerDependency(wiersz.ID, wiersz.name, wiersz.winCount, wiersz.lostCount, wiersz.bestScore, wiersz.matchCount, wiersz.scoreRatio, wiersz.elo, place));
                place++;
            }
        }

        public void CollectionRatioBest()
        {
            eloObservable.Clear();
            var query = (from c in dbase._dbconnection.Table<EloTable>() orderby c.ratioBest descending select c);
            foreach (var wiersz in query)
            {
                eloObservable.Add(new EloTableDependency(wiersz.ID, wiersz.name, wiersz.elo, wiersz.ratioBest));
                System.Diagnostics.Debug.WriteLine(wiersz.ID + " " + wiersz.name + " " + wiersz.elo + " " + wiersz.ratioBest);
            }
        }

        public bool Exist (int id)
        {
            if (dbase._dbconnection.Table<EloTable>().Where(u => u.ID == id).Any())
                return true;
            else
                return false;

        }
    }
}
