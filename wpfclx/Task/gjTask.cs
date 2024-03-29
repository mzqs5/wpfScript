﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 关机
    /// </summary>
    public class gjTask : TaskBase
    {
        public gjTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            IntPtr hscrdc = WinApi.GetWindowDC(handle);
            WinApi.RECT eCT = new WinApi.RECT();
            WinApi.GetWindowRect(handle, ref eCT);
            WinApi.ReleaseDC(handle, hscrdc);//删除用过的DC
            int width = eCT.Right - eCT.Left;                      
            int height = eCT.Bottom - eCT.Top;
            Bg.SetWindowText(handle, width.ToString() + "," + height.ToString());

            //var r = Bg.FindPic(handle, Resource1.c60, new XRECT() { Left = 1057, Top = 415, Right = 1098, Bottom = 438 },0.95f, FindDirection.LeftTopToRightDown,true);
            //if (!r.IsEmpty)
            //{
            //    Bg.SetWindowText(handle, r.ToString());

            //}

            //List<xsTask>  list = new List<xsTask>();

            //if (model.xsselhw)
            //{
            //    list.Add(new xsTask() { bitmap = Resource1.悬赏_十二连环坞, taskName = "selhwCopy" });
            //}
            //if (model.xsxjz)
            //{
            //    list.Add(new xsTask() { bitmap = Resource1.悬赏_薛家庄, taskName = "xjzCopy" });
            //}
            //if (model.xsmysj)
            //{
            //    list.Add(new xsTask() { bitmap = Resource1.悬赏_麻衣圣教, taskName = "mysjCopy" });
            //}

            //var capture = Bg.Capture(handle);
            //foreach (var item in list)
            //{
            //    var r = Bg.FindPicEx(handle, capture, item.bitmap, new XRECT() { Left = 200, Top = 250, Right = 340, Bottom = 390 }, 0.85f,FindDirection.LeftTopToRightDown,true);
            //    if (!r.IsEmpty) {
            //        Bg.SetWindowText(handle,r.ToString());
            //    }

            //}
            Thread.Sleep(10000);
            //Bg.ExitWindowsEx();
        }

        public class xsTask
        {
            public Bitmap bitmap { get; set; }

            public string taskName { get; set; }
        }
    }
}
