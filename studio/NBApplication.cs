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
                Process.Start("D:/github/newbrush/dist/win32/lib/nbplayer.exe", "abc");
            }
            catch (Exception) { }
        }

    }
}
