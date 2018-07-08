using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using wpfclx.Abstract;


namespace wpfclx.Concrete
{
    /// <summary>
    /// 人的行为
    /// </summary>
    public class PersonAction
    {
        protected IMonitor monitor => new Monitor(handle);

        protected IntPtr handle;
        internal PersonAction(IntPtr handle)
        {
            this.handle = handle;
            
        }
        public void Init(object obj)
        {
            Point p = Bg.FindPic(handle, Resource1.wifi, new XRECT() { Left = 150, Top = 700, Right = 220, Bottom = 740 });
            if (!p.IsEmpty)
            {
                //当前是手游模式 开始切换端游模式 并设置画质节约cpu
                Bg.SetWindowText(handle, "检测到目前是手机模式，开始切换端游模式...");
                this.ChangeMode();
                for (int i = 0; i < 10; i++)
                {
                    Bg.MouseWheel(handle, new Point() { X = 1, Y = 1 });
                    Thread.Sleep(500);
                }
                Bg.SetWindowText(handle, "切换端游模式完成...");
            }
            Bg.SetWindowText(handle, "窗口绑定成功...");
        }

        /// <summary>
        /// 切换端游模式
        /// </summary>
        public void ChangeMode()
        {
            Bg.LeftMouseClick(handle, new Point() { X = 1269, Y = 207 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 1268, Y = 660 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 290, Y = 423 });
            Thread.Sleep(500);
            //修改镜头模式
            Bg.LeftMouseClick(handle, new Point() { X = 1157, Y = 283 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 151, Y = 492 });
            Thread.Sleep(500);
            //修改画质
            Bg.LeftMouseClick(handle, new Point() { X = 1157, Y = 385 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 313, Y = 213 });
            Thread.Sleep(500);
            //修改偏好
            Bg.LeftMouseClick(handle, new Point() { X = 1159, Y = 481 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 735, Y = 193 });
            Thread.Sleep(500);
            Bg.LeftMouseClick(handle, new Point() { X = 735, Y = 254 });
            Thread.Sleep(500);
            //关闭设置面板
            Bg.LeftMouseClick(handle, new Point() { X = 1152, Y = 67 });
            Thread.Sleep(500);
        }

    }
}
