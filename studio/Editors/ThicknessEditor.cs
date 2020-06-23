using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class ThicknessEditor : TypeEditor<ThicknessBindingCtrl>
    {
        protected override ThicknessBindingCtrl CreateEditor()
        {
            return new ThicknessBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 40;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = ThicknessBindingCtrl.ValueProperty;
        }
    }
    
}
