using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpfclx.Helper
{
    public class selhwCopy : CopyBase
    {
        public selhwCopy(IntPtr handle) : base(handle)
        {
            Count = 2;
        }

        protected override void OrganizeTeam()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 260, Y = 521 });
            Thread.Sleep(500);
        }

        /// <summary>
        /// 开始副本
        /// </summary>
        protected override void StartCopy()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    var r = Bg.FindPic(handle, Resource1.剧情, new XRECT() { Left = 0, Top = 10, Right = 66, Bottom = 56 });
            //    if (!r.IsEmpty)
            //        break;
            //    Thread.Sleep(2000);
            //}
            var isok = false;
            for (int i = 0; i < 10; i++)
            {
                var r = Bg.FindPic(handle, Resource1.剧情, new XRECT() { Left = 0, Top = 10, Right = 66, Bottom = 56 });
                if (!r.IsEmpty)
                {
                    isok = true;
                    break;
                }
                r = Bg.FindPic(handle, Resource1.确, new XRECT() { Left = 986, Top = 574, Right = 1194, Bottom = 661 });
                if (!r.IsEmpty)
                {
                    isok = true;
                    break;
                }
                Thread.Sleep(2000);
            }
            if (isok)
            {
                Bg.SetWindowText(handle, "检测到副本剧情...");
                Thread.Sleep(10000);
                int c = 0;
                while (true)
                {
                    var r = Bg.FindPic(handle, Resource1.副本结算, new XRECT() { Left = 1269, Top = 12, Right = 1319, Bottom = 66 });
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, r);
                        c++;
                    }
                    if (c >= Count)
                        break;
                    Thread.Sleep(5000);
                }
                //点击退出副本
                Bg.LeftMouseClick(handle, new Point() { X = 1296, Y = 203 });
                Thread.Sleep(500);
                //点击确定
                Bg.LeftMouseClick(handle, new Point() { X = 881, Y = 527 });
                Thread.Sleep(500);
                Bg.LeftMouseClick(handle, new Point() { X = 1296, Y = 203 });
                Thread.Sleep(300000);//挂起300秒等待结算结束
            }
            else
            {
                Start();
            }
            return;
        }
    }
}
