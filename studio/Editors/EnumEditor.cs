using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Collections;

namespace studio
{
    public class EnumEditor : TypeEditor<EnumBindingCtrl>
    {
        protected override EnumBindingCtrl CreateEditor()
        {
            return new EnumBindingCtrl();
        }
        
        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
            EnumBindingCtrl e = Editor as EnumBindingCtrl;
            e.ItemsSource = (propertyItem.PropertyDescriptor as MyPropertyDescriptor).MetaPropertyDescriptor.EnumSource;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = EnumBindingCtrl.SelectItemProperty;
        }
    }
}
