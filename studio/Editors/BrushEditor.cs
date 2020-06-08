using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows;
using System.Windows.Controls;

namespace studio
{
    public class BrushEditor : TypeEditor<BrushPicker>
    {
        protected override BrushPicker CreateEditor()
        {
            return new BrushPicker();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            //Editor.Height = 18;
            //   Editor.BorderThickness = new System.Windows.Thickness(0);
            //   Editor.DisplayColorAndName = true;
        }
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = ColorPicker.SelectedColorProperty;
        }
    }

}
