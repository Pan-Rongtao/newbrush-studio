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
using System.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Newtonsoft.Json.Linq;

using Grpc.Core;
using NBPlayer;

namespace studio
{
    public partial class MainWindow : MetroWindow
    {
        NBApplication nbApp = new NBApplication();

        Channel channel = new Channel("127.0.0.1:8888", ChannelCredentials.Insecure);

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nbApp.work();

        }
        
        private void uniformList_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btn_buildShader_Click(object sender, RoutedEventArgs e)
        {
            var client = new Shader.ShaderClient(channel);
            try
            {
                var reply = client.BuildShader(new BuildShaderRequest { VShaderCode = vshader_code.Text, FShaderCode = fshader_code.Text });
                this.uniformList.Items.Clear();
                foreach (var item in reply.VarInfos)
                {
                    this.uniformList.Items.Add(new UniformItem1(item.Value, item.Key, ""));
                }
            }
            catch(Grpc.Core.RpcException){ }
            
        }
    }
}