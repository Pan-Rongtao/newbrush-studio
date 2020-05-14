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
    /// ResourceFilesTree.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceFilesTree : UserControl
    {
        public static ResourceNode Root = new ResourceNode();

        public ResourceFilesTree()
        {
            InitializeComponent();

            Root.Children.Add(new ResourceNode("3D Assets", ResourceTypeFlags.Folder | ResourceTypeFlags.ThreeDAssets));
            Root.Children.Add(new ResourceNode("Images", ResourceTypeFlags.Folder | ResourceTypeFlags.Image));
            Root.Children.Add(new ResourceNode("Fonts", ResourceTypeFlags.Folder | ResourceTypeFlags.Font));
            Root.Children.Add(new ResourceNode("Shaders", ResourceTypeFlags.Folder | ResourceTypeFlags.ShaderSource));
           // tv.ItemsSource = Root.Children;
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

        private void treeView1_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void tv_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
