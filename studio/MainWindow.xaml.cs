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
using System.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using MaterialDesignThemes.Wpf;
using System.Reflection;

using System.Collections.ObjectModel;
using Nbrpc;

namespace studio
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            Plugin plugin = new Plugin("D:/github/newbrush/dist/win32/lib/NbGuid.dll");
            plugin.Update();
            InitializeComponent();

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

        private void Solution2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
          //  Console.WriteLine(e.PropertyName);
         //   if(pv != null)
         //       pv.moveTo();
        }
    }
}