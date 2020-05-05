using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using MaterialDesignThemes.Wpf;

namespace studio
{
    public class MetaObject
    {
        public ClassDescriptor Descriptor { get; }
        public List<PropertyDescriptor> Properties { get; }

        public MetaObject(ClassDescriptor descriptor, List<PropertyDescriptor> properties)
        {
            Descriptor = descriptor;
            Properties = properties;
        }

        public class ClassDescriptor
        {
            public ClassDescriptor(string type, string category, string displayname, string description, PackIconKind icon)
            {
                Type = type;
                Category = category;
                DisplayName = displayname;
                Description = description;
                Icon = icon;
            }

            public string Type { get; }
            public string Category { get; }
            public string DisplayName { get; }
            public string Description { get; }
            public PackIconKind Icon { get; }

            static private Dictionary<string, PackIconKind> _typeIconDictionary = new Dictionary<string, PackIconKind>
            {
                { "nb::Canvas", PackIconKind.DrawingBox }, { "nb::DockPanel", PackIconKind.ViewQuilt }, { "nb::WrapPanel", PackIconKind.FormatTextWrappingWrap },
                { "nb::StackPanel", PackIconKind.ViewWeek }, { "nb::Grid", PackIconKind.Grid }, { "nb::UniformGrid", PackIconKind.GridLarge }, { "nb::Button", PackIconKind.AlphabetBBoxOutline },
                { "nb::RepeatButton", PackIconKind.GestureTapButton }, { "nb::Window", PackIconKind.Airplay }, { "nb::Line", PackIconKind.VectorLine },
                { "nb::Polyline", PackIconKind.VectorPolyline }, { "nb::Polygon", PackIconKind.VectorPolygon }, { "nb::Path", PackIconKind.MapMarkerPath },
                { "nb::Rectangle", PackIconKind.RectangleOutline }, { "nb::Ellipse", PackIconKind.EllipseOutline },
            };

            static public PackIconKind TypeToIcon(string type)
            {
                PackIconKind icon = PackIconKind.GlobeLight;
                try
                {
                    icon = _typeIconDictionary[type];
                }
                catch (Exception) { }
                return icon;
            }
        }

        public class PropertyDescriptor
        {
            public Int32 Type { get; }
            public string Category { get; }
            public string Name { get; }
            public string Description { get; }

            public PropertyDescriptor(Int32 type, string category, string name, string description)
            {
                Type = type;
                Category = category;
                Name = name;
                Description = description;
            }
        }
    }


    class PluginManager
    {
        static public List<Plugin> Plugins
        {
            get { return _plugins; }
        }

        public static MetaObject FindMetaDataByTypeName(String type)
        {
            foreach (Plugin plugin in Plugins)
            {
                foreach(MetaObject m in plugin.MetaObjects)
                {
                    if (m.Descriptor.Type == type)
                    {
                        return m;
                    }
                }
            }
            return null;
        }

        static private List<Plugin> _plugins = new List<Plugin>();
    }

    class Plugin
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern int LoadLibrary(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(int hModule,
            [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        public static extern bool FreeLibrary(int hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int GetClassCount();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void GetMetaClasses(IntPtr classes, int count);

        public Plugin(string dllPath)
        {
            DllPath = dllPath;
            PluginManager.Plugins.Add(this);
        }

        public string DllPath { get; set; }

        public List<MetaObject> MetaObjects { get { return _metaObjs; } }
        private List<MetaObject> _metaObjs;

        public void Update()
        {
            string oldEnv = Environment.CurrentDirectory;
            Environment.CurrentDirectory = Path.GetDirectoryName(DllPath);
            int dll = LoadLibrary(DllPath);
            IntPtr intPtr = GetProcAddress(dll, "getMetaClassesCount");
            IntPtr intPtr1 = GetProcAddress(dll, "getMetaClasses");
            
            //3. 将函数指针封装成委托
            GetClassCount getClassFunction = (GetClassCount)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetClassCount));
            GetMetaClasses getMetaClassesFunction = (GetMetaClasses)Marshal.GetDelegateForFunctionPointer(intPtr1, typeof(GetMetaClasses));

            int count = getClassFunction();
            CClassInfo[] metaDatas = new CClassInfo[count];
            _metaObjs = new List<MetaObject>(count);
            
            int size = Marshal.SizeOf(typeof(CClassInfo)) * count;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            getMetaClassesFunction(buffer, count);
            for (int i = 0; i < count; ++i)
            {
                IntPtr p = new IntPtr(buffer.ToInt64() + Marshal.SizeOf(typeof(CClassInfo)) * i);
                metaDatas[i] = (CClassInfo)Marshal.PtrToStructure(p, typeof(CClassInfo));

                MetaObject.ClassDescriptor descriptor = new MetaObject.ClassDescriptor(metaDatas[i].Type, metaDatas[i].Category, metaDatas[i].DisplayName, metaDatas[i].Description, MetaObject.ClassDescriptor.TypeToIcon(metaDatas[i].Type));
                List<MetaObject.PropertyDescriptor> properties = new List<MetaObject.PropertyDescriptor>();
                foreach(var pd in metaDatas[i].PropertyData)
                {
                    if(pd.Type != 0)
                    {
                        properties.Add(new MetaObject.PropertyDescriptor(pd.Type, pd.Category, pd.DisplayName, pd.Description));
                    }
                }

                _metaObjs.Add(new MetaObject(descriptor, properties));
            }
            Marshal.FreeHGlobal(buffer);
            FreeLibrary(dll);
            Environment.CurrentDirectory = oldEnv;

            OutputPanel.LogData.Add(LogLevel.Info, "更新插件完毕！");
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CClassInfo        //size=64+64+64+256 + 388*100=39248
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string Type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string Category;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string DisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Description;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 100)]
            public CPropertyInfo[] PropertyData;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CPropertyInfo    //size=4+64+64+256=388
        {
            public Int32 Type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string Category;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string DisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Description;
        }


    }

}
