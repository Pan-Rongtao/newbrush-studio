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
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace studio
{
    public class MetaClass
    {
        public MetaClassDescriptor Descriptor { get; }
        public List<MetaPropertyDescriptor> Properties { get; }

        public MetaClass(MetaClassDescriptor descriptor, List<MetaPropertyDescriptor> properties)
        {
            Descriptor = descriptor;
            Properties = properties;
        }

    }
    
    public class MetaClassDescriptor
    {
        public MetaClassDescriptor(string typeName, string category, string displayname, string description, PackIconKind icon)
        {
            TypeName = typeName;
            Category = category;
            DisplayName = displayname;
            Description = description;
            Icon = icon;
        }

        public string TypeName { get; }
        public string Category { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public PackIconKind Icon { get; }

        static private Dictionary<string, PackIconKind> _typeIconDictionary = new Dictionary<string, PackIconKind>
        {
            { "nb::Canvas", PackIconKind.DrawingBox },
            { "nb::DockPanel", PackIconKind.ViewQuilt },
            { "nb::WrapPanel", PackIconKind.FormatTextWrappingWrap },
            { "nb::StackPanel", PackIconKind.ViewWeek },
            { "nb::Grid", PackIconKind.Grid },
            { "nb::UniformGrid", PackIconKind.GridLarge },
            { "nb::Button", PackIconKind.AlphabetBBoxOutline },
            { "nb::RepeatButton", PackIconKind.GestureTapButton },
            { "nb::Window", PackIconKind.Airplay },
            { "nb::Line", PackIconKind.VectorLine },
            { "nb::Polyline", PackIconKind.VectorPolyline },
            { "nb::Polygon", PackIconKind.PentagonOutline },
            { "nb::Path", PackIconKind.MapMarkerPath },
            { "nb::Rectangle", PackIconKind.RectangleOutline },
            { "nb::Ellipse", PackIconKind.EllipseOutline },
            { "nb::Image", PackIconKind.FileImageOutline },
            { "nb::TextBlock", PackIconKind.FormatTextRotationNone },
        };

        static public PackIconKind GetIcon(string cppType)
        {
            PackIconKind icon = PackIconKind.GlobeLight;
            try
            {
                icon = _typeIconDictionary[cppType];
            }
            catch (Exception) { }
            return icon;
        }

    }

    
    public class MetaPropertyDescriptor
    {
        public UInt64 CppTypeID { get; }
        public string CppTypeName { get; }
        public string Category { get; }
        public string DisplayName { get; }
        public int Order { get; }
        public string Description { get; }
        public string[] EnumSource { get; }
        public bool IsEnum { get { return EnumSource.Length != 0; } }
        public Type CShapeType { get; }
        public Type EditorType { get; }
        public object Value { get; set; }

        public MetaPropertyDescriptor(string cppTypeName, UInt64 cppTypeID, string category, string displayName, int order, string description, string[] enumSource,
            Type cshapeType, Type editorType, object value)
        {
            CppTypeName = cppTypeName;
            CppTypeID = cppTypeID;
            Category = category;
            DisplayName = displayName;
            Order = order;
            Description = description;
            EnumSource = enumSource;
            CShapeType = cshapeType;
            EditorType = editorType;
            Value = value;
            
        }

        #region 根据cpp的属性值类型获取对应的CShape值类型，编辑格类型，默认值
        static public Tuple<Type, Type, object> GetTupleInfo(string cppType, string[] enumSource)
        {
            Tuple<Type, Type, object> ret = null;
            if (enumSource.Length != 0)            //如果是枚举
            {
                ret = new Tuple<Type, Type, object>(typeof(string), typeof(EnumEditor), enumSource[0]);
            }
            else
            {
                try { ret = _typeMap[cppType];  }
                catch (Exception) { }
            }
            return ret;
        }

        static private Dictionary<string, Tuple<Type, Type, object>> _typeMap = new Dictionary<string, Tuple<Type, Type, object>>
        {
            { "bool",                                       new Tuple<Type, Type, object>(typeof(TimeSpan), typeof(TimeSpanEditor), new TimeSpan())},
            { "char",                                       new Tuple<Type, Type, object>(typeof(byte), typeof(ByteEditor), new byte())},          //当作int8来处理
            { "signedchar",                                 new Tuple<Type, Type, object>(typeof(sbyte), typeof(SByteEditor), new sbyte())},
            { "unsignedchar",                               new Tuple<Type, Type, object>(typeof(byte), typeof(ByteEditor), new byte())},
            { "wchar",                                      new Tuple<Type, Type, object>(typeof(Int16), typeof(Int16Editor), new Int16())},       //当作int16来处理
            { "short",                                      new Tuple<Type, Type, object>(typeof(Int16), typeof(Int16Editor), new Int16())},
            { "unsignedshort",                              new Tuple<Type, Type, object>(typeof(UInt16), typeof(UInt16Editor), new UInt16())},
            { "int",                                        new Tuple<Type, Type, object>(typeof(Int32), typeof(Int32Editor), new Int32())},
            { "unsignedint",                                new Tuple<Type, Type, object>(typeof(UInt32), typeof(UInt32Editor), new UInt32())},
            { "long",                                       new Tuple<Type, Type, object>(typeof(Int32), typeof(Int32Editor), new Int32())},
            { "unsignedlong",                               new Tuple<Type, Type, object>(typeof(UInt32), typeof(UInt32Editor), new UInt32())},
            { "__int64",                                    new Tuple<Type, Type, object>(typeof(Int64), typeof(Int64Editor), new Int64())},
            { "unsigned__int64",                            new Tuple<Type, Type, object>(typeof(UInt64), typeof(UInt64Editor), new UInt64())},
            { "float",                                      new Tuple<Type, Type, object>(typeof(float), typeof(FloatEditor), new float())},
            { "double",                                     new Tuple<Type, Type, object>(typeof(double), typeof(DoubleEditor), new double())},
            { "longdouble",                                 new Tuple<Type, Type, object>(typeof(decimal), typeof(DecimalEditor), new decimal())},
            { "std::string",                                new Tuple<Type, Type, object>(typeof(string), typeof(StringEditor), string.Empty)},

            { "classnb::Point",                             new Tuple<Type, Type, object>(typeof(Point), typeof(PointEditor), new Point())},
            { "classnb::Color",                             new Tuple<Type, Type, object>(typeof(Color), typeof(ColorEditor), new Color())},
            { "classnb::Thickness",                         new Tuple<Type, Type, object>(typeof(Thickness), typeof(ThicknessEditor), new Thickness())},
            { "classnb::DateTime",                          new Tuple<Type, Type, object>(typeof(DateTime), typeof(DateTimeEditor), new DateTime())},
            { "classnb::TimeSpan",                          new Tuple<Type, Type, object>(typeof(TimeSpan), typeof(TimeSpanEditor), new TimeSpan())},

            { "classstd::shared_ptr<classnb::Transform>",   new Tuple<Type, Type, object>(typeof(Transform), null, null) },
            { "classstd::shared_ptr<classnb::ImageSource>", new Tuple<Type, Type, object>(typeof(ImageSource), typeof(ImageSourceEditor), null) },
            { "classstd::shared_ptr<classnb::Brush>",       new Tuple<Type, Type, object>(typeof(Brush), typeof(BrushEditor), null)},
            { "classstd::shared_ptr<classnb::Font>",        new Tuple<Type, Type, object>(typeof(FontFamily), null, new FontFamily())},
            { "classstd::shared_ptr<classnb::UIElement>",   new Tuple<Type, Type, object>(typeof(UIElement), null, null)},
            { "classstd::shared_ptr<classnb::ControlTemplate>", new Tuple<Type, Type, object>(typeof(FontFamily), null, null)},

            { "std::vector<float>",                         new Tuple<Type, Type, object>(typeof(Collection<Float>), typeof(CollectionEditor), new Collection<Float>())},
            { "classstd::vector<classnb::Point,classstd::allocator<classnb::Point> >",  new Tuple<Type, Type, object>(typeof(Collection<Point>), typeof(CollectionEditor), new Collection<Point>() {})},
            
        };

    }
    #endregion
    
    public class Float
    {
        public float Value { get; set; }
    }

    #region 插件对象
    public class Plugin
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct CCategoryOrderInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string Name;
            public int Order;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CClassInfo        //size=64+64+64+256 + 388*100=39248
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string TypeName;
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
            public UInt64 TypeID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string ValueTypeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string Category;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string DisplayName;
            public int Order;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Description;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string EnumSource;
        }

        public string DllPath { get; set; }
        public List<MetaClass> MetaClasses { get { return _metaClasses; } }
        private List<MetaClass> _metaClasses = new List<MetaClass>();
        
        public void Update()
        {
            //先把工作路径设置为dll的路径才能，加载后恢复
            string oldEnv = Environment.CurrentDirectory;
            Environment.CurrentDirectory = Path.GetDirectoryName(DllPath);

            int dllHandle = Win32.LoadLibrary(DllPath);
            if (dllHandle == 0)
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

            UpdateMetaClasses(dllHandle, "getMetaClassCount", "getMetaClasses");
            UpdateCategoryOrders(dllHandle, "getCategoryOrderCount", "getCategoryOrders");

            //FreeLibrary(dllHandle);  先去掉，待SDK修复registerPorperty的问题，否则挂死
            Environment.CurrentDirectory = oldEnv;

            ViewModel.LogData.Add(LogLevel.Info, "更新插件[ {0} ]完毕", DllPath);
        }

        private Tuple<Delegate, Delegate> GetDelegate<T1, T2>(int dllHandle, string getCountMetho, string getInfoMetho)
        {
            IntPtr intPtr0 = Win32.GetProcAddress(dllHandle, getCountMetho);
            IntPtr intPtr1 = Win32.GetProcAddress(dllHandle, getInfoMetho);
            if (intPtr0 == IntPtr.Zero || intPtr1 == IntPtr.Zero)
            {
                ViewModel.LogData.Add(LogLevel.Error, "[{0}]不是一个合法的Newbrush Studio插件，请检查DLL是否定义了[{1}]和[{2}]", DllPath, getCountMetho, getInfoMetho);
                return new Tuple<Delegate, Delegate>(null, null);
            }
            else
            {
                Delegate delegate0 = Marshal.GetDelegateForFunctionPointer(intPtr0, typeof(T1));
                Delegate delegate1 = Marshal.GetDelegateForFunctionPointer(intPtr1, typeof(T2));
                return new Tuple<Delegate, Delegate>(delegate0, delegate1);
            }

        }

        private void UpdateMetaClasses(int dllHandle, string getCountMetho, string getInfoMetho)
        {
            Tuple<Delegate, Delegate> delegates = GetDelegate<Win32.GetMetaClassCountDelegate, Win32.GetMetaClassesDelegate>(dllHandle, getCountMetho, getInfoMetho);

            int count = (delegates.Item1 as Win32.GetMetaClassCountDelegate)();
            CClassInfo[] metaDatas = new CClassInfo[count];
            _metaClasses.Clear();

            int size = Marshal.SizeOf(typeof(CClassInfo)) * count;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            (delegates.Item2 as Win32.GetMetaClassesDelegate)(buffer, count);
            for (int i = 0; i < count; ++i)
            {
                IntPtr p = new IntPtr(buffer.ToInt64() + Marshal.SizeOf(typeof(CClassInfo)) * i);
                metaDatas[i] = (CClassInfo)Marshal.PtrToStructure(p, typeof(CClassInfo));

                var classDescriptor = new MetaClassDescriptor(metaDatas[i].TypeName, metaDatas[i].Category, metaDatas[i].DisplayName, metaDatas[i].Description, MetaClassDescriptor.GetIcon(metaDatas[i].TypeName));
                var properties = new List<MetaPropertyDescriptor>();
                foreach (var pd in metaDatas[i].PropertyData)
                {
                    //如果ID为0表示这个属性信息为空
                    if (pd.TypeID != 0)
                    {
                        var enumSource = pd.EnumSource.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        var x = MetaPropertyDescriptor.GetTupleInfo(pd.ValueTypeName, enumSource);
                        if (x == null)
                        {
                            ViewModel.LogData.Add(LogLevel.Warn, "元件{0}的属性{1}使用了不支持的C++属性类型{2}", classDescriptor.TypeName, pd.DisplayName, pd.ValueTypeName);
                            continue;
                        }
                        properties.Add(new MetaPropertyDescriptor(pd.ValueTypeName, pd.TypeID, pd.Category, pd.DisplayName, pd.Order, pd.Description, enumSource, x.Item1, x.Item2, x.Item3));
                    }
                }
                _metaClasses.Add(new MetaClass(classDescriptor, properties));
            }

            Marshal.FreeHGlobal(buffer);
        }

        //更新CategoryOrders
        private void UpdateCategoryOrders(int dllHandle, string getCountMetho, string getInfoMetho)
        {
            Tuple<Delegate, Delegate> delegates = GetDelegate<Win32.GetCategoryOrderCountDelegate, Win32.GetCategoryOrdersDelegate>(dllHandle, getCountMetho, getInfoMetho);

            int count = (delegates.Item1 as Win32.GetCategoryOrderCountDelegate)();
            CCategoryOrderInfo[] categoryOrders = new CCategoryOrderInfo[count];
            CategoryOrderAttribute[] categoryOrderAttributes = new CategoryOrderAttribute[count];

            IntPtr buffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CCategoryOrderInfo)) * count);
            (delegates.Item2 as Win32.GetCategoryOrdersDelegate)(buffer, count);
            for (int i = 0; i < count; ++i)
            {
                IntPtr p = new IntPtr(buffer.ToInt64() + Marshal.SizeOf(typeof(CCategoryOrderInfo)) * i);
                categoryOrders[i] = (CCategoryOrderInfo)Marshal.PtrToStructure(p, typeof(CCategoryOrderInfo));
                categoryOrderAttributes[i] = new CategoryOrderAttribute(categoryOrders[i].Name, categoryOrders[i].Order);
            }

            App.Current.Resources["CategoryOrderAttributes"] = categoryOrderAttributes;
            Marshal.FreeHGlobal(buffer);
        }

    }
    #endregion
}
