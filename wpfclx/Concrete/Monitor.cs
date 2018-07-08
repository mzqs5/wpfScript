using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Abstract;

namespace wpfclx
{
    public class Monitor : IMonitor
    {
        public IntPtr handle { get; set; }

        public Monitor(IntPtr handle)
        {
            this.handle = handle;
        }


        /// <summary>
        /// 采集监控 是否有配方需要学习 生活装备是否不足
        /// </summary>
        public void StudyMonitor()
        {
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

        public void DrinkingMonitor()
        {
            StudyMonitor();
        }

        public void UseMonitor()
        {
            StudyMonitor();
        }

        public void EdibleMonitor()
        {
            StudyMonitor();
        }
    }
}
