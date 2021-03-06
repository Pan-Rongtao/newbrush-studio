﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace studio
{
    class DateTimeEditor : TypeEditor<DateTimeBindingCtrl>
    {
        protected override DateTimeBindingCtrl CreateEditor()
        {
            return new DateTimeBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = DateTimeBindingCtrl.ValueProperty;
        }
    }
    
}
