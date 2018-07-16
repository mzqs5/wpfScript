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
                Sleep(20000);
                Bg.SetWindowText(handle, "势力任务正在进行中...");
                while (true)
                {
                    var r = Bg.FindPicFast(handle, Resource1.答对, new XRECT() { Left = 1172, Top = 106, Right = 1272, Bottom = 159 });
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, new Point() { X = 1039, Y = 206 });
                        Sleep(1000);
                        r = Bg.FindPic(handle, Resource1.关系, new XRECT() { Left = 633, Top = 139, Right = 740, Bottom = 193 });
                        if (!r.IsEmpty)
                        {
                            Bg.SetWindowText(handle,"本次势力任务已结束");
                            Sleep(3000);
                            break;
                        }
                        else
                            Bg.LeftMouseClick(handle, new Point() { X = 129, Y = 242 });
                    }
                    Sleep(500);

                    r = Bg.FindPic(handle, Resource1.关系, new XRECT() { Left = 633, Top = 139, Right = 740, Bottom = 193 });
                    if (!r.IsEmpty)
                    {
                        Bg.SetWindowText(handle, "本次势力任务已结束");
                        Sleep(3000);
                        break;
                    }
                    Sleep(1000);
                }
            }
        }
    }
}
