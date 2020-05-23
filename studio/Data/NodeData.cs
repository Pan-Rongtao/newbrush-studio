using System;
using System.Collections.Generic;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Collections;

namespace studio
{
    public class NodeData
    {
        public NodeData()
        {
        }

        public NodeData(string type, string name)
        {
            _type = type;
            _name = name;
            _iconType = MetaObject.ClassDescriptor.TypeToIcon(type);
            MetaObject meta = ViewModel.Plugins.FindMetaObject(type);
            if (meta != null)
            {
                foreach (MetaObject.PropertyDescriptor p in meta.Properties)
                {
                    object defaultValue;
                    ArrayList itemsSource = null;
                    if (p.ValueType == typeof(String))
                    {
                        defaultValue = string.Empty;
                    }
                    else if (p.ValueType == typeof(string[]))
                    {
                        var strs = p.Extra.Split('|');
                        ArrayList items = new ArrayList();
                        foreach (string s in strs)
                            items.Add(s);
                        
                        defaultValue = strs[0];
                        itemsSource = items;
                    }
                    else if (p.ValueType == typeof(Brush))
                    {
                        defaultValue = new SolidColorBrush();
                    }
                    else
                    {
                        defaultValue = System.Activator.CreateInstance(p.ValueType);
                    }
                    MyPropertyDescriptor pds = new MyPropertyDescriptor(p.Type, p.Category, p.Name, p.Description, p.ValueType, defaultValue, itemsSource);
                    PropertyGridData.Data.Add(pds.DisplayName, pds);
                }
            }
        }

        public String Type
        {
            get { return _type; }
            set { _type = value; IconType = MetaObject.ClassDescriptor.TypeToIcon(value); }
        }

        public PackIconKind IconType
        {
            get { return _iconType; }
            set
            {
                _iconType = value;
            }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
            }
        }

        public NotifyProperyDescriptorCollection PropertyGridData = new NotifyProperyDescriptorCollection() { Data = new MyPropertyDescriptorCollection() };

        public NodeData GetChild(string name)
        {
            foreach(NodeData n in Children)
            {
                if (n.Name == name)
                    return n;
            }
            return null;
        }

        public NodeData Find(string path)
        {
            NodeData p = this;
            string[] names = path.Split('.');
            foreach (string name in names)
            {
                NodeData child = p.GetChild(name);
                if (child == null)
                {
                    return null;
                }
                else
                {
                    p = child;
                }
            }
            return p;
        }

        public ObservableCollection<NodeData> Children { get { return _children; } set { _children = value; } }
        private ObservableCollection<NodeData> _children = new ObservableCollection<NodeData>();

        private String _type;
        private PackIconKind _iconType = PackIconKind.GlobeLight;
        private string _name;
        private bool _visibility = true;
    }
        
}
