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
using Google.Protobuf;

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

        static public Google.Protobuf.ByteString ValueToString(object value)
        {
            Google.Protobuf.ByteString ret;
            if (value is bool || value is char || value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint
                 || value is long || value is ulong || value is Int64 || value is UInt64 || value is float || value is double || value is decimal || value is string
                 || value is Point || value is System.Windows.Media.Color || value is System.Windows.Thickness)
            {
                ret = Google.Protobuf.ByteString.CopyFromUtf8(value.ToString());
            }
            else if (value is System.Windows.Media.Color)
            {
                var c = (System.Windows.Media.Color)value;
                ret = Google.Protobuf.ByteString.CopyFromUtf8(string.Format("{0},{1},{2},{3}", c.R, c.G, c.B, c.A));
            }
            else if (value is ImageSource)
            {
                var bm = value as BitmapImage;
                string path = bm.UriSource.AbsolutePath;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);

                ret = Google.Protobuf.ByteString.CopyFrom(bytes);
            }
            else if (value is SolidColorBrush)
            {
                var c = (value as SolidColorBrush).Color;
                ret = Google.Protobuf.ByteString.CopyFromUtf8(string.Format("{0},{1},{2},{3}", c.R, c.G, c.B, c.A));
            }
            else if (value is ImageBrush)
            {
                ImageBrush imgBrush = value as ImageBrush;
                var bm = imgBrush.ImageSource as BitmapImage;
                string path = bm.UriSource.AbsolutePath;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);

                ret = Google.Protobuf.ByteString.CopyFrom(bytes);
            }
            else if (value is List<bool> || value is List<int> || value is List<float> || value is List<double>)
            {
                ret = Google.Protobuf.ByteString.CopyFromUtf8(value.ToString());
            }
            else
            {
                //throw new Exception("ConverToString");
                ret = Google.Protobuf.ByteString.CopyFromUtf8("");
            }
            return ret;
        }


        private async void propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            PropertyItem item = e.OriginalSource as PropertyItem;
            //PropertyGrid嵌套PropertyGrid的情况，父PropertyGrid也会收到changed消息
            //这里过滤子PropertyGrid的情况
            MyPropertyDescriptor itemPropertyDescriptor = item.PropertyDescriptor as MyPropertyDescriptor;
            if (itemPropertyDescriptor == null)
            {
                return;
            }
            
            Type propertyType = item.PropertyType;
            object propertyValue = item.Value;

            var request = new SetPropertyRequest();
            request.Path = VisualTree.SelectItemPath;
            request.PropertyID = itemPropertyDescriptor.MetaPropertyDescriptor.CppTypeID;
            request.PropertyType = itemPropertyDescriptor.MetaPropertyDescriptor.CppTypeName;
            request.PropertyValue = ValueToString(propertyValue);
            try
            {
                var s = Rpc._channel.State;
                var reply = await Rpc.NodeClient.SetPropertyAsync(request);
                if (!reply.Success)
                {
                    ViewModel.LogData.Add(LogLevel.Error, reply.Msg);
                }
            }
            catch(Exception ex)
            {
                ViewModel.LogData.Add(LogLevel.Error, ex.Message);
            }
            ViewModel.LogData.Add(LogLevel.Info, "{0} changed to {1}", itemPropertyDescriptor.DisplayName, propertyValue);
        }

    }
}
