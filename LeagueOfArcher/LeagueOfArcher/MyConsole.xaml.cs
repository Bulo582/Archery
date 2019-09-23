using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LeagueOfArcher.Console;
using LeagueOfArcher.Classes;


namespace LeagueOfArcher
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyConsole : ContentPage
    {
        ConsoleCommand cmd;
        SQLBase mySqliteBase;
        Queue<string> saveCommand;
        string[] commandArray;

        public MyConsole(ref SQLBase db)
		{
            saveCommand = new Queue<string>();
            mySqliteBase = db;
            cmd = new ConsoleCommand(ref mySqliteBase);
            InitializeComponent ();
            this.BindingContext = cmd;
		}

        public void FillCommandArray()
        {
            commandArray = new string[] { "help","info","logs" };
 
        }

        private void Btn_ent_Clicked(object sender, EventArgs e)
        {
            if (ent_ent.Text != null)
            {
                saveCommand.Enqueue(ent_ent.Text);
                cmd.RunCommand(ent_ent.Text, out bool exit, out Entry ent);
                ent_ent.Text = ent.Text;
                if (exit)
                    Navigation.PopToRootAsync(true);
            }
        }

        private void Btn_back_Clicked(object sender, EventArgs e)
        {
            if (saveCommand.Count > 0)
                ent_ent.Text = saveCommand.Dequeue();
            
        }

    }
}