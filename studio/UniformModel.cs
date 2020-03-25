using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nbrpc;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Collections.ObjectModel;

namespace studio
{
    public class UniformModel
    {
        static public ObservableCollection<UniformItem> Model
        {
            get { return _data; }
            set { _data = value; }
        }

        static private ObservableCollection<UniformItem> _data = new ObservableCollection<UniformItem>();
    }

    public class UniformItem : INotifyPropertyChanged
    {

        public UniformItem(UniformType type, string uniform)
        {
            this._type = type;
            this._uniform = uniform;
        }

        public UniformType Type
        {
            get { return _type; }
            set { this._type = value; OnPropertyChanged("Type"); }
        }

        public string Uniform
        {
            get { return _uniform; }
            set { this._uniform = value; OnPropertyChanged("Uniform"); }
        }
        
        public bool BooleanValue
        {
            get { return _boolValue; }
            set { this._boolValue = value; OnPropertyChanged("BooleanValue"); }
        }
        public float FloatValue
        {
            get { return _floatValue; }
            set { this._floatValue = value; OnPropertyChanged("FloatValue"); }
        }

        public int IntValue
        {
            get { return _intValue; }
            set { this._intValue = value; OnPropertyChanged("IntValue"); }
        }

        public Vector Vec2Value
        {
            get { return _vec2Value; }
            set { this._vec2Value = value; OnPropertyChanged("Vec2Value"); }
        }

        public Vector3D Vec3Value
        {
            get { return _vec3Value; }
            set { this._vec3Value = value; OnPropertyChanged("Vec3Value"); }
        }

        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                Console.WriteLine("{0} changed", propertyName);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        UniformType _type;
        private string _uniform;
        private bool _boolValue;
        private float _floatValue;
        private int _intValue;
        private Vector _vec2Value;
        private Vector3D _vec3Value;

    }
}
