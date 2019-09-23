using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LeagueOfArcher.Classes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LeagueOfArcher
{
    public partial class App : Application
    {
        public static Settings mysettings { get; set; }

        public App()
        {
            

            BindingContext = this;

            InitializeComponent();

            mysettings = new Settings();

            MainPage = new NavigationPage(new MainPage());
        }

        public string Background
        {
            get { return mysettings.Background; }
        }

        public string TextColor
        {
            get { return mysettings.TextColor; }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
