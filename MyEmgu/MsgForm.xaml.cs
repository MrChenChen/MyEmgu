﻿using System.Windows;
using System.Windows.Input;

namespace MyEmgu
{
    /// <summary>
    /// MsgForm.xaml 的交互逻辑
    /// </summary>
    public partial class MsgForm : Window
    {
        //关闭窗体是先执行关闭动画，再关闭窗体
        private bool isclose = false;

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

        private void buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //拖动窗体
        private void MianGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}