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
    /// ThicknessBindingCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class ThicknessBindingCtrl : UserControl
    {
        public ThicknessBindingCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(Thickness), typeof(ThicknessBindingCtrl), new PropertyMetadata(new Thickness(), null));

        private void DoubleUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Thickness tn = new Thickness();
            tn.Left = (left != null && left.Value.HasValue) ? left.Value.Value : 0.0;
            tn.Right = (right != null && right.Value.HasValue) ? right.Value.Value : 0.0;
            tn.Top = (top != null && top.Value.HasValue) ? top.Value.Value : 0.0;
            tn.Bottom = (bottom != null && bottom.Value.HasValue) ? bottom.Value.Value : 0.0;
            SetValue(ValueProperty, tn);
        }
        
    }
}
