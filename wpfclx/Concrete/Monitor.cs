using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfclx.Abstract;

namespace wpfclx
{
    public class Monitor : IMonitor
    {
        public IntPtr handle { get; set; }

        public Monitor(IntPtr handle)
        {
            this.handle = handle;
        }

        
        /// <summary>
        /// 采集监控 是否有配方需要学习 生活装备是否不足
        /// </summary>
        public void StudyMonitor()
        {
            var r = Bg.FindPic(handle, Resource1.学习, new XRECT() { Left = 1028, Top = 394, Right = 1091, Bottom = 419 });
            if (!r.IsEmpty)
            {

            }
        }

        public void DrinkingMonitor()
        {
            throw new NotImplementedException();
        }

        public void UseMonitor()
        {
            throw new NotImplementedException();
        }
    }
}
