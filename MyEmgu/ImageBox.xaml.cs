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

namespace MyEmgu
{
    /// <summary>
    /// ImageBox.xaml 的交互逻辑
    /// </summary>
    public partial class ImageBox : UserControl
    {

        public ImageBox()
        {
            InitializeComponent();

        }



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
                Image = ImageProcess.LoadImage(dialog.FileName, Emgu.CV.CvEnum.LoadImageType.Unchanged);
            }
        }



        #endregion


        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var menu = (ContextMenu)FindResource("ImageBoxContextMenu");

            menu.HorizontalOffset = 105 + 12 * menu.Items.Cast<MenuItem>().Max(p => p.Header.ToString().Length);

            menu.IsOpen = true;
        }
    }
}
