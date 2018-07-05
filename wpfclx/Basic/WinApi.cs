using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace wpfclx
{
    /// <summary>
    /// 引入user32.dll windows api
    /// </summary>
    public class WinApi
    {

        #region 根据句柄寻找窗体并发送消息

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //参数1:指的是类名。参数2，指的是窗口的标题名。两者至少要知道1个
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint wMsg, int wParam, int lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern int SetWindowText(
            IntPtr hwnd,
            string lpString
        );

        #endregion

        #region 获取窗体位置
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }
        #endregion

        #region 设置窗体显示形式

        public enum nCmdShow : uint
        {
            SW_NONE,//初始值
            SW_FORCEMINIMIZE,//：在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
            SW_MIOE,//：隐藏窗口并激活其他窗口。
            SW_MAXIMIZE,//：最大化指定的窗口。
            SW_MINIMIZE,//：最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
            SW_RESTORE,//：激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
            SW_SHOW,//：在窗口原来的位置以原来的尺寸激活和显示窗口。
            SW_SHOWDEFAULT,//：依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
            SW_SHOWMAXIMIZED,//：激活窗口并将其最大化。
            SW_SHOWMINIMIZED,//：激活窗口并将其最小化。
            SW_SHOWMINNOACTIVATE,//：窗口最小化，激活窗口仍然维持激活状态。
            SW_SHOWNA,//：以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
            SW_SHOWNOACTIVATE,//：以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
            SW_SHOWNOMAL,//：激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
        }

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region 控制鼠标移动

        //移动鼠标 
        public const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        #endregion

        #region 获取坐标钩子

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        //安装钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸载钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //调用下一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

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
        public static extern IntPtr GetWindowDC(
        IntPtr hwnd);
        #endregion

    }

}