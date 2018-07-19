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
            //Bitmap capture = Bg.Capture(handle);
            //Bitmap temp=BitmapHelper.ConvertToFormat(capture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //capture.Dispose();
            //BitmapHelper.ColorReplace(temp,BitmapHelper.colorHx16toRGB("F0F0F0"));
            //temp.Save($"C:\\clx\\source{new Random().Next(100, 200)}.bmp");
            var capture = Bg.Capture(handle);
            var r = Bg.FindPicEx(handle, capture, Resource1.副本退出, new XRECT() { Left = 1150, Top = 190, Right = 1180, Bottom = 220 },0.8f);
            Bg.SetWindowText(handle, r.ToString());


            Thread.Sleep(10000);
            //Bg.ExitWindowsEx();
        }
        public class xsTask
        {
            public Bitmap bitmap { get; set; }

            public string taskName { get; set; }
        }
    }
}
