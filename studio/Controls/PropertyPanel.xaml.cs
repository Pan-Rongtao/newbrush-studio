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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Dynamic;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace studio
{
    /// <summary>
    /// PropertyEditor.xaml 的交互逻辑
    /// </summary>
    public partial class PropertyPanel : UserControl
    {
        public PropertyPanel()
        {
            InitializeComponent();
            
            PropertiesData.Changed += PropertiesData_Changed1;
        }
        
        private void PropertiesData_Changed1(object sender, PropertyCollection e)
        {
            propertyGrid.SelectedObject = e;
        }

        static public NotifyProperyCollection PropertiesData = new NotifyProperyCollection();

        private void propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            var item = e.OriginalSource as PropertyItem;

            //Type t = Value.GetType();
            //if (t == typeof(bool))
            //{
            //    SetPropertyBoolRequest request = new SetPropertyBoolRequest() { Base = new SetPropertyBaseRequest() { }, Value = (bool)(Value) };
            //    var reply = RpcManager.NodeClient.SetPropertyBool(request);
            //}
            //else if (t == typeof(string))
            //{
            //    SetPropertyStringRequest request = new SetPropertyStringRequest() { Base = new SetPropertyBaseRequest() { }, Value = (string)(Value) };
            //    var reply = RpcManager.NodeClient.SetPropertyString(request);
            //}
        }
    }
    
}
