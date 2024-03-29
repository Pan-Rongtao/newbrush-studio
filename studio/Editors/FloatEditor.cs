﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows.Controls;
using System.Windows;

namespace studio
{
    class FloatEditor : TypeEditor<FloatBindingCtrl>
    {
        protected override FloatBindingCtrl CreateEditor()
        {
            return new FloatBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }
        //public override FrameworkElement ResolveEditor(PropertyItem propertyItem)
        //{
        //    return null;
        //}
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = FloatBindingCtrl.ValueProperty;
        }
    }
}
