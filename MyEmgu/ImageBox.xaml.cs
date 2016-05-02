using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyEmgu
{
    /// <summary>
    /// ImageBox.xaml 的交互逻辑
    /// </summary>
    public partial class ImageBox : UserControl
    {
        DispatcherTimer m_timer = null;
        long flag = 0;
        public ImageBox()
        {
            InitializeComponent();

            m_timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 300) };

            m_timer.Tick += (obj, e) =>
            {
                mainrect.StrokeDashOffset = flag++;

            };

            m_timer.Start();
        }



        #region Mat相关



        private Mat _mat = new Mat();

        public Mat Mat
        {
            set
            {
                _mat = value;

                if (!_mat.IsEmpty)
                {
                    Image = BitmapSourceConvert.ToBitmapSource(_mat);
                }
            }
            get
            {
                return _mat;
            }


        }



        #endregion


        #region 依赖属性

        public ImageSource Image
        {
            get
            {
                return (ImageSource)GetValue(ImageProperty);
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageBox), new PropertyMetadata(null));

        #endregion



        #region 菜单事件

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.InitialDirectory = "D:\\";
            dialog.Multiselect = false;
            dialog.Title = "选择一张图片";
            dialog.Filter = "JPG|*.jpg|BMP|*.bmp|PNG|*.png";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Mat = new Mat(dialog.FileName, Emgu.CV.CvEnum.LoadImageType.Unchanged);
            }
        }


        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var menu = (ContextMenu)FindResource("ImageBoxContextMenu");

            menu.HorizontalOffset = 105 + 12 * menu.Items.Cast<MenuItem>().Max(p => p.Header.ToString().Length);

            menu.IsOpen = true;
        }



        #endregion


        #region 绘制相关

        public bool StartDraw_Flag = false;

        bool MouseDown_Flag = false;

        Point MouseDown_Point = new Point();

        private void mainimg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StartDraw_Flag)
            {
                MouseDown_Flag = true;

                MouseDown_Point = e.GetPosition(mainimg);

                mainrect.Margin = new Thickness(MouseDown_Point.X, MouseDown_Point.Y, 0, 0);

                mainrect.Width = 0;
                mainrect.Height = 0;

                Canvas.SetLeft(mainimg, MouseDown_Point.X);
                Canvas.SetTop(mainimg, MouseDown_Point.Y);
                Canvas.SetLeft(secondrect, MouseDown_Point.X);
                Canvas.SetTop(secondrect, MouseDown_Point.Y);
            }
        }


        private void mainimg_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && StartDraw_Flag && MouseDown_Flag)
            {
                var pos = e.GetPosition(mainimg);

                mainrect.Width = Math.Abs(pos.X - MouseDown_Point.X);

                mainrect.Height = Math.Abs(pos.Y - MouseDown_Point.Y);




            }

        }

        private void mainimg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (StartDraw_Flag)
            {
                MouseDown_Flag = false;
            }

        }


        #endregion


    }
}
