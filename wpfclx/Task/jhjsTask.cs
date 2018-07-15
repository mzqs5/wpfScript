using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wpfclx.Helper;
using wpfclx.Models;

namespace wpfclx.Task
{
    /// <summary>
    /// 江湖纪事
    /// </summary>
    public class jhjsTask : TaskBase
    {
        public jhjsTask(IntPtr handle) : base(handle)
        {
        }


        private List<string> list = new List<string>();
        public override void Start(TaskModel model)
        {
            if (model.jhselhw)
                list.Add("selhw");
            if (model.jhxjz)
                list.Add("xjz");
            if (model.jhmysj)
                list.Add("mysj");
            foreach (var item in list)
            {
                Bg.LeftMouseClick(handle, new Point() { X = 260, Y = 521 });
                Sleep(500);
                var copy = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetTypes().Where(o => o.Name == $"{item}Copy").FirstOrDefault(), handle) as CopyBase;
                copy.Start();
                copy = null;
                
            }
        }
    }
}
