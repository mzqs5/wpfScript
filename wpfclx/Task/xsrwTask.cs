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

        private int isok { get; set; }
        private string taskName { get; set; }
        public override void Start(TaskModel model)
        {
            while (true)
            {
                OpenMall(Resource1.活动);
                Bg.LeftMouseClick(handle, new Point() { X = 930, Y = 55 });
                Sleep(1000);
                var r = Bg.FindPic(handle, Resource1.悬赏_领取任务, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 });
                if (r.IsEmpty)
                {
                    StartRob();
                }
                if (isok == 2)
                {
                    Bg.SetWindowText(handle,"悬赏任务次数已达上限");
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
            while (true)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 170, Y = 600 });
                Sleep(500);
                var capture = Bg.Capture(handle);
                List<xsTask> list = new List<xsTask>();
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_奕中幻境, taskName = "yzhjCopy" });
                list.Add(new xsTask() { bitmap = Resource1.悬赏_选中_麻衣圣教, taskName = "mysjCopy" });
                isok = 0;
                foreach (var item in list)
                {
                    var r = Bg.FindPicEx(handle, capture, item.bitmap, new XRECT() { Left = 140, Top = 250, Right = 330, Bottom = 420 });
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, r);
                        Sleep(500);
                        Bg.LeftMouseClick(handle, new Point() { X = 1030, Y = 600 });
                        Sleep(500);
                        Bg.LeftMouseClick(handle, new Point() { X = 880, Y = 520 });
                        Sleep(500);
                        //检测是否抢领成功
                        r = Bg.FindPic(handle, Resource1.悬赏_领取任务, new XRECT() { Left = 960, Top = 580, Right = 1048, Bottom = 620 });
                        if (r.IsEmpty)
                        {
                            taskName = item.taskName;
                            Bg.SetWindowText(handle, "领取悬赏任务成功，开始前往悬赏");
                            isok = 1;
                            break;
                        }
                        else
                        {
                            //两种情况 一种没抢到 继续抢 一种悬赏任务次数已上限 退出悬赏任务

                        }
                    }
                }
                capture.Dispose();
                if (isok == 1 || isok == 2)
                    break;

                Sleep(500);
            }
        }
        public class xsTask
        {
            public Bitmap bitmap { get; set; }

            public string taskName { get; set; }
        }
    }
}
