using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class FloatEditor : TypeEditor<FloatBindingCtrl>
    {
        protected override FloatBindingCtrl CreateEditor()
        {
            return new FloatBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            //Editor.Height = 18;
            //   Editor.BorderThickness = new System.Windows.Thickness(0);
            //   Editor.DisplayColorAndName = true;
        }
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = FloatBindingCtrl.ValueProperty;
        }
    }
}
