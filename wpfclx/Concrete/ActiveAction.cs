using System;
using System.Drawing;
using System.IO;
using System.Threading;
using wpfclx.Abstract;


namespace wpfclx.Concrete
{
    /// <summary>
    /// 主动行为
    /// </summary>
    public class ActiveAction : PersonAction
    {
        protected ILife life => new Life(handle);

        protected ICopy copy => new Copy(handle);

        protected IRobBuy robBuy => new RobBuy(handle);

        internal ActiveAction(IntPtr handle) : base(handle)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Init), null);
        }

        public void FixedPointCollect(object CollectCount)
        {
            int Count = Convert.ToInt32(CollectCount);
            while (true)
            {
                for (int i = 0; i < 5; i++)
                {
                    //Bg.MouseWheel(handle, new Point() { X = 1, Y = 1 });
                    //Thread.Sleep(200);
                    Bg.MouseMove(handle, new Point() { X = 600, Y = 480 }, new Point() { X = 800, Y = 480 });
                    Thread.Sleep(500);
                    if (life.LifeCollect(Count))
                    {
                        monitor.StudyMonitor();
                        break;
                    }
                }
                Thread.Sleep(1000);
                life.LifeChangeLine();
            }
        }

        public void StallRobBuy(object RobBuyCount)
        {
            int Count = Convert.ToInt32(RobBuyCount);
            if (robBuy.OpenMall())
            {
                robBuy.StallRobBuy(Count);
            }
        }

        public void MarketRobBuy(object RobBuyCount)
        {
            int Count = Convert.ToInt32(RobBuyCount);
            if (robBuy.OpenMall())
            {
                robBuy.MarketRobBuy(Count);
            }
        }

        public void SimpleOne()
        {
            if (copy.OpenMall())
            {
                copy.Chronicle();
            }
        }
        /// <summary>
        /// 论剑
        /// </summary>
        public void lj(object ljCount)
        {
            int Count = Convert.ToInt32(ljCount);
            copy.QuitTeam();
            for (int i = 0; i < Count; i++)
            {
                if (copy.OpenMall())
                {
                    copy.lj();
                }
            }
            Thread.CurrentThread.Abort();
        }

        public void Start()
        {
            var r = Bg.FindPic(handle, Resource1.活动, new XRECT() { Left = 0, Top = 0, Right = 560, Bottom = 80 });
            Bg.SetWindowText(handle, r.ToString());
            
        }
    }
}
