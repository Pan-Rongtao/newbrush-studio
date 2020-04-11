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

        public Preview()
        {
            InitializeComponent();
        //    launch();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //            launch();
            Plugin.update();
        }
        
        private void launch()
        {
            string path = Environment.CurrentDirectory + "/../../../../newbrush/dist/lib/nbplayer.exe";
            try
            {
                _process = Process.Start(path);
                Thread.Sleep(500);
                form1.Child = new System.Windows.Forms.PictureBox();
                SetParent(_process.MainWindowHandle, form1.Handle); //panel1.Handle为要显示外部程序的容器
                ShowWindow(_process.MainWindowHandle, 3);
            }
            catch (Exception) { }
        }
        
    }
}
