using System;
using System.Collections.Generic;
using System.Text;
using LeagueOfArcher.Classes;
using System.ComponentModel;

namespace LeagueOfArcher.Classes
{
    public class GameScore : Player, INotifyPropertyChanged // zalecenie zmien nazwe klasy na PlayerScore
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region pola

        int score1 = 0;
        int score2 = 0;
        int score3 = 0;
        int score4 = 0;
        int score5 = 0;
        int score6 = 0;
        int score7 = 0;
        int score8 = 0;
        

        int sum = 0;
        int place;
        int arrowCount;
        int roundCount;
        int playerCount;
        float ratio;
        float bestElo;
        float sumAllPlayerELo;
        string sumString;
        string sumcolor = "Default";
        bool theBest = false;
        bool bestHitFlag = false;
        bool ratioRecord = false;
        #endregion

        #region kontruktory

        public GameScore () { }

        public GameScore (int id,string name, int wincount, int lostcount, int bestscore, int matchcount, float scoreratio ,float elo, float sumallplayerelo, float bestelo) : base(id, name, wincount, lostcount, bestscore, matchcount, scoreratio ,elo)
        {
            this.ID = id;
            this.name = name;
            this.winCount = wincount;
            this.lostCount = lostcount;
            this.bestScore = bestscore;
            this.matchCount = matchcount;
            this.scoreRatio = scoreratio;
            this.elo = elo;
            this.sumAllPlayerELo = sumallplayerelo;
            this.bestElo = bestelo;
        }

        #endregion

        #region wlasciwosci



        public string InfoDebug()
        {
            System.Diagnostics.Debug.WriteLine(name+" - "+MultiplerFromPlace()+ " * " + MultiplerFromPlayerCount() + " * " + MultiplerFromRoundCount() + " * " + MultiplerFromMatchCount() + " * " + MultiplerFromArrowCount() + " * " + MultiplerFromRatio() + " + " + MultiplerEloRelation() + "+" + BonusForFirstMatch());
            return String.Format(name + " - " + MultiplerFromPlace() + " * " + MultiplerFromPlayerCount() + " * " + MultiplerFromRoundCount() + " * " + MultiplerFromMatchCount() + " * " + MultiplerFromArrowCount() + " * " + MultiplerFromRatio() + " + " + MultiplerEloRelation() + "+" + BonusForFirstMatch());
        }



        public string InfoDebug2()
        {
            System.Diagnostics.Debug.WriteLine(name + " w- " + winCount + " m- " + matchCount);
            return String.Format(name + " win- " + winCount + " match- " + matchCount);
        }


        public float AddElo
        {
            get { return (MultiplerFromPlace() * MultiplerFromPlayerCount() * MultiplerFromRoundCount() * MultiplerFromMatchCount() * MultiplerFromArrowCount() * MultiplerFromRatio()  + MultiplerEloRelation() + BonusForFirstMatch());  }
        }


        public bool RatioRecord
        {
            get { return this.ratioRecord; }
            set { this.ratioRecord = value; }
        }

        public int Place
        {
            set { this.place = value; }
        }

        public bool TheBest
        {
            set
            {
                this.theBest = value;
                SumColor = "";
            }
        }

        public string SumColor
        {
            get
            {
                return this.sumcolor;
            }
            private set
            {
                if (theBest)
                    this.sumcolor = "SeaGreen";
                else
                    this.sumcolor = App.mysettings.TextColor;

                NotifyPropertyChanged(nameof(SumColor));
            }
        }


        public int ArrowCount
        {
            set { this.arrowCount = value; }
            get { return this.arrowCount; }
        }

        public int RoundCount
        {
            set { this.roundCount = value; }
            private get { return this.roundCount; }
        }

        public int PlayerCount
        {
            set { this.playerCount = value; }
            private get { return this.playerCount; }
        }

