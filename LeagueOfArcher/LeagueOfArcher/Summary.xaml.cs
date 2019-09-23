using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LeagueOfArcher.Classes;

namespace LeagueOfArcher
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Summary : ContentPage
	{
        SQLBase myBaseSqlite;
        SaveMatch match;
        Players VievModel;
        Settings sets;

        public Summary (SaveMatch saveMatch, ref SQLBase db)
		{
            NavigationPage.SetHasNavigationBar(this, false);
            sets = new Settings();
            myBaseSqlite = db;
            match = saveMatch;
            VievModel = new Players(myBaseSqlite);
            EloExist();
            SaveToBase();
            InitializeComponent();
            InitalizeLabel();
            this.BindingContext = match;

            //logi
            sets.AddLogs(DateTime.Now.ToString());

            sets.AddLogs(match.InfoDebug);
            sets.EndLog();

            List <Player> players = myBaseSqlite.GetPlayersList();
            foreach (Player item in players)
            {
                System.Diagnostics.Debug.WriteLine("Player " + item.name + "elo = " + item.elo);

                sets.AddLogs("Player " + item.name + "elo = " + item.elo);
            }

            sets.EndLog();

            System.Diagnostics.Debug.WriteLine("MultiplerFromPlace() * MultiplerFromPlayerCount() * MultiplerFromRoundCount() * MultiplerFromMatchCount() * MultiplerFromArrowCount() * MultiplerFromRatio()  + MultiplerEloRelation() + BonusForFirstMatch())");
            sets.AddLogs("MultiplerFromPlace() * MultiplerFromPlayerCount() * MultiplerFromRoundCount() * MultiplerFromMatchCount() * MultiplerFromArrowCount() * MultiplerFromRatio()  + MultiplerEloRelation() + BonusForFirstMatch())\n");
            foreach (KeyValuePair<int, GameScore> item in match.DictGameScore())
            {
                if (!(item.Value is null))
                {
                    item.Value.InfoDebug();
                    item.Value.InfoDebug2();

                    sets.AddLogs(item.Value.InfoDebug());
                    sets.AddLogs(item.Value.InfoDebug2());
                    System.Diagnostics.Debug.WriteLine(item.Value.RatioRecord + "<-RadioRecord|" + item.Value.name +"|BestShoot-> " + item.Value.BestShotFlag);
                    sets.AddLogs(item.Value.RatioRecord + "<-RadioRecord|" + item.Value.name + "|BestShoot-> " + item.Value.BestShotFlag);
                }
            }
            sets.EndLog();
        }


        #region Dictionary

        /// <summary>
        /// start key = 1
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Label> Dict_Stat_Label_1()
        {
            return new Dictionary<int, Label>()
            {
                {1, lbl_poz_1 },
                {2, lbl_a_1_1 },
                {3, lbl_a_1_2 },
                {4, lbl_a_1_3 },
                {5, lbl_a_1_4 }
            };
        }

        private Dictionary<int, Label> Dict_Stat_Label_2()
        {
            return new Dictionary<int, Label>()
            {
                {1, lbl_poz_2 },
                {2, lbl_a_2_1 },
                {3, lbl_a_2_2 },
                {4, lbl_a_2_3 },
                {5, lbl_a_2_4 }
            };
        }

        private Dictionary<int, Label> Dict_Stat_Label_3()
        {
            return new Dictionary<int, Label>()
            {
                {1, lbl_poz_3 },
                {2, lbl_a_3_1 },
                {3, lbl_a_3_2 },
                {4, lbl_a_3_3 },
                {5, lbl_a_3_4 }
            };
        }

        private Dictionary<int, Label> Dict_Stat_Label_4()
        {
            return new Dictionary<int, Label>()
            {
                {1, lbl_poz_4 },
                {2, lbl_a_4_1 },
                {3, lbl_a_4_2 },
                {4, lbl_a_4_3 },
                {5, lbl_a_4_4 }
            };
        }

        private Dictionary<int, Label> Dict_Stat_Label_5()
        {
            return new Dictionary<int, Label>()
            {
                {1, lbl_poz_5 },
                {2, lbl_a_5_1 },
                {3, lbl_a_5_2 },
                {4, lbl_a_5_3 },
                {5, lbl_a_5_4 }
            };
        }

        #endregion

        public void SaveToBase()
        {
            for (int i = 1; i <= match.playerCount; i++)
            {
                match.DictGameScore().TryGetValue(i, out GameScore player);
                Player p = myBaseSqlite.GetPlayer(player.name);
                p.elo += player.AddElo;
                p.matchCount++;
                myBaseSqlite._dbconnection.Update(p);
            }
        }


        #region InitializeLabel
        public void InitalizeLabel()
        {
            if (match.playerCount >= 1)
                SwitchOnLabelScore(Dict_Stat_Label_1());

            if (match.playerCount >= 2)
                SwitchOnLabelScore(Dict_Stat_Label_2());

            if (match.playerCount >= 3)
                SwitchOnLabelScore(Dict_Stat_Label_3());

            if (match.playerCount >= 4)
                SwitchOnLabelScore(Dict_Stat_Label_4());

            if (match.playerCount >= 5)
                SwitchOnLabelScore(Dict_Stat_Label_5());
        }

        private void SwitchOnLabelScore(Dictionary<int, Label> dictLbl)
        {
            Label currentLabel;

            for (int i = 1; i <= dictLbl.Count ; i++)
            {
                dictLbl.TryGetValue(i, out currentLabel);
                currentLabel.IsVisible = true;
                currentLabel.IsEnabled = true;
            }
        }
        #endregion


        /// <summary>
        /// Test ave record and fill to elo table if elo table dont exist build 3 position the table
        /// </summary>
        private void EloExist()  // zle kazdy ma ave record
        {
            EloTable table;
            GameScore bestAvePlayer;
            for (int i = 1; i <= 3; i++)
            {
                if (!(VievModel.Exist(i)))
                {
                    myBaseSqlite.AddPlayerElo(new EloTable());

                    table = myBaseSqlite.GetPlayerElo(i);

                    if (i <= match.playerCount)
                    {
                        if (match.aveList[i - 1] > table.ratioBest)
                        {
                            table.elo = match.DictGameScore()[i].elo;
                            table.name = match.DictGameScore()[i].name;
                            table.ratioBest = match.DictGameScore()[i].Ratio;
                            myBaseSqlite._dbconnection.Update(table);

                            for (int j = 1; j <= match.playerCount; j++)
                            {
                                match.DictGameScore().TryGetValue(j, out GameScore bestAve);
                                if (bestAve.Ratio == match.aveList[i - 1])
                                {
                                    bestAvePlayer = bestAve;
                                    bestAve.RatioRecord = true; // Ave Record
                                    continue;
                                }
                            }
                        }
                    }
                }
                else
                {
                    table = myBaseSqlite.GetPlayerElo(i);

                    if (i <= match.playerCount)
                    {
                        if (match.aveList[i - 1] > table.ratioBest)
                        {
                            table.elo = match.DictGameScore()[i].elo;
                            table.name = match.DictGameScore()[i].name;
                            table.ratioBest = match.DictGameScore()[i].Ratio;
                            myBaseSqlite._dbconnection.Update(table);
                            

                            for (int j = 1; j <= match.playerCount; j++)
                            {
                                match.DictGameScore().TryGetValue(j, out GameScore bestAve);
                                if (bestAve.Ratio == match.aveList[i - 1])
                                {
                                    bestAvePlayer = bestAve;
                                    bestAve.RatioRecord = false; // Ave Record
                                    continue;
                                }
                            }
                        }
                    }
                }
                //log
                string logEloExist = "EloExist() :" + myBaseSqlite.GetPlayerElo(i).ratioBest + " = " + myBaseSqlite.GetPlayerElo(i).name;
                System.Diagnostics.Debug.WriteLine(logEloExist);
                sets.AddLogs(logEloExist);
            }
        }

        private void Btn_newGame_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(true);
        }
    }
}