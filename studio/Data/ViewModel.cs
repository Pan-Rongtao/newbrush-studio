using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studio
{
    class ViewModel
    {
        static public LogData LogData
        {
            get { return _logData; }
            set { _logData = value; }
        }
        static private LogData _logData = new LogData();

        static public NotifyProperyDescriptorCollection PropertiesData
        {
            get { return _propertieData; }
            set { _propertieData = value; }
        }
        static private NotifyProperyDescriptorCollection _propertieData = new NotifyProperyDescriptorCollection();

        public static NodeData VisualTreeModel
        {
            get { return _visualTreeModel; }
            set { _visualTreeModel = value; }
        }
        static private NodeData _visualTreeModel = new NodeData();

    }
}
