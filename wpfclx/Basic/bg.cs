using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace wpfclx
{
    /// <summary>
    /// 调用windows api 封装基本后台操作方法
    /// </summary>
    internal class Bg
    {
        private static int deviationX = 8;//窗口左偏移量
        private static int deviationY = 32;//窗口上偏移量
        private static List<FontLibrary> fonts;

        //static Bg()
        //{
        //    fonts = new List<FontLibrary>();
        //    var strList = Resource1.楚留香字库.Split((char)10);
        //    for (int i = 0; i < strList.Length; i++)
        //    {
        //        FontLibrary library = new FontLibrary();
        //        library.ByteStr = strList[i].Split('$')[0];
        //        library.TextName = strList[i].Split('$')[1];
        //        library.Color = strList[i].Split('$')[2];
        //        fonts.Add(library);
        //    }
        //}

        /// <summary>
        /// 鼠标左键单击
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void LeftMouseClick(IntPtr handle, Point r)
        {
            r.X += new Random().Next(-2, 2);
            r.Y += new Random().Next(-2, 2);
            //移动鼠标到指定位置
            MouseMove(handle, r);

            //按下鼠标左键
            LeftMouseDown(handle, r);

            //松开鼠标左键
            LeftMouseUp(handle, r);

        }
        /// <summary>
        /// 移动鼠标到指定位置
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void MouseMove(IntPtr handle, Point r)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_NCHITTEST, 0, r.X + (r.Y << 16));
            Thread.Sleep(new Random().Next(10, 20));
        }

        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void LeftMouseDown(IntPtr handle, Point r)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_LBUTTONDOWN, 0, r.X + (r.Y << 16));
            Thread.Sleep(new Random().Next(10, 20));
        }

        /// <summary>
        /// 松开鼠标左键
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void LeftMouseUp(IntPtr handle, Point r)
        {
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
        /// 区域找图
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="temp"></param>
        /// <param name="r"></param>
        /// <param name="debug"></param>
        /// <returns>返回第一个找到的坐标</returns>
        internal static Point FindPic(IntPtr handle, Bitmap temp, XRECT r, FindDirection findType = FindDirection.LeftTopToRightDown, float similarity = 0.9f)
        {
            var soure = Capture(handle, r);
            var tempnew = BitmapHelper.ConvertToFormat(temp, PixelFormat.Format24bppRgb);
            var rect = AforgeHelper.ProcessImage(soure, tempnew, findType, similarity);
            Point p = new Point();
            if (!rect.IsEmpty)
            {
                p.X = r.Left + rect.Left;
                p.Y = r.Top + rect.Top;
            }
            return p;
        }

        /// <summary>
        /// 区域找字 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="str"></param>
        /// <param name="color"></param>
        /// <param name="r"></param>
        /// <param name="similarity"></param>
        /// <returns>返回第一个找到的坐标</returns>
        internal static Point FindStr(IntPtr handle, string str, string color, XRECT r, FindDirection findType = FindDirection.LeftTopToRightDown, float similarity = 0.9f)
        {
            var soure = Capture(handle, r);
            var temp = BitmapHelper.ByteStrToBitmap(str);
            var tempnew = BitmapHelper.ConvertToFormat(temp, PixelFormat.Format24bppRgb);
            BitmapHelper.ColorReplace(soure, color);
            BitmapHelper.ColorReplace(tempnew, color);
            AforgeHelper.GrayscaleThresholdBlobsFiltering(soure);
            AforgeHelper.GrayscaleThresholdBlobsFiltering(tempnew);
            var rect = AforgeHelper.ProcessImage(soure, tempnew, findType, similarity);
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
        internal static Bitmap Capture(IntPtr hWnd, XRECT r)
        {
            r.Left += deviationX;
            r.Right += deviationX;
            r.Top += deviationY;
            r.Bottom += deviationY;

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
            Bitmap ect = BitmapHelper.ConvertToFormat(bmp, PixelFormat.Format24bppRgb, r);
            return ect;
        }

    }
}
