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
        public TaskBase(IntPtr handle)
        {
            this.handle = handle;
        }
        public IntPtr handle { get; private set; }
        /// <summary>
        /// 所有任务必须实现此方法
        /// </summary>
        /// <param name="model"></param>
        public abstract void Start(TaskModel model);
        /// <summary>
        /// 挂起
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        public virtual bool OpenMall(Bitmap temp)
        {
            var r = Bg.FindPic(handle, temp, new XRECT() { Left = 0, Top = 0, Right = 560, Bottom = 80 }, 0.9f, FindDirection.LeftTopToRightDown);
            if (r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 16 });
                
                Sleep(3000);
                return OpenMall(temp);
            }
            else
            {
                r.X += 5;
                r.Y += 5;
                Bg.LeftMouseClick(handle, r);
                Sleep(1000);
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
        /// 体力回复
        /// </summary>
        public void PhysicalRecovery() {
            var r = Bg.FindPic(handle, Resource1.确定, new XRECT() { Left = 627, Top = 508, Right = 704, Bottom = 550 });
            if (!r.IsEmpty)
                Bg.LeftMouseClick(handle, r);
        }

        public void Dialogue()
        {
            var r = Bg.FindPic(handle, Resource1.对话中, new XRECT() { Left = 1264, Top = 605, Right = 1315, Bottom = 644 });
            if (!r.IsEmpty)
                Bg.LeftMouseClick(handle, new Point() { X = 500, Y = 500 });
        }

        /// <summary>
        /// 监控使用物品 饮用 食用 学习
        /// </summary>
        public virtual void MonitorUse()
        {
            var r = Bg.FindPic(handle, Resource1.物品, new XRECT() { Left = 900, Top = 350, Right = 1200, Bottom = 580 });
            if (!r.IsEmpty)
            {
                r.X -= 20;
                r.Y -= 20;
                Bg.LeftMouseClick(handle, r);
                Sleep(1000);
                var rb = Bg.FindPic(handle, Resource1.确定, new XRECT() { Left = 800, Top = 480, Right = 980, Bottom = 600 });
                if (!rb.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, rb);
                    Sleep(200);
                }
            }
        }
        /// <summary>
        /// 离开队伍
        /// </summary>
        public virtual void QuitTeam()
        {
            Bg.SetWindowText(handle, "检查是否在队伍中...");
            Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 320 });
            Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 320 });
            Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.退出队伍, new XRECT() { Left = 1048, Top = 566, Right = 1203, Bottom = 631 });
            if (!r.IsEmpty)
            {
                Bg.SetWindowText(handle, "退出队伍...");
                Bg.LeftMouseClick(handle, r);
                Sleep(1000);
                Bg.LeftMouseClick(handle, new Point() { X = 881, Y = 522 });
                Sleep(1000);
            }
            Bg.SetWindowText(handle, "关闭队伍面板...");
            Bg.LeftMouseClick(handle, new Point() { X = 1154, Y = 66 });
            Sleep(1000);
        }
        /// <summary>
        /// 打开便捷组队
        /// </summary>
        public virtual void ConvenientTeam() {
            Bg.SetWindowText(handle, "开始便捷组队...");
            Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 320 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 320 });
            Thread.Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.退出队伍, new XRECT() { Left = 1048, Top = 566, Right = 1203, Bottom = 631 });
            if (!r.IsEmpty)
            {
                Bg.SetWindowText(handle, "退出队伍...");
                Bg.LeftMouseClick(handle, r);
                Sleep(1500);
                Bg.LeftMouseClick(handle, new Point() { X = 881, Y = 522 });
                Sleep(1000);
            }
            Bg.LeftMouseClick(handle, new Point() { X = 1117, Y = 604 });
            Sleep(1000);
        }

        /// <summary>
        /// 自动匹配
        /// </summary>
        /// <returns></returns>
        protected bool IsGetInto()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 942, Y = 603 });
            Bg.SetWindowText(handle, "正在自动匹配...");
            Thread.Sleep(1000);
            for (int i = 0; i < 10; i++)
            {
                var r = Bg.FindPic(handle, Resource1.跟随确认, new XRECT() { Left = 596, Top = 236, Right = 735, Bottom = 276 });
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "匹配成功，前往跟随...");
                    Bg.LeftMouseClick(handle, new Point() { X = 875, Y = 526 });
                    Thread.Sleep(1000);
                    return true;
                }
                Thread.Sleep(1000);
            }
            Bg.SetWindowText(handle, "取消匹配...");
            Bg.LeftMouseClick(handle, new Point() { X = 942, Y = 603 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 1111, Y = 602 });
            Thread.Sleep(500);
            
            return IsGetInto();
        }

        /// <summary>
        /// 打开任务 江湖面板
        /// </summary>
        protected void openTask()
        {
            Bg.SetWindowText(handle, "开始打开任务江湖面板...");
            Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 220 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 220 });
            Thread.Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.任务_江湖, new XRECT() { Left = 220, Top = 230, Right = 300, Bottom = 460 },0.95f);
            if (!r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 关闭任务面板
        /// </summary>
        protected void closeTask()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 1155, Y = 67 });
            Thread.Sleep(1000);
        }

    }
}
