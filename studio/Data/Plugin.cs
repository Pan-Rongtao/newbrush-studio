using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace studio
{
    class Plugin
    {
        [DllImport("NbGuid.dll", EntryPoint = "getClassesCount", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getClassesCount();

        [DllImport("NbGuid.dll", EntryPoint = "getEditorInfos", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getEditorInfos(IntPtr infos, int count);

        [StructLayout(LayoutKind.Sequential)]
        public struct MetaData
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string Type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string DefaultName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Description;
        }

        private static MetaData[] _metaDatas;

        public static void Update()
        {
            int count = getClassesCount();
            _metaDatas = new MetaData[count];

            int size = Marshal.SizeOf(typeof(MetaData)) * count;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            getEditorInfos(buffer, count);
            for (int i = 0; i < count; ++i)
            {
                IntPtr p = new IntPtr(buffer.ToInt64() + Marshal.SizeOf(typeof(MetaData)) * i);
                _metaDatas[i] = (MetaData)Marshal.PtrToStructure(p, typeof(MetaData));
            }
            Marshal.FreeHGlobal(buffer);
        }

        public static MetaData findMetaDataByTypeName(String type)
        {
            foreach(MetaData m in _metaDatas)
            {
                if(m.Type == type)
                {
                    return m;
                }
            }
            return new MetaData();
        }
    }
}
