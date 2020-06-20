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
    /// Int64BindingCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class Int64BindingCtrl : UserControl
    {
        public Int64BindingCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(Int64), typeof(Int64BindingCtrl), new PropertyMetadata((Int64)0, null));
    }
}
