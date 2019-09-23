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
	public partial class CreatePlayerPage : ContentPage
	{
        private SQLBase myBaseSqlite;
        private Players playerViewModel;
        private bool editMode = false;
        private bool funBool = true;
        private Player editModel;


        public CreatePlayerPage (ref SQLBase db)
		{
            editMode = false;
            InitializeComponent ();
            myBaseSqlite = db;
            playerViewModel = new Players(db);
            playerViewModel.Collection();
            LoadItemSource();
            
        }

        public void LoadItemSource()
        {
            playerViewModel.Collection();
            players_list.ItemsSource = playerViewModel.playerObservable;
        }


        #region events
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (ent_name.Text != null)
            {
                if(!editMode)
                {
                    myBaseSqlite.AddPlayer(new Player(ent_name.Text));
                    ent_name.Text = String.Empty;
                    LoadItemSource();
                }
                else
                {
                    editModel.name = ent_name.Text;
                    myBaseSqlite._dbconnection.Update(editModel);
                    ent_name.Text = String.Empty;
                    LoadItemSource();
                    editMode = false;
                }

            }
            else
                DisplayAlert("Info", "Brak imienia!", "OK");
        }

        private void Players_list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (funBool)
            {
                if (((Player)players_list.SelectedItem).name == "Stołek")
                    DisplayAlert("Witamy", "No i Pan Stołkson", "Elo");

                else if (((Player)players_list.SelectedItem).name == "Chudy")
                    DisplayAlert("Witamy", "Na następnych zawodach Cię pokonam", "Aha");
            }
        }

        private void Tbi_edit_Clicked(object sender, EventArgs e)
        {
            if (players_list.SelectedItem != null)
            {
                if ((Player)players_list.SelectedItem is Player)
                {
                    ent_name.Text = ((Player)players_list.SelectedItem).name;
                    editModel = myBaseSqlite.GetPlayer(((Player)players_list.SelectedItem).name);
                    editMode = true;
                }
            }
            else
                DisplayAlert("Info", "Nie wybrano żadnej opcji.", "OK");
        }

        private void Tbi_delete_Clicked(object sender, EventArgs e)
        {
            if(players_list.SelectedItem != null)
            {
                if((Player)players_list.SelectedItem is Player)
                {
                    playerViewModel.playerObservable.Remove((Player)players_list.SelectedItem);
                    myBaseSqlite.DeletePlayer((Player)players_list.SelectedItem);
                    LoadItemSource();
                }
            }
            else
                DisplayAlert("Info", "Nie wybrano żadnej opcji.", "OK");
        }
        #endregion
    }
}