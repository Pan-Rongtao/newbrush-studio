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
using Nbrpc;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;

namespace studio
{
    /// <summary>
    /// UniformDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class UniformDataGrid : UserControl
    {
        public UniformDataGrid()
        {
            InitializeComponent();
            uniformGrid.ItemsSource = UniformModel.Model;
        }
    }


    public class UniformModel
    {
        static public ObservableCollection<UniformItem> Model
        {
            get { return _data; }
            set { _data = value; }
        }

        static private ObservableCollection<UniformItem> _data = new ObservableCollection<UniformItem>();
    }

    public class UniformItem
    {

        public UniformItem(UniformType type, string uniform)
        {
            this._type = type;
            this._uniform = uniform;
        }

        public UniformType Type
        {
            get { return _type; }
            set { this._type = value; }
        }

        public string Uniform
        {
            get { return _uniform; }
            set { this._uniform = value; }
        }

        public bool BooleanValue
        {
            get { return _boolValue; }
            set
            {
                this._boolValue = value;
                RpcClients.ShaderStubClient.UniformBool(new UniformBoolRequest { Name = Uniform, Value = _boolValue });
            }
        }
        public float FloatValue
        {
            get { return _floatValue; }
            set
            {
                this._floatValue = value;
                RpcClients.ShaderStubClient.UniformFloat(new UniformFloatRequest { Name = Uniform, Value = _floatValue });
            }
        }

        public int IntValue
        {
            get { return _intValue; }
            set
            {
                this._intValue = value;
                RpcClients.ShaderStubClient.UniformInteger(new UniformIntegerRequest { Name = Uniform, Value = _intValue });
            }
        }

        public Vector Vec2Value
        {
            get { return _vec2Value; }
            set
            {
                this._vec2Value = value;
                Nbrpc.Vec2 vec2 = new Vec2() { X = (float)_vec2Value.X, Y = (float)_vec2Value.Y };
                RpcClients.ShaderStubClient.UniformVec2(new UniformVec2Request { Name = Uniform, Value = vec2 });
            }
        }

        public Vector3D Vec3Value
        {
            get { return _vec3Value; }
            set
            {
                this._vec3Value = value;
                Nbrpc.Vec3 vec3 = new Vec3() { X = (float)_vec3Value.X, Y = (float)_vec3Value.Y, Z = (float)_vec3Value.Z };
                RpcClients.ShaderStubClient.UniformVec3(new UniformVec3Request { Name = Uniform, Value = vec3 });
            }
        }

        UniformType _type;
        private string _uniform;
        private bool _boolValue;
        private float _floatValue;
        private int _intValue;
        private Vector _vec2Value;
        private Vector3D _vec3Value;

    }
}
