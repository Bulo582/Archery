using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfArcher.Classes
{
    public class SaveMatch 
    {
        //uporządkowane
        public GameScore p1 { get; set; }
        public GameScore p2 { get; set; }
        public GameScore p3 { get; set; }
        public GameScore p4 { get; set; }
        public GameScore p5 { get; set; }

        public int arrowCount { get; private set; }
        public int roundCount { get; private set; }
        public int playerCount { get; private set; }

        public string InfoDebug
        {
            get
            {
                System.Diagnostics.Debug.WriteLine($"arr{arrowCount}, round{roundCount}, pla{playerCount}");
                return String.Format($"arr{arrowCount}, round{roundCount}, pla{playerCount}");
            }
        }


        readonly DateTime dateTime;

        public float[] aveList;

        public GameScore[] sortedList { get; set; }

        public SaveMatch() { }

        public SaveMatch(int arrowcount, int roundcount, int playercount, GameScore[] sortedscore) 
        {
            this.arrowCount = arrowcount;
            this.roundCount = roundcount;
            this.playerCount = playercount;
            this.sortedList = sortedscore;
            dateTime = DateTime.Now;

            if (playerCount >= 1)
                p1 = sortedscore[0];

            if (playerCount >= 2)
                p2 = sortedscore[1];

            if (playerCount >= 3)
                p3 = sortedscore[2];

            if (playerCount >= 4)
                p4 = sortedscore[3];

            if (playerCount >= 5)
                p5 = sortedscore[4];
        }

        public DateTime Date
        {
            get { return this.dateTime; }
        }

        /// <summary>
        /// key start = 1
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, GameScore> DictGameScore()
        {
            return new Dictionary<int, GameScore>()
            {
                {1, p1},
                {2, p2},
                {3, p3},
                {4, p4},
                {5, p5},
            };
        }



        public void SaveGame(GameSetting gameSetting) // uporządkowane według miejsc, podzielic na wiecej metod -- brakuje null p1 z dict
        {
            int bestHitPlayerId = 0; 
            int bestHit = 0;
            int currentPlayerShot;
            float[] _aveList = new float[playerCount];
            
            for (int i = 1; i <= playerCount; i++)
            {
                this.DictGameScore()[i].Ratio = 20f;
                this.DictGameScore()[i].Place = i; // moment przypisanie miejsca
                // konkurs na najlepszego best shota 
                currentPlayerShot = this.DictGameScore()[i].BestShot();

                if (currentPlayerShot > bestHit)
                {
                    bestHitPlayerId = i;
                    bestHit = currentPlayerShot;
                }

                // konkurs na najlepszego averange
                _aveList[i-1] = this.DictGameScore()[i].Ratio;

                
                this.DictGameScore()[i].ArrowCount = arrowCount;
                this.DictGameScore()[i].PlayerCount = playerCount;
                this.DictGameScore()[i].RoundCount = roundCount;
            }

            if (bestHitPlayerId != 0 && playerCount > 2)
            {
                this.DictGameScore()[bestHitPlayerId].BestShotFlag = true;
            }

            aveList = _aveList;
            Array.Sort(aveList);
            Array.Reverse(aveList);

            
        }

        
    }
}
