using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfclx.Concrete
{
    /// <summary>
    /// 基础行为类
    /// </summary>
    internal class Basic
    {
        protected IntPtr handle;
        internal Basic(IntPtr handle) {
            this.handle = handle;
        }

        internal void Init() {

        }

    }
}
