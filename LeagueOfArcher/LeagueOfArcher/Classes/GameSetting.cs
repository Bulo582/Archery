using System;
using System.Collections.Generic;
using System.Text;
using LeagueOfArcher.Classes;


namespace LeagueOfArcher.Classes
{
    public class GameSetting 
    {
        #region pola

        protected int arrowCount;
        protected int roundCount;
        protected int playerCount;
        public float maxEloPlayersIntoGame;
        public float BestEloPlayerIntoGame;

        string[] playerList;
        string player1;
        string player2;
        string player3;
        string player4;
        string player5;

        public GameScore s1 { get; set; }
        public GameScore s2 { get; set; }
        public GameScore s3 { get; set; }
        public GameScore s4 { get; set; }
        public GameScore s5 { get; set; }

        SQLBase myBaseSqlite;

        Random rnd = new Random();

        #endregion

        #region konstruktory
        public GameSetting () { }

        public GameSetting(SQLBase db) { }

        public GameSetting(int arrowcount, int roundcount, int playercount, SQLBase db)
        {
            this.arrowCount = arrowcount;
            this.roundCount = roundcount;
            this.playerCount = playercount;
            myBaseSqlite = db;
        }
        #endregion

        #region Bests Score

        /// <summary>
        /// Get list of sorted player according to descending score 
        /// </summary>
        /// <param name="lastsum">Variable if is true that the final result is run, else if that change who is the best now</param>
        /// <returns></returns>
        public GameScore[] Best (bool lastsum = false)
        {
            int[] bests = new int[PlayerCount];
            GameScore score;

            for (int i = 0; i < PlayerCount; i++)
            {
                DictGameScore().TryGetValue(i, out score);
                bests[i] = score.Sum;
            }

            Array.Sort(bests);
            Array.Reverse(bests); // zwraca liste wynikow od najepszego

            GameScore[] gameScores = new GameScore[playerCount]; 
            for (int i = 0; i < PlayerCount; i++)
            {
                for (int j = 0; j < PlayerCount; j++)
                {
                    DictGameScore().TryGetValue(j, out score);

                    if ((i < PlayerCount - 1) && bests[i] == bests[i + 1] && score.Sum == bests[i]) // wejdzie jesli wynik nastepny jest taki sam jak aktualny
                    {
                        if (i > 0 && gameScores[i - 1] != score)
                        {
                            gameScores[i] = score;
                            break;
                        }
                        else if (i == 0)
                        {
                            gameScores[i] = score;
                            break;
                        }
                    }

                    else if (score.Sum == bests[i]) // wejdzie jeśli suma punktow gracza = liscie najlepszych wynikow
                    {
                        if (i == playerCount - 1 && gameScores[j] != score && lastsum == true)
                            gameScores[i] = score;

                        else if (i > 0 && bests[i] == bests[i - 1] && lastsum == true) // 0 = 0
                            continue;

                        else if (i > 0 && gameScores[i - 1] != score)
                            gameScores[i] = score;

                        else if (i == 0)
                            gameScores[i] = score;
                    }

                }
            }

            // if array have null value
            bool arrayNullValue = false;
            int idNull = 0;

            for (int i = 0; i < playerCount; i++)
            {
                if (gameScores[i] == null)
                {
                    arrayNullValue = true;
                    idNull = i;
                }
            }


            //anty double value
            if (!arrayNullValue && lastsum)
            {
                for (int i = 0; i < gameScores.Length; i++)
                {
                    if (i > 0 && gameScores[i] == gameScores[i - 1])
                    {
                        if (CoinToss())
                        {
                            gameScores[i] = null;
                            idNull = i;
                        }
                        else
                        {
                            gameScores[i - 1] = null;
                            idNull = i - 1;
                        }
                        arrayNullValue = true;
                    }
                }
               
            }



            //anty error null value
            if (arrayNullValue)
            {
                Dictionary<int, GameScore> nullValue = DictGameScore();

                for (int i = PlayerCount; i <= nullValue.Count; i++)
                {
                    nullValue.Remove(i);
                }

                for (int i = 0; i < playerCount; i++)
                {
                    for (int j = 0; j < DictGameScore().Count; j++)
                    {
                        DictGameScore().TryGetValue(j, out GameScore value);
                        if (gameScores[i] == value)
                            nullValue.Remove(j);
                    }
                }

                foreach (KeyValuePair<int, GameScore> item in nullValue)
                {
                    gameScores[idNull] = item.Value;
                }
            }

            // remis sprawdza best hit
            for (int i = 0; i < PlayerCount - 1; i++)
            {

                if (gameScores[i].Sum == gameScores[i + 1].Sum)
                {
                    GameScore best = new GameScore();
                    int bestHit = 0;
                    int currentValue;
                    for (int j = 0; j < roundCount; j++)
                    {
                        gameScores[i].DictScoreEveryRound().TryGetValue(j + 1, out int value1);
                        gameScores[i + 1].DictScoreEveryRound().TryGetValue(j + 1, out int value2);
                        currentValue = GetMax(value1, value2);

                        if (currentValue > bestHit)
                        {
                            bestHit = currentValue;
                            if (bestHit == value1)
                                best = gameScores[i];
                            else if (bestHit == value2)
                                best = gameScores[i + 1];
                        }
                    }

                    if (best == gameScores[i + 1])
                    {
                        GameScore p1 = gameScores[i];
                        GameScore p2 = gameScores[i + 1];
                        gameScores[i] = p2;
                        gameScores[i + 1] = p1;
                    }
                }
            }

            return gameScores;
        }

