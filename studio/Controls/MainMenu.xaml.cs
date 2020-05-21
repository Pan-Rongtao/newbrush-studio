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

namespace studio
{
    /// <summary>
    /// Menu.xaml 的交互逻辑
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "选择模型文件";
            openFileDialog.Filter = "FBX文件|*.fbx";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "fbx";
            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }
            string filePath = openFileDialog.FileName;
            int n = filePath.LastIndexOf('\\');
            string fileDir = filePath.Substring(0, n);
            LoadModelRequest request = new LoadModelRequest() { ModelPath = filePath, TexturePath = fileDir };
            try
            {
                Rpc.ShaderClient.LoadModel(request);
            }
            catch (Grpc.Core.RpcException) { }
        }
    }
}
