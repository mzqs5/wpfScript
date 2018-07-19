using System;
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
                var r = Bg.FindPicEx(handle, capture, Resource1.副本退出, new XRECT() { Left = 1150, Top = 190, Right = 1180, Bottom = 220 }, 0.7f);
                if (!r.IsEmpty)
                    break;
                capture.Dispose();
                Thread.Sleep(1000);
            }
            Bg.SetWindowText(handle,"检测到副本退出");
            Thread.Sleep(10000);
        }


        public void Start()
        {
            ConvenientTeam();//打开便捷组队面板
            OrganizeTeam();//打开对应的副本组队菜单
            IsGetInto();//组队监控
            CopyTesting();//检测是否进入副本
            Bg.SetWindowText(handle, "检测到已进入副本...");
            StartTestingCopy();
            GC.Collect();
        }

        protected void CopyTesting()
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
                Bg.LeftMouseClick(handle, new Point() { X = 13, Y = 322 });
                Thread.Sleep(1000);
                Start();
                return;
            }
        }


        protected bool IsGetInto()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 942, Y = 603 });
            Bg.SetWindowText(handle, "正在自动匹配...");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                var r = Bg.FindPic(handle, Resource1.跟随确认, new XRECT() { Left = 596, Top = 236, Right = 735, Bottom = 276 });
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
            Bg.LeftMouseClick(handle, new Point() { X = 13, Y = 322 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 13, Y = 322 });
            Thread.Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.退出队伍, new XRECT() { Left = 1048, Top = 566, Right = 1203, Bottom = 631 });
            if (!r.IsEmpty)
            {
                Bg.SetWindowText(handle, "退出队伍...");
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(1500);
                Bg.LeftMouseClick(handle, new Point() { X = 881, Y = 522 });
                Thread.Sleep(1000);
            }
            Bg.LeftMouseClick(handle, new Point() { X = 1117, Y = 604 });
            Thread.Sleep(1000);
        }

    }
}
