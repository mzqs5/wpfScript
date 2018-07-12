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
    /// 课业
    /// </summary>
    public class kyTask : TaskBase
    {
        public kyTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            OpenMall(Resource1.活动);
            Bg.LeftMouseClick(handle, new Point() { X = 117, Y = 697 });
            Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 143, Y = 270 });
            Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 283, Y = 519 });
            Sleep(500);
            Bg.SetWindowText(handle, "开始前往课业...");
            Sleep(10000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.课业, new XRECT() { Left = 1113, Top = 453, Right = 1207, Bottom = 503 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    break;
                }
                Sleep(2000);
            }
            Difficulty();//选择课业
            Sleep(10000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.答对, new XRECT() { Left = 1113, Top = 453, Right = 1207, Bottom = 503 });
                if (!r.IsEmpty)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Bg.LeftMouseClick(handle, new Point() { X = 1147, Y = 213 });
                        Sleep(1000);
                    }
                }
                r = Bg.FindPic(handle, Resource1.购买, new XRECT() { Left = 620, Top = 509, Right = 700, Bottom = 550 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(500);
                    Bg.LeftMouseClick(handle, new Point() { X = 875, Y = 526 });
                    Sleep(1000);
                }
                r = Bg.FindPic(handle, Resource1.一键提交, new XRECT() { Left = 1050, Top = 412, Right = 1179, Bottom = 449 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 883, Y = 527 });
                    break;
                }
                Sleep(3000);
            }
            GC.Collect();
        }

        private void Difficulty()
        {
            var r = Bg.FindPic(handle, Resource1.课业困难, new XRECT() { Left = 309, Top = 444, Right = 1024, Bottom = 505 });
            if (!r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, r);
                Bg.SetWindowText(handle, "开始执行困难课业");
                Sleep(1000);
            }
            else
            {
                Bg.LeftMouseClick(handle, new Point() { X = 246, Y = 623 });
                Sleep(500);
                Bg.LeftMouseClick(handle, new Point() { X = 880, Y = 524 });
                Sleep(500);
                Difficulty();
            }
        }
    }
}
