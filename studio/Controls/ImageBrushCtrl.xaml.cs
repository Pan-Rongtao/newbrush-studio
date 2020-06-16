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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace studio
{
    /// <summary>
    /// ImageBrushCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageBrushCtrl : UserControl
    {
        public ImageBrushCtrl()
        {
            InitializeComponent();
            
            Data d = new Data();

            this.pg.SelectedObject = d;
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Brush),
            typeof(ImageBrushCtrl), new PropertyMetadata(null, onValueChanged));

        static void onValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //    Console.WriteLine("onValueChanged:{0}", e.NewValue as SolidColorBrush);
        }

        public Brush Value
        {
            get { return (Brush)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        class Data
        {
            public string ImageSource { get; set; }

            public Stretch Stretch { get; set; }

            public TileMode TileMode { get; set; }
            
            public double Opacity { get; set; }
        }

        private void pg_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            PropertyItem item = e.OriginalSource as PropertyItem;
            switch(item.DisplayName)
            {
                case "ImageSource":
                    {
                        try
                        {
                            Uri u = new Uri(e.NewValue as string);
                            BitmapImage bm = new BitmapImage(u);
                            Value = new ImageBrush(bm);
                        }
                        catch(Exception) { }

                        break;
                    }
                case "Stretch": (Value as ImageBrush) .Stretch = (Stretch)(e.NewValue); break;
                case "TileMode": (Value as ImageBrush).TileMode = (TileMode)(e.NewValue); break;
                case "Opacity": (Value as ImageBrush).Opacity = (double)(e.NewValue); break;
            }
        }
    }
}
