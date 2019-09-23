using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLitePCL;
using LeagueOfArcher.Classes;
using System.Threading;
using System.Reflection;
using System.IO;

namespace LeagueOfArcher
{
    public partial class MainPage : ContentPage
    {
        public SQLBase myBaseSqlite;
        Players eloViewModel;
        public int counter = 0;
        CancellationTokenSource cts;
        bool taskRunOlnyInsidePage;

        public MainPage()
        {
            //App.mysettings.FirstRun(false);
            taskRunOlnyInsidePage = true;
            App.mysettings.SetFirstRun();
            App.mysettings.FirstStartLog();
            InitializeComponent();
            myBaseSqlite = new SQLBase(App.mysettings.Server);
            eloViewModel = new Players(myBaseSqlite);
            LoadItemSource();
            System.Diagnostics.Debug.WriteLine(App.mysettings.Background);
            ManageMessage();

        }

        public void LoadItemSource()
        {
            eloViewModel.CollectionRatioBest();
            if (eloViewModel.Exist(1))
                this.BindingContext = eloViewModel.eloObservable.First();
        }

        //Odpowiedzialne za refresh strony po nawigowaniu wstecz
        protected override void OnAppearing()
        {
            LoadItemSource();
            InitializeComponent();
            counter = 0;
            if(taskRunOlnyInsidePage)
                ManageMessage();

            base.OnAppearing();
        }

        private void ManageMessage()
        {
            taskRunOlnyInsidePage = false;
            cts = new CancellationTokenSource();
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (counter <= 1 )
                    l_message.Text = "Archer Tournament PST v0.42"; 
                else if (counter >=4 && counter < 6 && eloViewModel.Exist(1))
                    l_message.Text = "Best ratio belong to " + eloViewModel.eloObservable.First().name + " = " + eloViewModel.eloObservable.First().ratioBest.ToString("0.00");
                else if (counter >= 7 && counter < 9)
                    l_message.Text = "Mode : " + App.mysettings.Server;

                Task.Run(async () =>
                {
                    try
                    {
                        counter++;
                        System.Diagnostics.Debug.WriteLine(counter);

                    }
                    catch (System.OperationCanceledException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Text load cancelled: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                },cts.Token);
                if (counter > 10)
                {
                    counter = 0;
                    ManageMessage();
                    return false;
                }
                else
                    return true;
            });
        }

        private void Btn_start_Clicked(object sender, EventArgs e)
        {
            taskRunOlnyInsidePage = true;
            cts.Cancel();
            Navigation.PushAsync(new PrepareGame(ref myBaseSqlite));
        }

        private void Btn_player_create_Clicked(object sender, EventArgs e)
        {
            taskRunOlnyInsidePage = true;
            cts.Cancel();
            Navigation.PushAsync(new CreatePlayerPage(ref myBaseSqlite));
        }

        private void Btn_player_list_Clicked(object sender, EventArgs e)
        {
            taskRunOlnyInsidePage = true;
            cts.Cancel();
            Navigation.PushAsync(new PlayerList(ref myBaseSqlite));
        }

        private void Btn_console_Clicked(object sender, EventArgs e)
        {
            taskRunOlnyInsidePage = true;
            cts.Cancel();
            Navigation.PushAsync(new MyConsole(ref myBaseSqlite));
        }

        private void Btn_debug_Clicked(object sender, EventArgs e)
        {
            taskRunOlnyInsidePage = true;
            cts.Cancel();
        }
    }
}
