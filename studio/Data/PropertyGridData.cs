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
        public MyPropertyDescriptor(UInt64 propertyID, string category, string displayName, int order, string description, Type propertyType, object defaultValue, ArrayList itemsSource)
            : base(displayName, new Attribute[0])
        {
            PropertyID = propertyID;
            _category = category;
            _displayName = displayName;
            _order = order;
            _description = description;
            _value = defaultValue;
            _propertyType = propertyType;
            ItemsSource = itemsSource;

            Affirm(propertyType);
        }

        public UInt64 PropertyID { get; }
        private string _category;
        private string _displayName;
        private int _order;
        private string _description;
        private object _value;
        private Type _propertyType;
        private Type _editorType;
        public ArrayList ItemsSource { get; }

        private void Affirm(Type propertyType)
        {
            if (propertyType == typeof(string))
            {
                _editorType = typeof(TextBoxEditor);
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                _editorType = typeof(CheckBoxEditor);
            }
            else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
            {
                _editorType = typeof(DecimalUpDownEditor);
            }
            else if (propertyType == typeof(double) || propertyType == typeof(double?))
            {
                _editorType = typeof(DoubleUpDownEditor);
            }
            else if (propertyType == typeof(int) || propertyType == typeof(int?))
            {
                _editorType = typeof(DoubleUpDownEditor);
            }
            else if (propertyType == typeof(short) || propertyType == typeof(short?))
            {
                _editorType = typeof(ShortUpDownEditor);
            }
            else if (propertyType == typeof(long) || propertyType == typeof(long?))
            {
                _editorType = typeof(LongUpDownEditor);
            }
            else if (propertyType == typeof(float) || propertyType == typeof(float?))
            {
                _editorType = typeof(SingleUpDownEditor);
            }
            else if (propertyType == typeof(byte) || propertyType == typeof(byte?))
            {
                _editorType = typeof(ByteUpDownEditor);
            }
            else if (propertyType == typeof(sbyte) || propertyType == typeof(sbyte?))
            {
                //_editorType = typeof(SByteUpDownEditor);
            }
            else if (propertyType == typeof(uint) || propertyType == typeof(uint?))
            {
                //_editorType = typeof(UIntegerUpDownEditor);
            }
            else if (propertyType == typeof(ulong) || propertyType == typeof(ulong?))
            {
                //_editorType = typeof(UShortUpDownEditor);
            }
            else if (propertyType == typeof(ushort) || propertyType == typeof(ushort?))
            {
                //_editorType = typeof(UShortUpDownEditor);
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                _editorType = typeof(DateTimeUpDownEditor);
            }
            else if ((propertyType == typeof(Color)) || (propertyType == typeof(Color?)))
            {
                _editorType = typeof(ColorEditor);
            }
            else if (propertyType.IsEnum)
            {
                _editorType = typeof(EnumComboBoxEditor);
            }
            else if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeSpan?))
            {
                _editorType = typeof(TimeSpanUpDownEditor);
            }
            else if (propertyType == typeof(FontFamily) || propertyType == typeof(FontWeight) || propertyType == typeof(FontStyle) || propertyType == typeof(FontStretch))
            {
                _editorType = typeof(FontComboBoxEditor);
            }
            else if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
            {
                _editorType = typeof(MaskedTextBoxEditor);// { ValueDataType = propertyType, Mask = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA" };
            }
            else if (propertyType == typeof(char) || propertyType == typeof(char?))
            {
                _editorType = typeof(MaskedTextBoxEditor); //{ ValueDataType = propertyType, Mask = "&" };
            }
            else if (propertyType == typeof(object))
            {
                _editorType = typeof(TextBoxEditor);
            }
            else if(propertyType == typeof(string[]))
            {
                _editorType = typeof(StringCombox);
                //_itemsSourceType = typeof(StringListCollection);
            }
            else
            {
                //throw new Exception();
            }

        }

        public override bool CanResetValue(object component){return true;}

        public override object GetValue(object component) { return _value; }
        public override void ResetValue(object component) { }
        public override void SetValue(object component, object value) { _value = value; }

        public override bool ShouldSerializeValue(object component) { return false; }

        public override Type ComponentType { get { return this.GetType(); } }
        public override bool IsReadOnly { get { return false; } }
        public override Type PropertyType { get { return _propertyType; } }

        public override string Category { get { return _category; } }
        public override string Description { get { return _description; } }
        
        protected override Attribute[] AttributeArray
        {
            get
            {
                List<Attribute> ac = new List<Attribute>() { new PropertyOrderAttribute(_order) };
                if (_editorType != null)
                {
                    ac.Add(new EditorAttribute(_editorType, _editorType));
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
