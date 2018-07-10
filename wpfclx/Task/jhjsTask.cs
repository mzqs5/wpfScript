using System;
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
    /// 江湖纪事
    /// </summary>
    public class jhjsTask : TaskBase
    {
        public jhjsTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
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
                if (i == 1)
                {
                    Bg.LeftMouseClick(handle, new Point() { X = 441, Y = 597 });
                }

                Thread.Sleep(50000);
            }
        }
    }
}
