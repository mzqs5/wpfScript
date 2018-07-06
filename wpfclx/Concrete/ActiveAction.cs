using System;
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

        internal ActiveAction(IntPtr handle) : base(handle)
        {
        }

        public void Init() {


        }


        public void FixedPointCollect(object CollectCount) {
            int Count = Convert.ToInt32(CollectCount);
            life.LifeCollect(Count);
            life.LifeChangeLine();
        }


    }
}
