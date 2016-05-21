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
        public delegate int SetPopContextMenu();

        public SetPopContextMenu m_SetPopContextMenu;

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



        //private Mat _mat = new Mat();

        //public Mat Mat
        //{
        //    set
        //    {
        //        _mat = value;

        //        if (!_mat.IsEmpty)
        //        {
        //            Image = BitmapSourceConvert.ToBitmapSource(_mat);
        //        }
        //    }
        //    get
        //    {
        //        return _mat;
        //    }


        //}



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

                //mainrect.Margin = new Thickness(MouseDown_Point.X, MouseDown_Point.Y, 0, 0);

                mainrect.Width = 0;
                mainrect.Height = 0;

                Canvas.SetLeft(mainrect, MouseDown_Point.X);
                Canvas.SetTop(mainrect, MouseDown_Point.Y);
                Canvas.SetLeft(secondrect, MouseDown_Point.X);
                Canvas.SetTop(secondrect, MouseDown_Point.Y);
            }
        }


        private void mainimg_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && StartDraw_Flag && MouseDown_Flag)
            {
                var pos = e.GetPosition(mainimg);

                var rect = new Rect(MouseDown_Point, pos);


                Canvas.SetLeft(mainrect, rect.X);
                Canvas.SetTop(mainrect, rect.Y);
                Canvas.SetLeft(secondrect, rect.X);
                Canvas.SetTop(secondrect, rect.Y);
                mainrect.Width = rect.Width;

                mainrect.Height = rect.Height;



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
