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
        private XDocument mainformxmlfile;
        private ObservableCollection<string> myDebugQueue = new ObservableCollection<string>();
        private DetailList myDetailList = DetailList.GetOnlyInstance();
        private DispatcherTimer timer = new DispatcherTimer();
        private Rect last_status = new Rect();


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
            last_status = new Rect(200, 200, 1024, 768);

            //Width = SystemParameters.PrimaryScreenWidth;
            //Height = SystemParameters.PrimaryScreenHeight;
            //Left = 0;
            //Top = 0;

            SystemConfig.st = SysStation.MI;

            myDetailList.Init();

            listBoxDetail.ItemsSource = myDetailList;
            listboxDebug.ItemsSource = myDebugQueue;

            LoadMainFormLanguage();

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



        /// <summary>
        /// 加载主窗体语言设置
        /// </summary>
        private void LoadMainFormLanguage()
        {
            if (!System.IO.File.Exists("MainFormConfig.xml"))
            {
                return;
            }

            mainformxmlfile = XDocument.Load("MainFormConfig.xml");

            //主窗体按钮

            buttonCamera.Content = FindMainFormXmlElement("camera");

            buttonCameraSetting.Content = FindMainFormXmlElement("camerasetting");

            buttonTeach.Content = FindMainFormXmlElement("teach");

            buttonStart.Content = FindMainFormXmlElement("start");

            buttonStop.Content = FindMainFormXmlElement("stop");

            buttonTest.Content = FindMainFormXmlElement("test");

            buttonRefresh.Content = FindMainFormXmlElement("refresh");

            //buttonExit.Content = FindMainFormXmlElement("exit");

            //MoudleName.DetailName = FindMainFormXmlElement("modulename");

            var x0 = (from c in myDetailList where c.DetailName == "modulename" select c);
            if (x0.Count() == 1)
            {
                x0.First().DetailContent = FindMainFormXmlElement("modulename");
            }

            var x1 = (from c in myDetailList where c.DetailName == "looptime" select c);
            if (x1.Count() == 1)
            {
                x1.First().DetailContent = FindMainFormXmlElement("looptime");
            }

            var x2 = (from c in myDetailList where c.DetailName == "speed" select c);
            if (x2.Count() == 1)
            {
                x2.FirstOrDefault().DetailContent = FindMainFormXmlElement("speed");
            }

            var x3 = (from c in myDetailList where c.DetailName == "good" select c);
            if (x3.Count() == 1)
            {
                x3.First().DetailContent = FindMainFormXmlElement("good");
            }

            var x4 = (from c in myDetailList where c.DetailName == "bad" select c);
            if (x4.Count() == 1)
            {
                x4.First().DetailContent = FindMainFormXmlElement("bad");
            }

            var x5 = (from c in myDetailList where c.DetailName == "percent" select c);
            if (x5.Count() == 1)
            {
                x5.First().DetailContent = FindMainFormXmlElement("percent");
            }

            //LoopTime.DetailName = FindMainFormXmlElement("looptime");
            //Speed.DetailName = FindMainFormXmlElement("speed");
            //Good.DetailName = FindMainFormXmlElement("good");
            //Bad.DetailName = FindMainFormXmlElement("bad");
            //Percent.DetailName = FindMainFormXmlElement("percent");

            //菜单

            menuFile.Header = FindMainFormXmlElement("fileheader");
            menuConfig.Header = FindMainFormXmlElement("configheader");

            menuSecurity.Header = FindMainFormXmlElement("securityheader");
            menuTools.Header = FindMainFormXmlElement("toolsheader");

            menuReport.Header = FindMainFormXmlElement("reportheader");
            menuHelp.Header = FindMainFormXmlElement("helpheader");
        }



        /// <summary>
        /// 查找 Config.xml文件中的元素信息
        /// </summary>
        private string FindMainFormXmlElement(string elementname)
        {
            var x = from n in mainformxmlfile.Descendants(elementname)
                    select n.Value;
            foreach (var item in x)
            {
                return item;
            }
            return string.Empty;
        }

        #endregion



        #region 稳定点击事件

        //打开工作路径
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Process p = new Process();
            p.StartInfo.Arguments = textWorkpath.Text;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.FileName = "explorer.exe";
            p.Start();

        }

        //菜单退出事件
        private void Exit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion



        #region UI刷新

        private void DisenableUI()
        {
            buttonStop.IsEnabled = true;

            buttonStart.IsEnabled = false;
            buttonCamera.IsEnabled = false;
            buttonSetting.IsEnabled = false;
            buttonTeach.IsEnabled = false;
            buttonTest.IsEnabled = false;
            buttonCameraSetting.IsEnabled = false;
            menu.IsEnabled = false;
            buttonRefresh.IsEnabled = false;
        }

        private void EnableUI()
        {
            buttonStop.IsEnabled = false;

            buttonStart.IsEnabled = true;
            buttonCamera.IsEnabled = true;
            buttonSetting.IsEnabled = true;
            buttonTeach.IsEnabled = true;
            buttonTest.IsEnabled = true;
            buttonCameraSetting.IsEnabled = true;
            menu.IsEnabled = true;
            buttonRefresh.IsEnabled = true;
        }
        #endregion



        #region 右上角3个按钮事件

        private void Exit_MouseDown(object sender, EventArgs e)
        {
            Close();
        }

        private void Max_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Min_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion 右上角3个按钮事件



        #region 上方 7 按钮事件


        //拍照
        private void buttonCamera_Click(object sender, RoutedEventArgs e)
        {
        }

        //相机设置
        private void buttonCameraSetting_Click(object sender, RoutedEventArgs e)
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
            DisenableUI();
        }

        //停止检测
        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            EnableUI();
        }

        //教授
        private void buttonTeach_Click(object sender, RoutedEventArgs e)
        {
            MsgForm Msg_Form = new MsgForm("请教教授第一行");
            Msg_Form.Show();
        }

        //测试
        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {


        }

        #endregion



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


        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Border _border = (Border)VisualTreeHelper.GetChild(this, 0);

            if (WindowState == WindowState.Maximized)
            {
                _border.Margin = new Thickness(0);
            }
            else
            {
                _border.Margin = new Thickness(0);
            }

            return base.ArrangeOverride(arrangeBounds);
        }


        protected override void OnSourceInitialized(EventArgs e)
        {

            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                //hwndSource.AddHook(new HwndSourceHook(WndProc));
            }


        }

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_NCHITTEST:


                    break;
            }

            return IntPtr.Zero;
        }

        // Invoke  -----

        public const int WM_NCHITTEST = 0x84;
        public const int WM_SYSCOMMAND = 0x112;
        public const int HTCAPTION = 2;
        public const int SC_MOVE = 0xF010;


        // 获取鼠标指针的当前位置
        [DllImport("user32", EntryPoint = "GetCursorPos", SetLastError = false,
        CharSet = CharSet.Auto, ExactSpelling = false,
        CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCursorPos(ref Point lpPoint);

        // 调用一个窗口的窗口函数，将一条消息发给那个窗口。除非消息处理完毕，否则该函数不会返回。SendMessageBynum， SendMessageByString是该函数的“类型安全”声明形式
        [DllImport("user32", EntryPoint = "SendMessage", SetLastError = false,
        CharSet = CharSet.Auto, ExactSpelling = false,
        CallingConvention = CallingConvention.StdCall)]
        public static extern int SendMessage(
            IntPtr hWnd,
            int wMsg,
            int wParam,
            int lParam
        );


        private void Title_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        #endregion





    }
}