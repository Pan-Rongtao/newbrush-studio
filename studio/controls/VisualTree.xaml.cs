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

    public class NodeItem
    {
        public PackIconKind IconType
        {
            get { return _iconType; }
            set
            {
                _iconType = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public bool Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
            }
        }

        public List<NodeItem> Children { get; set; }
        public NodeItem()
        {
            Children = new List<NodeItem>();
        }

        private PackIconKind _iconType = PackIconKind.GlobeLight;
        private string _name;
        private bool _visibility = true;

        static public NodeItem WindowNode(string name) { return new NodeItem() { IconType = PackIconKind.Airplay, Name = "[" + name + "]" }; }
        static public NodeItem ButtonNode(string name) { return new NodeItem() { IconType = PackIconKind.AlphabetBBoxOutline, Name = "[" + name + "]" }; }
        static public NodeItem ToggleButtonNode(string name) { return new NodeItem() { IconType = PackIconKind.ToggleSwitchOutline, Name = "[" + name + "]" }; }
        static public NodeItem RadioButton(string name) { return new NodeItem() { IconType = PackIconKind.RadioButtonChecked, Name = "[" + name + "]" }; }
        static public NodeItem CheckBox(string name) { return new NodeItem() { IconType = PackIconKind.CheckBoxOutline, Name = "[" + name + "]" }; }
        static public NodeItem Label(string name) { return new NodeItem() { IconType = PackIconKind.LabelOutline, Name = "[" + name + "]" }; }
        static public NodeItem ToolTip(string name) { return new NodeItem() { IconType = PackIconKind.TooltipOutline, Name = "[" + name + "]" }; }
        static public NodeItem ScrollViewer(string name) { return new NodeItem() { IconType = PackIconKind.ImageSizeSelectLarge, Name = "[" + name + "]" }; }
        static public NodeItem UserControl(string name) { return new NodeItem() { IconType = PackIconKind.GlobeLight, Name = "[" + name + "]" }; }
        static public NodeItem ComboBoxItem(string name) { return new NodeItem() { IconType = PackIconKind.BallotOutline, Name = "[" + name + "]" }; }
        static public NodeItem ListBoxItem(string name) { return new NodeItem() { IconType = PackIconKind.FormatListBulleted, Name = "[" + name + "]" }; }
        static public NodeItem GropBox(string name) { return new NodeItem() { IconType = PackIconKind.IframeOutline, Name = "[" + name + "]" }; }
        static public NodeItem Expander(string name) { return new NodeItem() { IconType = PackIconKind.ArrowCollapseVertical, Name = "[" + name + "]" }; }
        static public NodeItem TabItem(string name) { return new NodeItem() { IconType = PackIconKind.FolderTableOutline, Name = "[" + name + "]" }; }
        static public NodeItem ComboBox(string name) { return new NodeItem() { IconType = PackIconKind.BallotOutline, Name = "[" + name + "]" }; }
        static public NodeItem ListBox(string name) { return new NodeItem() { IconType = PackIconKind.FormatListBulleted, Name = "[" + name + "]" }; }
        static public NodeItem ListView(string name) { return new NodeItem() { IconType = PackIconKind.FileTableBoxOutline, Name = "[" + name + "]" }; }
        static public NodeItem TabControl(string name) { return new NodeItem() { IconType = PackIconKind.Tab, Name = "[" + name + "]" }; }
        static public NodeItem TreeView(string name) { return new NodeItem() { IconType = PackIconKind.FileTree, Name = "[" + name + "]" }; }
        static public NodeItem StatusBar(string name) { return new NodeItem() { IconType = PackIconKind.ExpansionCardVariant, Name = "[" + name + "]" }; }
        static public NodeItem Menu(string name) { return new NodeItem() { IconType = PackIconKind.ContentCopy, Name = "[" + name + "]" }; }
        static public NodeItem TextBox(string name) { return new NodeItem() { IconType = PackIconKind.TextRecognition, Name = "[" + name + "]" }; }
        static public NodeItem RickTextBox(string name) { return new NodeItem() { IconType = PackIconKind.SignatureText, Name = "[" + name + "]" }; }
        static public NodeItem PasswordBox(string name) { return new NodeItem() { IconType = PackIconKind.TextboxPassword, Name = "[" + name + "]" }; }
        static public NodeItem Image(string name) { return new NodeItem() { IconType = PackIconKind.FileImageOutline, Name = "[" + name + "]" }; }
        static public NodeItem TextBlock(string name) { return new NodeItem() { IconType = PackIconKind.FormatTextRotationNone, Name = "[" + name + "]" }; }

        static public NodeItem Line(string name) { return new NodeItem() { IconType = PackIconKind.VectorLine, Name = "[" + name + "]" }; }
        static public NodeItem Polyline(string name) { return new NodeItem() { IconType = PackIconKind.VectorPolyline, Name = "[" + name + "]" }; }
        static public NodeItem Polygon(string name) { return new NodeItem() { IconType = PackIconKind.VectorPolygon, Name = "[" + name + "]" }; }
        static public NodeItem Path(string name) { return new NodeItem() { IconType = PackIconKind.MapMarkerPath, Name = "[" + name + "]" }; }
        static public NodeItem Rectangle(string name) { return new NodeItem() { IconType = PackIconKind.RectangleOutline, Name = "[" + name + "]" }; }
        static public NodeItem Ellipse(string name) { return new NodeItem() { IconType = PackIconKind.EllipseOutline, Name = "[" + name + "]" }; }


        static public NodeItem Canvas(string name) { return new NodeItem() { IconType = PackIconKind.DrawingBox, Name = "[" + name + "]" }; }
        static public NodeItem DockPanel(string name) { return new NodeItem() { IconType = PackIconKind.ViewQuilt, Name = "[" + name + "]" }; }
        static public NodeItem WrapPanel(string name) { return new NodeItem() { IconType = PackIconKind.FormatTextWrappingWrap, Name = "[" + name + "]" }; }
        static public NodeItem StackPanel(string name) { return new NodeItem() { IconType = PackIconKind.ViewWeek, Name = "[" + name + "]" }; }
        static public NodeItem Grid(string name) { return new NodeItem() { IconType = PackIconKind.Grid, Name = "[" + name + "]" }; }
        static public NodeItem UniformGrid(string name) { return new NodeItem() { IconType = PackIconKind.GridLarge, Name = "[" + name + "]" }; }
    }
    /// <summary>
    /// VisualTree.xaml 的交互逻辑
    /// </summary>
    public partial class VisualTree : UserControl
    {
        public static List<NodeItem> Model
        {
            get { return _model; }
            set { _model = value; }
        }
        static private List<NodeItem> _model = new List<NodeItem>();

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

            Model.Add(root);
            tv.ItemsSource = Model;
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
        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }
    }
}
