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

namespace studio.controls
{
    class ValueChangedEventArgs : RoutedEventArgs
    {
        public Object NewValue { get; set; }
    }
    /// <summary>
    /// BooleanBar.xaml 的交互逻辑
    /// </summary>
    public partial class BooleanEdit : UserControl
    {
        public BooleanEdit()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChangedEvent", RoutingStrategy.Bubble, typeof(EventHandler<ValueChangedEventArgs>), typeof(BooleanEdit));

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ValueChangedEventArgs args = new ValueChangedEventArgs();
            args.NewValue = tb.IsChecked;
            args.RoutedEvent = ValueChangedEvent;
            this.RaiseEvent(args);
        }

    }
}
