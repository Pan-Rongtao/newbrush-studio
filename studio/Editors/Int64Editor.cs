using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class Int64Editor : TypeEditor<Int64BindingCtrl>
    {
        protected override Int64BindingCtrl CreateEditor()
        {
            return new Int64BindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = Int64BindingCtrl.ValueProperty;
        }
    }
}
