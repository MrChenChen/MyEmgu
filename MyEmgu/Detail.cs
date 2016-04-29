using System.ComponentModel;
using System.Windows.Media;

namespace MyEmgu
{
    /// <summary>
    /// 显示在Listbox中的详细信息的类
    /// </summary>
    public class Detail : INotifyPropertyChanged
    {
        private SolidColorBrush _ContentBrush = new SolidColorBrush(Colors.Black);
        private string _DetailContent;
        private string _DetailData;
        private string _DetailName;

        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush ContentBrush
        {
            get { return _ContentBrush; }
            set
            {
                _ContentBrush = value;
                OnPropertyChanged("ContentBrush");
            }
        }

        public string DetailContent
        {
            get { return _DetailContent; }
            set
            {
                _DetailContent = value;
                OnPropertyChanged("DetailContent");
            }
        }

        public string DetailData
        {
            get
            {
                return _DetailData;
            }
            set
            {
                _DetailData = value;
                OnPropertyChanged("DetailData");
            }
        }

        public string DetailName
        {
            get { return _DetailName; }
            set
            {
                _DetailName = value;
                OnPropertyChanged("DetailName");
            }
        }

        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}