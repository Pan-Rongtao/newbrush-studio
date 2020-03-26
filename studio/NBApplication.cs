using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace studio
{
    class NBPlayer
    {
        static public void launch()
        {
            try
            {
                string s = Environment.CurrentDirectory;
                string nbPlayerPath = s + "/../../../../newbrush/dist/lib/nbplayer.exe";
                Process.Start(nbPlayerPath);
            }
            catch (Exception) { }
        }

    }
}
