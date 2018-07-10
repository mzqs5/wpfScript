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
    /// 自动挖宝
    /// </summary>
    public class zdwbTask : TaskBase
    {
        public zdwbTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            Bg.SetWindowText(handle, "开始自动挖宝...");
            Bg.KeyClick(handle, KeyCode.B);
            Sleep(1000);
            for (int i = 0; i < 5; i++)
            {
                var r = Bg.FindPic(handle, Resource1.藏宝图, new XRECT() { });
                if (r.IsEmpty)
                {
                    Bg.MouseMove(handle, new Point() { }, new Point() { });
                }
                else
                {
                    Bg.LeftMouseClick(handle, r);
                    break;
                }
            }
            Sleep(10000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.藏宝图, new XRECT() { });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(10000);
                    while (true)
                    {
                        r = Bg.FindPic(handle, Resource1.藏宝图, new XRECT() { });
                        if (!r.IsEmpty)
                        {
                            Bg.LeftMouseClick(handle, r);
                            Sleep(30000);
                        }
                    }
                }

            }

        }


    }
}
