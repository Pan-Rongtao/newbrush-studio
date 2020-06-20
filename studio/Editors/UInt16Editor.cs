using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class UInt16Editor : TypeEditor<UInt16BindingCtrl>
    {
        protected override UInt16BindingCtrl CreateEditor()
        {
            return new UInt16BindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = UInt16BindingCtrl.ValueProperty;
        }
    }
}
