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
using System.Windows.Controls.Primitives;
using Grpc.Core;
using NBPlayer;

namespace studio
{
    /// <summary>
    /// UniformItem.xaml 的交互逻辑
    /// </summary>
    public partial class UniformItem : UserControl
    {
        public UniformItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DataTypeProperty = DependencyProperty.Register("DataType", typeof(NBPlayer.BuildShaderReply.Types.ShaderVarType), 
            typeof(UniformItem), new PropertyMetadata(BuildShaderReply.Types.ShaderVarType.Unknown, new PropertyChangedCallback(onDataTypeChanged)));

        public static readonly DependencyProperty UniformNameProperty = DependencyProperty.Register("UniformName", typeof(string),
            typeof(UniformItem), new PropertyMetadata("", new PropertyChangedCallback(onUniformNameChanged)));

        static void onDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as UniformItem;
            var t = (NBPlayer.BuildShaderReply.Types.ShaderVarType)e.NewValue;
            switch (t)
            {
                case BuildShaderReply.Types.ShaderVarType.Boolean:  ctrl.container.Content = new ToggleButton(); break;
            //    case BuildShaderReply.Types.ShaderVarType.Bvec2:    ctrl.container.Content = 
            }
        }

        static void onUniformNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
