using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpfclx.Helper
{
    public class selhwCopy : CopyBase
    {
        public selhwCopy(IntPtr handle) : base(handle)
        {

        }

        protected override void OrganizeTeam()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 260, Y = 521 });
            Thread.Sleep(500);
        }

        protected override void StartTestingCopy()
        {
            
        }
    }
}
