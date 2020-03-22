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

namespace studio
{
    public partial class MainWindow : MetroWindow
    {
        NBApplication nbApp = new NBApplication();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nbApp.work();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void uniformList_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 10; ++i)
            {
                this.uniformList.Items.Add(new UniformItem("uniform" + i.ToString(), ""));
            }
        }

        private void vshader_code_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
    }
}