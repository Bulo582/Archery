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
    public partial class PrepareGame : ContentPage
    {
        Pickers myPickers = new Pickers();
        SQLBase myBaseSqlite;
        List<string> playersName;

        public PrepareGame(ref SQLBase db)
        {
            InitializeComponent();
            FillPickers();
            myBaseSqlite = db;
            GetPlayerNameList();
        }

        private void GetPlayerNameList()
        {
            playersName = myBaseSqlite.GetPlayersNameList();
        }

        private void FillPickers()
        {
            // Fill the arrows picker
            for (int i = 0; i < myPickers.arrowArray.Length; i++)
            {
                if (i == 0)
                    arrow_count_picker.Items.Add(myPickers.arrowArray[i].ToString() + " strzała");
                else
                    arrow_count_picker.Items.Add(myPickers.arrowArray[i].ToString() + " strzały");
            }

            // Fill the rounds picker
            foreach (int value in myPickers.roundArray)
            {
                round_count_picker.Items.Add(value.ToString() + " rund");
            }

            // Fill the players picker
            for (int i = 0; i < myPickers.playerArray.Length; i++)
            {
                if (i == 0)
                    member_count_picker.Items.Add(myPickers.playerArray[i].ToString() + " gracz");
                else
                    member_count_picker.Items.Add(myPickers.playerArray[i].ToString() + " graczy");
            }
        }

        private void ChagneMemberSlot(int count)
        {
            var picker = new Picker();

            for (int i = 1; i <= 5; i++)
            {
                DictPicker().TryGetValue(i, out picker);
                if (i <= count)
                {
                    picker.IsVisible = true;
                    picker.ItemsSource = playersName;
                }
                else
                    picker.IsVisible = false;
            }
        }

        private void ChangeMemberEnable(string currentName, int idException)
        {
            var picker = new Picker();

            for (int i = 1; i <= 5; i++)
            {
                DictPicker().TryGetValue(i, out picker);
                if (picker.SelectedItem != null)
                {
                    if (i != idException)
                    {
                        if (picker.SelectedItem.ToString() == currentName)
                            picker.SelectedItem = null;
                    }

                }
            }
        }

        private Dictionary<int, Picker> DictPicker()
        {
            return new Dictionary<int, Picker>()
               {
                    {1,  pick_player_1},
                    {2,  pick_player_2},
                    {3,  pick_player_3},
                    {4,  pick_player_4},
                    {5,  pick_player_5}
               };
        }

        private void Member_count_picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (member_count_picker.SelectedItem.ToString() == "1 gracz")
                ChagneMemberSlot(1);
            else if (member_count_picker.SelectedItem.ToString() == "2 graczy")
                ChagneMemberSlot(2);
            else if (member_count_picker.SelectedItem.ToString() == "3 graczy")
                ChagneMemberSlot(3);
            else if (member_count_picker.SelectedItem.ToString() == "4 graczy")
                ChagneMemberSlot(4);
            else if (member_count_picker.SelectedItem.ToString() == "5 graczy")
                ChagneMemberSlot(5);
        }

        private void Btn_run_Clicked(object sender, EventArgs e)
        {

            if (round_count_picker.SelectedItem == null || member_count_picker.SelectedItem == null || arrow_count_picker.SelectedItem == null)
                DisplayAlert("Info", "Wymagany element nie został wybrany", "OK");
            else
            {
                int arrow = int.Parse(arrow_count_picker.SelectedItem.ToString().Substring(0, arrow_count_picker.SelectedItem.ToString().LastIndexOf(" ")));
                int round = int.Parse(round_count_picker.SelectedItem.ToString().Substring(0, round_count_picker.SelectedItem.ToString().LastIndexOf(" ")));
                int player = int.Parse(member_count_picker.SelectedItem.ToString().Substring(0, member_count_picker.SelectedItem.ToString().LastIndexOf(" ")));

                GameSetting game = new GameSetting(arrow, round, player, myBaseSqlite);
                ImpPlayer(ref game, out bool accessToGame);

                if (accessToGame)
                    Navigation.PushAsync(new Game(game, ref myBaseSqlite));
            }
        }

        private void ImpPlayer( ref GameSetting game, out bool accessToGame)
        {
            bool[] checkAccces = new bool[5];
            int i = 0;
            string[] players;
            game.PlayerArray(out players);
            foreach (KeyValuePair<int, Picker> item in DictPicker())
            {

                if (item.Value.IsVisible)
                {
                    if (item.Value.SelectedItem != null)
                    {
                        players[i] = item.Value.SelectedItem.ToString();
                        checkAccces[i] = true;
                    }

                    else
                    {
                        DisplayAlert("Info", "Gracz nie został wybrany", "OK");
                        checkAccces[i] = false;
                        break;
                    }
                }
                else
                    checkAccces[i] = true;
                i++;
            }
            if (CheckBoolArray(checkAccces, checkAccces.Length))
            {
                game.FillPlayerClass(players);
                game.PlayerList = players;
                accessToGame = true;
            }
            else
                accessToGame = false;
        }

        private bool CheckBoolArray(bool[] boolArray, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (boolArray[i] == false)
                {
                    return false;
                }
            }
            return true;
        }

        private void Pick_player_1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (pick_player_1.SelectedItem != null)
                ChangeMemberEnable(pick_player_1.SelectedItem.ToString(),1);
        }

        private void Pick_player_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pick_player_2.SelectedItem != null)
                ChangeMemberEnable(pick_player_2.SelectedItem.ToString(), 2);
        }

        private void Pick_player_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pick_player_3.SelectedItem != null)
                ChangeMemberEnable(pick_player_3.SelectedItem.ToString(), 3);
        }

        private void Pick_player_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pick_player_4.SelectedItem != null)
                ChangeMemberEnable(pick_player_4.SelectedItem.ToString(), 4);
        }

        private void Pick_player_5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pick_player_5.SelectedItem != null)
                ChangeMemberEnable(pick_player_5.SelectedItem.ToString(), 5);
        }
    }
}