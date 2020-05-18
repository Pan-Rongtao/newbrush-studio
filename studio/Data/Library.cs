using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studio
{
    public class Library
    {
        public Library()
        {
            ResouceFilesRoot.Children.Add(new ResourceNode("3D Assets", ResourceType.Folder | ResourceType.ThreeDAssets));
            ResouceFilesRoot.Children.Add(new ResourceNode("Images", ResourceType.Folder | ResourceType.Image));
            ResouceFilesRoot.Children.Add(new ResourceNode("Fonts", ResourceType.Folder | ResourceType.Font));
            ResouceFilesRoot.Children.Add(new ResourceNode("Shaders", ResourceType.Folder | ResourceType.ShaderSource));
        }

        public ResourceNode ResouceFilesRoot { get { return _resouceFilesRoot; } }
        private ResourceNode _resouceFilesRoot = new ResourceNode("资源文件", ResourceType.Folder);
        
        public ResourceNode ResourceFilesSubRoot(ResourceType t)
        {
            if (t.HasFlag(ResourceType.Font)) return ResouceFilesRoot.GetChild("3D Assets");
            else if (t.HasFlag(ResourceType.Font)) return ResouceFilesRoot.GetChild("Images");
            else if (t.HasFlag(ResourceType.Font)) return ResouceFilesRoot.GetChild("Fonts");
            else if (t.HasFlag(ResourceType.Font)) return ResouceFilesRoot.GetChild("Shaders");
            else return null;
        }

    }
}
