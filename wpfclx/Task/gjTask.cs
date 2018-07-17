using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var r = Bg.FindPic(handle, Resource1.副本中, new XRECT() { Left = 1280, Top = 180, Right = 1310, Bottom = 210 }, FindDirection.LeftTopToRightDown, 0.9f, true);
            if (!r.IsEmpty)
            {
                Bg.SetWindowText(handle, r.ToString());
            }
            //Bg.ExitWindowsEx();
        }
    }
}
