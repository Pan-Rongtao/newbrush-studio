using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class Int16Editor : TypeEditor<Int16BindingCtrl>
    {
        protected override Int16BindingCtrl CreateEditor()
        {
            return new Int16BindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = Int16BindingCtrl.ValueProperty;
        }
    }
}
