using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpfclx.Helper
{
    public class jypyCopy : CopyBase
    {
        public jypyCopy(IntPtr handle) : base(handle)
        {

        }

        protected override void OrganizeTeam()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 257, Y = 441 });
            Thread.Sleep(1000);
            Bg.LeftMouseClick(handle, new Point() { X = 257, Y = 606 });
            Thread.Sleep(1000);
        }

        protected override void StartTestingCopy()
        {
            
        }
    }
}
