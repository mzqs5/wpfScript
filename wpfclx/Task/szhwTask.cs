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
    /// 山珍海味
    /// </summary>
    public class szhwTask : TaskBase
    {
        public szhwTask(IntPtr handle) : base(handle)
        {
        }

        public override void Start(TaskModel model)
        {
            for (int i = 0; i < 2; i++)
            {
                var copy = new szhwCopy(handle);
                copy.Start();
                copy = null;
            }
        }
    }
}
