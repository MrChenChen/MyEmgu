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
    /// ThresholdForm.xaml 的交互逻辑
    /// </summary>
    public partial class ThresholdForm : Window
    {
        public ThresholdForm()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - 340;
            Top = SystemParameters.PrimaryScreenHeight - 215;
        }

        //拖动窗体
        private void MianGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //确定关闭
        private void buttonOk_Click(object sender, RoutedEventArgs e)
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

        //打开图像
        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