        public float Ratio
        {
            get { return this.ratio; }
            set { this.ratio = Sum / (ArrowCount * RoundCount * 3f); }
        }

        public string Name
        {
            get { return "Gracz: " + this.name; }
        }

        public string ScoreRatiorange
        {
            get { return "Punkty/Ratio: " + Sum + " / " + ratio.ToString("0.00"); }
        }

        public string BestHit
        {
            get { return "Najlepszy strzał: " + BestShot().ToString(); }
        }

        public string Elo
        {
            get { return "Elo: (" + AddElo.ToString("0.00") + ") = " + (elo + AddElo).ToString("0.00"); }
        }

        public bool BestShotFlag
        {
            get { return this.bestHitFlag; }
            set { this.bestHitFlag = value; }
        }

        public int Sum
        {
            get { return this.sum; }
            set
            {
                this.sum = value;
                SumString = this.name + " : " + Sum.ToString();
                NotifyPropertyChanged(nameof(Sum));
            }
        }

        public string SumString
        {
            get { return this.sumString; }
            set
            {
                sumString = this.name + " : " + Sum.ToString();
                NotifyPropertyChanged(nameof(SumString));
            }
        }

        public int Score1 
        {
           get { return this.score1; }
           set
            {
                this.score1 = value;
                NotifyPropertyChanged(nameof(Score1));
            }
        }

        public int Score2
        {
            get { return this.score2; }
            set
            {
                this.score2 = value;
                NotifyPropertyChanged(nameof(Score2));
            }
        }

        public int Score3
        {
            get { return this.score3; }
            set
            {
                this.score3 = value;
                NotifyPropertyChanged(nameof(Score3));
            }
        }

        public int Score4
        {
            get { return this.score4; }
            set
            {
                this.score4 = value;
                NotifyPropertyChanged(nameof(Score4));
            }
        }

        public int Score5
        {
            get { return this.score5; }
            set
            {
                this.score5 = value;
                NotifyPropertyChanged(nameof(Score5));
            }
        }

        public int Score6
        {
            get { return this.score6; }
            set
            {
                this.score6 = value;
                NotifyPropertyChanged(nameof(Score6));
            }
        }

        public int Score7
        {
            get { return this.score7; }
            set
            {
                this.score7 = value;
                NotifyPropertyChanged(nameof(Score7));
            }
        }

        public int Score8
        {
            get { return this.score8; }
            set
            {
                this.score8 = value;
                NotifyPropertyChanged(nameof(Score8));
            }
        }

        #endregion

        #region mnozniki

        public float BonusForFirstMatch()
        {
            if (matchCount == 0)
                return 0.25f;
            else return 0f;
        }

        public float MultiplerEloRelation() //1
        {
            float eloAverange = sumAllPlayerELo / (float)playerCount;
            if (eloAverange == 0)
                return 0.5f;


            if (elo > eloAverange && elo < (2 * eloAverange))
                return sum / 100f;
            else if (elo > (2 * eloAverange))
                return 0f;
            else if (elo < eloAverange)
                return 0.2f;
            else
                return -0.5f;
        }

        public float MultiplerWinLostRelation() //1
        {
            if (matchCount != 0)
            {
                float multipler = (float)winCount / (float)matchCount;
                if (multipler > 0.7f)
                    return 0.15f;
                else if (multipler > 0.5f)
                    return 0.3f;
                else if (multipler > 0.3f)
                    return 0.35f;
                else if (multipler > 0.1f)
                    return 0.5f;
                else
                    return 0.6f;
            }
            else
                return 1f;

        }

        public float MultiplerFromRatio()
        {
            return Ratio * 4f;
        }

