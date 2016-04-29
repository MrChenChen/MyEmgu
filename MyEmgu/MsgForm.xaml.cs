using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyEmgu
{
    /// <summary>
    /// MsgForm.xaml 的交互逻辑
    /// </summary>
    public partial class MsgForm : Window
    {
        //初始化
        public MsgForm()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - 340;
            Top = SystemParameters.PrimaryScreenHeight - 215;
        }

        public MsgForm(string show_msg)
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - 340;
            Top = SystemParameters.PrimaryScreenHeight - 215;

            textMessage.Text = show_msg;

        }

       


        //拖动窗体
        private void MianGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {

            Close();
            
        }

        private void buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //关闭窗体是先执行关闭动画，再关闭窗体
        bool isclose = false;
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Topmost = false;
            base.OnClosing(e);

            if (!isclose)
            {
                FormUnload_Storyboard.Completed += delegate
                {
                    isclose = true;
                    this.Close();
                };

                FormUnload_Storyboard.Begin();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }


      
    }
}
