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

namespace studio
{
    /// <summary>
    /// ResourceFilesTree.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceFilesTree : UserControl
    {
        public static string SelectItemPath;
        
        public ResourceFilesTree()
        {
            InitializeComponent();
            tree.ItemsSource = ViewModel.Library.ResouceFilesRoot.Children;
        }
        
        private MenuItem MakeMenuItem(PackIconKind iconk, string hearder, RoutedUICommand cmd, object cmdParam)
        {
            PackIcon icon = new PackIcon() { Kind = iconk, Margin = new System.Windows.Thickness(5, 0, 0, 0) };
            MenuItem menuItem = new MenuItem();
            menuItem.Header = hearder;
            menuItem.Icon = icon;
            menuItem.Command = cmd;
            menuItem.CommandParameter = cmdParam;
            return menuItem;
        }

        private ContextMenu CreateContextMenu(ResourceNode node)
        {
            ContextMenu cm = new ContextMenu();
            if(node.Is(ResourceType.Folder))
            {
                if (node.Is(ResourceType.Image))             cm.Items.Add(MakeMenuItem(PackIconKind.ImageAdd, "导入图片", Commands.ImportFile, node));
                else if (node.Is(ResourceType.Font))         cm.Items.Add(MakeMenuItem(PackIconKind.FormatFont, "导入字体", Commands.ImportFile, node));
                else if (node.Is(ResourceType.ShaderSource)) cm.Items.Add(MakeMenuItem(PackIconKind.NoteAddOutline, "导入Shader", Commands.ImportFile, node));
                else if (node.Is(ResourceType.ThreeDAssets)) cm.Items.Add(MakeMenuItem(PackIconKind.Codepen, "导入3D模型", Commands.ImportFile, node));

                cm.Items.Add(MakeMenuItem(PackIconKind.CreateNewFolderOutline, "新建文件夹", Commands.InputFolderName, node));
            }
            else
            {
                cm.Items.Add(MakeMenuItem(PackIconKind.TrashCanOutline, "删除", Commands.RemoveResourceNode, node));
                cm.Items.Add(MakeMenuItem(PackIconKind.RenameBox, "重命名", Commands.RenameResourceNodeDlg, node));
            }
            return cm;
        }
        
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
            ResourceNode n = ViewModel.Library.ResouceFilesRoot.Find(SelectItemPath);
            ContextMenu cm = CreateContextMenu(n);
            tree.ContextMenu = cm;

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
                    var dc = ((TreeViewItem)source).DataContext as ResourceNode;
                    path = dc.Name + "/" + path;
                }
                source = VisualTreeHelper.GetParent(source);
            }
            if (!String.IsNullOrEmpty(path))
            {
                path = path.Substring(0, path.Length - 1);
            }

            return path;
        }

        private void treeView1_Selected(object sender, RoutedEventArgs e)
        {
            SelectItemPath = GetFullPath(e.OriginalSource as DependencyObject);
        }

        private void tv_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            tree.ContextMenu = null; //(ContextMenu)this.FindResource("ImageResourceFolderContextMenu");
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

    }
}
