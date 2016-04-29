using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MyEmgu
{
    /// <summary>
    /// 显示在Listbox中的详细信息的类
    /// </summary>
    public class Detail : INotifyPropertyChanged
    {
        string _DetailName;

        public string DetailName
        {
            get { return _DetailName; }
            set
            {
                _DetailName = value;
                OnPropertyChanged("DetailName");
            }
        }


        string _DetailContent;

        public string DetailContent
        {
            get { return _DetailContent; }
            set
            {
                _DetailContent = value;
                OnPropertyChanged("DetailContent");
            }
        }



        string _DetailData;

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



        SolidColorBrush _ContentBrush = new SolidColorBrush(Colors.Black);
        public SolidColorBrush ContentBrush
        {
            get { return _ContentBrush; }
            set
            {
                _ContentBrush = value;
                OnPropertyChanged("ContentBrush");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        virtual internal protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }





}
