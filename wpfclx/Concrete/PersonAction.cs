using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using wpfclx.Abstract;

namespace wpfclx.Concrete
{
    /// <summary>
    /// 人的行为
    /// </summary>
    public class PersonAction
    {
        
        protected IntPtr handle;
        internal PersonAction(IntPtr handle)
        {
            this.handle = handle;
            
        }



    }
}
