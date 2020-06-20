using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class ByteEditor : TypeEditor<ByteBindingCtrl>
    {
        protected override ByteBindingCtrl CreateEditor()
        {
            return new ByteBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = ByteBindingCtrl.ValueProperty;
        }
    }
    
}
