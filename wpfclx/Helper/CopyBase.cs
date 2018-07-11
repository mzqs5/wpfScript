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
        public int Count { get; set; }
        public IntPtr handle { get; private set; }
        public abstract void OrganizeTeam();

        public void Start()
        {
            ConvenientTeam();//打开便捷组队面板
            OrganizeTeam();//打开对应的副本组队菜单
            IsGetInto();//组队监控

            StartCopy();//开始副本
        }
        /// <summary>
        /// 开始副本
        /// </summary>
        protected virtual void StartCopy()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    var r = Bg.FindPic(handle, Resource1.剧情, new XRECT() { Left = 0, Top = 10, Right = 66, Bottom = 56 });
            //    if (!r.IsEmpty)
            //        break;
            //    Thread.Sleep(2000);
            //}
            for (int i = 0; i < 10; i++)
            {
                var r = Bg.FindPic(handle, Resource1.剧情, new XRECT() { Left = 0, Top = 10, Right = 66, Bottom = 56 });
                if (!r.IsEmpty)
                    break;
                Thread.Sleep(2000);
            }
            for (int i = 0; i < Count; i++)
            {
                var r = Bg.FindPic(handle, Resource1.副本结算, new XRECT() { Left = 1269, Top = 12, Right = 1319, Bottom = 66 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    break;
                }
            }
            //点击退出副本
            Bg.LeftMouseClick(handle,new Point() { X= 1296 ,Y= 203 });
            Thread.Sleep(500);
            //点击确定
            Bg.LeftMouseClick(handle, new Point() { X = 881, Y = 527 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 1296, Y = 203 });
            Thread.Sleep(300000);//挂起300秒等待结算结束
        }

        protected bool IsGetInto()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 942, Y = 603 });
            Thread.Sleep(500);
            for (int i = 0; i < 10; i++)
            {
                var r = Bg.FindPic(handle, Resource1.确定, new XRECT() { Left = 1048, Top = 566, Right = 1203, Bottom = 631 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Thread.Sleep(500);
                    return true;
                }
            }
            Bg.LeftMouseClick(handle, new Point() { X = 942, Y = 603 });
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
