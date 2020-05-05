using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studio
{
    public enum LogLevel
    {
        Info = 0,
        Warn,
        Error,
    }

    public class LogData
    {
        public List<LogItem> Items
        {
            get { return _logList; }
            set { _logList = value; }
        }

        public event EventHandler<LogItem> AddEvent;
        public event EventHandler ClearEvent;

        public void Add(LogLevel level, string text)
        {
            LogItem item = new LogItem() { Level = level, Text = text };
            _logList.Add(item);
            if(AddEvent != null)
                AddEvent(this, item);
        }

        public void Clear()
        {
            _logList.Clear();
            if (ClearEvent != null)
                ClearEvent(this, new EventArgs());
        }

        private List<LogItem> _logList = new List<LogItem>();
    }

    public class LogItem
    {
        public LogLevel Level;
        public string Text;
    }
    
}
