using System;
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
    class ImageSourceEditor : TypeEditor<ImageSourceBindingCtrl>
    {
        protected override ImageSourceBindingCtrl CreateEditor()
        {
            return new ImageSourceBindingCtrl();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.Height = 20;
        }

        protected override void SetValueDependencyProperty()
        {
            ValueProperty = ImageSourceBindingCtrl.ValueProperty;
        }
    }
}
