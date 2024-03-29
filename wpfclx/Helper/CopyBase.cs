﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpfclx.Helper
{
    public abstract class CopyBase
    {
        public CopyBase(IntPtr handle)
        {
            this.handle = handle;
        }
        protected IntPtr handle { get; private set; }
        protected abstract void OrganizeTeam();
        protected virtual void StartTestingCopy() {
            Thread.Sleep(30000);
            while (true)
            {
                var capture = Bg.Capture(handle);
                var r = Bg.FindPicEx(handle, capture, Resource1.副本退出, new XRECT() { Left = 1150, Top = 190, Right = 1180, Bottom = 220 }, 0.85f);
                if (!r.IsEmpty)
                    break;
                capture.Dispose();
                Thread.Sleep(1000);
            }
            MonitorUse();
            Bg.SetWindowText(handle,"检测到副本退出");
            Thread.Sleep(10000);
        }

        /// <summary>
        /// 打开任务 江湖面板
        /// </summary>
        protected void openTask()
        {
            Bg.SetWindowText(handle, "开始打开任务江湖面板...");
            var r = Bg.FindPic(handle, Resource1.任务, new XRECT() { Left = 0, Top = 200, Right = 30, Bottom = 250 }, 0.95f);
            if (r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 220 });
                Thread.Sleep(1000);
            }
            Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 220 });
            Thread.Sleep(1000);
            r = Bg.FindPic(handle, Resource1.任务_江湖, new XRECT() { Left = 220, Top = 230, Right = 300, Bottom = 460 }, 0.95f);
            if (!r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 关闭任务面板
        /// </summary>
        protected void closeTask() {
            Bg.LeftMouseClick(handle, new Point() { X = 1155, Y = 67 });
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 监控使用物品 饮用 食用 学习
        /// </summary>
        protected virtual void MonitorUse()
        {
            var r = Bg.FindPic(handle, Resource1.物品, new XRECT() { Left = 1050, Top = 240, Right = 1100, Bottom = 280 }, 0.9f);
            if (!r.IsEmpty)
            {
                r.X -= 50;
                r.Y += 150;
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(1000);
                var rb = Bg.FindPic(handle, Resource1.确定, new XRECT() { Left = 800, Top = 480, Right = 980, Bottom = 600 }, 0.9f);
                if (!rb.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, rb);
                    Thread.Sleep(200);
                }
            }
        }


        public virtual void Start()
        {
            ConvenientTeam();//打开便捷组队面板
            OrganizeTeam();//打开对应的副本组队菜单
            IsGetInto();//组队监控
            CopyTesting();//检测是否进入副本
            Bg.SetWindowText(handle, "检测到已开始任务...");
            StartTestingCopy();
            GC.Collect();
        }

        protected virtual void CopyTesting()
        {
            var isok = false;
            for (int i = 0; i < 20; i++)
            {
                var r = Bg.FindPic(handle, Resource1.副本中, new XRECT() { Left = 1280, Top = 180, Right = 1310, Bottom = 220 });
                if (!r.IsEmpty)
                {
                    isok = true;
                    break;
                }
                Thread.Sleep(3000);
            }
            if (!isok)
            {
                Start();
                return;
            }
        }


        private bool IsGetInto()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 942, Y = 603 });
            Bg.SetWindowText(handle, "正在自动匹配...");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                var r = Bg.FindPic(handle, Resource1.跟随确认, new XRECT() { Left = 596, Top = 236, Right = 735, Bottom = 276 },0.9f);
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "匹配成功，前往跟随...");
                    Bg.LeftMouseClick(handle, new Point() { X = 875, Y = 526 });
                    Thread.Sleep(10000);
                    return true;
                }
            }
            Bg.SetWindowText(handle, "取消匹配...");
            Bg.LeftMouseClick(handle, new Point() { X = 942, Y = 603 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 1111, Y = 602 });
            Thread.Sleep(1000);
            return IsGetInto();
        }

        private void ConvenientTeam()
        {
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
                Thread.Sleep(1500);
                Bg.LeftMouseClick(handle, new Point() { X = 881, Y = 522 });
                Thread.Sleep(10000);
            }
            Bg.LeftMouseClick(handle, new Point() { X = 1117, Y = 604 });
            Thread.Sleep(1000);
        }

    }
}
