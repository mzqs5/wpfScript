using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfclx.Helper;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 弈中幻境
    /// </summary>
    public class yzhjTask : TaskBase
    {
        public yzhjTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            for (int i = 0; i < 2; i++)
            {
                var copy = new yzhjCopy(handle);
                copy.Start();
                copy = null;
            }
        }
    }
}
