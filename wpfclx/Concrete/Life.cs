using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfclx.Abstract;

namespace wpfclx.Concrete
{
    internal class Life : Basic, ILife
    {
        public Life(IntPtr handle) : base(handle)
        {

        }

        public void LifeAroundLookUp()
        {
            throw new NotImplementedException();
        }

        public void LifeCollect()
        {
            throw new NotImplementedException();
        }

        public void LifeWorldLookUp()
        {
            throw new NotImplementedException();
        }
    }
}
