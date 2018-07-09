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
    public class Copy : ICopy
    {
        public Copy(IntPtr handle)
        {
            this.handle = handle;
        }



        public IntPtr handle { get; set; }

        public void AutoMatch()
        {
            throw new NotImplementedException();
        }

        public void Chronicle()
        {
            for (int i = 0; i < 3; i++)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 124, Y = 701 });
                Thread.Sleep(1000);
                Bg.LeftMouseClick(handle, new Point() { X = 751, Y = 553 });
                Thread.Sleep(1000);
                if (i < 2)
                {
                    Bg.MouseMove(handle, new Point() { X = 600, Y = 380 }, new Point() { X = 700, Y = 380 });
                    Thread.Sleep(1000);
                    Bg.MouseMove(handle, new Point() { X = 600, Y = 380 }, new Point() { X = 700, Y = 380 });
                    Thread.Sleep(1000);
                }
                if (i == 1) {
                    Bg.LeftMouseClick(handle,new Point() { X= 441 ,Y= 597 });
                }

                Thread.Sleep(50000);
            }
        }

        public void CreateTeam()
        {
            throw new NotImplementedException();
        }

        public void Dialogue()
        {
            throw new NotImplementedException();
        }

        public void GoTo()
        {
            throw new NotImplementedException();
        }

        public void lj()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 494, Y = 702 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 239, Y = 460 });
            Thread.Sleep(1000);
            Bg.SetWindowText(handle, "正在匹配");
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.确定, new XRECT() { Left = 850, Top = 500, Right = 900, Bottom = 550 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                }
                Thread.Sleep(500);
                r = Bg.FindPic(handle, Resource1.论剑匹配成功, new XRECT() { Left = 630, Top = 410, Right = 670, Bottom = 450 });
                if (!r.IsEmpty)
                    break;
                Thread.Sleep(1500);
            }
            Bg.SetWindowText(handle, "匹配成功");
            Thread.Sleep(25000);
            Bg.SetWindowText(handle, "开始退出");
            Bg.LeftMouseClick(handle, new Point() { X = 1296, Y = 201 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 882, Y = 528 });
            Thread.Sleep(20000);
            //Bg.SetWindowText(handle, "开始倒计时");
            //while (true)
            //{
            //    var r = Bg.FindPic(handle, Resource1.倒计时, new XRECT() { Left = 630, Top = 320, Right = 800, Bottom = 450 });
            //    if (!r.IsEmpty)
            //        break;
            //    Thread.Sleep(1000);
            //}
            //Bg.SetWindowText(handle, "开始战斗");
            ////开始战斗
            //Bg.KeyDown(handle, KeyCode.W);
            //Thread.Sleep(1000);
            //Bg.KeyClick(handle, KeyCode.VK_SPACE);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_SPACE);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_SPACE);
            //Thread.Sleep(200);
            //Bg.KeyUp(handle, KeyCode.W);
            //Thread.Sleep(200);
            //Bg.KeyClick(handle, KeyCode.Q);
            //Thread.Sleep(1000);
            //Bg.KeyClick(handle, KeyCode.Q);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_2);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_8);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_4);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_5);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_9);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_3);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_6);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_6);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_6);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_1);
            //Thread.Sleep(500);
            Bg.SetWindowText(handle, "开始检查论剑面板是否打开...");
            for (int i = 0; i < 5; i++)
            {
                var r = Bg.FindPic(handle, Resource1.关闭设置, new XRECT() { Left = 1127, Top = 48, Right = 1185, Bottom = 100 });
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "关闭论剑面板...");
                    Bg.LeftMouseClick(handle, r);
                    break;
                }
                Thread.Sleep(2000);
            }
            Bg.SetWindowText(handle, "开始下一次论剑");
        }

        public bool OpenMall()
        {
            var r = Bg.FindPic(handle, Resource1.活动, new XRECT() { Left = 0, Top = 0, Right = 560, Bottom = 80 });
            if (r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 16 });
                Thread.Sleep(1000);
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

        public void QuitTeam()
        {
            Bg.SetWindowText(handle, "检查是否在队伍中...");
            Bg.LeftMouseClick(handle, new Point() { X = 13, Y = 322 });
            Thread.Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.退出队伍, new XRECT() { Left = 1048, Top = 566, Right = 1203, Bottom = 631 });
            if (!r.IsEmpty)
            {
                Bg.SetWindowText(handle, "退出队伍...");
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(500);
            }
            Bg.SetWindowText(handle, "关闭队伍面板...");
            Bg.LeftMouseClick(handle, new Point() { X = 1154, Y = 66 });
            Thread.Sleep(500);
        }
    }
}
