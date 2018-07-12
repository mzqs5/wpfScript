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
    /// 换线采集
    /// </summary>
    public class hxcjTask : TaskBase
    {
        private static List<Point> host = new List<Point>();

        public hxcjTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            int j = 0;
            while (j < model.cjCount)
            {
                for (int i = 0; i < 5; i++)
                {
                    Bg.MouseMove(handle, new Point() { X = 600, Y = 480 }, new Point() { X = 800, Y = 480 });
                    Thread.Sleep(500);
                    var r = Bg.FindPicFast(handle, Resource1.采集, new XRECT() { Left = 900, Top = 360, Right = 1000, Bottom = 400 }, FindDirection.LeftTopToRightDown, 0.9f);
                    if (!r.IsEmpty)
                    {
                        r.X -= 30;
                        Bg.SetWindowText(handle, "找到采集物，开始采集...");
                        Bg.LeftMouseClick(handle, r);
                        Thread.Sleep(500);
                        Bg.LeftMouseClick(handle, new Point() { X = 775, Y = 300 });
                        Thread.Sleep(12000);
                        MonitorUse();
                        j++;
                        break;
                    }
                }
                Thread.Sleep(1000);
                //换线
                Bg.LeftMouseClick(handle, new Point() { X = 1310, Y = 39 });
                Thread.Sleep(500);
                var b = Bg.FindPicEx(handle, Resource1.线, new XRECT() { Left = 1000, Top = 60, Right = 1086, Bottom = 680 });
                if (b.Count > 0)
                {
                    Bg.SetWindowText(handle, "开始换线...");
                    var p = b.FindAll(o => !host.Select(h => h.Y).Contains(o.Y));
                    if (p.Count == 0)
                    {
                        host.Clear();
                        p = b;
                    }
                    Thread.Sleep(500);
                    Bg.LeftMouseClick(handle, p[0]);
                    host.Add(p[0]);
                    Thread.Sleep(500);
                }
                GC.Collect();
            }
        }
        
    }
}
