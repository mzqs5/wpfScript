using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfclx.Abstract
{
    /// <summary>
    /// 副本类接口
    /// </summary>
    public interface ICopy
    {
        void CreateTeam();

        void QuitTeam();

        void AutoMatch();

        void GoTo();

        void Dialogue();

        bool OpenMall();

        void Chronicle();

        void lj();
    }
}
