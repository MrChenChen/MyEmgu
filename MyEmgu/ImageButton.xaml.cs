using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : ButtonBase
    {
        public ImageButton()
        {
            InitializeComponent();
            ImgBtnGrid.DataContext = this;
        }

        #region Image属性

        public static ImageSource GetImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(Image);
        }

        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(Image, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Image =
            DependencyProperty.RegisterAttached("Image", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        #endregion


        #region Text属性

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(Text);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(Text, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Text =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(ImageButton), new PropertyMetadata(null));

        #endregion



    }
}
