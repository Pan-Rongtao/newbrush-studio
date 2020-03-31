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
using MaterialDesignThemes.Wpf;
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
        }

        private void Solution2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
          //  Console.WriteLine(e.PropertyName);
         //   if(pv != null)
         //       pv.moveTo();
        }
    }
}