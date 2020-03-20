using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studio
{
    public class UniformItem
    {
        private string _uniform;
        private string _value;

        public UniformItem(string uniform, string value)
        {
            this._uniform = uniform;
            this._value = value;
        }

        public string Uniform
        {
            get { return _uniform; }
            set { this._uniform = value; }
        }

        public string Value
        {
            get { return _value; }
            set { this._value = value; }
        }
    }
}
