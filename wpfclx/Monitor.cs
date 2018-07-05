using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfclx
{
    public class Monitor
    {
        /// <summary>
        /// 采集监控 是否有配方需要学习 生活装备是否不足
        /// </summary>
        public static void CJMonitor(IntPtr handle)
        {
            var r = bg.FindPic(handle, Resource1.手帕, new XRECT() { Left = 1010, Top = 458, Right = 1080, Bottom = 500 }, true);
            if (!r.IsEmpty)
            {

            }
        }

    }
}
