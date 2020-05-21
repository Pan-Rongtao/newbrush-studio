using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Collections;
using Xceed.Wpf.Toolkit;

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
                if(Changed != null)
                {
                    Changed(this, _data);
                }
            }
        }
        private MyPropertyDescriptorCollection _data;
    }
    
    public class PropertyAttr
    {
        public UInt32 PropertyID { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }

        public PropertyAttr(UInt32 propertyID, string category, string name, string description, object value)
        {
            PropertyID = propertyID;
            Category = category;
            Name = name;
            Description = description;
            Value = value;
        }
    }

    //定义自己的属性描述
    class MyPropertyDescriptor : PropertyDescriptor
    {
        public MyPropertyDescriptor(PropertyAttr attr, Attribute[] propertyAttributes) : base(attr.Name, propertyAttributes)
        {
            _attr = attr;
        }
        
        public PropertyAttr Attr { get { return _attr; } }
        private PropertyAttr _attr;

        public override bool CanResetValue(object component){return true;}

        public override object GetValue(object component) { return _attr.Value; }
        public override void ResetValue(object component) { }
        public override void SetValue(object component, object value) { _attr.Value = value; }

        public override bool ShouldSerializeValue(object component) { return false; }

        public override Type ComponentType { get { return this.GetType(); } }
        public override bool IsReadOnly { get { return false; } }
        public override Type PropertyType { get { return _attr.Value.GetType(); } }

        public override string Category { get { return _attr.Category; } }
        public override string Description { get { return _attr.Description; } }
        
    }

    //定义属性集合描述
    public class MyPropertyDescriptorCollection : Dictionary<string, PropertyAttr>, ICustomTypeDescriptor
    {
        public void Add(PropertyAttr attr)
        {
            Add(attr.Name, attr);
        }
        
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            Attribute[] a = new Attribute[1];
            System.ComponentModel.EditorAttribute ea = new EditorAttribute(typeof(StringListComboboxEditor), typeof(StringListComboboxEditor));
            a[0] = ea;
            return GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] pds = new PropertyDescriptor[Count];
            int index = 0;
            foreach (PropertyAttr item in Values)
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

}
