using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace Utilities.CommonClass
{
   public class SoundPlayerHelper
    {
        public static string configpath = AppDomain.CurrentDomain.BaseDirectory + "\\music\\8737.wav";
        public static void Play()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = configpath;
            player.Load();
            player.Play();
        }
    }
}
