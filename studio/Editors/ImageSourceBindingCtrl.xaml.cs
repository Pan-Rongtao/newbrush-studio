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
using Xceed.Wpf.Toolkit;

namespace studio
{
    /// <summary>
    /// ImageSourceBindingCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageSourceBindingCtrl : UserControl
    {
        public ImageSourceBindingCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
            typeof(ImageSource), typeof(ImageSourceBindingCtrl), new PropertyMetadata(null, null));

        private void AutoSelectTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Uri uri = new Uri((sender as AutoSelectTextBox).Text);
                BitmapImage bm = new BitmapImage(uri);
                SetValue(ValueProperty, bm);
            }
            catch (Exception) { }
        }
    }
}
