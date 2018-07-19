using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 江湖飘渺
    /// </summary>
    public class jhpmTask : TaskBase
    {
        public jhpmTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            OpenMall(Resource1.活动);
            Bg.LeftMouseClick(handle, new Point() { X = 117, Y = 697 });
            Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 436, Y = 283 });
            Sleep(500);
            Bg.SetWindowText(handle, "自动寻路中...");
            Sleep(15000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.前去打探, new XRECT() { Left = 1150, Top = 455, Right = 1221, Bottom = 490 }, 0.8f, FindDirection.LeftTopToRightDown);
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "前去打探...");
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 213, Y = 354 });
                    Sleep(1000);
                    Bg.MouseMove(handle, new Point() { X = 950, Y = 510 }, new Point() { X = 950, Y = 250 });
                    Sleep(3000);
                    r = Bg.FindPic(handle, Resource1.前往, new XRECT() { Left = 1037, Top = 173, Right = 1158, Bottom = 617 }, findType: FindDirection.RightDownToLeftTop);
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, r);
                        Bg.SetWindowText(handle, "自动寻路中...");
                    }
                    break;
                }
                Sleep(3000);
            }
            Sleep(15000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.对话车, new XRECT() { Left = 974, Top = 400, Right = 1028, Bottom = 550 });
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "交给我吧...");
                    Bg.LeftMouseClick(handle, r);
                    Sleep(20000);
                    break;
                }
                Sleep(3000);
            }
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.前往, new XRECT() { Left = 1037, Top = 173, Right = 1158, Bottom = 617 }, findType: FindDirection.RightDownToLeftTop);
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "第一次江湖飘渺已结束，等待退出副本...");
                    Sleep(10000);
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 137, Y = 257 });
                    Bg.SetWindowText(handle, "自动寻路中...");
                    break;
                }
                Sleep(5000);
            }
            Sleep(15000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.对话车, new XRECT() { Left = 974, Top = 400, Right = 1028, Bottom = 550 });
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "交给我吧...");
                    Bg.LeftMouseClick(handle, r);
                    Sleep(20000);
                    break;
                }
                Sleep(3000);
            }
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.关闭设置, new XRECT() { Left = 1124, Top = 45, Right = 1183, Bottom = 92 });
                if (!r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "江湖飘渺录任务已结束...");
                    Bg.LeftMouseClick(handle, r);
                    Sleep(3000);
                    break;
                }
                Sleep(3000);
            }
        }
    }
}
