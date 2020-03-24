using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBPlayer;
using System.ComponentModel;

namespace studio
{
    public class UniformItem1 : INotifyPropertyChanged
    {
        BuildShaderReply.Types.ShaderVarType _type;
        private string _uniform;
        private string _value;

        public UniformItem1(BuildShaderReply.Types.ShaderVarType type, string uniform, string value)
        {
            this._type = type;
            this._uniform = uniform;
            this._value = value;
        }

        public BuildShaderReply.Types.ShaderVarType Type
        {
            get { return _type; }
            set { this._type = value; OnPropertyChanged("Type"); }
        }

        public string Uniform
        {
            get { return _uniform; }
            set { this._uniform = value; OnPropertyChanged("Uniform"); }
        }

        public string Value
        {
            get { return _value; }
            set { this._value = value; OnPropertyChanged("Value"); }
        }

        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
