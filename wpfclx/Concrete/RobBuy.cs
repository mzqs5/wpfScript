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
    public class RobBuy : IRobBuy
    {
        public RobBuy(IntPtr handle)
        {
            this.handle = handle;
        }

        public IntPtr handle { get; set; }

        public bool BalanceLack()
        {
            var r = Bg.FindPic(handle, Resource1.余额不足, new XRECT() { Left = 400, Top = 500, Right = 500, Bottom = 560 });
            if (!r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(100);
                return true;
            }
            return false;
        }

        public void Buy(int Count)
        {
            Bg.LeftMouseClick(handle, new Point() { X = 543, Y = 271 });
            Thread.Sleep(100);
            this.BuyPlusNumber(Count);
            Bg.LeftMouseClick(handle, new Point() { X = 659, Y = 526 });
            Thread.Sleep(100);
            Bg.LeftMouseClick(handle, new Point() { X = 885, Y = 528 });
            Thread.Sleep(100);
        }

        public void BuyPlusNumber(int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 794, Y = 385 });
                Thread.Sleep(10);
            }
        }

        public void MarketRobBuy(int Count)
        {
            Bg.LeftMouseClick(handle, new Point() { X = 1164, Y = 375 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 350 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 260 });
            Thread.Sleep(1000);
            int i = 0;
            Bg.SetWindowText(handle, "成功打开抢购界面，开始监控...");
            while (i < Count)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 260 });
                Thread.Sleep(200);
                var r = Bg.FindPic(handle, Resource1.没有在售的珍品, new XRECT() { Left = 651, Top = 316, Right = 756, Bottom = 439 });
                if (r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "检测到关注物品上架，开始抢购...");
                    this.Buy(Count);
                    if (this.BalanceLack())
                    {
                        Bg.SetWindowText(handle, "余额不足，已停止抢购");
                        break;
                    }
                }
            }
        }

        public bool OpenMall()
        {
            var r = Bg.FindPic(handle, Resource1.珍宝阁, new XRECT() { Left = 0, Top = 0, Right = 560, Bottom = 80 });
            if (r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 16 });
                Thread.Sleep(200);
                return OpenMall();
            }
            else
            {
                r.X += 5;
                r.Y += 5;
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(1000);
            }
            return true;
        }

        public void StallRobBuy(int Count)
        {
            Bg.LeftMouseClick(handle, new Point() { X = 1152, Y = 276 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 350 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 260 });
            Thread.Sleep(1000);
            int i = 0;
            Bg.SetWindowText(handle, "成功打开抢购界面，开始监控...");
            while (i < Count)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 235, Y = 260 });
                Thread.Sleep(200);
                var r = Bg.FindPic(handle, Resource1.在售, new XRECT() { Left = 450, Top = 200, Right = 950, Bottom = 580 }, FindDirection.LeftTopToRightDown, 0.95f);
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "检测到关注物品上架，开始抢购...");
                    Bg.LeftMouseClick(handle, r);
                    Thread.Sleep(100);
                    this.Buy(Count);
                }
            }
        }
    }
}
