using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace wpfclx
{
    /// <summary>
    /// 调用windows api 封装基本后台操作方法
    /// </summary>
    internal class bg
    {
        /// <summary>
        /// 鼠标左键单击
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void LeftMouseClick(IntPtr handle, Point r)
        {
            r.X += new Random().Next(-2, 2);
            r.Y += new Random().Next(-2, 2);
            //先移动鼠标到指定位置

            WinApi.PostMessage(handle, (uint)MsgType.WM_NCHITTEST, 0, r.X + (r.Y << 16));

            Thread.Sleep(new Random().Next(10, 20));
            //按下鼠标左键
            WinApi.PostMessage(handle, (uint)MsgType.WM_LBUTTONDOWN, 0, r.X + (r.Y << 16));

            Thread.Sleep(new Random().Next(10, 20));
            //松开鼠标左键
            WinApi.PostMessage(handle, (uint)MsgType.WM_LBUTTONUP, 0, r.X + (r.Y << 16));
            Thread.Sleep(new Random().Next(10, 20));
        }

        /// <summary>
        /// 设置窗口标题
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static int SetWindowText(IntPtr handle, string text)
        {
            return WinApi.SetWindowText(handle, text);
        }

        /// <summary>
        /// 图像识别
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void Ocr(IntPtr handle, XRECT r)
        {
            var soure = Capture(handle, r);
        }

        /// <summary>
        /// 找图
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="temp"></param>
        /// <param name="r"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        internal static Point FindPic(IntPtr handle, Bitmap temp, XRECT r, bool debug = false)
        {
            var soure = Capture(handle, r);
            //soure.Save($"C:\\clx\\click{new Random().Next(100, 200)}.bmp");
            var tempnew = aforge.ConvertToFormat(temp, PixelFormat.Format24bppRgb);
            var rect = aforge.ProcessImage(soure, tempnew, debug);
            Point p = new Point();
            if (!rect.IsEmpty)
            {
                p.X = r.Left + rect.Left;
                p.Y = r.Top + rect.Top;
            }
            return p;
        }

        /// <summary>
        /// 捕获当前窗体坐标区域图像
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Bitmap Capture(IntPtr hWnd, XRECT r)
        {
            IntPtr hscrdc = WinApi.GetWindowDC(hWnd);
            WinApi.RECT eCT = new WinApi.RECT();
            WinApi.GetWindowRect(hWnd, ref eCT);
            IntPtr hbitmap = WinApi.CreateCompatibleBitmap(hscrdc, eCT.Right - eCT.Left, eCT.Bottom - eCT.Top);
            IntPtr hmemdc = WinApi.CreateCompatibleDC(hscrdc);
            WinApi.SelectObject(hmemdc, hbitmap);
            WinApi.PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
            WinApi.DeleteDC(hscrdc);//删除用过的对象 
            WinApi.DeleteDC(hmemdc);//删除用过的对象 
            return aforge.ConvertToFormat(bmp, PixelFormat.Format24bppRgb, r);
        }

    }
}
