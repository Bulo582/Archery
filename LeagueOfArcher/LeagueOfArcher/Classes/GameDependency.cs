using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfArcher.Classes
{
    public class GameDependency : GameSetting
    {
        int currentRound;
        int currentPlayer;


        public GameDependency () { }

        public GameDependency(int arrowcount, int rountcount, int playercount, SQLBase db) : base(arrowcount, rountcount, playercount, db)
        {
            this.arrowCount = arrowcount;
            this.roundCount = rountcount;
            this.playerCount = playercount;
            MyBaseSQLite = db;
        }


        public string BottonLabel
        {
            get
            {
                return $"Archer Tournament PST \t Arrow = {arrowCount}";
            }
        }

        public int ColumnSpan
        {
            get
            {
                if (PlayerCount == 1)
                    return 1;
                else if (PlayerCount == 2)
                    return 2;
                else if (PlayerCount == 3)
                    return 3;
                else if (PlayerCount == 4)
                    return 4;
                else
                    return 5;
            }

        }

        public int CurrentRound
        {
            get { return this.currentRound; }
            set { this.currentRound = value; }
        }

        public int CurrentPlayer
        {
            get { return this.currentPlayer; }
            set { this.currentPlayer = value; }
        }

    }
}
