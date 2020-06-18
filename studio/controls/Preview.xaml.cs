using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
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

        DispatcherTimer t = new DispatcherTimer();
        public Preview()
        {
            InitializeComponent();
            t.Interval = new TimeSpan(100);
            t.Tick += T_Tick;
            t.Start();
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
                Thread.Sleep(1000);
                System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
                form1.Child = pb;
                Win32.SetParent(_process.MainWindowHandle, form1.Handle); //panel1.Handle为要显示外部程序的容器
                Win32.ShowWindow(_process.MainWindowHandle, 9);
            }
            catch (Exception) { }
        }
        
        
    }
}
