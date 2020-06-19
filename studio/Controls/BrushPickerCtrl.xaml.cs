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
    /// Brush.xaml 的交互逻辑
    /// </summary>
    public partial class BrushPickerCtrl : UserControl
    {
        private bool _PopIsOpenBeforeGridPressed = false;
        public BrushPickerCtrl()
        {
            InitializeComponent();
            
        }

        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register("Brush", typeof(Brush),
            typeof(BrushPickerCtrl), new PropertyMetadata(null, onValueChanged));

        static void onValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        //        Console.WriteLine("BrushPicker::onBrushChanged:{0}", e.NewValue as Brush);
        }

        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        private void grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_PopIsOpenBeforeGridPressed)
            {
                pop.IsOpen = true;
            }
        }
        
        private void grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _PopIsOpenBeforeGridPressed = pop.IsOpen;
        }

        private void Operator_ResetValue(object sender, RoutedEventArgs e)
        {
            brushPop.Reset();
        }

        private void brushPop_BrushChanged(object sender, RoutedEventArgs e)
        {
            Brush brush = (sender as BrushCtrl).Brush;
            Brush = brush;
        }
    }
}
