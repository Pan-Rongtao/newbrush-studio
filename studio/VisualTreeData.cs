using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using System.Windows.Data;
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

        static public NodeItem WindowNode(string name) { return new NodeItem() { IconType = PackIconKind.Airplay, Name = "[" + name + "]"}; }
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
}
