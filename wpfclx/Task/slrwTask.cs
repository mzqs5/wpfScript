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
                Bg.SetWindowText(handle, "势力任务正在进行中...");
                openTask();
                while (true)
                {
                    var capture = Bg.Capture(handle);

                    //检测势力任务是否完成
                    var r = Bg.FindPicEx(handle, capture, Resource1.任务_帮派, new XRECT() { Left = 410, Top = 130, Right = 500, Bottom = 170 });
                    if (r.IsEmpty)
                        break;
                    Bg.LeftMouseClick(handle,new Point() { X= 975 ,Y= 597 });
                    r = Bg.FindPicEx(handle, capture, Resource1.对话中, new XRECT() { Left = 1266, Top = 603, Right = 1310, Bottom = 645 });
                    if (!r.IsEmpty)
                    {
                        Bg.LeftMouseClick(handle, new Point() { X = 1039, Y = 206 });
                        Sleep(1000);
                        r = Bg.FindPic(handle, Resource1.关系, new XRECT() { Left = 633, Top = 139, Right = 740, Bottom = 193 });
                        if (!r.IsEmpty)
                        {
                            Bg.SetWindowText(handle, "本次势力任务已结束");
                            Sleep(3000);
                            break;
                        }
                        else {

                        }
                    }
                    capture.Dispose();
                    Sleep(10000);
                }
                closeTask();
            }
        }
    }
}
