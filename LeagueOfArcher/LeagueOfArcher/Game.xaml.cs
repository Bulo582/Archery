using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueOfArcher.Classes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LeagueOfArcher.EFX;

namespace LeagueOfArcher
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Game : ContentPage
	{
        #region Pola

        SQLBase myBaseSqlite;
        GameDependency game;
        Playmp3 playerMp3;

        int lastID;

        #endregion

        public Game (GameSetting gameSett, ref SQLBase db)
		{
            playerMp3 = new Playmp3();
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);
            myBaseSqlite = db;
            this.game = ConversionToGameDependecy(gameSett);
            game.InitializePlayerScore();
            game.InitializePlayersElo();
            SwitchOnGame();
            ViewCell();
            SwitchOnSumLbl();
            this.BindingContext = game;
        }

        private GameDependency ConversionToGameDependecy(GameSetting gameSetting)
        {
            GameDependency dependency = new GameDependency(gameSetting.ArrowCount, gameSetting.RoundCount, gameSetting.PlayerCount, myBaseSqlite)
            {
                Player1 = gameSetting.Player1,
                Player2 = gameSetting.Player2,
                Player3 = gameSetting.Player3,
                Player4 = gameSetting.Player4,
                Player5 = gameSetting.Player5,
                PlayerList = gameSetting.PlayerList,
                CurrentRound = 1,
                CurrentPlayer = 1
            };
            return dependency;
        }

        #region Dictionary

        /// <summary>
        /// start key = 1 
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, GameScore> DictGameScore()
        {
            return new Dictionary<int, GameScore>()
            {
                {1,game.s1 },
                {2,game.s2 },
                {3,game.s3 },
                {4,game.s4 },
                {5,game.s5 }
            };
        }

        /// <summary>
        /// start key = 1
        /// </summary>
        /// <returns></returns>
        private Dictionary <int, Button> DictPlayerField()
        {
            return new Dictionary<int, Button>()
            {
                {1,btn_name_file_1 },
                {2,btn_name_file_2 },
                {3,btn_name_file_3 },
                {4,btn_name_file_4 },
                {5,btn_name_file_5 }
            };
        }

        /// <summary>
        /// start key = 1
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Entry> DictScoreFiled()
        {
            return new Dictionary<int, Entry>()
            {
                {1,ent_player_1},
                {2,ent_player_2},
                {3,ent_player_3},
                {4,ent_player_4},
                {5,ent_player_5}
            };
        }

        /// <summary>
        /// start key = 0
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Label> DictScoreLbl_1()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_1 },
                {1, lbl_score_1_1 },
                {2, lbl_score_1_2 },
                {3, lbl_score_1_3 },
                {4, lbl_score_1_4 },
                {5, lbl_score_1_5 },
            };
        }

        private Dictionary<int, Label> DictScoreLbl_2()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_2 },
                {1, lbl_score_2_1 },
                {2, lbl_score_2_2 },
                {3, lbl_score_2_3 },
                {4, lbl_score_2_4 },
                {5, lbl_score_2_5 },
            };
        }

        private Dictionary<int, Label> DictScoreLbl_3()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_3 },
                {1, lbl_score_3_1 },
                {2, lbl_score_3_2 },
                {3, lbl_score_3_3 },
                {4, lbl_score_3_4 },
                {5, lbl_score_3_5 },
            };
        }

        private Dictionary<int, Label> DictScoreLbl_4()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_4 },
                {1, lbl_score_4_1 },
                {2, lbl_score_4_2 },
                {3, lbl_score_4_3 },
                {4, lbl_score_4_4 },
                {5, lbl_score_4_5 },
            };
        }

        private Dictionary<int, Label> DictScoreLbl_5()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_5 },
                {1, lbl_score_5_1 },
                {2, lbl_score_5_2 },
                {3, lbl_score_5_3 },
                {4, lbl_score_5_4 },
                {5, lbl_score_5_5 },
            };
        }

        private Dictionary<int, Label> DictScoreLbl_6()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_6 },
                {1, lbl_score_6_1 },
                {2, lbl_score_6_2 },
                {3, lbl_score_6_3 },
                {4, lbl_score_6_4 },
                {5, lbl_score_6_5 },
            };
        }

        private Dictionary<int, Label> DictScoreLbl_7()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_7 },
                {1, lbl_score_7_1 },
                {2, lbl_score_7_2 },
                {3, lbl_score_7_3 },
                {4, lbl_score_7_4 },
                {5, lbl_score_7_5 },
            };
        }

        private Dictionary<int, Label> DictScoreLbl_8()
        {
            return new Dictionary<int, Label>()
            {
                {0, runda_8 },
                {1, lbl_score_8_1 },
                {2, lbl_score_8_2 },
                {3, lbl_score_8_3 },
                {4, lbl_score_8_4 },
                {5, lbl_score_8_5 },
            };
        }

        /// <summary>
        /// start key = 0
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Label> DictSum_Lbl()
        {
            return new Dictionary<int, Label>()
            {
                {0, lbl_sum     },
                {1, lbl_score1  },
                {2, lbl_score2  },
                {3, lbl_score3  },
                {4, lbl_score4  },
                {5, lbl_score5  },
            };
        }

        /// <summary>
        /// start key = 1
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, BoxView> DictBox()
        {
            return new Dictionary<int, BoxView>()
            {
                {1,box_r_1 },
                {2,box_r_2 },
                {3,box_r_3 },
                {4,box_r_4 },
                {5,box_r_5 },
                {6,box_r_6 },
                {7,box_r_7 },
                {8,box_r_8 },
            };
        }


        #endregion
        #region SwitchOn - ViewCell

        private void SwitchOnLabelScore(Dictionary<int, Label> dictLbl)
        {
            Label currentLabel;

            for (int i = 0; i < game.PlayerCount + 1; i++)
            {
                dictLbl.TryGetValue(i, out currentLabel);
                currentLabel.IsVisible = true;
            }
        }

        private void SwitchOnBoxes(Dictionary<int, BoxView> dictBox)
        {
            BoxView currentBox;

            for (int i = 1; i <= game.RoundCount; i++)
            {

                DictBox().TryGetValue(i, out currentBox);
                currentBox.IsVisible = true;
            }
        }

        

        private void SwitchOnSumLbl()
        {
            Label sum;
            
            for (int i = 0; i <= game.PlayerCount; i++)
            {
                DictSum_Lbl().TryGetValue(i, out sum);
                sum.IsVisible = true;
            }
        }

        /// <summary>
        /// Visibility Button and Entry Controls for each player
        /// </summary>
        private void SwitchOnGame()
        {
            Button playerField;
            Entry scoreEnt;
            for (int i = 1; i <= game.PlayerCount; i++)
            {
                DictPlayerField().TryGetValue(i, out playerField);
                DictScoreFiled().TryGetValue(i, out scoreEnt);
                playerField.IsVisible = true;
                playerField.Text = game.PlayerList[i-1].Substring(0,3);
                scoreEnt.IsVisible = true;

                if (i == 1)
                {
                    playerField.IsEnabled = true;
                    scoreEnt.IsEnabled = true;
                }
            }
        }

        private void ViewCell()
        {
            if(game.RoundCount >= 1)
            {
                SwitchOnLabelScore(DictScoreLbl_1());
            }

            if (game.RoundCount >= 2)
            {
                SwitchOnLabelScore(DictScoreLbl_2());
            }

            if (game.RoundCount >= 3)
            {
                SwitchOnLabelScore(DictScoreLbl_3());
            }

            if (game.RoundCount >= 4)
            {
                SwitchOnLabelScore(DictScoreLbl_4());
            }

            if (game.RoundCount >= 5)
            {
                SwitchOnLabelScore(DictScoreLbl_5());
            }

            if (game.RoundCount >= 6)
            {
                SwitchOnLabelScore(DictScoreLbl_6());
            }

            if (game.RoundCount >= 7)
            {
                SwitchOnLabelScore(DictScoreLbl_7());
            }

            if (game.RoundCount >= 8)
            {
                SwitchOnLabelScore(DictScoreLbl_8());
            }

            SwitchOnBoxes(DictBox());
        }

        private void SwitchOffAll()
        {
            Entry ent;
            Button btn;
            for (int i = 1; i <= game.PlayerCount; i++)
            {
                DictScoreFiled().TryGetValue(i, out ent);
                ent.IsEnabled = false;
                DictPlayerField().TryGetValue(i, out btn);
                btn.IsEnabled = false;
            }
        }

        #endregion

        private void WhoWin(out string name, out GameScore[] scoreList) 
        {
            name = "nobody";
            //int best = 0;
            int[] bests = new int[game.PlayerCount];
            scoreList = game.Best(true);

            if (game.PlayerCount != 1)
            {
                name = scoreList[0].name;
                Player winner = myBaseSqlite.GetPlayer(name);
                winner.winCount++;
                myBaseSqlite._dbconnection.Update(winner);
            }
        }

        private void SwitchOnOffButton(int id)
        {
            Button btn;
            Entry ent;
            GameScore score;
            lastID = id;
            DictScoreFiled().TryGetValue(id,out ent);
            DictGameScore().TryGetValue(id, out score);

            if (ent != null)
            {
                if (Int32.TryParse(ent.Text, out int value))
                {

                    if (value >= 0 && value <= game.MaxScore)
                    {
                        if (value >= Math.Ceiling((decimal)((game.ArrowCount * 3f) / 2f)))
                            playerMp3.PlayArrow();

                        if (value == 0)
                            playerMp3.PlayPeacefull();

                        if (id == game.PlayerCount)
                        {
                            DictPlayerField().TryGetValue(id, out btn);
                            ent.IsEnabled = false;
                            btn.IsEnabled = false;
                            score.ScoreEveryRound(game.CurrentRound, value);
                            score.Sum += value;
                            game.ChangeColorSums();
                            ent.Text = null; // usuwanie punktów
                            DictPlayerField().TryGetValue(1, out btn);
                            DictScoreFiled().TryGetValue(1, out ent);
                            ent.IsEnabled = true;
                            btn.IsEnabled = true;
                            game.CurrentRound++;
                            if (game.CurrentRound > game.RoundCount)
                            {
                                SwitchOffAll();
                                WhoWin(out string name, out GameScore[] sortedPlayer);
                                DisplayAlert("Info", $"Wygrał {name}", "OK");
                                SaveMatch saveMatch = new SaveMatch(game.ArrowCount, game.RoundCount, game.PlayerCount, sortedPlayer);
                                saveMatch.SaveGame(game);
                                Navigation.PushAsync(new Summary(saveMatch, ref myBaseSqlite));
                            }
                        }
                        else
                        {
                            DictPlayerField().TryGetValue(id, out btn);
                            btn.IsEnabled = false;
                            ent.IsEnabled = false;
                            score.ScoreEveryRound(game.CurrentRound, value);
                            score.Sum += value;
                            game.ChangeColorSums();
                            ent.Text = null;
                            DictPlayerField().TryGetValue(id + 1, out btn);
                            DictScoreFiled().TryGetValue(id + 1, out ent);
                            btn.IsEnabled = true;
                            ent.IsEnabled = true;
                        }
                    }

                    else
                    {
                        DisplayAlert("Info", $"Wprowadź wartość liczbową od 0 do {game.MaxScore}", "OK");
                        DictScoreFiled().TryGetValue(id, out ent);
                        ent.Text = String.Empty;
                    }
                }
                else
                {
                    DisplayAlert("Info", $"Wprowadź wartość liczbową od 0 do {game.MaxScore}", "OK");
                    DictScoreFiled().TryGetValue(id, out ent);
                    ent.Text = String.Empty;
                }
            }
            else
                DisplayAlert("Info", $"Wprowadź wartość liczbową od 0 do {game.MaxScore}", "OK");
        }

        private void Btn_name_file_1_Clicked(object sender, EventArgs e)
        {
            SwitchOnOffButton(1);
        }

        private void Btn_name_file_2_Clicked(object sender, EventArgs e)
        {
            SwitchOnOffButton(2);
        }

        private void Btn_name_file_3_Clicked(object sender, EventArgs e)
        {
            SwitchOnOffButton(3);
        }

        private void Btn_name_file_4_Clicked(object sender, EventArgs e)
        {
            SwitchOnOffButton(4);
        }

        private void Btn_name_file_5_Clicked(object sender, EventArgs e)
        {
            SwitchOnOffButton(5);
        }

        private void Tbi_exit_Clicked(object sender, EventArgs e)
        {
            ExitGameDisplay(sender, e);
        }

        async void ExitGameDisplay(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Zakończ turniej", "Czy chcesz zakończyć turniej", "Tak", "Nie");
            if (answer)
                Navigation.RemovePage(this);
        }

        private void Tbi_cofnij_Clicked(object sender, EventArgs e)
        {
            Button btn;
            Entry ent;

            if (lastID > 0)
            {
                if (lastID == game.PlayerCount)
                {
                    game.CurrentRound--;

                    DictGameScore().TryGetValue(lastID, out GameScore score);
                    score.Sum -= score.GetValueScore(game.CurrentRound);
                    score.ScoreEveryRound(game.CurrentRound, 0);

                    game.ChangeColorSums();
                    DictPlayerField().TryGetValue(1, out btn);
                    DictScoreFiled().TryGetValue(1, out ent);
                    btn.IsEnabled = false;
                    ent.Text = null;
                    ent.IsEnabled = false;
                    DictPlayerField().TryGetValue(lastID, out btn);
                    DictScoreFiled().TryGetValue(lastID, out ent);
                    btn.IsEnabled = true;
                    ent.Text = null;
                    ent.IsEnabled = true;
                    lastID--;
                }
                else
                {
                    DictGameScore().TryGetValue(lastID, out GameScore score);
                    score.Sum -= score.GetValueScore(game.CurrentRound);
                    score.ScoreEveryRound(game.CurrentRound, 0);
                    game.ChangeColorSums();
                    DictPlayerField().TryGetValue(lastID + 1, out btn);
                    DictScoreFiled().TryGetValue(lastID + 1, out ent);
                    btn.IsEnabled = false;
                    ent.Text = null;
                    ent.IsEnabled = false;
                    DictPlayerField().TryGetValue(lastID , out btn);
                    DictScoreFiled().TryGetValue(lastID, out ent);
                    btn.IsEnabled = true;
                    ent.Text = null;
                    ent.IsEnabled = true;
                    lastID--;
                }
            }
        }
    }
}