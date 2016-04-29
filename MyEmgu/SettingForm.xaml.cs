using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Xml.Linq;

namespace MyEmgu
{
    /// <summary>
    /// SettingForm.xaml 的交互逻辑
    /// </summary>
    public partial class SettingForm : Window, INotifyPropertyChanged
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettingFormLanguage();

        }


        #region 加载设置窗体语言



        XDocument settingformxmlfile;

        /// <summary>
        /// 加载设置窗体语言设置
        /// </summary>
        private void LoadSettingFormLanguage()
        {
            if (!System.IO.File.Exists("SettingFormConfig.xml"))
            {
                return;
            }

            settingformxmlfile = XDocument.Load("SettingFormConfig.xml");
            //加载每一页眉头
            tabitemMarktolerance.Header = FindSettingFormXmlElement("marktoleranceheader");
            tabitemWaringsetting.Header = FindSettingFormXmlElement("warningsettingheader");
            tabitemAboutchar.Header = FindSettingFormXmlElement("aboutcharheader");
            tabitemAbouttakeimage.Header = FindSettingFormXmlElement("abouttakeimageheader");
            tabitemIOsetting.Header = FindSettingFormXmlElement("iosettingheader");

            //印记宽容度
            textCharsensitivity.Text = FindSettingFormXmlElement("textCharsensitivity");
            textLogosensitivity.Text = FindSettingFormXmlElement("textLogosensitivity");
            textExtrapixels.Text = FindSettingFormXmlElement("textExtrapixels");
            textChartiltangle.Text = FindSettingFormXmlElement("textChartiltangle");

            textChardefectsensitivity.Text = FindSettingFormXmlElement("textChardefectsensitivity");
            textLogodefectsensitivity.Text = FindSettingFormXmlElement("textLogodefectsensitivity");
            textChardefectsize.Text = FindSettingFormXmlElement("textChardefectsize");
            textLogodefectsize.Text = FindSettingFormXmlElement("textLogodefectsize");

            //警告设置

            DataContext = this;

        }


        string FindSettingFormXmlElement(string elementname)
        {
            var x = from n in settingformxmlfile.Descendants(elementname)
                    select n.Value;
            foreach (var item in x)
            {
                return item;
            }
            return string.Empty;
        }


        #endregion



        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

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

        private void buttonCancle_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        //按下ESC关闭窗体
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
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


        //各种属性的定义==============================================================================

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        //Sensitivity(char)
        public decimal Charsensitivity
        {
            get { return (decimal)GetValue(CharsensitivityProperty); }
            set { SetValue(CharsensitivityProperty, value); OnPropertyChanged("Charsensitivity"); textboxCharsensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }
        // Using a DependencyProperty as the backing store for Charsensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharsensitivityProperty =
            DependencyProperty.Register("Charsensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.5)));





        //Sensitivity(LOGO)
        public decimal Logosensitivity
        {
            get { return (decimal)GetValue(LogosensitivityProperty); }
            set { SetValue(LogosensitivityProperty, value); OnPropertyChanged("Logosensitivity"); textboxLogosensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        // Using a DependencyProperty as the backing store for Logosensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogosensitivityProperty =
            DependencyProperty.Register("Logosensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0)));



        ///多余像素
        public decimal Extrapixels
        {
            get { return (decimal)GetValue(ExtrapixelsProperty); }
            set { SetValue(ExtrapixelsProperty, value); OnPropertyChanged("Extrapixels"); textboxExtrapixels.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        // Using a DependencyProperty as the backing store for Extrapixels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtrapixelsProperty =
            DependencyProperty.Register("Extrapixels", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.55)));



        //字符倾斜角度
        public decimal Chartiltangle
        {
            get { return (decimal)GetValue(ChartiltangleProperty); }
            set { SetValue(ChartiltangleProperty, value); OnPropertyChanged("Chartiltangle"); textboxChartiltangle.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        // Using a DependencyProperty as the backing store for Chartiltangle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartiltangleProperty =
            DependencyProperty.Register("Chartiltangle", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0)));




        //字符缺陷灵敏度
        public decimal Chardefectsensitivity
        {
            get { return (decimal)GetValue(ChardefectsensitivityProperty); }
            set { SetValue(ChardefectsensitivityProperty, value); OnPropertyChanged("Chardefectsensitivity"); textboxChardefectsensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        // Using a DependencyProperty as the backing store for Chardefectsensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChardefectsensitivityProperty =
            DependencyProperty.Register("Chardefectsensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.3)));


        //LOGO缺陷灵敏度
        public decimal Logodefectsensitivity
        {
            get { return (decimal)GetValue(LogodefectsensitivityProperty); }
            set { SetValue(LogodefectsensitivityProperty, value); OnPropertyChanged("Logodefectsensitivity"); textboxLogodefectsensitivity.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        // Using a DependencyProperty as the backing store for Logodefectsensitivity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogodefectsensitivityProperty =
            DependencyProperty.Register("Logodefectsensitivity", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(0.3)));


        //字符缺陷尺寸
        public decimal Chardefectsize
        {
            get { return (decimal)GetValue(ChardefectsizeProperty); }
            set { SetValue(ChardefectsizeProperty, value); OnPropertyChanged("Chardefectsize"); textboxChardefectsize.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        // Using a DependencyProperty as the backing store for Chardefectsize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChardefectsizeProperty =
            DependencyProperty.Register("Chardefectsize", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(12)));


        //LOGO缺陷尺寸
        public decimal Logodefectsize
        {
            get { return (decimal)GetValue(LogodefectsizeProperty); }
            set { SetValue(LogodefectsizeProperty, value); OnPropertyChanged("Logodefectsize"); textboxLogodefectsize.BorderBrush = (SolidColorBrush)FindResource("textboxChangedForeColor"); }
        }

        // Using a DependencyProperty as the backing store for Logodefectsize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogodefectsizeProperty =
            DependencyProperty.Register("Logodefectsize", typeof(decimal), typeof(MainWindow), new PropertyMetadata(Convert.ToDecimal(11)));







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

        private int ErrorCount = 0;












    }




    //错误验证

    public class MaxMinValueRule : ValidationRule
    {
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

        private decimal min = 0;
        private decimal max = decimal.MaxValue;

        public decimal Min
        {
            get { return min; }
            set { min = value; }
        }

        public decimal Max
        {
            get { return max; }
            set { max = value; }
        }



    }



}
