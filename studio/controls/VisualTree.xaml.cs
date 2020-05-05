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
    public class VisibilityIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? PackIconKind.EyeOutline : PackIconKind.EyeOffOutline;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// VisualTree.xaml 的交互逻辑
    /// </summary>
    public partial class VisualTree : UserControl
    {
        public static String SelectNodePath;
        public static ObservableCollection<NodeData> Model
        {
            get { return _model; }
            set { _model = value; }
        }
        static private ObservableCollection<NodeData> _model = new ObservableCollection<NodeData>();

        public VisualTree()
        {
            InitializeComponent();
            InitData();
            UpdateAddGroupMenuComponent();
        }

        public void ExpandAll(bool expanded)
        {
            loopExpand(tv, expanded);
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

        private void InitData()
        {
            Window w = new Window();
/*            NodeItem grid = NodeItem.Grid("Grid");
            NodeItem grid1 = NodeItem.Grid("Grid1");

            root.Children.Add(grid);
            root.Children.Add(grid1);
            grid.Children.Add(NodeItem.ButtonNode("Button"));
            grid.Children.Add(NodeItem.ToggleButtonNode("ToggleButton"));
            grid.Children.Add(NodeItem.RadioButton("RadioButton"));
            grid.Children.Add(NodeItem.CheckBox("CheckBox"));
            grid.Children.Add(NodeItem.Label("Label"));
            grid.Children.Add(NodeItem.ToolTip("ToopTip"));
            grid.Children.Add(NodeItem.ScrollViewer("ScrollViewer"));
            grid.Children.Add(NodeItem.UserControl("UserControl"));
            grid.Children.Add(NodeItem.ComboBoxItem("ComboBoxItem"));
            grid.Children.Add(NodeItem.ListBoxItem("ListBoxItem"));
            grid.Children.Add(NodeItem.GropBox("GropBox"));
            grid.Children.Add(NodeItem.Expander("Expander"));
            grid.Children.Add(NodeItem.TabItem("TabItem"));
            grid.Children.Add(NodeItem.ListBox("ListBox"));
            grid.Children.Add(NodeItem.ListView("ListView"));
            grid.Children.Add(NodeItem.TabControl("TabControl"));
            grid.Children.Add(NodeItem.TreeView("TreeView"));
            grid.Children.Add(NodeItem.StatusBar("StatusBar"));
            grid.Children.Add(NodeItem.Menu("Menu"));
            grid.Children.Add(NodeItem.TextBox("TextBox"));
            grid.Children.Add(NodeItem.RickTextBox("RickTextBox"));
            grid.Children.Add(NodeItem.PasswordBox("PasswordBox"));
            grid.Children.Add(NodeItem.Image("Image"));
            grid.Children.Add(NodeItem.TextBlock("TextBlock"));

            grid.Children.Add(NodeItem.Line("Line"));
            grid.Children.Add(NodeItem.Polyline("Polyline"));
            grid.Children.Add(NodeItem.Polygon("Polygon"));
            grid.Children.Add(NodeItem.Path("Path"));
            grid.Children.Add(NodeItem.Rectangle("Rectangle"));
            grid.Children.Add(NodeItem.Ellipse("Ellipse"));

            grid.Children.Add(NodeItem.Canvas("Canvas"));
            grid.Children.Add(NodeItem.DockPanel("DockPanel"));
            grid.Children.Add(NodeItem.WrapPanel("WrapPanel"));
            grid.Children.Add(NodeItem.StackPanel("StackPanel"));
            grid.Children.Add(NodeItem.Grid("Grid"));
            grid.Children.Add(NodeItem.UniformGrid("UniformGrid"));
*/
            tv.ItemsSource = Model;
            Model.Add(w);

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
                    path = dc.Name + ".";
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
            SelectNodePath = GetFullPath(e.OriginalSource as DependencyObject);
        }

        private void createGrid_Click(object sender, RoutedEventArgs e)
        {
        }
        
        private void createRectangle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MetaObject md = PluginManager.FindMetaDataByTypeName("nb::Rectangle");
                AddNodeRequest request = new AddNodeRequest() { Path = SelectNodePath, ChildType = md.Descriptor.Type, ChildName = "Rectangle" };
                var reply = RpcManager.NodeClient.AddNode(request);
            }
            catch (Grpc.Core.RpcException ex) { Console.WriteLine(ex.Message); }
        }

        public void UpdateAddGroupMenuComponent()
        {
            foreach(Plugin plugin in PluginManager.Plugins)
            {
                foreach(MetaObject m in plugin.MetaObjects)
                {
                    MenuItem item = new MenuItem();
                    item.Header = m.Descriptor.DisplayName;
                    PackIcon icon = new PackIcon();
                    icon.Kind = MetaObject.ClassDescriptor.TypeToIcon(m.Descriptor.Type);
                    icon.Margin = new Thickness(5, 0, 0, 0);
                    item.Icon = icon;
                    item.Click += Item_Click;
                    item.Tag = m;
                    this.AddGroupMenuItem.Items.Add(item);
                }
            }

        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            MetaObject metaObj = item.Tag as MetaObject;
            AddNodeRequest request = new AddNodeRequest() { Path = SelectNodePath, ChildType = metaObj.Descriptor.Type, ChildName = metaObj.Descriptor.DisplayName };
            var reply = RpcManager.NodeClient.AddNode(request);
            
            NodeData node = new NodeData() { TypeName = metaObj.Descriptor.Type, IconType = MetaObject.ClassDescriptor.TypeToIcon(metaObj.Descriptor.Type), Name = "111" };
            Model.Add(node);
        }
    }
}
