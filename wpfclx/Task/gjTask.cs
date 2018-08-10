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
            List<xsTask>  list = new List<xsTask>();

            if (model.xsselhw)
            {
                list.Add(new xsTask() { bitmap = AforgeHelper.GrayscaleThresholdBlobsFiltering(BitmapHelper.ConvertToFormat(Resource1.悬赏_十二连环坞, System.Drawing.Imaging.PixelFormat.Format24bppRgb), 100), taskName = "selhwCopy" });
            }
            if (model.xsxjz)
            {
                list.Add(new xsTask() { bitmap = AforgeHelper.GrayscaleThresholdBlobsFiltering(BitmapHelper.ConvertToFormat(Resource1.悬赏_薛家庄, System.Drawing.Imaging.PixelFormat.Format24bppRgb), 100), taskName = "xjzCopy" });
            }
            if (model.xsmysj)
            {
                list.Add(new xsTask() { bitmap = AforgeHelper.GrayscaleThresholdBlobsFiltering(BitmapHelper.ConvertToFormat(Resource1.悬赏_麻衣圣教, System.Drawing.Imaging.PixelFormat.Format24bppRgb), 100), taskName = "mysjCopy" });
            }

            AforgeHelper.GrayscaleThresholdBlobsFiltering(BitmapHelper.ConvertToFormat(Resource1.悬赏_麻衣圣教, System.Drawing.Imaging.PixelFormat.Format24bppRgb), 100).Save($"C:\\clx\\test{ new Random().Next(100, 200)}.bmp");
            var capture = Bg.Capture(handle);
            foreach (var item in list)
            {
                var r = Bg.FindPicFastEx(handle, capture, item.bitmap, new XRECT() { Left = 200, Top = 295, Right = 340, Bottom = 340 }, 0.85f,FindDirection.LeftTopToRightDown,true);
                if (!r.IsEmpty) {
                    Bg.SetWindowText(handle,r.ToString());
                }

            }
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
