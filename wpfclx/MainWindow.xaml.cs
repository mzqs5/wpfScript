
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
using wpfclx.Concrete;

namespace wpfclx
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ActiveAction active;
        private PassiveAction passive;
        private Timer timer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btntest_Click(object sender, RoutedEventArgs e)
        {
            ThreadExecute(() =>
            {
                btntest.Content = "正在采集...";
                timer = new Timer(new TimerCallback(active.FixedPointCollect), cjCount.Text, 1000, 2000);
            }, () =>
            {
                btntest.Content = "定点采集";
            });
        }

        public void ThreadExecute(Action start, Action end)
        {
            if (timer!=null)
            {
                timer.Change(-1,0);
                timer.Dispose();
                timer = null;
                end();
            }
            else
            {
                start();
            }
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            if (active == null)
            {
                var handle = WinApi.FindWindow("Messiah_Game", null);
                active = new ActiveAction(handle);
                passive = new PassiveAction(handle);
                btnBind.Content = "已绑定";
                btnBind.IsEnabled = false;
            }
        }
    }
}
