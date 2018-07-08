using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Abstract;

namespace wpfclx.Concrete
{
    public class Life : ILife
    {
        public Life(IntPtr handle)
        {
            this.handle = handle;
        }

        public IntPtr handle { get; set; }

        public int count { get; set; }

        public static List<Point> host = new List<Point>();

        /// <summary>
        /// 生活周围查找
        /// </summary>
        public void LifeAroundLookUp()
        {
            Bg.SetWindowText(handle, "开始查找矿物...");
            var r = Bg.FindPicFast(handle, Resource1.乌饭团, new XRECT() { Left = 100, Top = 80, Right = 1280, Bottom = 640 }, FindDirection.CoreToAround);
            if (!r.IsEmpty)
            {
                r.X -= 30;
                Bg.SetWindowText(handle, "找到生活物，开始移动...");
                Bg.LeftMouseClick(handle, r);
                Bg.SetWindowText(handle, r.ToString());
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// 换线
        /// </summary>
        public void LifeChangeLine()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 1310, Y = 39 });
            Thread.Sleep(500);
            var r = Bg.FindPicEx(handle, Resource1.线, new XRECT() { Left = 1000, Top = 60, Right = 1086, Bottom = 680 });
            if (r.Count > 0)
            {
                Bg.SetWindowText(handle, "开始换线...");
                var p = r.FindAll(o => !host.Select(h => h.Y).Contains(o.Y));
                if (p.Count == 0)
                {
                    host.Clear();
                    p = r;
                }
                Thread.Sleep(500);
                Bg.LeftMouseClick(handle, p[0]);
                Thread.Sleep(500);
                host.Add(p[0]);
            }
        }

        /// <summary>
        /// 生活采集
        /// </summary>

        public bool LifeCollect(int count)
        {
            var r = Bg.FindPicFast(handle, Resource1.采集, new XRECT() { Left = 900, Top = 360, Right = 1000, Bottom = 400 }, FindDirection.LeftTopToRightDown, 0.9f);
            if (!r.IsEmpty)
            {
                r.X -= 30;
                Bg.SetWindowText(handle, "找到矿物，开始挖矿...");
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(500);
                Bg.LeftMouseClick(handle, new Point() { X = 775, Y = 300 });
                Thread.Sleep(12000);
                count += 1;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 生活世界查找
        /// </summary>
        public void LifeWorldLookUp()
        {
            throw new NotImplementedException();
        }
    }
}
