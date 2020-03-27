using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace studio
{
    public class NodeItem
    {
        public PackIconKind IconType { get; set; }
        public string Name { get; set; }

        public List<NodeItem> Children { get; set; }
        public NodeItem()
        {
            Children = new List<NodeItem>();
        }

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
        static public NodeItem TabControl(string name) { return new NodeItem() { IconType = PackIconKind.FolderTableOutline, Name = "[" + name + "]" }; }
        static public NodeItem TreeView(string name) { return new NodeItem() { IconType = PackIconKind.FileTree, Name = "[" + name + "]" }; }
        static public NodeItem StatusBar(string name) { return new NodeItem() { IconType = PackIconKind.ExpansionCardVariant, Name = "[" + name + "]" }; }
        static public NodeItem Menu(string name) { return new NodeItem() { IconType = PackIconKind.ContentCopy, Name = "[" + name + "]" }; }

        static public NodeItem Grid(string name) { return new NodeItem() { IconType = PackIconKind.Grid, Name = "[" + name + "]" }; }
    }
}
