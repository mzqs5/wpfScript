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
    /// 佳肴荟萃
    /// </summary>
    public class jyhcTask : TaskBase
    {
        public jyhcTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i != 2)
                {
                    OpenMall(Resource1.活动);
                    Bg.SetWindowText(handle, "开始前往佳肴荟萃...");
                    Bg.LeftMouseClick(handle, new Point() { X = 668, Y = 700 });
                    Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 138, Y = 286 });
                    Bg.SetWindowText(handle, "自动寻路中...");
                    Sleep(20000);
                    while (true)
                    {
                        var p = Bg.FindPic(handle, Resource1.单人任务, new XRECT() { Left = 980, Top = 450, Right = 1024, Bottom = 500 });
                        if (!p.IsEmpty)
                        {
                            Bg.SetWindowText(handle, "开始佳肴荟萃...");
                            Bg.LeftMouseClick(handle, p);
                            Sleep(1000);
                            break;
                        }
                        Sleep(3000);
                    }
                }
                var r = Bg.FindPic(handle, Resource1.购买, new XRECT() { Left = 620, Top = 509, Right = 700, Bottom = 550 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 875, Y = 526 });
                    Sleep(1000);
                }
                Sleep(1000);
                r = Bg.FindPic(handle, Resource1.一键提交, new XRECT() { Left = 1050, Top = 412, Right = 1179, Bottom = 449 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Bg.SetWindowText(handle, "已提交");
                    Sleep(2000);
                    Bg.LeftMouseClick(handle, new Point() { X = 1, Y = 1 });
                }
                Sleep(1000);
            }
        }
    }
}
