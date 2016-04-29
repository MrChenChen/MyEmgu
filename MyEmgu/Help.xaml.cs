using System.Windows;
using System.Windows.Input;

namespace MyEmgu
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        //关闭窗体是先执行关闭动画，再关闭窗体
        private bool isclose = false;

        public About()
        {
            InitializeComponent();
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

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MrChenChen/MyEmgu");
        }

        private void MianGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}