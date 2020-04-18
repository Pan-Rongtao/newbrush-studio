using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Nbrpc;

namespace studio
{
    class RpcManager
    {
        static public ShaderRpc.ShaderRpcClient ShaderClient
        {
            get { return _shaderClient; }
            set { _shaderClient = value; }
        }

        static public NodeRpc.NodeRpcClient NodeClient
        {
            get { return _nodeClient; }
            set { _nodeClient = value; }
        }

        static private Channel _channel = new Channel("127.0.0.1:8888", ChannelCredentials.Insecure);
        static private ShaderRpc.ShaderRpcClient _shaderClient = new ShaderRpc.ShaderRpcClient(_channel);
        static private NodeRpc.NodeRpcClient _nodeClient = new NodeRpc.NodeRpcClient(_channel);

    }
}
