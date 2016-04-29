using System.Collections.ObjectModel;
using System.Windows.Media;

namespace MyEmgu
{
    public class DetailList : ObservableCollection<Detail>
    {
        private static DetailList OnlyInstance = new DetailList();

        private DetailList()
        {
        }

        /// <summary>
        /// 获取DetailList唯一实例
        /// </summary>
        /// <returns></returns>
        public static DetailList GetOnlyInstance()
        {
            return OnlyInstance;
        }

        /// <summary>
        /// 向DetailList中添加新项
        /// </summary>
        /// <param name="_instance">DetailList的实例（其静态方法GetOnlyInstance返回唯一实例）</param>
        /// <param name="_detailname">ListBox每一行的名称</param>
        /// <param name="_detailcontent">>ListBox每一行左边显示的名称</param>
        /// <param name="_detaildata">>ListBox每一行右边显示的数据</param>
        public void AddItem(string _detailname, string _detailcontent, string _detaildata)
        {
            Detail temp = new Detail() { DetailName = _detailname, DetailContent = _detailcontent, DetailData = _detaildata, ContentBrush = new System.Windows.Media.SolidColorBrush(Colors.Black) };
            this.Add(temp);
        }

        /// <summary>
        /// 初始化显示信息
        /// </summary>
        public void Init()
        {
            this.Clear();

            switch (SystemConfig.st)
            {
                case SysStation.MI:
                    AddItem("modulename", "模块名称", "vision");
                    AddItem("looptime", "循环时间", "0ms");
                    AddItem("speed", "速度(pph)", "0");
                    AddItem("good", "良品", "0");
                    AddItem("bad", "不良", "0");
                    AddItem("percent", "良率", "100%");
                    break;

                case SysStation.SI:
                    AddItem("looptime", "循环时间", "0ms");
                    AddItem("speed", "速度(pph)", "0");
                    AddItem("good", "良品", "0");
                    AddItem("bad", "不良", "0");
                    AddItem("percent", "良率", "100%");
                    break;
            }
        }
    }
}