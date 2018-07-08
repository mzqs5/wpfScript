using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Abstract;

namespace wpfclx.Concrete
{
    /// <summary>
    /// 被动行为
    /// </summary>
    public class PassiveAction : PersonAction
    {

        internal PassiveAction(IntPtr handle) : base(handle)
        {

        }

        

    }
}