        public GameScore FirstOne()
        {
            int[] bests = new int[PlayerCount];
            GameScore score;

            for (int i = 0; i < PlayerCount; i++)
            {
                DictGameScore().TryGetValue(i , out score);
                bests[i] = score.Sum;
            }

            Array.Sort(bests);
            Array.Reverse(bests);
            GameScore firstOne = new GameScore();

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < PlayerCount; j++)
                {
                    DictGameScore().TryGetValue(j , out score);

                    if (score.Sum == bests[i])
                        firstOne = score;
                }
            }
            return firstOne;
        }

        #endregion

        #region wlasciowsci

        public SQLBase MyBaseSQLite
        {
            get { return this.myBaseSqlite; }
            set { this.myBaseSqlite = value; }
        }

        public float BestEloIntoGame()
        {
            float result = 0f;
            for (int i = 0; i < PlayerCount; i++)
            {
                DictGameScore().TryGetValue(i, out GameScore value);
                    if (value.elo > result)
                        result = value.elo;
            }
            return result;
        }

        public float MaxEloPlayers()
        {
            float result = 0f;
            for (int i = 0; i < PlayerCount; i++)
            {
                DictGameScore().TryGetValue(i, out GameScore value);
                    result =+ value.elo;
            }
            return result;
        }

        public int MaxScore
        {
            get { return arrowCount * 3; }
        }

        public string [] PlayerList
        {
            get { return this.playerList; }
            set { this.playerList = value; }
        }

        public string Player1
        {
            get { return this.player1; }
            set { this.player1 = value; }
        }

        public string Player2
        {
            get { return this.player2; }
            set { this.player2 = value; }
        }

        public string Player3
        {
            get { return this.player3; }
            set { this.player3 = value; }
        }

        public string Player4
        {
            get { return this.player4; }
            set { this.player4 = value; }
        }

        public string Player5
        {
            get { return this.player5; }
            set { this.player5 = value; }
        }

        public int ArrowCount
        {
            get { return this.arrowCount; }
        }

        public int RoundCount
        {
            get { return this.roundCount; }
        }

        public int PlayerCount
        {
            get { return this.playerCount; }
        }

        #endregion

        public static int GetMax(int first, int second) // Global later -----------------------------------------------!
        {
            if (first > second)
            {
                return first;
            }

            else if (first < second)
            {
                return second;
            }
            else
            {
                return second;
            }
        }

        #region metody

        public bool CoinToss()
        {
            int value = rnd.Next(0, 2);
            if (value == 1)
                return true;
            else
                return false;
        }

        public void ChangeColorSums()
        {
            GameScore[] allScores = Best();
            for (int i = 0; i < allScores.Length; i++)
            {
                if (i == 0)
                    allScores[i].TheBest = true;
                else
                    allScores[i].TheBest = false;
            }
        }


        public void PlayerArray( out string[] playerArray)
        {
            playerArray = new string[5];
            playerArray[0] = Player1;
            playerArray[1] = Player2;
            playerArray[2] = Player3;
            playerArray[3] = Player4;
            playerArray[4] = Player5;
        }

        public void FillPlayerClass(string[] playerArray)
        {
            Player1 = playerArray[0];
            Player2 = playerArray[1];
            Player3 = playerArray[2];
            Player4 = playerArray[3];
            Player5 = playerArray[4];
        }

        public void InitializePlayersElo()
        {
            maxEloPlayersIntoGame = MaxEloPlayers();
            BestEloPlayerIntoGame = BestEloIntoGame();
        }

        public void InitializePlayerScore() // informacje o wcześniejszych poczynaniach gracza oraz inicjalizacja GameScore ! 
        {
            Player player; 

            if (PlayerCount >= 1)
            {   
                player = myBaseSqlite.GetPlayer(player1);
                s1 = new GameScore(player.ID, player.name, player.winCount, player.lostCount, player.bestScore, player.matchCount, player.scoreRatio ,player.elo, maxEloPlayersIntoGame, BestEloPlayerIntoGame);
                s1.ArrowCount = ArrowCount;
                s1.RoundCount = RoundCount;
                s1.PlayerCount = PlayerCount;
                
            }

            if (PlayerCount >= 2)
            {
                player = myBaseSqlite.GetPlayer(player2);
                s2 = new GameScore(player.ID, player.name, player.winCount, player.lostCount, player.bestScore, player.matchCount, player.scoreRatio, player.elo, maxEloPlayersIntoGame, BestEloPlayerIntoGame);
                s2.ArrowCount = ArrowCount;
                s2.RoundCount = RoundCount;
                s2.PlayerCount = PlayerCount;
            }

            if (PlayerCount >= 3)
            {
                player = myBaseSqlite.GetPlayer(player3);
                s3 = new GameScore(player.ID, player.name, player.winCount, player.lostCount, player.bestScore, player.matchCount, player.scoreRatio, player.elo, maxEloPlayersIntoGame, BestEloPlayerIntoGame);
                s3.ArrowCount = ArrowCount;
                s3.RoundCount = RoundCount;
                s3.PlayerCount = PlayerCount;
            }

            if (PlayerCount >= 4)
            {
                player = myBaseSqlite.GetPlayer(player4);
                s4 = new GameScore(player.ID, player.name, player.winCount, player.lostCount, player.bestScore, player.matchCount, player.scoreRatio, player.elo, maxEloPlayersIntoGame, BestEloPlayerIntoGame);
                s4.ArrowCount = ArrowCount;
                s4.RoundCount = RoundCount;
                s4.PlayerCount = PlayerCount;
            }

            if (PlayerCount >= 5)
            {
                player = myBaseSqlite.GetPlayer(player5);
                s5 = new GameScore(player.ID, player.name, player.winCount, player.lostCount, player.bestScore, player.matchCount, player.scoreRatio, player.elo, maxEloPlayersIntoGame, BestEloPlayerIntoGame);
                s5.ArrowCount = ArrowCount;
                s5.RoundCount = RoundCount;
                s5.PlayerCount = PlayerCount;
            }
        }

        #endregion

        #region slowniki

        /// <summary>
        /// start key = 0
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, GameScore> DictGameScore()
        {
            return new Dictionary<int, GameScore>()
            {
                {0, s1 },
                {1, s2 },
                {2, s3 },
                {3, s4 },
                {4, s5 }
            };
        }

        #endregion
    }
}
