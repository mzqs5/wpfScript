﻿using System;
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
            Sleep(20000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.课业, new XRECT() { Left = 1113, Top = 453, Right = 1207, Bottom = 503 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 1149, Y = 474 });
                    Sleep(1000);
                    break;
                }
                Sleep(3000);
            }
            Difficulty();//选择课业
            Sleep(10000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.任务, new XRECT() { Left = 0, Top = 180, Right = 25, Bottom = 250 }, 0.95f);
                if (r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 220 });
                    Sleep(1000);
                }
                Bg.LeftMouseClick(handle, new Point() { X = 148, Y = 192 });
                Sleep(500);
                Bg.LeftMouseClick(handle, new Point() { X = 142, Y = 235 });
                Sleep(500);
                var capture = Bg.Capture(handle);
                r = Bg.FindPicEx(handle, capture, Resource1.答对, new XRECT() { Left = 1172, Top = 106, Right = 1272, Bottom = 159 });
                if (!r.IsEmpty)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Bg.LeftMouseClick(handle, new Point() { X = 1039, Y = 206 });
                        Sleep(1000);
                    }
                    continue;
                }
                r = Bg.FindPicEx(handle, capture, Resource1.铜币购买, new XRECT() { Left = 812, Top = 574, Right = 962, Bottom = 630 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(500);
                    Bg.LeftMouseClick(handle, r);
                    Sleep(500);
                    continue;
                }
                r = Bg.FindPicEx(handle, capture, Resource1.购买, new XRECT() { Left = 620, Top = 509, Right = 700, Bottom = 550 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(500);
                    Bg.LeftMouseClick(handle, new Point() { X = 875, Y = 526 });
                    Sleep(1000);
                    continue;
                }
                r = Bg.FindPic(handle, Resource1.单人任务进行中, new XRECT() { Left = 961, Top = 357, Right = 1043, Bottom = 570 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    continue;
                }
                r = Bg.FindPicEx(handle, capture, Resource1.一键提交, new XRECT() { Left = 1050, Top = 412, Right = 1179, Bottom = 449 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Bg.SetWindowText(handle, "课业已提交完成");
                    Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 883, Y = 527 });
                    Sleep(20000);
                    break;
                }
                else
                {
                    Bg.LeftMouseClick(handle, new Point() { X = 110, Y = 155 });
                }
                capture.Dispose();
                Sleep(5000);
            }
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
