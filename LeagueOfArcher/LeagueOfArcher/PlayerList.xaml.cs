using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LeagueOfArcher.Classes;
using SQLite;

namespace LeagueOfArcher
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerList : ContentPage
	{
        SQLBase myBaseSqlite;
        private Players playerViewModel;

        public PlayerList (ref SQLBase db)
		{
            myBaseSqlite = db;
            InitializeComponent();
            playerViewModel = new Players(db);
            LoadItemSource();
		}

        public void LoadItemSource()
        {
            playerViewModel.Collection();
            players_list.ItemsSource = playerViewModel.playerObservable;
        }

        private void Players_list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new PlayerDetail());
        }
    }
}