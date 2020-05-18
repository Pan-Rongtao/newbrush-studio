using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.ComponentModel;

namespace studio
{
    public enum ResourceType
    {
        Folder = 0x0001,
        Image = 0x0002,
        Font = 0x0004,
        ShaderSource = 0x0008,
        ThreeDAssets = 0x0010,

        ImageFolder = Folder | Image,
        FontFolder = Folder | Font,
        ShaderSourceFolder = Folder | ShaderSource,
        ThreeDAssetsFolder = Folder | ThreeDAssets,
    }
    
    public class ResourceTypeIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ResourceType flags = (ResourceType)value;
            if((flags.HasFlag(ResourceType.Folder)))                return PackIconKind.FolderOutline;
            else if((flags.HasFlag(ResourceType.Font)))             return PackIconKind.FormatFont;
            else if ((flags.HasFlag(ResourceType.Image)))           return PackIconKind.FileImageOutline;
            else if ((flags.HasFlag(ResourceType.ShaderSource)))    return PackIconKind.FileCodeOutline;
            else if ((flags.HasFlag(ResourceType.ThreeDAssets)))    return PackIconKind.CubeOutline;//Codepen
            else throw new Exception("");

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ResourceNode : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ResourceNode(string name, ResourceType type)
        {
            Name = name;
            ResourceType = type;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        private string _name;

        public ResourceType ResourceType { get; }
        public Byte[] Data { set; get; }

        
        public ObservableCollection<ResourceNode> Children { get { return _children; } }
        private ObservableCollection<ResourceNode> _children = new ObservableCollection<ResourceNode>();

        public bool Is(ResourceType t)
        {
            return ResourceType.HasFlag(t);
        }

        public ResourceNode GetChild(string name)
        {
            foreach (var child in Children)
            {
                if (child.Name == name)
                    return child;
            }
            return null;
        }

        public ResourceNode Find(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            var p = this;
            string[] names = path.Split('/');
            foreach (string name in names)
            {
                var child = p.GetChild(name);
                if (child == null)
                    return null;
                p = child;
            }
            return p;
        }
    }


}
