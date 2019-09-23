using System;
using System.Collections.Generic;
using System.Text;
using LeagueOfArcher.Classes;

namespace LeagueOfArcher.Classes
{
    public class PlayerDependency : Player
    {
        readonly int place;

        public PlayerDependency() { }

        public PlayerDependency(int id, string name, int wincount, int lostcount, int bestscore, int matchcount, float scoreratio ,float elo ,int place) : base(id, name, wincount, lostcount, bestscore, matchcount, scoreratio ,elo)
        {
            this.ID = id;
            this.name = name;
            this.winCount = wincount;
            this.lostCount = lostcount;
            this.bestScore = bestscore;
            this.matchCount = matchcount;
            this.scoreRatio = scoreratio;
            this.elo = elo;
            this.place = place;
        }

        public string ColorString
        {
            get
            {
                if (place == 1)
                    return "SeaGreen";
                else
                    return App.mysettings.TextColor;
            }
        }

        public string WinString
        {
            get { return "wygrane: " + this.winCount; }
        }

        public string EloString
        {
            get { return "elo: " + this.elo.ToString("0.00"); }
        }

        public string PlaceString
        {
            get { return place + "."; }
        }
    }
}
