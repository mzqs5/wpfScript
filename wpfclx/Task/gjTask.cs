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
            //var capture = Bg.Capture(handle);
            //var r = Bg.FindPicEx(handle, capture, Resource1.副本退出, new XRECT() { Left = 1150, Top = 190, Right = 1180, Bottom = 220 },0.8f);
            //Bg.SetWindowText(handle, r.ToString());

            var t = Bg.FindPic(handle, Resource1.任务, new XRECT() { Left = 0, Top = 200, Right = 30, Bottom = 250 }, 0.95f,FindDirection.LeftTopToRightDown,true);
                Bg.SetWindowText(handle,t.ToString());
            Thread.Sleep(10000);
            return;
            List<xsTask>  list = new List<xsTask>();
            if (model.xsjhxs)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_江湖行商, taskName = "jhxsCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_江湖行商, taskName = "jhxsCopy" });
            }
            if (model.xsjypy)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_聚义平冤, taskName = "jypyCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_聚义平冤, taskName = "jypyCopy" });
            }
            if (model.xsyzhj)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_奕中幻境, taskName = "yzhjCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_奕中幻境, taskName = "yzhjCopy" });
            }
            if (model.xsselhw)
            {
                //list.Add(new xsTask() { bitmap = Resource1.悬赏_十二连环坞, taskName = "selhwCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_十二连环坞, taskName = "selhwCopy" });
            }
            if (model.xsxjz)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_薛家庄, taskName = "xjzCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_薛家庄, taskName = "xjzCopy" });
            }
            if (model.xsmysj)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_麻衣圣教, taskName = "mysjCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_麻衣圣教, taskName = "mysjCopy" });
            }

            var capture = Bg.Capture(handle);
            foreach (var item in list)
            {
                var r = Bg.FindPicEx(handle, capture, item.bitmap, new XRECT() { Left = 180, Top = 250, Right = 340, Bottom = 400 }, 0.95f,FindDirection.LeftTopToRightDown,true);
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Bg.SetWindowText(handle, item.taskName);
                    //Sleep(200);
                    //Bg.LeftMouseClick(handle, new Point() { X = 1030, Y = 600 });
                    //Sleep(200);
                    //Bg.LeftMouseClick(handle, new Point() { X = 880, Y = 520 });
                    //Sleep(300);
                    //检测是否抢领成功
                    r = Bg.FindPic(handle, Resource1.前往悬赏, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                    //r = Bg.FindPic(handle, Resource1.悬赏_领取任务, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                    if (!r.IsEmpty)
                    {
                        //Bg.SetWindowText(handle, item.taskName+"1");
                        break;
                    }
                    else
                    {
                        //两种情况 一种没抢到 继续抢 一种悬赏任务次数已上限 退出悬赏任务

                    }
                }
            }
            capture.Dispose();
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
