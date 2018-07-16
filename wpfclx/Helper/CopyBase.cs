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
        protected int Count { get; set; }
        protected IntPtr handle { get; private set; }
        protected abstract void OrganizeTeam();
        protected virtual void StartCopy()
        {
            Bg.SetWindowText(handle, "检测到已进入副本...");
            Thread.Sleep(10000);
            int c = 0;
            while (Count > c)
            {
                var r = Bg.FindPic(handle, Resource1.副本结算, new XRECT() { Left = 1269, Top = 12, Right = 1319, Bottom = 66 });
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, $"已结算第{c + 1}个boss...");
                    Bg.LeftMouseClick(handle, r);
                    c++;
                }
                Thread.Sleep(5000);
            }
            Bg.SetWindowText(handle, "副本已结束等待退出...");
            //点击退出副本
            //Bg.LeftMouseClick(handle, new Point() { X = 1296, Y = 203 });
            //Thread.Sleep(500);
            ////点击确定
            //Bg.LeftMouseClick(handle, new Point() { X = 881, Y = 527 });
            //Thread.Sleep(500);
            //Bg.LeftMouseClick(handle, new Point() { X = 1296, Y = 203 });
            Thread.Sleep(300000);//挂起300秒等待结算结束
            Bg.SetWindowText(handle, "副本已结束...");
        }

        public void Start()
        {
            ConvenientTeam();//打开便捷组队面板
            OrganizeTeam();//打开对应的副本组队菜单
            IsGetInto();//组队监控
            CopyTesting();//检测是否进入副本
            StartCopy();//开始副本
        }

        protected void CopyTesting()
        {
            var isok = false;
            for (int i = 0; i < 10; i++)
            {
                var r = Bg.FindPic(handle, Resource1.副本中, new XRECT() { Left = 1290, Top = 191, Right = 1320, Bottom = 223 }, FindDirection.LeftTopToRightDown, 0.9f, true);
                if (!r.IsEmpty)
                {
                    isok = true;
                    break;
                }
                Thread.Sleep(5000);
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
            Thread.Sleep(500);
            for (int i = 0; i < 10; i++)
            {
                var r = Bg.FindPic(handle, Resource1.跟随确认, new XRECT() { Left = 596, Top = 236, Right = 735, Bottom = 276 });
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "匹配成功，前往跟随...");
                    Bg.LeftMouseClick(handle, new Point() { X = 875, Y = 526 });
                    Thread.Sleep(10000);
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

        private void ConvenientTeam()
        {
            Bg.SetWindowText(handle, "开始便捷组队...");
            Bg.LeftMouseClick(handle, new Point() { X = 13, Y = 322 });
            Thread.Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.退出队伍, new XRECT() { Left = 1048, Top = 566, Right = 1203, Bottom = 631 });
            if (!r.IsEmpty)
            {
                Bg.SetWindowText(handle, "退出队伍...");
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(500);
            }
            Bg.LeftMouseClick(handle, new Point() { X = 1117, Y = 604 });
            Thread.Sleep(500);
        }

    }
}
