using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpfclx.Helper
{
    public class jhxsCopy : CopyBase
    {
        /// <summary>
        /// 江湖行商
        /// </summary>
        /// <param name="handle"></param>
        public jhxsCopy(IntPtr handle) : base(handle)
        {

        }

        protected override void StartTestingCopy()
        {
            Thread.Sleep(30000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.任务_江湖行商, new XRECT() { Left = 150, Top = 320, Right = 350, Bottom = 380 });
                if (!r.IsEmpty)
                    break;
                Thread.Sleep(5000);
            }
            Bg.SetWindowText(handle, "检测到江湖行商任务已完成");
            //关闭任务面板
            closeTask();
            MonitorUse();
            Thread.Sleep(5000);
        }



        protected override void CopyTesting() {
            for (int i = 0; i < 20; i++)
            {
                var r = Bg.FindPic(handle, Resource1.挖宝, new XRECT() { Left = 850, Top = 550, Right = 950, Bottom = 650 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Thread.Sleep(1000);
                    //点击购买货单
                    Bg.LeftMouseClick(handle, new Point() { X = 667, Y = 512 });
                    Thread.Sleep(10000);
                    break;
                }
                Thread.Sleep(2000);
            }
            openTask();
            var p = Bg.FindPic(handle, Resource1.任务_江湖行商, new XRECT() { Left = 180, Top = 330, Right = 330, Bottom = 380 });
            if (p.IsEmpty)
            {
                //关闭任务面板
                closeTask();
                Start();
                return;
            }
        }


        protected override void OrganizeTeam()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 257, Y = 441 });
            Thread.Sleep(1000);
        }

    }
}
