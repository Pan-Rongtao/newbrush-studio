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
using System.Windows.Media.Media3D;

namespace studio
{
    /// <summary>
    /// Vec3Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Vec3Edit : UserControl
    {
        public Vec3Edit()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Vector3D),
            typeof(Vec3Edit), new PropertyMetadata(new Vector3D(0, 0, 0), new PropertyChangedCallback(onPropertyChanged)));

        public Vector3D Value
        {
            get { return (Vector3D)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        static void onPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (Vec3Edit)d;
            ctrl.Value = new Vector3D(ctrl.X, ctrl.Y, ctrl.Z);
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(float),
            typeof(Vec3Edit), new PropertyMetadata((float)0.0, new PropertyChangedCallback(onPropertyChanged)));

        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(float),
            typeof(Vec3Edit), new PropertyMetadata((float)0.0, new PropertyChangedCallback(onPropertyChanged)));

        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty ZProperty = DependencyProperty.Register("Z", typeof(float),
            typeof(Vec3Edit), new PropertyMetadata((float)0.0, new PropertyChangedCallback(onPropertyChanged)));

        public float Z
        {
            get { return (float)GetValue(ZProperty); }
            set { SetValue(ZProperty, value); }
        }
    }
}
