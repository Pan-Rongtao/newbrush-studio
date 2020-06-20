using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class DoubleEditor : TypeEditor<DoubleBindingCtrl>
    {
        protected override DoubleBindingCtrl CreateEditor()
        {
            return new DoubleBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = DoubleBindingCtrl.ValueProperty;
        }
    }
}
