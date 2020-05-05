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
        static public LogData LogData
        {
            get { return _logData; }
            set { _logData = value; }
        }

        public OutputPanel()
        {
            InitializeComponent();

            LogData.AddEvent += OutputLog_AddEvent;
            LogData.ClearEvent += OutputLog_ClearEvent;

            foreach(LogItem item in LogData.Items)
            {
                Log(item.Level, item.Text);
            }

        }

        private void OutputLog_ClearEvent(object sender, EventArgs e)
        {
            this.pg.Inlines.Clear();
        }

        private void OutputLog_AddEvent(object sender, LogItem e)
        {
            Log(e.Level, e.Text);
        }
        
        private void Log(LogLevel level, string msg)
        {
            string s = "[" + DateTime.Now.ToString("MM-dd hh:mm:ss.fff") + "] " + msg;
            Run run = new Run(s);
            Color c = level == LogLevel.Info ? Colors.Black : level == LogLevel.Warn ? Colors.Orange : Colors.Red;
            run.Foreground = new SolidColorBrush(c);
            this.pg.Inlines.Add(run);
            this.pg.Inlines.Add(new LineBreak());
        }

        #region Data
        static private LogData _logData = new LogData();
        #endregion
    }
}
