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

        }

        protected override void OrganizeTeam()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 260, Y = 521 });
            Thread.Sleep(500);
        }
        private int count { get; set; }
        protected override void StartTestingCopy()
        {
            Thread.Sleep(10000);
            var i = 0;
            while (true)
            {
                var capture = Bg.Capture(handle);
                var r = Bg.FindPicEx(handle, capture, Resource1.副本结算, new XRECT() { Left = 1280, Top = 12, Right = 1320, Bottom = 66 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    i++;
                    Bg.SetWindowText(handle, $"检测到第{i}次副本结算");
                    Thread.Sleep(3000);
                    continue;
                }

                r = Bg.FindPicEx(handle, capture, Resource1.就近复活, new XRECT() { Left = 988, Top = 563, Right = 1220, Bottom = 680 });
                if (!r.IsEmpty)
                {
                    count++;
                    Bg.LeftMouseClick(handle, r);
                    if (count > 3)
                    {
                        Bg.SetWindowText(handle, $"复活次数过多，重新组队");
                        Thread.Sleep(1000);
                        Start();
                        return;
                    }
                    Bg.SetWindowText(handle, $"检测到死亡，就近复活");
                    Thread.Sleep(5000);
                    continue;
                }

                r = Bg.FindPicEx(handle, capture, Resource1.副本退出, new XRECT() { Left = 1150, Top = 190, Right = 1180, Bottom = 220 });
                if (!r.IsEmpty)
                {
                    if (i >= 2)
                    {
                        Bg.SetWindowText(handle, $"检测到副本退出，副本结算已满足条件，副本完成。");
                        break;
                    }
                    else
                    {
                        Bg.SetWindowText(handle, $"检测到副本退出，但未检测到副本结算，重新开始。");
                        Start();
                        return;
                    }
                }
                capture.Dispose();
                Thread.Sleep(1000);
            }
            Bg.SetWindowText(handle, "副本已结束，等待最后一次boss奖励自动结算...");
            Thread.Sleep(300000);
        }
    }
}
