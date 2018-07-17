using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 关机
    /// </summary>
    public class gjTask : TaskBase
    {
        public gjTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            Bg.Ocr(handle);
            //var r = Bg.FindPic(handle, Resource1.副本退出, new XRECT() { Left = 1144, Top = 180, Right = 1181, Bottom = 220 }, FindDirection.LeftTopToRightDown, 0.9f, true);
            //if (!r.IsEmpty)
            //{
            //    Bg.SetWindowText(handle, r.ToString());
            //    Thread.Sleep(10000);
            //}
            //Bg.ExitWindowsEx();
        }
    }
}
