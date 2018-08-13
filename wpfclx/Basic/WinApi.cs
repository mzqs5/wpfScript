using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace wpfclx
{
    /// <summary>
    /// 引入user32.dll windows api
    /// </summary>
    public class WinApi
    {

        #region 根据句柄寻找窗体并发送消息

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        //参数1:指的是类名。参数2，指的是窗口的标题名。两者至少要知道1个
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        //[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        //public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        //[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        //public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hWnd, uint wMsg, int wParam, int lParam);

        //[DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
        //public static extern int PostMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, int lParam);

        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern int SetWindowText(
            IntPtr hwnd,
            string lpString
        );

        //[DllImport("user32.dll")]
        //public static extern int GetWindowTextLength(IntPtr hWnd);

        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);


        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

        //[DllImport("Kernel32.dll")]
        //public extern static int FormatMessage(int flag, ref IntPtr source, int msgid, int langid, ref string buf, int size, ref IntPtr args);

        //[DllImport("user32.dll")]
        //public static extern int MapVirtualKey(uint Ucode, uint uMapType);

        #endregion
        //hWndInsertAfter 参数可选值:
        internal const int HWND_TOP = 0;//在前面

        internal const int HWND_BOTTOM = 1;//在后面

        internal const int HWND_TOPMOST = -1;//在前面, 位于任何顶部窗口的前面

        internal const int HWND_NOTOPMOST = -2;//在前面, 位于其他顶部窗口的后面

        //uFlags 参数可选值:
        internal const uint SWP_NOSIZE = 1; //忽略 cx、cy, 保持大小
        internal const uint SWP_NOMOVE = 2; //忽略 X、Y, 不改变位置

        internal const uint SWP_NOZORDER = 4; //忽略 hWndInsertAfter, 保持 Z 顺序}
        internal const uint SWP_NOREDRAW = 8; //{不重绘}
        internal const uint SWP_NOACTIVATE = 10; //{不激活}
        internal const uint SWP_FRAMECHANGED = 20; //{强制发送 WM_NCCALCSIZE 消息, 一般只是在改变大小时才发送此消息}
        internal const uint SWP_SHOWWINDOW = 40; //{显示窗口}
        internal const uint SWP_HIDEWINDOW = 80; //{隐藏窗口}
        internal const uint SWP_NOCOPYBITS = 100; //{丢弃客户区}
        internal const uint SWP_NOOWNERZORDER = 200; //{忽略 hWndInsertAfter, 不改变 Z 序列的所有者}
        internal const uint SWP_NOSENDCHANGING = 400; //{不发出 WM_WINDOWPOSCHANGING 消息}
        internal const uint SWP_DRAWFRAME = SWP_FRAMECHANGED; //{画边框}
        internal const uint SWP_NOREPOSITION = SWP_NOOWNERZORDER;//{}
        internal const uint SWP_DEFERERASE = 2000; //{防止产生 WM_SYNCPAINT 消息}
        internal const uint SWP_ASYNCWINDOWPOS = 4000; //{若调用进程不拥有窗口, 系统会向拥有窗口的线程发出需求}

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, uint flags);

        #region 获取窗体位置
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }
        #endregion

        //#region 设置窗体显示形式

        //public enum nCmdShow : uint
        //{
        //    SW_NONE,//初始值
        //    SW_FORCEMINIMIZE,//：在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
        //    SW_MIOE,//：隐藏窗口并激活其他窗口。
        //    SW_MAXIMIZE,//：最大化指定的窗口。
        //    SW_MINIMIZE,//：最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
        //    SW_RESTORE,//：激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
        //    SW_SHOW,//：在窗口原来的位置以原来的尺寸激活和显示窗口。
        //    SW_SHOWDEFAULT,//：依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
        //    SW_SHOWMAXIMIZED,//：激活窗口并将其最大化。
        //    SW_SHOWMINIMIZED,//：激活窗口并将其最小化。
        //    SW_SHOWMINNOACTIVATE,//：窗口最小化，激活窗口仍然维持激活状态。
        //    SW_SHOWNA,//：以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
        //    SW_SHOWNOACTIVATE,//：以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
        //    SW_SHOWNOMAL,//：激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
        //}

        //public const int SW_HIDE = 0;
        //public const int SW_SHOWNORMAL = 1;
        //public const int SW_SHOWMINIMIZED = 2;
        //public const int SW_SHOWMAXIMIZED = 3;
        //public const int SW_MAXIMIZE = 3;
        //public const int SW_SHOWNOACTIVATE = 4;
        //public const int SW_SHOW = 5;
        //public const int SW_MINIMIZE = 6;
        //public const int SW_SHOWMINNOACTIVE = 7;
        //public const int SW_SHOWNA = 8;
        //public const int SW_RESTORE = 9;

        //[DllImport("User32.dll")]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        //[DllImport("User32.dll")]
        //public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //#endregion

        //#region 控制鼠标移动

        ////移动鼠标 
        //public const int MOUSEEVENTF_MOVE = 0x0001;
        ////模拟鼠标左键按下 
        //public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        ////模拟鼠标左键抬起 
        //public const int MOUSEEVENTF_LEFTUP = 0x0004;
        ////模拟鼠标右键按下 
        //public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        ////模拟鼠标右键抬起 
        //public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        ////模拟鼠标中键按下 
        //public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        ////模拟鼠标中键抬起 
        //public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        ////标示是否采用绝对坐标 
        //public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        //[Flags]
        //public enum MouseEventFlag : uint
        //{
        //    Move = 0x0001,
        //    LeftDown = 0x0002,
        //    LeftUp = 0x0004,
        //    RightDown = 0x0008,
        //    RightUp = 0x0010,
        //    MiddleDown = 0x0020,
        //    MiddleUp = 0x0040,
        //    XDown = 0x0080,
        //    XUp = 0x0100,
        //    Wheel = 0x0800,
        //    VirtualDesk = 0x4000,
        //    Absolute = 0x8000
        //}

        ////[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //[DllImport("user32.dll")]
        //public static extern bool SetCursorPos(int X, int Y);
        //[DllImport("user32.dll")]
        //public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //#endregion

        //#region 获取坐标钩子

        //[StructLayout(LayoutKind.Sequential)]
        //public class POINT
        //{
        //    public int X;
        //    public int Y;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public class MouseHookStruct
        //{
        //    public POINT pt;
        //    public int hwnd;
        //    public int wHitTestCode;
        //    public int dwExtraInfo;
        //}

        //public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        ////安装钩子
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        ////卸载钩子
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern bool UnhookWindowsHookEx(int idHook);
        ////调用下一个钩子
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        //#endregion

        #region gdi图色

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
string lpszDriver, // driver name驱动名 
string lpszDevice, // device name设备名 
string lpszOutput, // not used; should be NULL 
IntPtr lpInitData // optional printer data 
);
        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
        IntPtr hdcDest, // handle to destination DC目标设备的句柄 

        int nXDest, // x-coord of destination upper-left corner目标对象的左上角的X坐标 
        int nYDest, // y-coord of destination upper-left corner目标对象的左上角的Y坐标 
        int nWidth, // width of destination rectangle目标对象的矩形宽度 
        int nHeight, // height of destination rectangle目标对象的矩形长度 
        IntPtr hdcSrc, // handle to source DC源设备的句柄 
        int nXSrc, // x-coordinate of source upper-left corner源对象的左上角的X坐标 
        int nYSrc, // y-coordinate of source upper-left corner源对象的左上角的Y坐标 
        UInt32 dwRop // raster operation code光栅的操作值 
        );
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
        IntPtr hdc // handle to DC 
        );
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
        IntPtr hdc, // handle to DC 
        int nWidth, // width of bitmap, in pixels 
        int nHeight // height of bitmap, in pixels 
        );
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
        IntPtr hdc, // handle to DC 
        IntPtr hgdiobj // handle to object 
        );
        [DllImport("gdi32.dll")]
        public static extern int DeleteObject(
        IntPtr hdc // handle to object
        );
        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
        IntPtr hdc // handle to DC 
        );
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
        IntPtr hwnd, // Window to copy,Handle to the window that will be copied. 
        IntPtr hdcBlt, // HDC to print into,Handle to the device context. 
        UInt32 nFlags // Optional flags,Specifies the drawing options. It can be one of the following values. 
        );

        [DllImport("user32.dll")]
        //获取整个窗口DC
        public static extern IntPtr GetWindowDC(
        IntPtr hwnd);

        [DllImport("user32.dll")]
        //获取窗口客户区DC
        public static extern IntPtr GetDC(
        IntPtr hwnd);

        [DllImportAttribute("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        #endregion

    }

}