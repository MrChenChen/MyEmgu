using System.Windows;
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

        public Window m_caller = null;

        //初始化
        public MsgForm(Window _win)
        {
            InitializeComponent();

            m_caller = _win;

            SetStartPosition();
        }

        bool PositionFlag = false;

        public void SetStartPosition()
        {
            if (m_caller == null)
            {
                Left = SystemParameters.PrimaryScreenWidth - Width - 5;
                Top = SystemParameters.PrimaryScreenHeight - Height - 40 - 5;
            }
            else
            {
                Left = m_caller.Left + m_caller.ActualWidth - Width - 5;
                Top = m_caller.Top + m_caller.ActualHeight - Height - 5;
            }

            if (!PositionFlag)
            {
                m_caller.LocationChanged += (obj, e1) => { if (m_caller == this) this.SetStartPosition(); };

                PositionFlag = true;
            }

        }


        public MsgForm(string show_msg, Window _win)
        {
            InitializeComponent();

            m_caller = _win;

            SetStartPosition();

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