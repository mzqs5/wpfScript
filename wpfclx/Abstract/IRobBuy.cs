using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfclx.Abstract
{
    public interface IRobBuy
    {
        void StallRobBuy(int Count);

        void MarketRobBuy(int Count);

        void BuyPlusNumber(int Count);

        void Buy(int Count);

        bool BalanceLack();

        bool OpenMall();
    }
}
