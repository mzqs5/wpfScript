using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Abstract;

namespace wpfclx.Concrete
{
    public class Life : ILife
    {
        public Life(IntPtr handle)
        {
            this.handle = handle;
        }

        public IntPtr handle { get ; set ; }

        public int count { get; set; }

        /// <summary>
        /// 生活周围查找
        /// </summary>
        public void LifeAroundLookUp()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 换线
        /// </summary>
        public void LifeChangeLine()
        {
            
        }

        /// <summary>
        /// 生活采集
        /// </summary>

        public void LifeCollect(int count)
        {
            Bg.SetWindowText(handle, "开始查找矿物...");
            var r = Bg.FindPic(handle, Resource1.挖矿, new XRECT() { Left = 1010, Top = 458, Right = 1080, Bottom = 500 });
            if (!r.IsEmpty)
            {
                Bg.LeftMouseClick(handle, r);
                Thread.Sleep(500);
                Bg.SetWindowText(handle, "找到矿物，开始挖矿...");
                Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 828, Y = 380 });
                Thread.Sleep(8000);
                count += 1;
            }
        }
        /// <summary>
        /// 生活世界查找
        /// </summary>
        public void LifeWorldLookUp()
        {
            throw new NotImplementedException();
        }
    }
}
