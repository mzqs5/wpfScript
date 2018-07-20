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
    /// 悬赏任务
    /// </summary>
    public class xsrwTask : TaskBase
    {
        public xsrwTask(IntPtr handle) : base(handle)
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

            while (true)
            {
                OpenMall(Resource1.活动);
                Bg.LeftMouseClick(handle, new Point() { X = 930, Y = 55 });
                Sleep(1000);
                var capture = Bg.Capture(handle);
                var r = Bg.FindPicEx(handle, capture, Resource1.前往悬赏, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                if (r.IsEmpty)
                {
                    StartRob();
                }
                if (count >= 10)
                {
                    Bg.SetWindowText(handle, "悬赏任务次数已达上限");
                    Sleep(3000);
                    break;
                }
                StartMake();
            }
        }
        /// <summary>
        /// 开始做悬赏任务
        /// </summary>

        private void StartMake()
        {
            //关闭悬赏界面
            Bg.LeftMouseClick(handle, new Point() { X = 1156, Y = 70 });
            Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 1261, Y = 51 });
            Sleep(1000);
            //开始做悬赏任务
            var copy = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetTypes().Where(o => o.Name == taskName).FirstOrDefault(), handle) as CopyBase;
            copy.Start();
            copy = null;
        }

        /// <summary>
        /// 开始抢悬赏
        /// </summary>
        private void StartRob()
        {
            //开始抢悬赏任务
            Bg.SetWindowText(handle,"开始抢领悬赏任务...");
            while (true)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 170, Y = 600 });
                Sleep(300);
                var capture = Bg.Capture(handle);
                isok = 0;
                foreach (var item in list)
                {
                    var r = Bg.FindPicEx(handle, capture, item.bitmap, new XRECT() { Left = 180, Top = 250, Right = 340, Bottom = 400 }, 0.95f);
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, r);
                        Sleep(200);
                        Bg.LeftMouseClick(handle, new Point() { X = 1030, Y = 600 });
                        Sleep(200);
                        Bg.LeftMouseClick(handle, new Point() { X = 880, Y = 520 });
                        Sleep(300);
                        //检测是否抢领成功
                        r = Bg.FindPic(handle, Resource1.前往悬赏, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                        //r = Bg.FindPic(handle, Resource1.悬赏_领取任务, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 }, 0.95f);
                        if (!r.IsEmpty)
                        {
                            taskName = item.taskName;
                            Bg.SetWindowText(handle, "领取悬赏任务成功，开始前往悬赏");
                            isok = 1;
                            count++;
                            break;
                        }
                        else
                        {
                            //两种情况 一种没抢到 继续抢 一种悬赏任务次数已上限 退出悬赏任务
                            if (count == 10)
                            {
                                count++;
                                break;
                            }

                        }
                    }
                }
                capture.Dispose();
                if (isok == 1 || isok == 2)
                    break;

                Sleep(200);
            }
        }
        public class xsTask
        {
            public Bitmap bitmap { get; set; }

            public string taskName { get; set; }
        }
    }
}
