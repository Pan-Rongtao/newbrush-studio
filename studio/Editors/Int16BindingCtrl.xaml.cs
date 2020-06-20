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
    /// Int16BindingCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class Int16BindingCtrl : UserControl
    {
        public Int16BindingCtrl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(Int16), typeof(Int16BindingCtrl), new PropertyMetadata((Int16)0, null));
    }
}
