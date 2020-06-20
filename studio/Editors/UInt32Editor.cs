using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class UInt32Editor : TypeEditor<UInt32BindingCtrl>
    {
        protected override UInt32BindingCtrl CreateEditor()
        {
            return new UInt32BindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = UInt32BindingCtrl.ValueProperty;
        }
    }
}
