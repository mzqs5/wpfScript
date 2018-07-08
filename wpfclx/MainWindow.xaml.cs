
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
        private Thread activeThread;
        private Thread monitorThread;
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool V()
        {
            if (active == null)
            {
                MessageBox.Show("请先绑定窗口");
                return true;
            }
            else
                return false;
        }

        private void BtnFixedPointCollect_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnFixedPointCollect.Content = "正在采集...";
                activeThread = new Thread(active.FixedPointCollect);
                activeThread.Start(cjCount.Text); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnFixedPointCollect.Content = "换线采集";
            }
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            if (active == null)
            {
                btnBind.Content = "已绑定";
                btnBind.IsEnabled = false;
                var handle = WinApi.FindWindow("Messiah_Game", null);
                active = new ActiveAction(handle);
                passive = new PassiveAction(handle);
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            active.test();

        }

        private void BtnStallRobBuy_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnStallRobBuy.Content = "正在抢购...";
                activeThread = new Thread(active.StallRobBuy);
                activeThread.Start(qgCount.Text); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnStallRobBuy.Content = "摆摊抢购";
            }
        }

        private void BtnMarketRobBuy_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnMarketRobBuy.Content = "正在抢购...";
                activeThread = new Thread(active.MarketRobBuy);
                activeThread.Start(jsqgCount.Text); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnMarketRobBuy.Content = "集市抢购";
            }
        }

        private void BtnSimpleOne_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnSimpleOne.Content = "正在新秀一条...";
                activeThread = new Thread(active.SimpleOne);
                activeThread.Start(); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnSimpleOne.Content = "新秀一条";
            }
        }

        private void BtnReward_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnReward.Content = "正在悬赏...";
                activeThread = new Thread(active.test);
                activeThread.Start(); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnReward.Content = "悬赏";
            }
        }

        private void BtnTheSword_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnTheSword.Content = "正在论剑...";
                activeThread = new Thread(active.lj);
                activeThread.Start(ljCount.Text); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnTheSword.Content = "自动论剑";
            }
        }

        private void BtnBeacon_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnBeacon.Content = "正在烽火...";
                activeThread = new Thread(active.test);
                activeThread.Start(jsqgCount.Text); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnBeacon.Content = "烽火挂机";
            }
        }

        private void BtnAct_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {
                BtnAct.Content = "正在行当一条...";
                activeThread = new Thread(active.test);
                activeThread.Start(); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                BtnAct.Content = "行当一条";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
