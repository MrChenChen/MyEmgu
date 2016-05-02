using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace MyEmgu
{
    public class MaxMinValueRule : ValidationRule
    {
        private decimal max = decimal.MaxValue;

        private decimal min = 0;

        public decimal Max
        {
            get { return max; }
            set { max = value; }
        }

        public decimal Min
        {
            get { return min; }
            set { min = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal price = 0;

            if ((string)value == string.Empty)
            {
                return new ValidationResult(false, "未配置！");
            }

            try
            {
                if (((string)value).Length > 0)
                    price = decimal.Parse((string)value, NumberStyles.Any, cultureInfo);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "非法字符！");
            }

            if ((price < Min) || (price > Max))
            {
                return new ValidationResult(false, "数据溢出！\r\n" + Min + " ~ " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    /// <summary>
    /// SettingForm.xaml 的交互逻辑
    /// </summary>
    public partial class SettingForm : Window, INotifyPropertyChanged
    {
        // Using a DependencyProperty as the backing store for Chardefectsensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChardefectsensitivityProperty =
            DependencyProperty.Register("Chardefectsensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.3)));

        // Using a DependencyProperty as the backing store for Chardefectsize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChardefectsizeProperty =
            DependencyProperty.Register("Chardefectsize", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(12)));

        // Using a DependencyProperty as the backing store for Charsensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharsensitivityProperty =
            DependencyProperty.Register("Charsensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.5)));

        // Using a DependencyProperty as the backing store for Chartiltangle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartiltangleProperty =
            DependencyProperty.Register("Chartiltangle", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0)));

        // Using a DependencyProperty as the backing store for Extrapixels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtrapixelsProperty =
            DependencyProperty.Register("Extrapixels", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.55)));

        // Using a DependencyProperty as the backing store for Logodefectsensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogodefectsensitivityProperty =
            DependencyProperty.Register("Logodefectsensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.3)));

        // Using a DependencyProperty as the backing store for Logodefectsize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogodefectsizeProperty =
            DependencyProperty.Register("Logodefectsize", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(11)));

        // Using a DependencyProperty as the backing store for Logosensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogosensitivityProperty =
            DependencyProperty.Register("Logosensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0)));

        private int ErrorCount = 0;

        //关闭窗体是先执行关闭动画，再关闭窗体
        private bool isclose = false;

        public SettingForm()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //字符缺陷灵敏度
        public decimal Chardefectsensitivity
        {
            get { return (decimal)GetValue(ChardefectsensitivityProperty); }
            set { SetValue(ChardefectsensitivityProperty, value); OnPropertyChanged("Chardefectsensitivity"); textboxChardefectsensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        //字符缺陷尺寸
        public decimal Chardefectsize
        {
            get { return (decimal)GetValue(ChardefectsizeProperty); }
            set { SetValue(ChardefectsizeProperty, value); OnPropertyChanged("Chardefectsize"); textboxChardefectsize.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        //Sensitivity(char)
        public decimal Charsensitivity
        {
            get { return (decimal)GetValue(CharsensitivityProperty); }
            set { SetValue(CharsensitivityProperty, value); OnPropertyChanged("Charsensitivity"); textboxCharsensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        //字符倾斜角度
        public decimal Chartiltangle
        {
            get { return (decimal)GetValue(ChartiltangleProperty); }
            set { SetValue(ChartiltangleProperty, value); OnPropertyChanged("Chartiltangle"); textboxChartiltangle.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        ///多余像素
        public decimal Extrapixels
        {
            get { return (decimal)GetValue(ExtrapixelsProperty); }
            set { SetValue(ExtrapixelsProperty, value); OnPropertyChanged("Extrapixels"); textboxExtrapixels.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        //LOGO缺陷灵敏度
        public decimal Logodefectsensitivity
        {
            get { return (decimal)GetValue(LogodefectsensitivityProperty); }
            set { SetValue(LogodefectsensitivityProperty, value); OnPropertyChanged("Logodefectsensitivity"); textboxLogodefectsensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        //LOGO缺陷尺寸
        public decimal Logodefectsize
        {
            get { return (decimal)GetValue(LogodefectsizeProperty); }
            set { SetValue(LogodefectsizeProperty, value); OnPropertyChanged("Logodefectsize"); textboxLogodefectsize.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        //Sensitivity(LOGO)
        public decimal Logosensitivity
        {
            get { return (decimal)GetValue(LogosensitivityProperty); }
            set { SetValue(LogosensitivityProperty, value); OnPropertyChanged("Logosensitivity"); textboxLogosensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
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

        //各种属性的定义==============================================================================
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            ErrorCount = 0;
            EnumVisual(this);
            if (ErrorCount > 0)
            {
                return;
            }

            Close();
        }

        /// <summary>
        /// 枚举出现的错误
        /// </summary>
        /// <param name="myVisual"></param>
        private void EnumVisual(Visual myVisual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                if (childVisual != null)
                {
                    if (childVisual.GetType() == typeof(TextBox))
                    {
                        if (Validation.GetErrors(childVisual).Count > 0)
                        {
                            string temp = null;
                            foreach (var error in Validation.GetErrors(childVisual))
                            {
                                temp += error.ErrorContent.ToString();
                            }
                            ErrorCount++;
                            MessageBox.Show(childVisual.ToString() + "  " + temp, "配置错误！", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                EnumVisual(childVisual);
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //按下ESC关闭窗体
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        #region 加载设置窗体语言

        private XDocument settingformxmlfile;

   

        #endregion 加载设置窗体语言
    }

    //错误验证
}