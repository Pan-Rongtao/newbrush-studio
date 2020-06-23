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
    /// EnumBindingCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class EnumBindingCtrl : UserControl
    {
        public EnumBindingCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("Value", 
            typeof(string[]), typeof(EnumBindingCtrl), new PropertyMetadata(null, null));

        public string[] ItemsSource
        {
            get { return (string[])GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectItemProperty = DependencyProperty.Register("SelectItem",
            typeof(object), typeof(EnumBindingCtrl), new PropertyMetadata(null, null));

        public object SelectItem
        {
            get { return GetValue(SelectItemProperty); }
            set { SetValue(SelectItemProperty, value); }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetValue(SelectItemProperty, (sender as ComboBox).SelectedItem);
        }
    }
}
