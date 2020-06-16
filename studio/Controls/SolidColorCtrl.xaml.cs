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
    /// SolidColorCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class SolidColorCtrl : UserControl
    {
        public SolidColorCtrl()
        {
            InitializeComponent();
            
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Brush),
            typeof(SolidColorCtrl), new PropertyMetadata(null, onValueChanged));

        static void onValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
         //   Console.WriteLine("SolidColorCtrl::onValueChanged:{0}", e.NewValue as SolidColorBrush);
        }

        public Brush Value
        {
            get { return (Brush)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public void ClearValue()
        {
            Value.ClearValue(ValueProperty);
        }

        private void ColorCanvas_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Value = e.NewValue.HasValue ? new SolidColorBrush(e.NewValue.Value) : null;
        }
    }
}
