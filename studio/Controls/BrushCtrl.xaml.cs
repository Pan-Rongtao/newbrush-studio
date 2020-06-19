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
    /// BrushPop.xaml 的交互逻辑
    /// </summary>
    public partial class BrushCtrl : UserControl
    {
        public BrushCtrl()
        {
            InitializeComponent();
            
        }
        
        public static readonly DependencyProperty BrushProperty = DependencyProperty.Register("Brush", typeof(Brush),
            typeof(BrushCtrl), new PropertyMetadata(null, onValueChanged));

        static void onValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //    Console.WriteLine("BrushPop::onBrushChanged:{0}", e.NewValue as Brush);
            BrushCtrl ctrl = d as BrushCtrl;
            RoutedEventArgs args = new RoutedEventArgs(BrushChangedEvent, ctrl);
            ctrl.RaiseEvent(args);
        }

        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        public void Reset()
        {
          //  ClearValue(BrushProperty);
            Brush = null;
        }

        private void UpdateBinding()
        {
            Binding bd = new Binding("Value");
            bd.Mode = BindingMode.TwoWay;
            switch (tab.SelectedIndex)
            {
                case 0: bd.Source = solidColorCtrl; break;
                case 1: bd.Source = gradientBrushCtrl; break;
                case 2: bd.Source = imageBrushCtrl; break;
                default: throw new Exception("TabControl_SelectionChanged");
            }
            this.SetBinding(BrushProperty, bd);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBinding();
        }
        
        public static readonly RoutedEvent BrushChangedEvent = EventManager.RegisterRoutedEvent("BrushChangedEvent", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(Operator));

        public event RoutedEventHandler BrushChanged
        {
            add { AddHandler(BrushChangedEvent, value); }
            remove { RemoveHandler(BrushChangedEvent, value); }
        }
    }
}
