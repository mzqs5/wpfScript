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
    /// 势力任务
    /// </summary>
    public class slrwTask : TaskBase
    {
        public slrwTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            for (int i = 0; i < 5; i++)
            {
                OpenMall(Resource1.活动);
                Bg.SetWindowText(handle, "开始势力任务...");
                Bg.LeftMouseClick(handle, new Point() { X = 310, Y = 697 });
                Sleep(1000);
                Bg.LeftMouseClick(handle, new Point() { X = 758, Y = 137 });
                Sleep(1000);
                Bg.LeftMouseClick(handle, new Point() { X = 114, Y = 608 });
                Sleep(1000);
                Bg.SetWindowText(handle, "自动寻路中...");
                Sleep(10000);
                Bg.SetWindowText(handle, "势力任务正在进行中...");
                while (true)
                {
                    var capture = Bg.Capture(handle);
                    var r = Bg.FindPicEx(handle, capture, Resource1.关系, new XRECT() { Left = 633, Top = 139, Right = 740, Bottom = 193 });
                    if (!r.IsEmpty)
                    {
                        Bg.SetWindowText(handle, "本次势力任务已结束");
                        Sleep(3000);
                        break;
                    }
                    r = Bg.FindPicEx(handle, capture, Resource1.答对, new XRECT() { Left = 1172, Top = 106, Right = 1272, Bottom = 159 });
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, new Point() { X = 1039, Y = 206 });
                        Sleep(1000);
                        r = Bg.FindPicEx(handle, capture, Resource1.关系, new XRECT() { Left = 633, Top = 139, Right = 740, Bottom = 193 });
                        if (!r.IsEmpty)
                        {
                            Bg.SetWindowText(handle, "本次势力任务已结束");
                            Sleep(3000);
                            break;
                        }
                        else
                        {
                            r = Bg.FindPic(handle, Resource1.任务, new XRECT() { Left = 0, Top = 180, Right = 30, Bottom = 250 }, 0.95f);
                            if (r.IsEmpty)
                            {
                                Bg.LeftMouseClick(handle, new Point() { X = 20, Y = 220 });
                                Sleep(1000);
                            }
                            Bg.LeftMouseClick(handle, new Point() { X = 148, Y = 192 });
                            Sleep(500);
                            Bg.LeftMouseClick(handle, new Point() { X = 142, Y = 235 });
                            Sleep(500);
                        }
                    }
                    capture.Dispose();
                    Sleep(500);
                }
            }
        }
    }
}
