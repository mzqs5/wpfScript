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
            Sleep(840000);
            while (true)
            {
                var r = Bg.FindPic(handle, Resource1.物品, new XRECT() { Left = 1000, Top = 380, Right = 1200, Bottom = 500 });
                if (!r.IsEmpty)
                {
                    r.X -= 20;
                    r.Y -= 20;
                    Bg.LeftMouseClick(handle, r);
                    break;
                }
                Sleep(3000);
            }
            Bg.SetWindowText(handle, "万里听风10环已完成，正在前往抓捕贼王...");
            Sleep(120000);
            Bg.SetWindowText(handle, "万里听风已完成");
        }

    }
}
