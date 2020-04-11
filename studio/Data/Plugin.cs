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
        public static MetaData[] MetaDatas;

        [DllImport("NbGuid.dll", EntryPoint = "getClassesCount", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getClassesCount();

        [DllImport("NbGuid.dll", EntryPoint = "getEditorInfos", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getEditorInfos(IntPtr infos, int count);

        [StructLayout(LayoutKind.Sequential)]
        public struct MetaData
        {
            Int64 id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string name;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string description;
        }

        public static void update()
        {
            int count = getClassesCount();
            MetaDatas = new MetaData[count];

            int size = Marshal.SizeOf(typeof(MetaData)) * count;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            getEditorInfos(buffer, count);
            for (int i = 0; i < count; ++i)
            {
                IntPtr p = new IntPtr(buffer.ToInt64() + Marshal.SizeOf(typeof(MetaData)) * i);
                MetaDatas[i] = (MetaData)Marshal.PtrToStructure(p, typeof(MetaData));
            }
            Marshal.FreeHGlobal(buffer);
        }
    }
}
