
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




        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

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
                    if (item.Name == cb.Name) {
                        selecttask.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (V()) return;
            if (activeThread == null)
            {


                btnStart.Content = "正在执行...";
                activeThread = new Thread(active.Start);
                activeThread.Start(); //开始执行线程
            }
            else
            {
                activeThread.Abort();
                activeThread = null;
                btnStart.Content = "开始任务";
            }
        }
    }
}
