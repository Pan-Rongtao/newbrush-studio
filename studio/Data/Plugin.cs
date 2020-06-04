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
    }

    public class MetaPropertyDescriptor
    {
        public UInt64 TypeID { get; }
        public string ValueTypeName { get; }
        public string Category { get; }
        public string DisplayName { get; }
        public int Order { get; }
        public string Description { get; }
        public string EnumSource { get; }

        public MetaPropertyDescriptor(UInt64 typeName, string valueTypeName, string category, string displayName, int order, string description, string enumSource)
        {
            TypeID = typeName;
            ValueTypeName = valueTypeName;
            Category = category;
            DisplayName = displayName;
            Order = order;
            Description = description;
            EnumSource = enumSource;
        }

        public bool IsEnumType()
        {
            return !string.IsNullOrEmpty(EnumSource);
        }
    }

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

                var classDescriptor = new MetaClassDescriptor(metaDatas[i].TypeName, metaDatas[i].Category, metaDatas[i].DisplayName, metaDatas[i].Description, CppCShapeTypeParser.TypeNameToIcon(metaDatas[i].TypeName));
                var properties = new List<MetaPropertyDescriptor>();
                foreach (var pd in metaDatas[i].PropertyData)
                {
                    if (pd.TypeID != 0)
                    {
                        properties.Add(new MetaPropertyDescriptor(pd.TypeID, pd.ValueTypeName, pd.Category, pd.DisplayName, pd.Order, pd.Description, pd.EnumSource));
                    }
                }
                _metaClasses.Add(new MetaClass(classDescriptor, properties));
            }

            Marshal.FreeHGlobal(buffer);
        }

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
}
