using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Collections;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows.Media;
using System.Windows;

namespace studio
{
    public class NotifyProperyDescriptorCollection
    {
        public event EventHandler<MyPropertyDescriptorCollection> Changed;
        public MyPropertyDescriptorCollection Data
        {
            get { return _data; }
            set
            {
                _data = value;
                Changed?.Invoke(this, _data);
            }
        }
        private MyPropertyDescriptorCollection _data;
    }

    //定义自己的属性描述
    public class MyPropertyDescriptor : PropertyDescriptor
    {
        public MyPropertyDescriptor(MetaPropertyDescriptor p)
            : base(p.DisplayName, new Attribute[0])
        {
            MetaPropertyDescriptor = p;
        }

        public MetaPropertyDescriptor MetaPropertyDescriptor { get;}
        

        public override bool CanResetValue(object component){return true;}

        public override object GetValue(object component) { return MetaPropertyDescriptor.Value; }
        public override void ResetValue(object component) { }
        public override void SetValue(object component, object value) { MetaPropertyDescriptor.Value = value; }

        public override bool ShouldSerializeValue(object component) { return false; }

        public override Type ComponentType { get { return this.GetType(); } }
        public override bool IsReadOnly { get { return false; } }
        public override Type PropertyType { get { return MetaPropertyDescriptor.CShapeType; } }

        public override string Category { get { return MetaPropertyDescriptor.Category; } }
        public override string Description { get { return MetaPropertyDescriptor.Category; } }
        
        protected override Attribute[] AttributeArray
        {
            get
            {
                List<Attribute> ac = new List<Attribute>() { new PropertyOrderAttribute(MetaPropertyDescriptor.Order) };
                if (MetaPropertyDescriptor.EditorType != null)
                {
                    ac.Add(new EditorAttribute(MetaPropertyDescriptor.EditorType, MetaPropertyDescriptor.EditorType));
                }
                return ac.ToArray();
            }
        }

    }

    //定义属性集合描述
    //[CategoryOrder("Information", 0)]
    //[CategoryOrder("Conections", 1)]
    //[CategoryOrder("Other", 2)]
    public class MyPropertyDescriptorCollection : Dictionary<string, MyPropertyDescriptor>, ICustomTypeDescriptor
    {
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] pds = new PropertyDescriptor[Count];
            int index = 0;
            foreach (MyPropertyDescriptor item in Values)
            {
                pds[index++] = item;
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

}
