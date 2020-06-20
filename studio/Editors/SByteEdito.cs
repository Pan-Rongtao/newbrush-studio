using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class SByteEditor : TypeEditor<SByteBindingCtrl>
    {
        protected override SByteBindingCtrl CreateEditor()
        {
            return new SByteBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = SByteBindingCtrl.ValueProperty;
        }
    }
}
