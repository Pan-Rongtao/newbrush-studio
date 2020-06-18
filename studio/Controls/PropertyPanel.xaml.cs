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
        
        private void propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
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
            request.PropertyID = itemPropertyDescriptor.ID;
            request.PropertyType = itemPropertyDescriptor.TypeName;
            request.PropertyValue = CppCShapeTypeMapping.ConverToString(propertyValue);
            try
            {
                var reply = Rpc.NodeClient.SetProperty(request);
                if (!reply.Success)
                {
                    ViewModel.LogData.Add(LogLevel.Error, reply.Msg);
                }
            }
            catch(Exception ex)
            {
                ViewModel.LogData.Add(LogLevel.Error, ex.Message);
            }
        }

    }
}
