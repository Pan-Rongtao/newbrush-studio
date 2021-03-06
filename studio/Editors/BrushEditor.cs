﻿using System;
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
    public class BrushEditor : TypeEditor<BrushPickerCtrl>
    {
        protected override BrushPickerCtrl CreateEditor()
        {
            return new BrushPickerCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = BrushPickerCtrl.BrushProperty;
        }
    }

}
