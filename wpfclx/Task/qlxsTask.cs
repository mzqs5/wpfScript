using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wpfclx.Helper;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 抢领悬赏
    /// </summary>
    public class qlxsTask : TaskBase
    {
        public qlxsTask(IntPtr handle) : base(handle)
        {
        }
        private List<xsTask> list;
        private int isok { get; set; }
        private string taskName { get; set; }
        private int count { get; set; }
        public override void Start(TaskModel model)
        {
            list = new List<xsTask>();
            
            if (model.xsselhw)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_十二连环坞, taskName = "selhwCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_十二连环坞, taskName = "selhwCopy" });
            }
            if (model.xsxjz)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_薛家庄, taskName = "xjzCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_薛家庄, taskName = "xjzCopy" });
            }
            if (model.xsmysj)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_圣教, taskName = "mysjCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_圣教, taskName = "mysjCopy" });
            }
            if (model.xsmysz)
            {
                list.Add(new xsTask() { bitmap = Resource1.悬赏_明月山庄, taskName = "myszCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_明月山庄, taskName = "myszCopy" });
            }

            OpenMall(Resource1.活动);
            Bg.LeftMouseClick(handle, new Point() { X = 930, Y = 55 });
            Sleep(1000);
            //开始抢悬赏任务
            Bg.SetWindowText(handle, "开始抢领悬赏任务...");
            while (count < 3)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 170, Y = 600 });
                Sleep(100);
                var capture = Bg.Capture(handle);
                foreach (var item in list)
                {
                    var r = Bg.FindPicEx(handle, capture, item.bitmap, new XRECT() { Left = 200, Top = 250 + count * 40, Right = 340, Bottom = 390 - (2 - count) * 40 }, 0.95f);
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, r);
                        Sleep(80);
                        var capture2 = Bg.Capture(handle);
                        if (model.xsCount)
                        {
                            r = Bg.FindPicEx(handle, capture2, Resource1.c60, new XRECT() { Left = 1057, Top = 415, Right = 1098, Bottom = 438 }, 0.95f);
                            if (r.IsEmpty)
                            {
                                Bg.SetWindowText(handle, $"检测到箱子数量不符合");
                                break;
                            }
                        }
                        r = Bg.FindPicEx(handle, capture2, Resource1.前往悬赏, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                        if (!r.IsEmpty)
                        {
                            Bg.SetWindowText(handle, $"检测到已领取{count+1}次悬赏");
                            count++;
                            break;
                        }
                        capture2.Dispose();
                        Bg.LeftMouseClick(handle, new Point() { X = 1030, Y = 600 });
                        Sleep(30);
                        Bg.LeftMouseClick(handle, new Point() { X = 880, Y = 520 });
                        Sleep(100);
                        //检测是否抢领成功
                        r = Bg.FindPic(handle, Resource1.前往悬赏, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                        if (!r.IsEmpty)
                        {
                            taskName = item.taskName;
                            Bg.SetWindowText(handle, "领取悬赏任务成功");
                            count++;
                        }
                        else
                        {

                        }
                        break;
                    }
                }
                capture.Dispose();
            }

            Bg.SetWindowText(handle, "已抢领3次悬赏");
            Sleep(3000);
        }

        public class xsTask
        {
            public Bitmap bitmap { get; set; }

            public string taskName { get; set; }
        }
    }
}
