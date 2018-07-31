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
                list.Add(new xsTask() { bitmap = Resource1.悬赏_麻衣圣教, taskName = "mysjCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_麻衣圣教, taskName = "mysjCopy" });
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
                    var r = Bg.FindPicEx(handle, capture, item.bitmap, new XRECT() { Left = 180, Top = 250 + count * 40, Right = 340, Bottom = 400 }, 0.95f);
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, r);
                        Sleep(100);
                        r = Bg.FindPic(handle, Resource1.前往悬赏, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                        if (!r.IsEmpty)
                        {
                            Bg.SetWindowText(handle, $"检测到已领取{count+1}次悬赏");
                            count++;
                            continue;
                        }
                        Bg.LeftMouseClick(handle, new Point() { X = 1030, Y = 600 });
                        Sleep(50);
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
