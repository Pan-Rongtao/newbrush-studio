using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace studio
{
    class Win32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, short State);

        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        public static extern bool FreeLibrary(int hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetMetaClassCountDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetMetaClassesDelegate(IntPtr classes, int count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetCategoryOrderCountDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetCategoryOrdersDelegate(IntPtr categorys, int count);

    }
}
