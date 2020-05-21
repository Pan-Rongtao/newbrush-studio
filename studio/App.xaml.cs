using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace studio
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Plugin plugin = new Plugin() { DllPath = "D:/github/newbrush/dist/win32/lib/NbGuid.dll" };
            plugin.Update();
            ViewModel.Plugins.Add(plugin);
            ViewModel.VisualTreeModel.Children.Add(new NodeData("nb::Window", "Window"));
        }
    }
}
