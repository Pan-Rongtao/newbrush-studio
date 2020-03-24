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

using NBPlayer;
namespace studio.controls
{
    /// <summary>
    /// VarEdit.xaml 的交互逻辑
    /// </summary>
    public partial class VarEdit : UserControl
    {
        public VarEdit()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DataTypeProperty = DependencyProperty.Register("DataType", typeof(NBPlayer.BuildShaderReply.Types.ShaderVarType),
            typeof(VarEdit), new PropertyMetadata(BuildShaderReply.Types.ShaderVarType.Unknown, new PropertyChangedCallback(onDataTypeChanged)));

        public NBPlayer.BuildShaderReply.Types.ShaderVarType DataType
        {
            get { return (NBPlayer.BuildShaderReply.Types.ShaderVarType)GetValue(DataTypeProperty); }
            set { SetValue(DataTypeProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Object),
            typeof(VarEdit), new PropertyMetadata("", new PropertyChangedCallback(onValueChanged)));

        public Object Value
        {
            get { return (Object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        static void onDataTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as VarEdit;
            var t = (NBPlayer.BuildShaderReply.Types.ShaderVarType)e.NewValue;
            switch (t)
            {
                case BuildShaderReply.Types.ShaderVarType.Boolean:
                    {
                        var edit = new BooleanEdit();
                        ctrl.container.Content = edit;
                        edit.ValueChanged += Edit_ValueChanged;
                        break;
                    }
                case BuildShaderReply.Types.ShaderVarType.Real: ctrl.container.Content = new FloatEdit();       break;
                case BuildShaderReply.Types.ShaderVarType.Integer: ctrl.container.Content = new IntegerEdit();  break;
                case BuildShaderReply.Types.ShaderVarType.Vec2: ctrl.container.Content = new Vec2Edit();        break;
                case BuildShaderReply.Types.ShaderVarType.Vec3: ctrl.container.Content = new Vec3Edit();        break;
                case BuildShaderReply.Types.ShaderVarType.Vec4: ctrl.container.Content = new Vec4Edit();        break;
            }
        }

        static void onValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChangedEvent", RoutingStrategy.Bubble, typeof(EventHandler<ValueChangedEventArgs>), typeof(VarEdit));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        private static void Edit_ValueChanged(object sender, RoutedEventArgs e)
        {
            ValueChangedEventArgs args = e as ValueChangedEventArgs;
            args.RoutedEvent = ValueChangedEvent;
            var ctrl = (FrameworkElement)(sender);
            ctrl.RaiseEvent(args);
        }

    }
}
