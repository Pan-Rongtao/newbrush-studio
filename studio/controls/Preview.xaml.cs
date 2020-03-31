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
        public Preview()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            launch();
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, short State);

        private Process preview;
        private void launch()
        {
            try
            {
                string s = Environment.CurrentDirectory;
                string nbPlayerPath = s + "/../../../../newbrush/dist/lib/nbplayer.exe";
                preview = Process.Start(nbPlayerPath);
                Thread.Sleep(1000);

                form1.Child = new System.Windows.Forms.PictureBox();

                SetParent(preview.MainWindowHandle, form1.Handle); //panel1.Handle为要显示外部程序的容器
                ShowWindow(preview.MainWindowHandle, 3);
            }
            catch (Exception) { }
        }

        public Rect GetAbsolutePlacement(FrameworkElement element, bool relativeToScreen = false)
        {
            Point absolutePos;
            try
            {
                absolutePos = element.PointToScreen(new System.Windows.Point(0, 0));
            }
            catch(Exception)
            {
                return new Rect();
            }
            if (relativeToScreen)
            {
                return new Rect(absolutePos.X, absolutePos.Y, element.ActualWidth, element.ActualHeight);
            }
            var posMW = Application.Current.MainWindow.PointToScreen(new System.Windows.Point(0, 0));
            absolutePos = new System.Windows.Point(absolutePos.X - posMW.X, absolutePos.Y - posMW.Y);
            return new Rect(absolutePos.X, absolutePos.Y, element.ActualWidth, element.ActualHeight);
        }
        
    }
}
