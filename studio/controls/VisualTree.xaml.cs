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
    /// VisualTree.xaml 的交互逻辑
    /// </summary>
    public partial class VisualTree : UserControl
    {
        public VisualTree()
        {
            InitializeComponent();
            InitData();
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
            List<NodeItem> itemList = new List<NodeItem>();
            NodeItem root = NodeItem.WindowNode("Window");
            NodeItem grid = NodeItem.Grid("Grid");
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

            itemList.Add(root);
            tv.ItemsSource = itemList;
        }
        
    }
}
