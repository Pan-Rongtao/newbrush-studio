using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Collections.ObjectModel;
using System.Collections;

namespace studio
{
    class StringComboxEditor : ComboBoxEditor
    {
        protected override IEnumerable CreateItemsSource(PropertyItem propertyItem)
        {
            return (propertyItem.PropertyDescriptor as MyPropertyDescriptor).MetaPropertyDescriptor.EnumSource;
        }
    }
}
