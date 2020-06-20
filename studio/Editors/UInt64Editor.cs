using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class UInt64Editor : TypeEditor<UInt64BindingCtrl>
    {
        protected override UInt64BindingCtrl CreateEditor()
        {
            return new UInt64BindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = UInt64BindingCtrl.ValueProperty;
        }
    }
}
