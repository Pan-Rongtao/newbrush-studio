using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studio
{
    enum ValueType
    {

    };

    class UniformDecoder
    {
        const string KeywordMain = "main";
        const string KeywordUniform = "uniform";
        const string KeywordStruct = "struct";
        const int    KeywordUniformSize	= 7;
        const int    KeywordStructSize	= 6;

        private Dictionary<string, ValueType> _uniforms;
        private Dictionary<string, Dictionary<string, ValueType>> _structDefines;

        public void decode(string verSource, string fragSource)
        {
            _uniforms.Clear();
            decodeOne(verSource);
            decodeOne(fragSource);
        }

        public Dictionary<string, ValueType> getUniforms()
        {
            return _uniforms;
        }

        private void decodeOne(string source)
        {
            string sCutMain = cutMain(source);
            if (string.IsNullOrEmpty(source))
            {
                return;
            }

            for(int i = 0; i <= sCutMain.Length - KeywordUniformSize;)
            {
                string sSubStruct = sCutMain.Substring(i, KeywordStructSize);
                string sSubUniform = sCutMain.Substring(i, KeywordUniformSize);
                if(sSubStruct.Equals(KeywordStruct, StringComparison.OrdinalIgnoreCase))
                {
                    int offset = i + KeywordStructSize + 1;
                    int end = sCutMain.IndexOf('}', offset);
                    if(end != -1)
                    {
                        string sStructDefineStr = sCutMain.Substring(i + KeywordStructSize, end - (i + KeywordStructSize) + 1);
                        string structName;
                        
                    }
                }
            }
        }

        private Tuple<string, Dictionary<string, ValueType>> extractStruct(string structDefineStr)
        {
            return null;

        }

        private string cutMain(string s)
        {
            int mainP = s.IndexOf("main");
            return mainP != -1 ? s.Substring(0, mainP - 5) : "";
        }
    }
}