        public float MultiplerFromPlace () 
        {
            if (playerCount == 5)
            {
                if (place == 1)
                    return 5f * 0.7f;
                else if (place == 2)
                    return 4f * 0.6f;
                else if (place == 3)
                    return 3f * 0.5f;
                else if (place == 4)
                {
                    if(Ratio > 0.25)
                        return 2f * 0.1f;
                    else
                        return 2f * -0.25f;
                }
                else
                    return 2f * -0.25f;
            }
            else if (playerCount == 4)
            {
                if (place == 1)
                    return 4f * 0.6f;
                else if (place == 2)
                    return 3f * 0.5f;
                else if (place == 3)
                {
                    if(Ratio > 0.25)
                        return 0.1f;
                    else
                        return 2f * -0.2f;
                }
                    
                   
                else 
                    return 2f * -0.2f;
            }

            else if (playerCount == 3)
            {
                if (place == 1)
                    return 3f * 0.5f;
                else if (place == 2)
                    return 2f * 0.5f;
                else
                {
                    if (Ratio > 0.25)
                        return 1f * 0.1f;
                    else
                        return 2f * -0.2f;
                }
            }

            else if (playerCount == 2)
            {
                if (place == 1)
                    return 2f * 0.2f;
                else 
                    return 2f * -0.1f;
            }

            else
            {
                return 1f;
            }
        }

        public float MultiplerFromMatchCount()
        {
            if (matchCount < 10)
                return 1f;
            else if (matchCount < 20)
                return 0.8f;
            else if (matchCount < 50)
                return 0.5f;
            else if (matchCount < 100)
                return 0.2f;
            else
                return 0.1f;
        }

        public float MultiplerFromPlayerCount()
        {   if (playerCount == 1)
                return 0.1f;
            else
                return (float)(PlayerCount * 0.2f);
        }

        public float MultiplerFromArrowCount()
        {
            if (arrowCount < 4)
                return 0.1f;
            else if (arrowCount >= 4 && arrowCount < 7)
                return 0.3f;
            else if (arrowCount >= 7 && arrowCount < 10)
                return 0.6f;
            else
                return 1f;
        }

        public float MultiplerFromRoundCount()
        {
            if (roundCount >= 3 && roundCount < 5)
                return 0.6f;
            else if (roundCount >= 5 && roundCount < 7)
                return 0.7f;
            else
                return 1f;
        }

        public float AwardBestHit()
        {
            if (BestShotFlag)
                return 0.5f;
            else
                return 0f;
        }

        public int AwardRatioRecord()
        {
            if (RatioRecord)
                return 1;
            else
                return 0;
        }

        #endregion

        #region metody
        public int BestShot()
        {
            int best = 0;
            for (int i = 1; i <= 8; i++)
            {
                DictScoreEveryRound().TryGetValue(i, out int thisOne);
                if (thisOne > best)
                    best = thisOne;
            }
            return best;
        }

        public void ScoreEveryRound(int id, int value)
        {
            if (id == 1)
                Score1 = value;

            else if (id == 2)
                Score2 = value;

            else if (id == 3)
                Score3 = value;

            else if (id == 4)
                Score4 = value;

            else if (id == 5)
                Score5 = value;

            else if (id == 6)
                Score6 = value;

            else if (id == 7)
                Score7 = value;

            else if (id == 8)
                Score8 = value;

        }

        public int GetValueScore(int round)
        {
            if (round == 1)
                return Score1;

            else if (round == 2)
                return Score2;

            else if (round == 3)
                return Score3;

            else if (round == 4)
                return Score4;

            else if (round == 5)
                return Score5;

            else if (round == 6)
                return Score6;

            else if (round == 7)
                return Score7;

            else if (round == 8)
                return Score8;

            else return 0;
        }

        #endregion

        #region slowniki

        /// <summary>
        /// key start = 1;
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> DictScoreEveryRound()
        {
            return new Dictionary<int, int>()
            {
                {1, Score1},
                {2, Score2},
                {3, Score3},
                {4, Score4},
                {5, Score5},
                {6, Score6},
                {7, Score7},
                {8, Score8}
            };

        }

        #endregion
    }
}
