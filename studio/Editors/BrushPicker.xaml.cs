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
    public partial class BrushPicker : UserControl
    {
        private bool _PopIsOpenBeforeGridPressed = false;
        public BrushPicker()
        {
            InitializeComponent();

            //cp.SelectedColorChanged += Cp_SelectedColorChanged;
        }

        private void Cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color? c = e.NewValue;
            if(c != null)
            {
                Value = new SolidColorBrush(c.Value);
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Brush),
            typeof(BrushPicker), new PropertyMetadata(null, null));

        public Brush Value
        {
            get { return (Brush)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        private void clearBrush_Click(object sender, RoutedEventArgs e)
        {
            Value = null;
        }

        private void solidBrush_Click(object sender, RoutedEventArgs e)
        {
            //cp.IsOpen = true;
        }

        private void gradentBrush_Click(object sender, RoutedEventArgs e)
        {

        }

        private void imageBrush_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Operator_ResetValue(object sender, RoutedEventArgs e)
        {

        }
        
        private void grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_PopIsOpenBeforeGridPressed)
                pop.IsOpen = true;
        }
        
        private void grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _PopIsOpenBeforeGridPressed = pop.IsOpen;
        }
    }
}
