using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpfclx.Helper
{
    public class yzhjCopy : CopyBase
    {
        public yzhjCopy(IntPtr handle) : base(handle)
        {

        }

        protected override void OrganizeTeam()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 257, Y = 441 });
            Thread.Sleep(1000);
            Find();
        }

        protected override void StartCopy()
        {
            Thread.Sleep(720000);
            Bg.SetWindowText(handle, "副本已结束...");
        }

        private void Find()
        {
            Bg.MouseMove(handle, new Point() { X = 260, Y = 520 }, new Point() { X = 260, Y = 250 });
            Thread.Sleep(1000);
            var r = Bg.FindPic(handle, Resource1.弈中幻境, new XRECT() { Left = 143, Top = 140, Right = 374, Bottom = 635 });
            if (!r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(500);
            }
            else
            {
                Find();
            }
        }
    }
}
