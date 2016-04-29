using System.Windows;
using System.Windows.Input;

namespace MyEmgu
{
    /// <summary>
    /// ThresholdForm.xaml 的交互逻辑
    /// </summary>
    public partial class ThresholdForm : Window
    {
        //关闭窗体是先执行关闭动画，再关闭窗体
        private bool isclose = false;

        public ThresholdForm()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - 340;
            Top = SystemParameters.PrimaryScreenHeight - 215;
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

        //确定关闭
        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //打开图像
        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
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