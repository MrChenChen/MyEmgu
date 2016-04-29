namespace MyEmgu
{
    /// <summary>
    /// 显示Debug信息
    /// </summary>
    public class DebugQueue
    {
        private int m_Count;
        private string m_Debug;

        public DebugQueue()
        {
            Count = 0;
        }

        public int Count
        {
            get
            {
                return m_Count;
            }
            set
            {
                m_Count = value;
            }
        }

        public string Debug
        {
            get
            {
                return m_Debug;
            }
            set
            {
                m_Debug = value;
            }
        }
    }
}