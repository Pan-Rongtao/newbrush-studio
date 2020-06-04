using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studio
{
    class ViewModel
    {
        public static LogData LogData { get { return _logData; } }
        private static LogData _logData = new LogData();

        public static NotifyProperyDescriptorCollection PropertiesGridPanelData { get { return _propertieData; } }
        private static NotifyProperyDescriptorCollection _propertieData = new NotifyProperyDescriptorCollection();

        public static NodeData VisualTreeModel { get { return _visualTreeModel; } }
        private static NodeData _visualTreeModel = NodeData.Empty;

        public static List<Plugin> Plugins { get { return _plugins; } }
        private static List<Plugin> _plugins = new List<Plugin>();

        public static Library Library { get { return _library; } }
        private static Library _library = new Library();
    }
}
