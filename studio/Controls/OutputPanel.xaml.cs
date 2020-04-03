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

namespace studio
{
    /// <summary>
    /// OutputPanel.xaml 的交互逻辑
    /// </summary>
    public partial class OutputPanel : UserControl
    {
        public OutputPanel()
        {
            InitializeComponent();

            Info("this is a info message");
            Warn("this is a warn message");
            Error("this is a error message");
        }

        public void Info(string msg)
        {
            Log(msg, Colors.Black);
        }

        public void Warn(string msg)
        {
            Log(msg, Colors.Orange);
        }

        public void Error(string msg)
        {
            Log(msg, Colors.Red);
        }

        private void Log(string msg, Color c)
        {
            string s = "[" + DateTime.Now.ToString("MM-dd hh:mm:ss.fff") + "] " + msg;
            Run run = new Run(s);
            run.Foreground = new SolidColorBrush(c);
            this.pg.Inlines.Add(run);
            this.pg.Inlines.Add(new LineBreak());
        }
    }
}
