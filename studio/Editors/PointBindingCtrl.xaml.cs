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
    /// PointBindingCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class PointBindingCtrl : UserControl
    {
        public PointBindingCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(Point), typeof(PointBindingCtrl), new PropertyMetadata(new Point(), null));

        private void SingleUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Point p = new Point();
            p.X = (ptx != null && ptx.Value.HasValue) ? ptx.Value.Value : 0.0;
            p.Y = (pty != null && pty.Value.HasValue) ? pty.Value.Value : 0.0;
            SetValue(ValueProperty, p);
        }
    }
}
