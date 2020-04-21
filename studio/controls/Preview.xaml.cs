using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;
using System.Windows.Threading;

namespace studio
{
    /// <summary>
    /// Preview.xaml 的交互逻辑
    /// </summary>
    public partial class Preview : UserControl
    {
        private Process _process;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, short State);

        DispatcherTimer t = new DispatcherTimer();
        public Preview()
        {
            InitializeComponent();

            t.Interval = new TimeSpan(100);
            t.Tick += T_Tick;
            t.Start();
            ResourceDictionary x;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            launch();
            t.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            launch();
        }
        
        public void launch()
        {
            if(Process.GetProcessesByName("nbPlayer").Length > 0)
            {
                return;
            }

            string path = Environment.CurrentDirectory + "/../../../../newbrush/dist/win32/lib/nbplayer.exe";
            try
            {
                _process = Process.Start(path);
                Thread.Sleep(500);
                System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
                form1.Child = pb;
                SetParent(_process.MainWindowHandle, form1.Handle); //panel1.Handle为要显示外部程序的容器
                ShowWindow(_process.MainWindowHandle, 9);
            }
            catch (Exception) { }
        }
        
        
    }
}
