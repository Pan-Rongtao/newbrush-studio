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
    /// Vec2Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Vec2Edit : UserControl
    {
        public Vec2Edit()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Vector),
            typeof(Vec2Edit), new PropertyMetadata(new Vector(0, 0), new PropertyChangedCallback(onPropertyChanged)));

        public Vector Value
        {
            get { return (Vector)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        static void onPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (Vec2Edit)d;
            ctrl.Value = new Vector(ctrl.X, ctrl.Y);
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(float),
            typeof(Vec2Edit), new PropertyMetadata((float)0.0, new PropertyChangedCallback(onPropertyChanged)));

        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(float),
            typeof(Vec2Edit), new PropertyMetadata((float)0.0, new PropertyChangedCallback(onPropertyChanged)));

        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
    }
}
