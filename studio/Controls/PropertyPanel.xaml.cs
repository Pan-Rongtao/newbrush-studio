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
using System.Dynamic;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Nbrpc;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace studio
{
    /// <summary>
    /// PropertyEditor.xaml 的交互逻辑
    /// </summary>
    public partial class PropertyPanel : UserControl
    {
        DispatcherTimer t = new DispatcherTimer();
        public PropertyPanel()
        {
            InitializeComponent();
            ViewModel.PropertiesGridPanelData.Changed += PropertiesData_Changed;
        }

        private void PropertiesData_Changed(object sender, MyPropertyDescriptorCollection e)
        {
            propertyGrid.SelectedObject = e;
        }
        
        private void propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            PropertyItem item = e.OriginalSource as PropertyItem;
            string path = VisualTree.SelectItemPath;
            UInt64 propertyID = (item.PropertyDescriptor as MyPropertyDescriptor).PropertyID;
            Type t = item.PropertyType;
            object v = item.Value;
            if (t == typeof(bool))
            {
                SetPropertyBoolRequest request = new SetPropertyBoolRequest() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (bool)(v) };
                var reply = Rpc.NodeClient.SetPropertyBool(request);
            }
            if (t == typeof(SByte))
            {
                SetPropertyInt8Request request = new SetPropertyInt8Request() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (SByte)(v) };
                var reply = Rpc.NodeClient.SetPropertyInt8(request);
            }
            else if (t == typeof(Int16))
            {
                SetPropertyInt16Request request = new SetPropertyInt16Request() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (Int16)(v) };
                var reply = Rpc.NodeClient.SetPropertyInt16(request);
            }
            else if (t == typeof(UInt16))
            {
                SetPropertyUInt16Request request = new SetPropertyUInt16Request() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (UInt16)(v) };
                var reply = Rpc.NodeClient.SetPropertyUInt16(request);
            }
            else if (t == typeof(Int32))
            {
                SetPropertyInt32Request request = new SetPropertyInt32Request() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (Int32)(v) };
                var reply = Rpc.NodeClient.SetPropertyInt32(request);
            }
            else if (t == typeof(UInt32))
            {
                SetPropertyUInt32Request request = new SetPropertyUInt32Request() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (UInt32)(v) };
                var reply = Rpc.NodeClient.SetPropertyUInt32(request);
            }
            else if (t == typeof(Int64))
            {
                SetPropertyInt64Request request = new SetPropertyInt64Request() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (Int64)(v) };
                var reply = Rpc.NodeClient.SetPropertyInt64(request);
            }
            else if (t == typeof(UInt64))
            {
                SetPropertyUInt64Request request = new SetPropertyUInt64Request() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (UInt64)(v) };
                var reply = Rpc.NodeClient.SetPropertyUInt64(request);
            }
            else if (t == typeof(string))
            {
                SetPropertyStringRequest request = new SetPropertyStringRequest() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (string)(v) };
                var reply = Rpc.NodeClient.SetPropertyString(request);
            }
            else if (t == typeof(float))
            {
                SetPropertyFloatRequest request = new SetPropertyFloatRequest() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (float)(v) };
                var reply = Rpc.NodeClient.SetPropertyFloat(request);
            }
            else if (t == typeof(double))
            {
                SetPropertyDoubleRequest request = new SetPropertyDoubleRequest() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = (double)(v) };
                var reply = Rpc.NodeClient.SetPropertyDouble(request);
            }
            else if (t == typeof(System.Windows.Media.Color))
            {
                System.Windows.Media.Color mc = (System.Windows.Media.Color)v;
                Nbrpc.Color c = new Nbrpc.Color() { A = mc.A, R = mc.R, G = mc.G, B = mc.B };
                SetPropertyColorRequest request = new SetPropertyColorRequest() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = c };
                var reply = Rpc.NodeClient.SetPropertyColor(request);
            }
            else if(t == typeof(string[]))
            {
                ArrayList itemsSource = (item.PropertyDescriptor as MyPropertyDescriptor).ItemsSource;
                int index = itemsSource.IndexOf(v);
                SetPropertyEnumRequest request = new SetPropertyEnumRequest() { Base = new SetPropertyBaseRequest() { Path = path, PropertyID = propertyID }, Value = index };
                var reply = Rpc.NodeClient.SetPropertyEnum(request);
                if(!reply.Success)
                {
                    ViewModel.LogData.Add(LogLevel.Error, reply.Msg);
                }
            }
        }

    }
}
