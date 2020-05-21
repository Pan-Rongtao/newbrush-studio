using System;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

namespace studio
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        
        private async void CommandBinding_Executed_InputImageFolderName(object sender, ExecutedRoutedEventArgs e)
        {
            MessageDialog dlg = new MessageDialog();
            dlg.MessageTag = e.Parameter;
            var result = await DialogHost.Show(dlg);
        }

        private void CommandBinding_Executed_NewFolder(object sender, ExecutedRoutedEventArgs e)
        {
            var selectPath = ResourceFilesTree.SelectItemPath;
            var parentNode = ViewModel.Library.ResouceFilesRoot.Find(selectPath);
            string newFolderName = (string)(e.Parameter);
            ResourceNode sourceNode = (e.OriginalSource as Button).Tag as ResourceNode;
            if (parentNode == null)
            {
                parentNode = ViewModel.Library.ResourceFilesSubRoot(sourceNode.ResourceType);
            }
            if(parentNode.GetChild(newFolderName) == null)
            {
                ResourceNode node = new ResourceNode(newFolderName, sourceNode.ResourceType);
                parentNode.Children.Add(node);
            }
            else
            {
                ViewModel.LogData.Add(LogLevel.Warn, "[{0}]下已存在同名目录[{1}]", selectPath.Replace(".", "/"), newFolderName);
            }
            dh.IsOpen = false;
        }

        private void CommandBinding_Executed_ImportFile(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //设置这个对话框的起始打开路径
            ofd.InitialDirectory = @"D:\";
            //设置打开的文件的类型，注意过滤器的语法
            ResourceNode sourceNode = e.Parameter as ResourceNode;
            switch (sourceNode.ResourceType)
            {
                case ResourceType.ImageFolder: ofd.Filter = "图片文件(*.jpg,*.gif,*.bmp,*.png)|*.jpg;*.gif;*.bmp;*.png"; break;
                case ResourceType.FontFolder: ofd.Filter = "字体文件(*.ttf,*.ttc)|*.ttf;*.ttc"; break;
                case ResourceType.ShaderSourceFolder: ofd.Filter = "着色器文件(*.vs,*.fs)|*.vs;*.fs"; break;
                case ResourceType.ThreeDAssetsFolder: ofd.Filter = "模型文件(*.fbx;*.obj;*.3DS;*.STL;*.xml)|*.fbx;*.obj;*.3DS;*.STL;*.xml"; break;
                default: break;
            }
            //调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮
            if (ofd.ShowDialog() == true)
            {
                string name = Path.GetFileName(ofd.FileName);
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                var selectPath = ResourceFilesTree.SelectItemPath;
                var parentNode = ViewModel.Library.ResouceFilesRoot.Find(selectPath);
                if (parentNode == null)
                {
                    parentNode = ViewModel.Library.ResourceFilesSubRoot(ResourceType.Image);
                }

                Byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                ResourceNode child = parentNode.GetChild(name);
                if (child == null)
                {
                    ResourceType t = sourceNode.ResourceType & (~ResourceType.Folder);  //移除Folder
                    ResourceNode node = new ResourceNode(name, t);
                    node.Data = bytes;
                    parentNode.Children.Add(node);
                }
                else
                {
                    child.Data = bytes;
                }

            }
        }

        private void CommandBinding_Executed_RemoveResourceNode(object sender, ExecutedRoutedEventArgs e)
        {
            string selectPath = ResourceFilesTree.SelectItemPath;
            if (ResourceFilesTree.SelectItemPath == null)
                return;

            string parentPath = Path.GetDirectoryName(selectPath).Replace('\\', '/');
            ResourceNode parentNode = ViewModel.Library.ResouceFilesRoot.Find(parentPath);
            ResourceNode childNode = e.Parameter as ResourceNode;
            parentNode.Children.Remove(childNode);
        }

        private void CommandBinding_Executed_RenameResourceNode(object sender, ExecutedRoutedEventArgs e)
        {
            string selectPath = ResourceFilesTree.SelectItemPath;
            if (ResourceFilesTree.SelectItemPath == null)
                return;
            
            ResourceNode targetNode = ViewModel.Library.ResouceFilesRoot.Find(selectPath);
            string ext = Path.GetExtension(selectPath);
            string newName = (string)(e.Parameter);
            if (!targetNode.Is(ResourceType.Folder))
            {
                targetNode.Name = newName + ext;
            }
            ResourceFilesTree.SelectItemPath = Path.GetDirectoryName(ResourceFilesTree.SelectItemPath) + targetNode.Name;
            dh.IsOpen = false;
        }

        private async void CommandBinding_Executed_RenameResourceNodeDlg(object sender, ExecutedRoutedEventArgs e)
        {
            RenameDialog dlg = new RenameDialog();
            var result = await DialogHost.Show(dlg);
        }
    }
}