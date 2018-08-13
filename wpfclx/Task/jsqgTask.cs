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
    /// 集市抢购
    /// </summary>
    public class jsqgTask : TaskBase
    {
        public jsqgTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            if (OpenMall(Resource1.珍宝阁))
            {
                Bg.LeftMouseClick(handle, new Point() { X = 1164, Y = 375 });
                Thread.Sleep(1000);
                Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 350 });
                Thread.Sleep(1000);
                Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 260 });
                Thread.Sleep(1000);
                Bg.SetWindowText(handle, "成功打开抢购界面，开始监控...");
                while (true)
                {
                    Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 260 });
                    Thread.Sleep(100);
                    Bg.SetWindowText(handle, "检测到关注物品上架，开始抢购...");
                    Bg.LeftMouseClick(handle, new Point() { X = 543, Y = 271 });
                    for (int i = 1; i < model.qgCount; i++)
                    {
                        Bg.LeftMouseClick(handle, new Point() { X = 794, Y = 385 });
                    }
                    Bg.LeftMouseClick(handle, new Point() { X = 659, Y = 526 });
                    Bg.LeftMouseClick(handle, new Point() { X = 885, Y = 528 });
                    Thread.Sleep(50);
                }
            }
        }

    }
}
