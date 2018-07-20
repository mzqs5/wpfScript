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
    /// 万里听风
    /// </summary>
    public class wltfTask : TaskBase
    {
        public wltfTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            ConvenientTeam();//打开便捷组队
            Bg.LeftMouseClick(handle, new Point() { X = 257, Y = 259 });
            Sleep(500);
            IsGetInto();//自动匹配
            Sleep(10000);
            openTask();
            var p = Bg.FindPic(handle, Resource1.任务_万里听风, new XRECT() { Left = 410, Top = 130, Right = 500, Bottom = 170 });
            if (p.IsEmpty)
            {
                closeTask();
                Start(model);
                return;
            }
            Bg.SetWindowText(handle, "万里听风正在进行中...");
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.任务_万里听风, new XRECT() { Left = 410, Top = 130, Right = 500, Bottom = 170 });
                if (r.IsEmpty)
                {
                    Bg.SetWindowText(handle, "万里听风10环已完成，正在前往抓捕贼王...");
                    break;
                }
                Sleep(5000);
            }
            closeTask();
            Sleep(120000);
            Bg.SetWindowText(handle, "万里听风已完成");
        }

    }
}
