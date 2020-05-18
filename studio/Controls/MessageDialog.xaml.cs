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
using System.Globalization;

namespace studio
{
    /// <summary>
    /// MessageDialog.xaml 的交互逻辑
    /// </summary>
    ///       
    public class LengthToBoolenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var length = (int?)value;
            return length > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class MessageDialog : UserControl
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MessageTagProperty = DependencyProperty.Register("MessageTag", typeof(object), typeof(MessageDialog), new PropertyMetadata(null));

        public object MessageTag
        {
            get { return GetValue(MessageTagProperty); }
            set { SetValue(MessageTagProperty, value); }
        }
    }
}
