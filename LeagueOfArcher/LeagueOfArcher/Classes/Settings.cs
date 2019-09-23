using System;
using System.Collections.Generic;
using System.Text;
using LeagueOfArcher.Classes;
using Xamarin.Forms;
using SQLite;

namespace LeagueOfArcher.Classes
{
    public class Settings
    {
        // KEYS
        readonly string PASS = "PASSWORD";
        readonly string TEST = "TEST";
        readonly string LOGS = "LOGS";
        readonly string BACKGROUND = "BACKGROUND";
        readonly string TEXTCOLOR = "TEXTCOLOR";
        readonly string FIRSTRUN = "FIRSTRUN";
        readonly string SERVER = "SERVER";

        readonly SQLBase Mysqlitebase;

        public Settings() { }

        public Settings(ref SQLBase db) { Mysqlitebase = db; }

        #region test
        public void WriteTest (int id)
        {
            Application.Current.Properties[TEST] = id;
        }

        #endregion

        #region password

        public void PasswordChange(string password)
        {
            Application.Current.Properties[PASS] = password;
        }
        public bool PasswordExist()
        {
            if (Application.Current.Properties.ContainsKey(PASS))
                return true;
            else
            {
                Application.Current.Properties[PASS] = "";
                return false;
            }
        }
        public bool PasswordCheck(string password)
        {
            if ((string)Application.Current.Properties[PASS] == password)
                return true;
            else
                return false;
        }
        #endregion

        #region logs
        public void AddLogs(string logs)
        {
            Application.Current.Properties[LOGS] += logs + "\n";
        }
        public void FirstStartLog()
        {
            Application.Current.Properties[LOGS] = DateTime.Now.ToString() + "\n" + "Aplikacja uruchomiona prawidłowo" + "\n-- - \n";
        }
        public void EndLog()
        {
            Application.Current.Properties[LOGS] += "\n-- - \n";
        }
        #endregion

        #region FirstRun

        public void SetFirstRun()
        {
            if (Application.Current.Properties.ContainsKey(FIRSTRUN) == false)
            {
                System.Diagnostics.Debug.WriteLine("Jestem Tu");
                Application.Current.Properties[BACKGROUND] = "White";
                Application.Current.Properties[SERVER] = "monkeyBase_rank.db";
                Application.Current.Properties[TEXTCOLOR] = "Black";
                Application.Current.Properties[FIRSTRUN] = 1;
            }
        }

        public void FirstRun(bool value)
        {
            if (value)
                Application.Current.Properties[FIRSTRUN] = 1;
            else
                Application.Current.Properties.Remove(FIRSTRUN);
        }

        #endregion

        #region SetProp

        public void SetServer(int id)
        {
            if(id == 1)
                Application.Current.Properties[SERVER] = "monkeyBase_rank.db";
            else if (id ==2)
                Application.Current.Properties[SERVER] = "monkeyBase_custom.db";
            else if(id == 3)
                Application.Current.Properties[SERVER] = "monkeyBase_test.db";
            else
                Application.Current.Properties[SERVER] = "monkeyBase_rank.db";
        }

        public void SetBackGround(int id)
        {
            if (id == 1)
            {
                Application.Current.Properties[BACKGROUND] = "GhostWhite";
                Application.Current.Properties[TEXTCOLOR] = "Black";
            }
            else if (id == 2)
            {
                Application.Current.Properties[BACKGROUND] = "#2D2D2F";
                Application.Current.Properties[TEXTCOLOR] = "White";
            }
            else if (id == 3)
            {
                Application.Current.Properties[BACKGROUND] = "#4C1919";
                Application.Current.Properties[TEXTCOLOR] = "White";
            }
            else if (id == 4)
            {
                Application.Current.Properties[BACKGROUND] = "#194C33";
                Application.Current.Properties[TEXTCOLOR] = "White";
            }
            else
            {
                Application.Current.Properties[BACKGROUND] = "Default";
                Application.Current.Properties[TEXTCOLOR] = "Black";
            }
        }

        #endregion

        #region Właściwości

        public string Background
        {
            get { return Application.Current.Properties[BACKGROUND] as string; }
        }

        public string TextColor
        {
            get { return Application.Current.Properties[TEXTCOLOR] as string; }
        }

        public string Server
        {
            get { return Application.Current.Properties[SERVER] as string; }
        }



        #endregion

    }
}
