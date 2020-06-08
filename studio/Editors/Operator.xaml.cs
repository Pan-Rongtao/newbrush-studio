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
    /// Operator.xaml 的交互逻辑
    /// </summary>
    public partial class Operator : UserControl
    {
        public Operator()
        {
            InitializeComponent();

            btn.ContextMenu = null;
        }

        public static readonly RoutedEvent ResetValueEvent = EventManager.RegisterRoutedEvent("ResetValueEvent", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(Operator));

        public event RoutedEventHandler ResetValue
        {
            add { AddHandler(ResetValueEvent, value); }
            remove { RemoveHandler(ResetValueEvent, value); }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            CM.PlacementTarget = btn;
            CM.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            CM.IsOpen = true;
        }

        private void binding_Click(object sender, RoutedEventArgs e)
        {

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ResetValueEvent, this);
            this.RaiseEvent(args);
        }

    }
}
