using System.Windows;
using System.Windows.Input;

namespace MyEmgu
{
    /// <summary>
    /// PassWordForm.xaml 的交互逻辑
    /// </summary>
    public partial class PassWordForm : Window
    {
        //关闭窗体是先执行关闭动画，再关闭窗体
        private bool isclose = false;

        public PassWordForm()
        {
            InitializeComponent();
        }

        public PassWordForm(string SecurityLevel)
        {
            InitializeComponent();
            textPasslevel.Text = SecurityLevel;
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

        //关闭
        private void buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //确定
        private void buttonOk_Click(object sender, RoutedEventArgs e)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textboxPassword.Focus();
        }
    }
}