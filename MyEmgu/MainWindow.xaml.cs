using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace MyEmgu
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        public IntPtr m_hwnd = IntPtr.Zero;
        private ObservableCollection<string> myDebugQueue = new ObservableCollection<string>();
        private DetailList myDetailList = DetailList.GetOnlyInstance();
        private DispatcherTimer timer = new DispatcherTimer();

        private Rect last_Rect = new Rect();

        //系统状态
        //SystemStatus m_sys = SystemStatus.enumNormal;

        public MainWindow()
        {

            InitializeComponent();

            m_hwnd = new WindowInteropHelper(this).Handle;

            nowtime.Text = DateTime.Now.ToString();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(delegate
            {
                nowtime.Text = DateTime.Now.ToString();
            });
            timer.Start();

            textWorkpath.Text = Environment.CurrentDirectory.ToString();

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            myDetailList.Init();

            listBoxDetail.ItemsSource = myDetailList;
            listboxDebug.ItemsSource = myDebugQueue;

            //LoadMainFormLanguage();


        }



        #region 公共函数


        /// <summary>
        /// 添加调试信息到界面上
        /// </summary>
        private void AddDebugInfo(string sTemp)
        {
            if (myDebugQueue.Count < 100)
            {
                myDebugQueue.Add(sTemp);
            }
            else if (myDebugQueue.Count == 100)
            {
                myDebugQueue.Add(sTemp);
                myDebugQueue.RemoveAt(0);
            }
        }


        #endregion



        #region 稳定点击事件

        //打开工作路径
        private void TextWorkPath_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.Arguments = textWorkpath.Text;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.FileName = "explorer.exe";
            p.Start();
        }

        //菜单退出事件
        private void Exit_MouseDown(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion



        #region UI刷新


        #endregion



        #region 右上角3个按钮事件

        private void Setting_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Exit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Max_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (WindowState != WindowState.Maximized)
            {
                last_Rect.X = Left;
                last_Rect.Y = Top;
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
                Left = last_Rect.X;
                Top = last_Rect.Y;
            }



        }

        private void Min_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion 右上角3个按钮事件



        #region 菜单事件

        //文件==============================================
        //      退出

        //配置==============================================

        //安全==============================================

        //      提高安全等级
        private void HigherLevel_Click(object sender, RoutedEventArgs e)
        {
            PassWordForm pwf = new PassWordForm();
            pwf.ShowDialog();
        }

        //关于==============================================
        private void menuHelp_Click(object sender, RoutedEventArgs e)
        {
            About aboutform = new About();
            aboutform.ShowDialog();
        }

        //工具==============================================
        //      显示调试信息
        private void menuShowDebugInfo_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as MenuItem).IsChecked)
            {
                borderDebug.Visibility = Visibility.Collapsed;
            }
            else
            {
                borderDebug.Visibility = Visibility.Visible;
            }

            (sender as MenuItem).IsChecked = !(sender as MenuItem).IsChecked;
        }

        //报告==============================================

        #endregion 菜单事件



        #region WPF 窗体相关 消息循环

        private void WindowResizeGrip_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            System.Windows.Controls.Primitives.Thumb thumb = (System.Windows.Controls.Primitives.Thumb)sender;

            var hor = e.HorizontalChange;
            var ver = e.VerticalChange;

            switch (thumb.Name)
            {
                case "SizeNWSE_Bottom":
                    //右下
                    if (MinHeight < ActualHeight + ver)
                    {
                        Height = ActualHeight + ver;
                    }
                    if (MinWidth < ActualWidth + hor)
                    {
                        Width = ActualWidth + hor;
                    }

                    break;
                case "SizeNESW_Top":
                    //左上
                    if (MinWidth < ActualWidth - hor)
                    {
                        Left += hor;
                        Width = ActualWidth - hor;
                    }
                    if (MinHeight < ActualHeight - ver)
                    {
                        Top += ver;
                        Height = ActualHeight - ver;
                    }
                    break;
                case "SizeNWSE_Top":
                    //右上
                    if (MinHeight < ActualHeight - ver)
                    {
                        Top += ver;
                        Height = ActualHeight - ver;
                        Width = ActualWidth + hor;
                    }
                    break;
                case "SizeNESW_Bottom":
                    //左下
                    if (MinWidth < ActualWidth - hor)
                    {
                        Left += hor;
                        Width = ActualWidth - hor;
                        Height = ActualHeight + ver;
                    }
                    break;
                case "SizeNS_Top":
                    //上
                    if (MinHeight < ActualHeight - ver)
                    {
                        Top += ver;
                        Height = ActualHeight - ver;
                    }
                    break;
                case "SizeWE_Right":
                    //右
                    if (MinWidth < ActualWidth + hor)
                    {
                        Width += hor;
                    }
                    break;
                case "SizeNS_Bottom":
                    //下
                    if (MinHeight < ActualHeight + ver)
                    {
                        Height = ActualHeight + ver;
                    }
                    break;
                case "SizeWE_Left":
                    //左
                    if (MinWidth < ActualWidth - hor)
                    {
                        Left += hor;
                        Width = ActualWidth - hor;
                    }

                    break;
                default:
                    break;
            }



        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ClickCount == 1)
            {
                if (WindowState != WindowState.Maximized)
                {
                    last_Rect.X = Left;
                    last_Rect.Y = Top;
                    last_Rect.Width = Width;
                    last_Rect.Height = Height;

                }
            }
            else if (e.ClickCount >= 2)
            {
                if (WindowState != WindowState.Maximized)
                {
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowState = WindowState.Normal;

                    Left = last_Rect.X;
                    Top = last_Rect.Y;
                }
                return;
            }


            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {

        }


        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Border _border = (Border)VisualTreeHelper.GetChild(this, 0);

            if (WindowState == WindowState.Maximized)
            {
                _border.Margin = new Thickness(0);

            }
            else
            {
                _border.Margin = new Thickness(10);
            }

            return base.ArrangeOverride(arrangeBounds);
        }


        protected override void OnSourceInitialized(EventArgs e)
        {

            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }


        }


        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    FullScreenManager.WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }
            return (IntPtr)0;
        }





        #endregion




        #region 上方 7 按钮事件


        //拍照
        private void buttonCamera_Click(object sender, RoutedEventArgs e)
        {

        }


        // 重置 刷新
        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
        }

        //设置 配置
        private void buttonSetting_Click(object sender, RoutedEventArgs e)
        {
            SettingForm setform = new SettingForm();
            setform.ShowDialog();
        }

        //开始检测
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {

        }

        //停止检测
        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {

        }

        //教授
        private void buttonSetROI_Click(object sender, RoutedEventArgs e)
        {
            MsgForm msg_Form = new MsgForm("请在图片上绘制 ROI", this);

            msg_Form.buttonOk.Content = "完成";

            //mainimage.StartDraw_Flag = true;

            msg_Form.buttonOk.Click += (obj, e1) =>
            {

                msg_Form.Close();

            };


            msg_Form.buttonCancle.Content = "重绘";


            msg_Form.Show();
        }

        //测试
        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(mainimage.Mat.IsEmpty.ToString());

        }

        #endregion





    }
}