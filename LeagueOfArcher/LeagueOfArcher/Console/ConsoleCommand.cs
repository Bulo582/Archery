using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LeagueOfArcher.Classes;




namespace LeagueOfArcher.Console
{
    class ConsoleCommand : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Settings sets;
        SQLBase mySqliteBase;
        Player currentPlayer;

        bool writeMode = false;
        bool testWrite = false;

        //Elo Mode
        bool setElo = false;
        bool eloModeName = false;

        //Password Mode
        bool passwordOld = false;
        bool setPassword = false;
        bool passwordMode = false;
        bool accessMode = false;

        string command;
        string consoleText = "PST TOURNAMENT CONSOLE";


        public ConsoleCommand (ref SQLBase db) { sets = new Settings(); mySqliteBase = db; }

        public void RunCommand(string command, out bool exit, out Entry ent)
        {
            ent = new Entry();
            exit = false;
            if (!writeMode)
            {
                // only string
                if (command == ".clear")
                {
                    this.consoleText = "Archer Tournament PST CONSOLE";
                    ent.Text = ".";
                }
                else if (command == ".logs")
                {
                    this.consoleText += $"\n-- - \n" +  Application.Current.Properties["LOGS"].ToString();
                    ent.Text = ".";
                }
                else if (command == ".info")
                {
                    this.consoleText += "\n-- - \n" +
                        "Witaj w aplikacji stworzonej do zawodów w strzelictwie łukowym." +
                        "\nNazwa: Archer Tournament PST" +
                        "\nWersja: 0.44 - 07-08-2019r." +
                        "\nWykonawca: Tomasz Radomski" +
                        "\n Software: Visual Studio 2017 - Xamarine";
                    ent.Text = ".";
                }
                else if (command == ".help")
                {
                    this.consoleText += "\n-- - \n" +
                        ".clear" +
                        "\n.info\n.pass: zmienia hasło" +
                        "\n.base: aktualny tryb" +
                            "\n\t-test: do testów" +
                            "\n\t-rank :do gier rankingowych" +
                            "\n\t-custom: do gier poza rankingiem" +
                            "\n\t-rst:* resetuje dane aktualnego trybu" +
                        "\n.background" +
                            "\n\t-classic" +
                            "\n\t-new" +
                            "\n\t-ground" +
                            "\n\t-archery" +
                            "\n\t-default" +
                        "\n.setelo:* Ustawia elo graczowi" +
                        "\n.pass: Zmień/Wprowadź hasło" +
                        "\n.login Loguje na admina" +
                            "\n\t.login-c Sprawdza czy jestes zalogowany" +
                        "\n.logs" +
                        "\n.exit";
                    ent.Text = ".";
                }

                // password
                else if (command == ".pass")
                {
                    if (sets.PasswordExist())
                    {
                        this.consoleText += "\n-- - \n" + "Wprowadź stare hasło";
                        passwordOld = true;
                        writeMode = true;
                        ent.Text = "";
                    }
                    else
                    {
                        this.consoleText += "\n-- - \n" + "Wprowadź hasło";
                        setPassword = true;
                        writeMode = true;
                        ent.Text = "";
                    }
                }
                else if (command == ".login")
                {
                    Password();
                }

                else if (command == ".login-c")
                {
                    if(accessMode)
                        this.consoleText += "\n-- - \n" + "Zalogowany";
                    else
                        this.consoleText += "\n-- - \n" + "Niezalogowany";
                }

                else if(command == ".backdoor-b")
                {
                    this.consoleText += "\nPodaj nowe hasło";
                    ent.Text = "";
                    writeMode = true;
                    setPassword = true;
                }

                //test

                else if (command == ".test-read")
                {
                    this.consoleText += $"\n-- - \n" + "Test(int)=" + Application.Current.Properties["id"].ToString();
                    ent.Text = ".";
                }
                else if (command == ".test-write")
                {
                    this.consoleText += $"\n-- - \n" + "Wpisz wartośc: ";
                    writeMode = true;
                    testWrite = true;
                    ent.Text = "";
                }
                else if (command == ".exit")
                {
                    accessMode = false;
                    writeMode = false;
                    exit = true;
                }

                //base
                else if (command == ".base")
                {
                    this.consoleText += "\n-- - \n" + "Tryb :" + sets.Server;
                    ent.Text = ".";
                }
                else if (command == ".base-test")
                {
                    this.consoleText += "\n-- - \n" + "Zmieniono tryb na: Testowy";
                    this.consoleText += "\nZrestartuj aplikacje";
                    sets.SetServer(3);
                    ent.Text = ".";
                    
                }
                else if (command == ".base-rank")
                {
                    this.consoleText += "\n-- - \n" + "Zmieniono tryb na: Rankingowy";
                    sets.SetServer(1);
                    ent.Text = ".";
                }
                else if (command == ".base-custom")
                {
                    sets.SetServer(2);
                    this.consoleText += "\n-- - \n" + "Zmieniono tryb na: Swobodny";
                    ent.Text = ".";
                }
                else if (command == ".base-rst")
                {
                    if (accessMode)
                    {
                        ResetRank();
                        this.consoleText += "\n-- - \n" + "Done! HARD RESET";
                        this.consoleText += "\n-- - \n" + "Done!";
                        sets.AddLogs(DateTime.Now.ToString());
                        sets.AddLogs("HARD RESET");
                        ent.Text = ".";
                    }
                    else
                    {
                        this.consoleText += "\nBrak Dostępu ~!~";
                        ent.Text = ".";
                    }
                }

                //Elo up
                else if (command == ".setelo")
                {
                    this.consoleText += "\n-- - \n" + "Wpisz nazwę gracza: ";
                    writeMode = true;
                    eloModeName = true;
                    ent.Text = "";
                }


                //background
                else if (command == ".background-classic") // white
                {
                    sets.SetBackGround(1);
                    this.consoleText += "\n-- - \n" + "Done!";
                    sets.AddLogs(DateTime.Now.ToString());
                    sets.AddLogs("Załadowano styl classic");
                    ent.Text = ".";
                }
                else if (command == ".background-new")
                {
                    sets.SetBackGround(2);
                    this.consoleText += "\n-- - \n" + "Done!";
                    sets.AddLogs(DateTime.Now.ToString());
                    sets.AddLogs("Załadowano styl new");
                    ent.Text = ".";
                }
                else if (command == ".background-ground")
                {
                    sets.SetBackGround(3);
                    this.consoleText += "\n-- - \n" + "Done!";
                    sets.AddLogs(DateTime.Now.ToString());
                    sets.AddLogs("Załadowano styl groud");
                    ent.Text = ".";
                }
                else if (command == ".background-archery")
                {
                    sets.SetBackGround(4);
                    this.consoleText += "\n-- - \n" + "Done!";
                    sets.AddLogs(DateTime.Now.ToString());
                    sets.AddLogs("Załadowano styl archery");
                    ent.Text = ".";
                }
                else if (command == ".background-default")
                {
                    sets.SetBackGround(0);
                    this.consoleText += "\n-- - \n" + "Done!";
                    sets.AddLogs(DateTime.Now.ToString());
                    sets.AddLogs("Załadowano styl default");
                    ent.Text = ".";
                }

                // else
                else if (command.IndexOf('.') == 0)
                {
                    this.command = command;
                    this.consoleText += ("\n-- - \n" + "Nie znam komendy: " + command);
                    ent.Text = ".";
                }
                else
                {
                    consoleText += "\n --- \n Brak kropki!";
                    ent.Text = ".";
                }
            }
            else // Tryb wpisywania -!
            {
                // Test
                if(testWrite)
                {
                    if (int.TryParse(command, out int result))
                    {
                        sets.WriteTest(result);
                        this.consoleText +=  result + "\nDone!";
                        ent.Text = ".";
                    }
                    else
                    {
                        this.consoleText += "\nZła wartośc! Tylko liczby całkowite";
                        command = ".";
                        writeMode = false;
                    }

                    testWrite = false;
                    writeMode = false;
                }

                //Set Elo
                else if(eloModeName && accessMode)
                {
                    if (mySqliteBase.PlayerExist(command))
                    {
                        currentPlayer = mySqliteBase.GetPlayer(command);
                        this.consoleText += command + "\nWprowadź Elo: ";
                        setElo = true;
                        ent.Text = "";
                    }
                    else
                    {
                        this.consoleText += "\nTaki gracz nie istnieje.";
                        writeMode = false;
                        ent.Text = ".";
                    }
                    eloModeName = false;
                }
                else if(setElo)
                {
                    if (float.TryParse(command, out float result))
                    {
                        currentPlayer.elo = result;
                        mySqliteBase._dbconnection.Update(currentPlayer);
                        this.consoleText += result + "\nDone!";
                        sets.AddLogs(DateTime.Now.ToString());
                        sets.AddLogs("Zmieniono Elo");
                        ent.Text = ".";
                    }
                    else
                    {
                        this.consoleText += "\nPodany wartość nie jest liczbą rzeczywistą";
                        ent.Text = ".";
                    }
                    setElo = false;
                    writeMode = false;
                }

                //Password
                else if(passwordOld)
                {
                    if (sets.PasswordCheck(command))
                    {
                        this.consoleText += "\nPodaj nowe hasło";
                        ent.Text = "";
                        passwordOld = false;
                        setPassword = true;
                    }
                    else
                    {
                        this.consoleText += "\nZłe hasło";
                        ent.Text = ".";
                        writeMode = false;
                        setPassword = false;
                    }

                }
                else if (setPassword)
                {
                    if(sets.PasswordCheck(command) == false)
                    {
                        if(command.Length >= 4)
                        {
                            sets.PasswordChange(command);
                            this.consoleText += "\nDone!";
                            sets.AddLogs(DateTime.Now.ToString());
                            sets.AddLogs("Zmieniono hasło");
                        }
                        else
                        {
                            this.consoleText += "\nLiczba znaków >= 4";
                        }
                    }
                    else
                    {
                        this.consoleText += "\nTakie samo hasło co wcześniej";
                    }
                    writeMode = false;
                    setPassword = false;
                    ent.Text = ".";
                }
                else if(passwordMode)
                {
                    if(sets.PasswordCheck(command))
                    {
                        accessMode = true;
                        this.consoleText += "\nDone!";
                    }
                    else
                    {
                        this.consoleText += "\nBłędne hasło ~!~";
                    }
                    writeMode = false;
                    passwordMode = false;
                    ent.Text = ".";
                }


                else
                {
                    this.consoleText += "\nBrak Dostępu ~!~";
                    ent.Text = ".";
                    writeMode = false;
                }
            }
        }

        // ---- !

        public void Password()
        {
            if (sets.PasswordExist())
            {

                this.consoleText += "\n-- - \n" + "password: ";
                passwordMode = true;
                writeMode = true;
            }
            else
            {
                this.consoleText += "\n-- - \n" + "Użyj najpierw komendy .pass aby dostać uprawnienia do tej komendy";
            }
        }


        public string ConsoleText
        {
            get { NotifyPropertyChanged(nameof(ConsoleText)); return this.consoleText; }
        }

        private void ResetRank()
        {
            mySqliteBase._dbconnection.DeleteAll<Player>();
            mySqliteBase._dbconnection.DeleteAll<EloTable>();
        }

    }
}
