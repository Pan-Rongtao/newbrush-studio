using System;
using System.Collections.Generic;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

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
