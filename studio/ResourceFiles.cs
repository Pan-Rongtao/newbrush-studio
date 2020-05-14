using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace studio
{
    public enum ResourceTypeFlags
    {
        Folder = 0x0001,
        Image = 0x0002,
        Font = 0x0004,
        ShaderSource = 0x0008,
        ThreeDAssets = 0x0010,
    }
    
    public class ResourceTypeIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ResourceTypeFlags flags = (ResourceTypeFlags)value;
            if((flags & ResourceTypeFlags.Folder) != 0)             return PackIconKind.FolderOutline;
            else if((flags & ResourceTypeFlags.Font) != 0)          return PackIconKind.FormatFont;
            else if ((flags & ResourceTypeFlags.Image) != 0)        return PackIconKind.Image;
            else if ((flags & ResourceTypeFlags.ShaderSource) != 0) return PackIconKind.Brush;
            else if ((flags & ResourceTypeFlags.ThreeDAssets) != 0) return PackIconKind.ViewModule;
            else throw new Exception("");

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ResourceNode
    {
        public ResourceNode() { }

        public ResourceNode(string name, ResourceTypeFlags type)
        {
            Name = name;
            ResourceType = type;
        }

        public string Name { get; set; }
        public ResourceTypeFlags ResourceType { get; }

        public ObservableCollection<ResourceNode> Children = new ObservableCollection<ResourceNode>();
        
    }


}
