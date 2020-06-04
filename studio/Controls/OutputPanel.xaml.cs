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

            ViewModel.LogData.AddEvent += OutputLog_AddEvent;
            ViewModel.LogData.ClearEvent += OutputLog_ClearEvent;

            foreach(LogItem item in ViewModel.LogData.Items)
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
            string s = string.Format("[{0}][{1}] {2}", DateTime.Now.ToString("MM月dd日 hh:mm:ss.fff"), level == LogLevel.Info ? "信息" : level == LogLevel.Warn ? "警告" : "错误", msg);
            Run run = new Run(s);
            Color c = level == LogLevel.Info ? Colors.Black : level == LogLevel.Warn ? Colors.DarkOrange : Colors.Red;
            run.Foreground = new SolidColorBrush(c);
            this.pg.Inlines.Add(run);
            this.pg.Inlines.Add(new LineBreak());
        }
        
    }
}
