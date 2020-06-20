using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace studio
{
    class BoolEditor : TypeEditor<BoolBindingCtrl>
    {
        protected override BoolBindingCtrl CreateEditor()
        {
            return new BoolBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = BoolBindingCtrl.ValueProperty;
        }
    }

    
}
