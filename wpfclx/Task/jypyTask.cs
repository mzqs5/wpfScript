using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfclx.Helper;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 聚义平冤
    /// </summary>
    public class jypyTask : TaskBase
    {
        public jypyTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            for (int i = 0; i < 2; i++)
            {
                var copy = new jypyCopy(handle);
                copy.Start();
                copy = null;
            }
        }
    }
}
