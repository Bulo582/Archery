using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LeagueOfArcher.Classes
{
    [Table("EloTable")]
    public class EloTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string name { get; set; }
        public float elo { get; set; }
        public float ratioBest { get; set; }

        public EloTable () { }

        public EloTable(string name)
        {
            this.name = name;
            ratioBest = 0;
            elo = 0;
        }

        public EloTable(int id, string name, float elo, float ratiobest)
        {
            this.ID = id;
            this.name = name;
            this.elo = elo;
            this.ratioBest = ratiobest;
            
        }
    }
}
