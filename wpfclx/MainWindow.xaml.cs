
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


namespace wpfclx
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr handle;
        private bool isE = false;//释放存在正在执行的线程
        public MainWindow()
        {
            InitializeComponent();
            handle = WinApi.FindWindow("Messiah_Game", null);
        }

        private void btntest_Click(object sender, RoutedEventArgs e)
        {
            ThreadExecute(() =>
            {
                btntest.Content = "正在采集...";
                ThreadPool.QueueUserWorkItem(new WaitCallback(挖矿), cjCount.Text);
            }, () =>
            {
                btntest.Content = "定点采集";
                bg.SetWindowText(handle, "本次挖矿结束");
            });
        }

        public void ThreadExecute(Action start, Action end)
        {
            if (isE)
            {
                end();
                isE = false;
            }
            else
            {
                start();
                isE = true;
            }
        }

        private void 挖矿(object cjCount)
        {
            int i = 0;
            int Count = Convert.ToInt32(cjCount);
            while (i < Count)
            {
                if (!isE)
                    Thread.CurrentThread.Abort();
                bg.SetWindowText(handle, "开始查找矿物...");
                var r = bg.FindPic(handle, Resource1.挖矿, new XRECT() { Left = 1010, Top = 458, Right = 1080, Bottom = 500 }, true);
                if (!r.IsEmpty)
                {
                    bg.SetWindowText(handle, "找到矿物，开始挖矿...");
                    bg.LeftMouseClick(handle, r);
                    Thread.Sleep(500);
                    bg.LeftMouseClick(handle, new System.Drawing.Point() { X = 828, Y = 380 });
                    Thread.Sleep(8000);
                    Monitor.CJMonitor(handle);
                }
                i++;
                Thread.Sleep(500);
            }
            bg.SetWindowText(handle, $"本次挖矿结束，总共挖矿{Count}次");
        }
    }
}
