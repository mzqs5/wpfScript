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
    /// 战场挂机
    /// </summary>
    public class zcgjTask : TaskBase
    {
        public zcgjTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            QuitTeam();
            for (int i = 0; i < model.zcCount; i++)
            {
                if (OpenMall(Resource1.活动))
                {
                    Bg.LeftMouseClick(handle, new Point() { X = 494, Y = 702 });
                    Thread.Sleep(1000);
                    Bg.LeftMouseClick(handle, new Point() { X = 447, Y = 291 });
                    Thread.Sleep(1000);
                    Bg.SetWindowText(handle, "正在匹配");
                    while (true)
                    {
                        Confirm();
                        Thread.Sleep(500);
                        var r = Bg.FindPic(handle, Resource1.论剑匹配成功, new XRECT() { Left = 630, Top = 410, Right = 670, Bottom = 450 });
                        if (!r.IsEmpty)
                            break;
                        Thread.Sleep(1500);
                    }
                    Bg.SetWindowText(handle, "匹配成功");
                    Thread.Sleep(20000);

                    while (true)
                    {
                        var r = Bg.FindPic(handle, Resource1.对话中, new XRECT() { Left = 40, Top = 25, Right = 115, Bottom = 100 });
                        if (!r.IsEmpty)
                            break;
                        Thread.Sleep(5000);
                    }

                    Bg.SetWindowText(handle, "正在结算...");
                    Thread.Sleep(20000);
                    
                    Bg.SetWindowText(handle, "开始检查战场面板是否打开...");
                    for (int j = 0; j < 5; j++)
                    {
                        var r = Bg.FindPic(handle, Resource1.关闭设置, new XRECT() { Left = 1127, Top = 48, Right = 1185, Bottom = 100 });
                        if (!r.IsEmpty)
                        {
                            Bg.SetWindowText(handle, "关闭战场面板...");
                            Bg.LeftMouseClick(handle, r);
                            break;
                        }
                        Thread.Sleep(2000);
                    }
                    Bg.SetWindowText(handle, "开始下一次战场");
                }
            }
        }

        private void zd() {
            //Bg.SetWindowText(handle, "开始倒计时");
            //while (true)
            //{
            //    var r = Bg.FindPic(handle, Resource1.倒计时, new XRECT() { Left = 630, Top = 320, Right = 800, Bottom = 450 });
            //    if (!r.IsEmpty)
            //        break;
            //    Thread.Sleep(1000);
            //}
            //Bg.SetWindowText(handle, "开始战斗");
            ////开始战斗
            //Bg.KeyDown(handle, KeyCode.W);
            //Thread.Sleep(1000);
            //Bg.KeyClick(handle, KeyCode.VK_SPACE);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_SPACE);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_SPACE);
            //Thread.Sleep(200);
            //Bg.KeyUp(handle, KeyCode.W);
            //Thread.Sleep(200);
            //Bg.KeyClick(handle, KeyCode.Q);
            //Thread.Sleep(1000);
            //Bg.KeyClick(handle, KeyCode.Q);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_2);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_8);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_4);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_5);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_9);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_3);
            //Thread.Sleep(1500);
            //Bg.KeyClick(handle, KeyCode.VK_6);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_6);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_6);
            //Thread.Sleep(500);
            //Bg.KeyClick(handle, KeyCode.VK_1);
            //Thread.Sleep(500);
        }
    }
}
