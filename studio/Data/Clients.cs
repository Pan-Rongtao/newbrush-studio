using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Nbrpc;

namespace studio
{
    class RpcClients
    {
        static private Channel _channel = new Channel("127.0.0.1:8888", ChannelCredentials.Insecure);
        static ShaderStub.ShaderStubClient _shaderStubClient = new ShaderStub.ShaderStubClient(_channel);

        static public ShaderStub.ShaderStubClient ShaderStubClient
        {
            get { return _shaderStubClient; }
            set { _shaderStubClient = value; }
        }

    }
}
