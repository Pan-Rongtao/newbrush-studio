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
using MaterialDesignThemes.Wpf;
using System.Globalization;
using System.Data;
using Nbrpc;
using System.Collections.ObjectModel;

namespace studio
{
    /// <summary>
    /// VisualTree.xaml 的交互逻辑
    /// </summary>
    public partial class VisualTree : UserControl
    {
        public static string SelectItemPath { get { return _selectItemPath; } }
        private static string _selectItemPath;

        public VisualTree()
        {
            InitializeComponent();
            tv.ItemsSource = ViewModel.VisualTreeModel.Children;

            UpdateAddGroupMenuComponent();
        }

        public void ExpandAll(ItemsControl c, bool expanded)
        {
            loopExpand(c, expanded);
        }

        private void loopExpand(ItemsControl ctrl, bool expanded)
        {
            if(ctrl == null)
            {
                return;
            }

            foreach(Object item in ctrl.Items)
            {
                TreeViewItem treeItem = ctrl.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if(treeItem == null || !treeItem.HasItems)
                {
                    continue;
                }

                treeItem.IsExpanded = expanded;
                loopExpand(treeItem, expanded);
            }
        }
        
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }

        DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }
        
        private string GetFullPath(DependencyObject source)
        {
            string path = "";
            while (source != null && source.GetType() != typeof(TreeView))
            {
                if (source.GetType() == typeof(TreeViewItem))
                {
                    NodeData dc = ((TreeViewItem)source).DataContext as NodeData;
                    path = dc.Name + "." + path;
                }
                source = VisualTreeHelper.GetParent(source);
            }
            if(!String.IsNullOrEmpty(path))
            {
                path = path.Substring(0, path.Length - 1);
            }

            return path;
        }
        
        private void treeView1_Selected(object sender, RoutedEventArgs e)
        {
            _selectItemPath = GetFullPath(e.OriginalSource as DependencyObject);
            NodeData dc = (e.OriginalSource as TreeViewItem).DataContext as NodeData;
            ViewModel.PropertiesGridPanelData.Data = dc.PropertyGridData.Data;
        }

        public void UpdateAddGroupMenuComponent()
        {
            foreach(Plugin plugin in ViewModel.Plugins)
            {
                foreach(MetaObject m in plugin.MetaObjects)
                {
                    PackIcon icon = new PackIcon();
                    icon.Kind = MetaObject.ClassDescriptor.TypeToIcon(m.Descriptor.Type);
                    icon.Margin = new System.Windows.Thickness(5, 0, 0, 0);
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = m.Descriptor.DisplayName;
                    menuItem.Icon = icon;
                    menuItem.Click += MenuItem_Click;
                    menuItem.Tag = m;
                    this.AddGroupMenuItem.Items.Add(menuItem);
                }
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MetaObject metaObj = (sender as MenuItem).Tag as MetaObject;
            AddNodeRequest request = new AddNodeRequest();
            request.Path = SelectItemPath;
            request.ChildType = metaObj.Descriptor.Type;
            request.ChildName = metaObj.Descriptor.DisplayName;
            try
            {
                var reply = Rpc.NodeClient.AddNode(request);
            }
            catch (Exception ex)
            {
                ViewModel.LogData.Add(LogLevel.Error, ex.Message);
            }
            
            NodeData node = new NodeData(request.ChildType, request.ChildName);
            NodeData parent = ViewModel.VisualTreeModel.Find(SelectItemPath);
            parent.Children.Add(node);
            ExpandAll(tv, true);
        }
        
    }
}
