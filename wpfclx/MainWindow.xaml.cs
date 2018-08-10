
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfclx.Helper;
using wpfclx.Models;
using wpfclx.Task;

namespace wpfclx
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskModel model;
        private List<Thread> threads;
        private List<IntPtr> hlist;
        private string PATH = AppDomain.CurrentDomain.BaseDirectory + "res\\";//程序运行目录
        public MainWindow()
        {
            InitializeComponent();
            hlist = new List<IntPtr>();
            
            //Directory.CreateDirectory(PATH);
            ////释放所有资源
            //IEnumerable<PropertyInfo> ps = typeof(Resource1).GetRuntimeProperties();
            //foreach (PropertyInfo i in ps)
            //{
            //    object obj = i.GetValue(i.Name, null);
            //    if (i.PropertyType == typeof(byte[]))
            //    {
            //        using (FileStream fs = File.OpenWrite(PATH + $"{i.Name}.dll"))
            //        {
            //            fs.Write((byte[])obj, 0, ((byte[])obj).Length);
            //        }
            //    }
            //    else if (i.PropertyType == typeof(System.Drawing.Bitmap))
            //    {
            //        var bmp = (System.Drawing.Bitmap)obj;
            //        bmp.Save(PATH + $"{i.Name}.bmp");
            //    }
            //    else if (i.PropertyType == typeof(string))
            //    {
            //        File.WriteAllText(PATH + $"{i.Name}.txt", obj.ToString());
            //    }

            //}
        }

        private bool V()
        {
            if (hlist.Count == 0)
            {
                MessageBox.Show("请先绑定窗口");
                return true;
            }
            else
                return false;
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            //handle = WinApi.FindWindow("Messiah_Game", null);
            for (int i = 0; i < 5; i++)
            {
                var handle2 = WinApi.FindWindowEx(IntPtr.Zero, i == 0 ? IntPtr.Zero : hlist[hlist.Count - 1], "Messiah_Game", null);
                if (handle2 != IntPtr.Zero && !hlist.Contains(handle2))
                    hlist.Add(handle2);
            }

            if (hlist.Count == 0)
            {
                MessageBox.Show("未找到游戏窗口，请以管理员身份运行。");
                Close();
                return;
            }
            foreach (var item in hlist)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(obj =>
                {
                    IntPtr handle = (IntPtr)obj;
                    System.Drawing.Point p = Bg.FindPic(handle, Resource1.电池, new XRECT() { Left = 130, Top = 700, Right = 180, Bottom = 740 });
                    if (!p.IsEmpty)
                    {
                        //当前是手游模式 开始切换端游模式 并设置画质节约cpu
                        Bg.SetWindowText(handle, "检测到目前是手机模式，开始切换端游模式...");
                        Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 1269, Y = 207 });
                        Thread.Sleep(500);
                        System.Drawing.Point r = Bg.FindPic(handle, Resource1.设置, new XRECT() { Left = 1236, Top = 559, Right = 1294, Bottom = 706 });
                        if (!r.IsEmpty)
                        {
                            Bg.LeftMouseClick(handle, r);
                            Thread.Sleep(500);

                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 290, Y = 423 });
                            Thread.Sleep(500);
                            //修改镜头模式
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 1157, Y = 283 });
                            Thread.Sleep(500);
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 151, Y = 492 });
                            Thread.Sleep(500);
                            //修改画质
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 1157, Y = 385 });
                            Thread.Sleep(500);
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 313, Y = 213 });
                            Thread.Sleep(500);
                            //修改偏好
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 1159, Y = 481 });
                            Thread.Sleep(500);
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 735, Y = 193 });
                            Thread.Sleep(500);
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 735, Y = 254 });
                            Thread.Sleep(500);
                            //关闭设置面板
                            Bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 1152, Y = 67 });
                            Thread.Sleep(500);
                        }
                    }
                    Bg.SetWindowText(handle, "窗口绑定成功...");

                }), item);
            }
            btnBind.Content = "已绑定";
            btnBind.IsEnabled = false;
            model = new TaskModel();
        }


        private void alltask_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.IsChecked.HasValue && cb.IsChecked.Value)
            {
                //选中
                ListBoxItem item = new ListBoxItem();
                item.Name = cb.Name;
                item.Content = cb.Content;
                selecttask.Items.Add(item);
            }
            else
            {
                //取消选中
                for (int i = 0; i < selecttask.Items.Count; i++)
                {
                    ListBoxItem item = (ListBoxItem)selecttask.Items[i];
                    if (item.Name == cb.Name)
                    {
                        selecttask.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        private List<string> list = new List<string>();
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (threads == null)
            {
                model.qgCount = Convert.ToInt32(qgCount.Text);
                model.cjCount = Convert.ToInt32(cjCount.Text);
                model.ljCount = Convert.ToInt32(ljCount.Text);
                model.zcCount = Convert.ToInt32(zcCount.Text);
                model.xsselhw = xsselhw.IsChecked.HasValue ? xsselhw.IsChecked.Value : false;
                model.xsxjz = xsxjz.IsChecked.HasValue ? xsxjz.IsChecked.Value : false;
                model.xsmysj = xsmysj.IsChecked.HasValue ? xsmysj.IsChecked.Value : false;
                model.xsmysz = xsmysz.IsChecked.HasValue ? xsmysz.IsChecked.Value : false;
                model.jhselhw = jhselhw.IsChecked.HasValue ? jhselhw.IsChecked.Value : false;
                model.jhxjz = jhxjz.IsChecked.HasValue ? jhxjz.IsChecked.Value : false;
                model.jhmysj = jhmysj.IsChecked.HasValue ? jhmysj.IsChecked.Value : false;
                model.xxpf = xxpf.IsChecked.HasValue ? xxpf.IsChecked.Value : false;
                model.bmzql = bmzql.IsChecked.HasValue ? bmzql.IsChecked.Value : false;
                model.fjls = fjls.IsChecked.HasValue ? fjls.IsChecked.Value : false;
                model.fjzs = fjzs.IsChecked.HasValue ? fjzs.IsChecked.Value : false;

                for (int i = 0; i < selecttask.Items.Count; i++)
                {
                    ListBoxItem item = (ListBoxItem)selecttask.Items[i];

                    list.Add(item.Name);
                }
                btnStart.Content = "正在执行...";
                threads = new List<Thread>();
                for (int i = 0; i < hlist.Count; i++)
                {
                    threads.Add(new Thread(TaskStart));
                    threads[i].Start(hlist[i]);
                }
            }
            else
            {
                for (int i = 0; i < threads.Count; i++)
                {
                    threads[i].Abort();
                    threads[i] = null;
                }
                threads.Clear();
                threads = null;
                list.Clear();
                btnStart.Content = "开始任务";
            }
        }

        private void TaskStart(object obj)
        {
            IntPtr handle = (IntPtr)obj;
            foreach (var item in list)
            {
                Type taskType = Assembly.GetExecutingAssembly().GetTypes().Where(o => o.Name == $"{item}Task").SingleOrDefault();

                var task = Activator.CreateInstance(taskType, handle) as TaskBase;

                task.Start(model);

                GC.Collect();
            }
            Bg.SetWindowText(handle, "任务已完成");
            Thread.CurrentThread.Abort();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            GC.Collect();
            Environment.Exit(0);
        }
    }
}
