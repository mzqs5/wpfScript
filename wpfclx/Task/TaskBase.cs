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
    public abstract class TaskBase
    {
        public TaskBase(IntPtr handle) {
            this.handle = handle;
        }
        public IntPtr handle { get; private set; }
        /// <summary>
        /// 所有任务必须实现此方法
        /// </summary>
        /// <param name="model"></param>
        public abstract void Start(TaskModel model);

        public virtual bool OpenMall(Bitmap temp)
        {
            var r = Bg.FindPic(handle, temp, new XRECT() { Left = 0, Top = 0, Right = 560, Bottom = 80 });
            if (r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 16 });
                Thread.Sleep(1000);
                return OpenMall(temp);
            }
            else
            {
                r.X += 5;
                r.Y += 5;
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(1000);
            }
            return true;
        }

        public virtual void Confirm()
        {
            var r = Bg.FindPic(handle, Resource1.确定, new XRECT() { Left = 850, Top = 500, Right = 900, Bottom = 550 });
            if (!r.IsEmpty)
                Bg.LeftMouseClick(handle, r);

        }
        /// <summary>
        /// 监控使用物品 饮用 食用 学习
        /// </summary>
        public virtual void MonitorUse() {
            var r = Bg.FindPic(handle, Resource1.物品, new XRECT() { Left = 1000, Top = 380, Right = 1200, Bottom = 500 });
            if (!r.IsEmpty)
            {
                r.X -= 20;
                r.Y -= 20;
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(500);
                var rb = Bg.FindPic(handle, Resource1.确定, new XRECT() { Left = 800, Top = 480, Right = 980, Bottom = 600 });
                if (!rb.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, rb);
                    Thread.Sleep(200);
                }
            }
        }

        public virtual void QuitTeam()
        {
            Bg.SetWindowText(handle, "检查是否在队伍中...");
            Bg.LeftMouseClick(handle, new Point() { X = 13, Y = 322 });
            Thread.Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.退出队伍, new XRECT() { Left = 1048, Top = 566, Right = 1203, Bottom = 631 });
            if (!r.IsEmpty)
            {
                Bg.SetWindowText(handle, "退出队伍...");
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(500);
            }
            Bg.SetWindowText(handle, "关闭队伍面板...");
            Bg.LeftMouseClick(handle, new Point() { X = 1154, Y = 66 });
            Thread.Sleep(500);
        }

    }
}
