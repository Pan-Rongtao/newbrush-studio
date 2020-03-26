﻿using System;
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

using System.Collections.ObjectModel;
using Nbrpc;

namespace studio
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            UniformModel.Model.Add(new UniformItem(UniformType.Boolean, "uniform"));
            UniformModel.Model.Add(new UniformItem(UniformType.Real, "uniform"));
            UniformModel.Model.Add(new UniformItem(UniformType.Integer, "uniform"));
            UniformModel.Model.Add(new UniformItem(UniformType.Vec2, "uniform"));
            UniformModel.Model.Add(new UniformItem(UniformType.Vec3, "uniform"));*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NBPlayer.launch();
        }
        
        private void btn_buildShader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reply = RpcClients.ShaderStubClient.BuildShader(new BuildShaderRequest { VShaderCode = vshader_code.Text, FShaderCode = fshader_code.Text });
                UniformModel.Model.Clear();
                foreach (var item in reply.UniformInfos)
                {
                    UniformModel.Model.Add(new UniformItem(item.Value, item.Key));
                }
            }
            catch(Grpc.Core.RpcException){ }
            
        }
        
    }
}