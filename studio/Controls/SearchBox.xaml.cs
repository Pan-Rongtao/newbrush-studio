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
    /// SearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                TextBox txt = sender as TextBox;
                txt.SelectAll();
                txt.PreviewMouseDown -= new MouseButtonEventHandler(txt_PreviewMouseDown);

            }
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                txt.PreviewMouseDown += new MouseButtonEventHandler(txt_PreviewMouseDown);
            }
        }

        private void txt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.Focus();
                e.Handled = true;
            }
        }
    }
}
