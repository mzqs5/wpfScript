﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Linq;
using System.Text;

namespace wpfclx
{
    /// <summary>
    /// 调用windows api 封装基本后台操作方法
    /// </summary>
    internal class Bg
    {
        private static int deviationX = 8;//窗口左偏移量
        private static int deviationY = 32;//窗口上偏移量
        //private static List<FontLibrary> fonts;

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

        internal static void KeyClick(IntPtr handle, KeyCode code)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_KEYDOWN, (int)code, 0);
            Thread.Sleep(new Random().Next(5, 10));
            WinApi.PostMessage(handle, (uint)MsgType.WM_KEYUP, (int)code, 0);
            Thread.Sleep(new Random().Next(5, 10));
        }

        internal static void KeyDown(IntPtr handle, KeyCode code)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_KEYDOWN, (int)code, 0);
            Thread.Sleep(new Random().Next(5, 10));
        }

        internal static void KeyUp(IntPtr handle, KeyCode code)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_KEYUP, (int)code, 0);
            Thread.Sleep(new Random().Next(5, 10));
        }

        /// <summary>
        /// 移动鼠标到指定位置
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void MouseMove(IntPtr handle, Point r, WPARAM wparam = WPARAM.MK_Normal)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_MOUSEMOVE, (int)wparam, r.X + (r.Y << 16));
            Thread.Sleep(new Random().Next(5, 10));
        }

        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void LeftMouseDown(IntPtr handle, Point r, WPARAM wparam = WPARAM.MK_LBUTTON)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_LBUTTONDOWN, (int)wparam, r.X + (r.Y << 16));
            Thread.Sleep(new Random().Next(5, 10));
        }

        /// <summary>
        /// 松开鼠标左键
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void LeftMouseUp(IntPtr handle, Point r, WPARAM wparam = WPARAM.MK_Normal)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_LBUTTONUP, (int)wparam, r.X + (r.Y << 16));
            Thread.Sleep(new Random().Next(5, 10));
        }

        /// <summary>
        /// 鼠标滚动
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="scroll">正数向上滚 负数向下滚</param>
        internal static void MouseWheel(IntPtr handle, Point r, int scroll = -1, WPARAM wparam = WPARAM.MK_Normal)
        {
            WinApi.PostMessage(handle, (uint)MsgType.WM_MOUSEWHEEL, (int)wparam + (scroll * 120 << 16), r.X + (r.Y << 16));
            Thread.Sleep(new Random().Next(5, 10));
        }

        /// <summary>
        /// 鼠标拖动
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void MouseMove(IntPtr handle, Point p1, Point p2)
        {

            //移动鼠标到指定位置
            MouseMove(handle, p1);
            //按下鼠标左键
            LeftMouseDown(handle, p1);

            var x = p2.X - p1.X;
            var y = p2.Y - p1.Y;
            var signX = 10;
            var signY = 10;
            var count = 1;
            if (x == 0)
            {
                signX = 0;
                count = y / 10;
            }
            else if (y == 0)
            {
                signY = 0;
                count = x / 10;
            }
            else if (x > y)
            {
                count = x / 10;
                signY = y / count;
            }
            else
            {
                count = y / 10;
                signX = x / count;
            }

            for (int i = 0; i < count; i++)
            {
                p1.X += signX;
                p1.Y += signY;
                //按住鼠标左键移动到指定位置
                MouseMove(handle, p1, WPARAM.MK_LBUTTON);
            }

            //松开鼠标左键
            LeftMouseUp(handle, p2);
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

        internal static int GetWindowText(IntPtr handle)
        {
            int length = WinApi.GetWindowTextLength(handle);
            StringBuilder windowName = new StringBuilder(length + 1);
            return WinApi.GetWindowText(handle, windowName, windowName.Capacity);
        }
        /// <summary>
        /// 图像识别
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="r"></param>
        internal static void Ocr(IntPtr handle, XRECT r)
        {
            var source = Capture(handle, r);
        }

        internal static void ExitWindowsEx()
        {
            WinApi.ExitWindowsEx(0x00000001, 0);
        }
        /// <summary>
        /// 区域找图
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="temp"></param>
        /// <param name="r"></param>
        /// <param name="debug"></param>
        /// <returns>返回第一个找到的坐标</returns>
        internal static Point FindPic(IntPtr handle, Bitmap temp, XRECT r, FindDirection findType = FindDirection.LeftTopToRightDown, float similarity = 0.9f, bool debug = false)
        {
            var source = Capture(handle, r);
            var tempnew = BitmapHelper.ConvertToFormat(temp, PixelFormat.Format24bppRgb);
            if (debug)
                source.Save($"C:\\clx\\source{new Random().Next(100, 200)}.bmp");
            var rect = AforgeHelper.ProcessImage(source, tempnew, findType, similarity);
            Point p = new Point();
            if (rect != null)
            {
                p.X = r.Left + rect[0].Rectangle.Left;
                p.Y = r.Top + rect[0].Rectangle.Top;
            }
            source.Dispose();
            tempnew.Dispose();
            return p;
        }

        /// <summary>
        /// 区域找图
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="temp"></param>
        /// <param name="r"></param>
        /// <param name="debug"></param>
        /// <returns>返回找到的坐标集合</returns>
        internal static List<Point> FindPicEx(IntPtr handle, Bitmap temp, XRECT r, FindDirection findType = FindDirection.LeftTopToRightDown, float similarity = 0.9f)
        {
            var source = Capture(handle, r);
            var tempnew = BitmapHelper.ConvertToFormat(temp, PixelFormat.Format24bppRgb);
            List<Point> list = new List<Point>();
            var rect = AforgeHelper.ProcessImage(source, tempnew, findType, similarity);
            if (rect != null)
                rect.ForEach(o =>
                {
                    Point p = new Point();
                    p.X = r.Left + o.Rectangle.Left;
                    p.Y = r.Top + o.Rectangle.Top;
                    list.Add(p);
                });
            source.Dispose();
            tempnew.Dispose();
            return list;
        }

        /// <summary>
        /// 区域找多图
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="temp"></param>
        /// <param name="r"></param>
        /// <param name="debug"></param>
        /// <returns>返回找到的坐标集合</returns>
        internal static List<Point> FindPicEx(IntPtr handle, List<Bitmap> temps, XRECT r, FindDirection findType = FindDirection.LeftTopToRightDown, float similarity = 0.9f)
        {
            var source = Capture(handle, r);

            List<Point> list = new List<Point>();
            foreach (var item in temps)
            {
                var tempnew = BitmapHelper.ConvertToFormat(item, PixelFormat.Format24bppRgb);
                var rect = AforgeHelper.ProcessImage(source, tempnew, findType, similarity);
                if (rect != null)
                    rect.ForEach(o =>
                    {
                        Point p = new Point();
                        p.X = r.Left + o.Rectangle.Left;
                        p.Y = r.Top + o.Rectangle.Top;
                        list.Add(p);
                    });
                tempnew.Dispose();
            }
            source.Dispose();

            return list;
        }

        /// <summary>
        /// 区域找透明文字
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="temp"></param>
        /// <param name="r"></param>
        /// <param name="debug"></param>
        /// <returns>返回第一个找到的坐标</returns>
        internal static Point FindPicFast(IntPtr handle, Bitmap temp, XRECT r, FindDirection findType = FindDirection.LeftTopToRightDown, float similarity = 0.9f)
        {
            var source = Capture(handle, r);
            var tempnew = BitmapHelper.ConvertToFormat(temp, PixelFormat.Format24bppRgb);
            source = AforgeHelper.GrayscaleThresholdBlobsFiltering(source, 90);
            tempnew = AforgeHelper.GrayscaleThresholdBlobsFiltering(tempnew, 90);
            //source.Save($"C:\\clx\\source{new Random().Next(100, 200)}.bmp");
            var rect = AforgeHelper.ProcessImage(source, tempnew, findType, similarity);
            Point p = new Point();
            if (rect != null)
            {
                p.X = r.Left + rect[0].Rectangle.Left;
                p.Y = r.Top + rect[0].Rectangle.Top;
            }
            source.Dispose();
            tempnew.Dispose();
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
            var source = Capture(handle, r);
            var temp = BitmapHelper.ByteStrToBitmap(str);
            var tempnew = BitmapHelper.ConvertToFormat(temp, PixelFormat.Format24bppRgb);
            BitmapHelper.ColorReplace(source, color);
            BitmapHelper.ColorReplace(tempnew, color);
            AforgeHelper.GrayscaleThresholdBlobsFiltering(source);
            AforgeHelper.GrayscaleThresholdBlobsFiltering(tempnew);
            var rect = AforgeHelper.ProcessImage(source, tempnew, findType, similarity);
            Point p = new Point();
            if (rect != null)
            {
                p.X = r.Left + rect[0].Rectangle.Left;
                p.Y = r.Top + rect[0].Rectangle.Top;
            }
            source.Dispose();
            tempnew.Dispose();
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
            //if (GetTimestamp() - times > 1000)
            //{
                IntPtr hscrdc = WinApi.GetWindowDC(hWnd);
                WinApi.RECT eCT = new WinApi.RECT();
                WinApi.GetWindowRect(hWnd, ref eCT);
                IntPtr hbitmap = WinApi.CreateCompatibleBitmap(hscrdc, eCT.Right - eCT.Left, eCT.Bottom - eCT.Top);
                IntPtr hmemdc = WinApi.CreateCompatibleDC(hscrdc);
                IntPtr ints = WinApi.SelectObject(hmemdc, hbitmap);
                WinApi.PrintWindow(hWnd, hmemdc, 0);
                Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
                WinApi.ReleaseDC(hWnd, hscrdc);//删除用过的DC
                WinApi.DeleteDC(hmemdc);//删除创建的DC
                WinApi.DeleteDC(hbitmap);//删除创建的DC
                WinApi.DeleteObject(ints);//删除用过的gdi对象
                Bitmap ect = BitmapHelper.ConvertToFormat(bmp, PixelFormat.Format24bppRgb, new XRECT() { Left = r.Left + deviationX, Right = r.Right + deviationX, Top = r.Top + deviationY, Bottom = r.Bottom + deviationY });
            //    capture = ect;
            //    times = GetTimestamp();
            //}
            return ect;
        }

        //private static double times;
        //private static Bitmap capture;
        //internal static double GetTimestamp()
        //{
        //    TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1);
        //    return ts.TotalMilliseconds;
        //}

        /// <summary>
        /// 获取系统错误信息描述
        /// </summary>
        /// <param name="errCode">系统错误码</param>
        /// <returns></returns>
        public static string GetSysErrMsg(int errCode)
        {
            IntPtr tempptr = IntPtr.Zero;
            string msg = null;
            WinApi.FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);
            return msg;
        }

    }
}
