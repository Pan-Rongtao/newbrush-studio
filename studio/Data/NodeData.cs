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
        static public NodeData Empty { get { return new NodeData(); } }

        public NodeData(string typeName, string name)
        {
            _typeName = typeName;
            _name = name;
            _iconType = TypeMapping.GetIcon(typeName);
            InitPropertyGridData(typeName);
        }

        public MetaClass FindMetaClass(String typeName)
        {
            foreach (Plugin plugin in ViewModel.Plugins)
            {
                foreach (var mc in plugin.MetaClasses)
                {
                    if (mc.Descriptor.TypeName == typeName)
                    {
                        return mc;
                    }
                }
            }
            return null;
        }

        private void InitPropertyGridData(string typeName)
        {
            var mc = FindMetaClass(typeName);
            if (mc == null)
            {
                ViewModel.LogData.Add(LogLevel.Error, "{0}不是一个已注册的类型", typeName);
                return;
            }

            foreach (var p in mc.Properties)
            {
                MyPropertyDescriptor pds = new MyPropertyDescriptor(p);
                try
                {
                    PropertyGridData.Data.Add(pds.DisplayName, pds);
                }
                catch(Exception)
                {
                    ViewModel.LogData.Add(LogLevel.Warn, "{0}注册了重复的属性名{1}，请检查插件代码", typeName, p.DisplayName);
                }

            }
        }

        public string Type { get { return _typeName; } }
        public PackIconKind IconType { get { return _iconType; } }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool Visibility
        {
            get { return _visibility; }
            set { _visibility = value; }
        }

        public NotifyProperyDescriptorCollection PropertyGridData { get { return _propertyGridData; } }

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

        private string _typeName;
        private PackIconKind _iconType = PackIconKind.GlobeLight;
        private string _name;
        private bool _visibility = true;
        private NotifyProperyDescriptorCollection _propertyGridData = new NotifyProperyDescriptorCollection() { Data = new MyPropertyDescriptorCollection() };

        private NodeData() { }
    }
        
}
