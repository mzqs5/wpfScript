using System;
using wpfclx.Abstract;
using System.Windows;

namespace wpfclx.Concrete
{
    /// <summary>
    /// 主动行为
    /// </summary>
    public class ActiveAction : PersonAction
    {
        protected ILife life => new Life(handle);

        protected ICopy copy => new Copy(handle);

        internal ActiveAction(IntPtr handle) : base(handle)
        {
        }

        public void Init()
        {


        }


        public void FixedPointCollect(object CollectCount)
        {
            int Count = Convert.ToInt32(CollectCount);
            life.LifeCollect(Count);
            life.LifeChangeLine();
        }

        public void RobBuy() {
            var r = Bg.FindPic(handle, Resource1.在售, new XRECT() { Left = 450, Top = 200, Right = 950, Bottom = 580 });
            if (!r.IsEmpty)
            {
                MessageBox.Show(r.ToString());
            }

        }

        public void test()
        {
            Bg.MouseWheel(handle);


        }
    }
}
