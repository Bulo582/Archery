using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace LeagueOfArcher.EFX
{
    public class Playmp3
    {
        public void PlayArrow()
        {
            try
            {

                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load(GetStreamFromFile("EFX.nice.mp3"));
                player.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                App.mysettings.AddLogs(ex.Message + " MainPage playsound!");
            }
        }
        public void PlayPeacefull()
        {
            try
            {

                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load(GetStreamFromFile("EFX.peacefull.mp3"));
                player.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                App.mysettings.AddLogs(ex.Message + " MainPage playsound!");
            }
        }


        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("LeagueOfArcher." + filename);

            return stream;
        }
    }
}
