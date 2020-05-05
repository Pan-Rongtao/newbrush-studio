using System;
using System.Collections.Generic;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

namespace studio
{
    public class NodeData
    {
        public String TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public PackIconKind IconType
        {
            get { return _iconType; }
            set
            {
                _iconType = value;
            }
        }

        public String Name
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

        public ObservableCollection<NodeData> Children { get; set; }

        public NodeData()
        {
            Children = new ObservableCollection<NodeData>();
        }

        private String _typeName;
        private PackIconKind _iconType = PackIconKind.GlobeLight;
        private string _name;
        private bool _visibility = true;


        static public NodeData ButtonNode(string name) { return new NodeData() { IconType = PackIconKind.AlphabetBBoxOutline, Name = "[" + name + "]" }; }
        static public NodeData ToggleButtonNode(string name) { return new NodeData() { IconType = PackIconKind.ToggleSwitchOutline, Name = "[" + name + "]" }; }
        static public NodeData RadioButton(string name) { return new NodeData() { IconType = PackIconKind.RadioButtonChecked, Name = "[" + name + "]" }; }
        static public NodeData CheckBox(string name) { return new NodeData() { IconType = PackIconKind.CheckBoxOutline, Name = "[" + name + "]" }; }
        static public NodeData Label(string name) { return new NodeData() { IconType = PackIconKind.LabelOutline, Name = "[" + name + "]" }; }
        static public NodeData ToolTip(string name) { return new NodeData() { IconType = PackIconKind.TooltipOutline, Name = "[" + name + "]" }; }
        static public NodeData ScrollViewer(string name) { return new NodeData() { IconType = PackIconKind.ImageSizeSelectLarge, Name = "[" + name + "]" }; }
        static public NodeData UserControl(string name) { return new NodeData() { IconType = PackIconKind.GlobeLight, Name = "[" + name + "]" }; }
        static public NodeData ComboBoxItem(string name) { return new NodeData() { IconType = PackIconKind.BallotOutline, Name = "[" + name + "]" }; }
        static public NodeData ListBoxItem(string name) { return new NodeData() { IconType = PackIconKind.FormatListBulleted, Name = "[" + name + "]" }; }
        static public NodeData GropBox(string name) { return new NodeData() { IconType = PackIconKind.IframeOutline, Name = "[" + name + "]" }; }
        static public NodeData Expander(string name) { return new NodeData() { IconType = PackIconKind.ArrowCollapseVertical, Name = "[" + name + "]" }; }
        static public NodeData TabItem(string name) { return new NodeData() { IconType = PackIconKind.FolderTableOutline, Name = "[" + name + "]" }; }
        static public NodeData ComboBox(string name) { return new NodeData() { IconType = PackIconKind.BallotOutline, Name = "[" + name + "]" }; }
        static public NodeData ListBox(string name) { return new NodeData() { IconType = PackIconKind.FormatListBulleted, Name = "[" + name + "]" }; }
        static public NodeData ListView(string name) { return new NodeData() { IconType = PackIconKind.FileTableBoxOutline, Name = "[" + name + "]" }; }
        static public NodeData TabControl(string name) { return new NodeData() { IconType = PackIconKind.Tab, Name = "[" + name + "]" }; }
        static public NodeData TreeView(string name) { return new NodeData() { IconType = PackIconKind.FileTree, Name = "[" + name + "]" }; }
        static public NodeData StatusBar(string name) { return new NodeData() { IconType = PackIconKind.ExpansionCardVariant, Name = "[" + name + "]" }; }
        static public NodeData Menu(string name) { return new NodeData() { IconType = PackIconKind.ContentCopy, Name = "[" + name + "]" }; }
        static public NodeData TextBox(string name) { return new NodeData() { IconType = PackIconKind.TextRecognition, Name = "[" + name + "]" }; }
        static public NodeData RickTextBox(string name) { return new NodeData() { IconType = PackIconKind.SignatureText, Name = "[" + name + "]" }; }
        static public NodeData PasswordBox(string name) { return new NodeData() { IconType = PackIconKind.TextboxPassword, Name = "[" + name + "]" }; }
        static public NodeData Image(string name) { return new NodeData() { IconType = PackIconKind.FileImageOutline, Name = "[" + name + "]" }; }
        static public NodeData TextBlock(string name) { return new NodeData() { IconType = PackIconKind.FormatTextRotationNone, Name = "[" + name + "]" }; }

        static public NodeData Line(string name) { return new NodeData() { IconType = PackIconKind.VectorLine, Name = "[" + name + "]" }; }
        static public NodeData Polyline(string name) { return new NodeData() { IconType = PackIconKind.VectorPolyline, Name = "[" + name + "]" }; }
        static public NodeData Polygon(string name) { return new NodeData() { IconType = PackIconKind.VectorPolygon, Name = "[" + name + "]" }; }
        static public NodeData Path(string name) { return new NodeData() { IconType = PackIconKind.MapMarkerPath, Name = "[" + name + "]" }; }
        static public NodeData Rectangle(string name) { return new NodeData() { IconType = PackIconKind.RectangleOutline, Name = "[" + name + "]" }; }
        static public NodeData Ellipse(string name) { return new NodeData() { IconType = PackIconKind.EllipseOutline, Name = "[" + name + "]" }; }


        static public NodeData Canvas(string name) { return new NodeData() { IconType = PackIconKind.DrawingBox, Name = "[" + name + "]" }; }
        static public NodeData DockPanel(string name) { return new NodeData() { IconType = PackIconKind.ViewQuilt, Name = "[" + name + "]" }; }
        static public NodeData WrapPanel(string name) { return new NodeData() { IconType = PackIconKind.FormatTextWrappingWrap, Name = "[" + name + "]" }; }
        static public NodeData StackPanel(string name) { return new NodeData() { IconType = PackIconKind.ViewWeek, Name = "[" + name + "]" }; }
        static public NodeData Grid(string name) { return new NodeData() { IconType = PackIconKind.Grid, Name = "[" + name + "]" }; }
        static public NodeData UniformGrid(string name) { return new NodeData() { IconType = PackIconKind.GridLarge, Name = "[" + name + "]" }; }
    }

    public class Window : NodeData
    {
        public Window()
        {
            TypeName = this.GetType().Name;
            Name = this.GetType().Name;
            IconType = PackIconKind.Airplay;
        }

        public NodeData Content
        {
            get { return _content; }
            set
            {
                _content = value;

            }
        }
        private NodeData _content;
    }

}
