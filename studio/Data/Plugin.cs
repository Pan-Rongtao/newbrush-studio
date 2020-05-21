using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Reflection;
using System.ComponentModel;
using System.Collections.ObjectModel;

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
            public UInt32 Type { get; }
            public string Category { get; }
            public string Name { get; }
            public string Description { get; }
            public Type ValueType { get; }
            public string Extra { get; }

            public PropertyDescriptor(UInt32 type, string category, string name, string description, Type valueType, string extra)
            {
                Type = type;
                Category = category;
                Name = name;
                Description = description;
                ValueType = valueType;
                Extra = extra;
            }
        }
    }


    public class PluginCollection : ObservableCollection<Plugin>
    {
        public MetaObject FindMetaObject(String type)
        {
            foreach (Plugin plugin in Items)
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

    }

    public class Plugin
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        public static extern bool FreeLibrary(int hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int GetMetaObjectCount();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void getMetaObjects(IntPtr classes, int count);
        
        public string DllPath { get; set; }

        public List<MetaObject> MetaObjects { get { return _metaObjs; } }
        private List<MetaObject> _metaObjs = new List<MetaObject>();

        private Dictionary<string, string> _propertyCategoryDic = new Dictionary<string, string>
        {
            { "画笔", "a画笔" }, { "外观", "b外观" }, { "公共", "c公共" }, { "自动化", "d自动化" }, { "布局", "e布局" },
            { "文本", "f文本" }, { "转换", "g转换" }, { "杂项", "h杂项" }, 
        };


        public void Update()
        {
            //先把工作路径设置为dll的路径才能，加载后恢复
            string oldEnv = Environment.CurrentDirectory;
            Environment.CurrentDirectory = Path.GetDirectoryName(DllPath);
            
            int dllHandle = LoadLibrary(DllPath);
            if(dllHandle == 0)
            {
                if (!File.Exists(DllPath))
                {
                    ViewModel.LogData.Add(LogLevel.Error, "无法找到[{0}]文件", DllPath);
                }
                else
                {
                    ViewModel.LogData.Add(LogLevel.Error, "无法加载[{0}]，可能的原因是，所依赖的库不在同一个目录", DllPath);
                }
                return;
            }

            const string metho0 = "getMetaObjectCount";
            const string metho1 = "getMetaObjects";
            IntPtr intPtr0 = GetProcAddress(dllHandle, metho0);
            IntPtr intPtr1 = GetProcAddress(dllHandle, metho1);
            if(intPtr0 == IntPtr.Zero || intPtr1 == IntPtr.Zero)
            {
                ViewModel.LogData.Add(LogLevel.Error, "[{0}]不是一个合法的Newbrush Studio插件，请检查DLL是否定义了[{1}]和[{2}]", DllPath, metho0, metho1);
                return;
            }

            GetMetaObjectCount getClassFunction = (GetMetaObjectCount)Marshal.GetDelegateForFunctionPointer(intPtr0, typeof(GetMetaObjectCount));
            getMetaObjects getMetaClassesFunction = (getMetaObjects)Marshal.GetDelegateForFunctionPointer(intPtr1, typeof(getMetaObjects));

            int count = getClassFunction();
            CClassInfo[] metaDatas = new CClassInfo[count];
            _metaObjs.Clear();
            
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
                        Type t = typeof(void);
                        switch(pd.ValueType)
                        {
                            case 0: t = typeof(Boolean); break;
                            case 1: t = typeof(Byte); break;
                            case 2: t = typeof(Int16); break;
                            case 3: t = typeof(Int32); break;
                            case 4: t = typeof(Int64); break;
                            case 5: t = typeof(SByte); break;
                            case 6: t = typeof(UInt16); break;
                            case 7: t = typeof(UInt32); break;
                            case 8: t = typeof(UInt64); break;
                            case 9: t = typeof(Single); break;
                            case 10: t = typeof(Double); break;
                            case 11: t = typeof(String); break;
                            case 12: t = typeof(void); break;
                            case 13: t = typeof(void); break;
                            case 14: t = typeof(void); break;
                            case 15: t = typeof(Matrix); break;
                            case 16: t = typeof(void); break;
                            case 17: t = typeof(void); break;
                            case 18: t = typeof(void); break;
                            case 19: t = typeof(void); break;
                            case 20: t = typeof(void); break;
                            case 21: t = typeof(void); break;
                            case 22: t = typeof(void); break;
                            case 23: t = typeof(Matrix3D); break;
                            case 24: t = typeof(Color); break;
                            case 25: t = typeof(DateTime); break;
                            case 26: t = typeof(TimeSpan); break;
                            case 27: t = typeof(Thickness); break;
                            case 28: t = typeof(List<String>); break;
                            case 29: t = typeof(Brush); break;
                            case 30: t = typeof(int); break;
                            case 31: t = typeof(List<Single>); break;
                            case 32: t = typeof(List<Point>); break;
                            default: break;
                        }
                        string category = "z其他";
                        try
                        {
                            category = _propertyCategoryDic[pd.Category];
                        }
                        catch (Exception) { }
                        properties.Add(new MetaObject.PropertyDescriptor(pd.Type, category, pd.DisplayName, pd.Description, t, pd.Extra));
                    }
                }
                _metaObjs.Add(new MetaObject(descriptor, properties));
            }

            Marshal.FreeHGlobal(buffer);
            //bool x = FreeLibrary(dllHandle);  先去掉，待SDK修复registerPorperty的问题，否则挂死
            Environment.CurrentDirectory = oldEnv;

            ViewModel.LogData.Add(LogLevel.Info, "更新插件[ {0} ]完毕", DllPath);
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
            public UInt32 Type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string Category;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string DisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Description;
            public Int32 ValueType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Extra;
        }


    }

}
