using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Dynamic;

namespace studio
{
    /// <summary>
    /// PropertyEditor.xaml 的交互逻辑
    /// </summary>
    public partial class PropertyEditor : UserControl
    {
        public PropertyEditor()
        {
            InitializeComponent();
            Type x;
            //RectangleProperties properties = new RectangleProperties();


            //properties.Opacity = 1.0;

            //var properties = new BusinessObject();
            //properties.Color = Colors.Blue;
            //pg.SelectedObject = properties;

            PropertyCollection pc = new PropertyCollection();
            pc.Add(new PropertyAttr("外观", "Fill", "111", Colors.Blue));
            pc.Add(new PropertyAttr("aa", "Margin", "222", new Thickness(1)));
            pc.Add(new PropertyAttr("aa", "Width", "333", 1.3));
            pg.SelectedObject = pc;

        }

        public class PropertyAttr
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public object Value { get; set; }

            public PropertyAttr(string category, string name, string description, object value)
            {
                Category = category;
                Name = name;
                Description = description;
                Value = value;
            }
        }

        //定义自己的属性描述
        public class MyPropertyDescriptor : PropertyDescriptor
        {
            private PropertyAttr _attr;
            public MyPropertyDescriptor(PropertyAttr attr, Attribute[] propertyAttributes) : base(attr.Name, propertyAttributes)
            {
                _attr = attr;
            }

            public override bool CanResetValue(object component)
            {
                return true;
            }

            public override object GetValue(object component)
            {
                return _attr.Value;
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
                _attr.Value = value;
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }

            public override Type ComponentType
            {
                get { return this.GetType(); }
            }

            public override bool IsReadOnly
            {
                get { return false; }
            }

            public override Type PropertyType
            {
                get { return _attr.Value.GetType(); }
            }

            public override string Category { get { return _attr.Category; } }

            public override string Description { get { return _attr.Description; } }

        }

        //定义属性集合描述
        public class PropertyCollection : ICustomTypeDescriptor
        {
            private Dictionary<string, PropertyAttr> _dc = new Dictionary<string, PropertyAttr>();
            public void Add(PropertyAttr attr)
            {
                if (!_dc.ContainsKey(attr.Name))
                {
                    _dc.Add(attr.Name, attr);
                }
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
            {
                return GetProperties(new Attribute[0]);
            }

            public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                int count = _dc.Values.Count;
                PropertyDescriptor[] pds = new PropertyDescriptor[count];
                int index = 0;
                foreach (PropertyAttr item in _dc.Values)
                {
                    pds[index] = new MyPropertyDescriptor(item, attributes);
                    index++;
                }
                return new PropertyDescriptorCollection(pds);
            }

            public object GetPropertyOwner(PropertyDescriptor pd)
            {
                return this;
            }

            public string GetClassName()
            {
                return GetType().Name;
            }

            public AttributeCollection GetAttributes()
            {
                return TypeDescriptor.GetAttributes(this, true);
            }

            public string GetComponentName()
            {
                throw new NotImplementedException();
            }

            public TypeConverter GetConverter()
            {
                return TypeDescriptor.GetConverter(this, true);
            }

            public EventDescriptor GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(this, true);
            }

            public PropertyDescriptor GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(this, true);
            }

            public object GetEditor(Type editorBaseType)
            {
                return TypeDescriptor.GetEditor(this, editorBaseType, true);
            }

            public EventDescriptorCollection GetEvents()
            {
                return TypeDescriptor.GetEvents(this, true);
            }

            public EventDescriptorCollection GetEvents(Attribute[] attributes)
            {
                return TypeDescriptor.GetEvents(this, attributes, true);
            }
            
        }

        [CategoryOrder("画笔", 0)]
        [CategoryOrder("外观", 1)]
        [CategoryOrder("布局", 2)]
        [CategoryOrder("转换", 3)]
        public class RectangleProperties
        {
            [Category("画笔")]
            public Color? Color { get; set; }
            [Category("画笔")]
            public Color? Stroke { get; set; }


            [Category("外观")]
            public double Opacity { get; set; }
            [Category("外观")]
            public Visibility Visibility { get; set; }
            [Category("外观")]
            public double RadiusX { get; set; }
            [Category("外观")]
            public double RadiusY { get; set; }
            [Category("外观")]
            public Thickness StrokeThickness { get; set; }
            [Category("外观")]
            public DoubleCollection StrokeDashArray { get; set; }
            [Category("外观")]
            public PenLineCap StrokeDashCap { get; set; }
            [Category("外观")]
            public double StrokeDashOffset { get; set; }
            [Category("外观")]
            public PenLineCap StrokeEndLineCap { get; set; }
            [Category("外观")]
            public PenLineJoin StrokeLineJoin { get; set; }
            [Category("外观")]
            public double StrokeMiterLimit { get; set; }
            [Category("外观")]
            public PenLineCap StrokeStartLineCap { get; set; }


            [Category("布局")]
            public double? Width { get; set; }
            [Category("布局")]
            public double? Height { get; set; }
            [Category("布局")]
            public HorizontalAlignment HorizontalAlignment { get; set; }
            [Category("布局")]
            public VerticalAlignment VerticalAlignment { get; set; }
            [Category("布局")]
            public Thickness Margin { get; set; }


            [Category("转换")]
            public TranslateTransform Translate { get; set; }
            [Category("转换")]
            public RotateTransform Rotate { get; set; }
            [Category("转换")]
            public ScaleTransform Scale { get; set; }
        }

    }
    
}
