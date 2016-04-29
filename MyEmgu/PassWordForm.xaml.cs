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
    /// PassWordForm.xaml 的交互逻辑
    /// </summary>
    public partial class PassWordForm : Window
    {
        public PassWordForm()
        {
            InitializeComponent();
        }

        public PassWordForm(string SecurityLevel)
        {
            InitializeComponent();
            textPasslevel.Text = SecurityLevel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textboxPassword.Focus();
        }

        //关闭
        private void buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //移动
        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        //确定
        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //回车确定
        private void textboxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonOk_Click(sender, e);
            }
        }

        //更新密码个数
        private void textboxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            textPassWordCount.Text = textboxPassword.Password.Length.ToString();
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
