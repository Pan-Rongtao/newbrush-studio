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
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Collections.ObjectModel;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace studio
{
    /// <summary>
    /// StringListComboboxEditor.xaml 的交互逻辑
    /// </summary>
    public partial class StringListComboboxEditor : UserControl, ITypeEditor
    {
        public StringListComboboxEditor()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(ObservableCollection<string>), typeof(StringListComboboxEditor), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            var binding = new Binding("Value");
            binding.Source = propertyItem;
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, ValueProperty, binding);
            return this;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
