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
        void StudyMonitor();

        void DrinkingMonitor();

        void UseMonitor();

        void EdibleMonitor();

    }
}
