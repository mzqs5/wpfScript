using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 关机
    /// </summary>
    public class gjTask : TaskBase
    {
        public gjTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            Bitmap capture = Bg.Capture(handle);
            Bitmap temp=BitmapHelper.ConvertToFormat(capture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            capture.Dispose();
            BitmapHelper.ColorReplace(temp,BitmapHelper.colorHx16toRGB("F0F0F0"));
            temp.Save($"C:\\clx\\source{new Random().Next(100, 200)}.bmp");
            //Bg.FindPicFast(handle, Resource1._5, new XRECT() { Left = 0, Top = 0, Right = 1300, Bottom = 750 }, FindDirection.LeftTopToRightDown, 0.9f, true);
            //var r = Bg.FindPicFast(handle, Resource1._5, new XRECT() { Left = 141, Top = 121, Right = 154, Bottom = 140 }, FindDirection.LeftTopToRightDown, 0.7f);
            //if (!r.IsEmpty)
            //{
            //    Bg.SetWindowText(handle, r.ToString());
            //}
            temp.Dispose();
            Thread.Sleep(10000);
            //Bg.ExitWindowsEx();
        }
    }
}
