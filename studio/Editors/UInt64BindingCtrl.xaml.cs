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
    /// UInt64BindingCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class UInt64BindingCtrl : UserControl
    {
        public UInt64BindingCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(UInt64), typeof(UInt64BindingCtrl), new PropertyMetadata((UInt64)0, null));
    }
}
