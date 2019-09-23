using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using LeagueOfArcher.Classes;

namespace LeagueOfArcher.Classes
{
    [Table("Player")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50), NotNull]
        public string name { get; set; }
        public int winCount { get; set; }
        public int lostCount { get; set; }
        public int bestScore { get; set; }
        public int matchCount { get; set; }
        public float scoreRatio { get; set; }
        public float elo { get; set; }

        public Player(string name)
        {
            this.name = name;
            this.winCount = 0;
            this.lostCount = 0;
            this.bestScore = 0;
            this.matchCount = 0;
            this.scoreRatio = 0;
            this.elo = 0f;
        }

        public Player(int id,string name, int wincount, int lostcount, int bestscore, int matchcount, float scoreratio ,float elo)
        {
            this.ID = id;
            this.name = name;
            this.winCount = wincount;
            this.lostCount = lostcount;
            this.bestScore = bestscore;
            this.matchCount = matchcount;
            this.scoreRatio = scoreratio;
            this.elo = elo;
        }

        public Player() { }
    }
}
