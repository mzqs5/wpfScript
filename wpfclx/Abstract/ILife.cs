using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfclx.Abstract
{
    /// <summary>
    /// 生活类接口
    /// </summary>
    public interface ILife
    {
        bool LifeCollect(int count);

        void LifeWorldLookUp();

        void LifeAroundLookUp();

        void LifeChangeLine();

    }
}
