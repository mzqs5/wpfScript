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
    /// 每日一卦
    /// </summary>
    public class mrygTask : TaskBase
    {
        public mrygTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            OpenMall(Resource1.活动);
            Bg.LeftMouseClick(handle, new Point() { X = 861, Y = 701 });
            Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 798, Y = 551 });
            Bg.SetWindowText(handle, "前往算命...");
            Sleep(10000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.算命卜卦, new XRECT() { Left = 1090, Top = 456, Right = 1221, Bottom = 497 });
                if (!r.IsEmpty)
                {
                    Bg.LeftMouseClick(handle, r);
                    Sleep(1000);
                    break;
                }
                Sleep(5000);
            }
            Bg.LeftMouseClick(handle, new Point() { X = 649, Y = 201 });
            Sleep(1000);
            Bg.MouseMove(handle, new Point() { X = 425, Y = 355 }, new Point() { X = 800, Y = 355 });
            Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 989, Y = 593 });
            Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 1136, Y = 457 });
            Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 880, Y = 524 });
            Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 1, Y = 1 });
            Bg.SetWindowText(handle, "算命结束...");
            Sleep(1000);
        }
    }
}
