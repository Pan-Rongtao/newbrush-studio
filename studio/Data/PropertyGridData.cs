﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace studio
{
    public class NotifyProperyCollection
    {
        public event EventHandler<PropertyCollection> Changed;
        public PropertyCollection Data
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
        private PropertyCollection _data;
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
        public void Add(PropertyAttr attr)
        {
            if (!_dc.ContainsKey(attr.Name))
            {
                _dc.Add(attr.Name, attr);
            }
        }

        public void Clear()
        {
            _dc.Clear();
        }

        private Dictionary<string, PropertyAttr> _dc = new Dictionary<string, PropertyAttr>();

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

}
