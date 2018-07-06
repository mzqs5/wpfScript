using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfclx.Abstract
{
    /// <summary>
    /// 监控类接口
    /// </summary>
    public interface IMonitor
    {
        IntPtr handle { get; set; }
        void StudyMonitor();

        void DrinkingMonitor();

        void UseMonitor();

    }
}
