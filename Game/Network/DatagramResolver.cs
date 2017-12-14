using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Game.Network
{
    /// <summary>
    /// 數據報文分析器,通過分析接收到的原始數據,得到完整的數據報文.
    /// 繼承該類可以實現自己的報文解析方法.
    /// 通常的報文識別方法包括:固定長度,長度標記,標記符等方法
    /// 本類的現實的是標記符的方法, 你可以在繼承類中實現其他的方法
    /// </summary>
    public class DatagramResolver
    {
        /// <summary>
        /// 報文結束標記
        /// </summary>
        private string endTag;
        /// <summary>
        /// 返回結束標記
        /// </summary>
        string EndTag
        {
            get
            {
                return endTag;
            }
        }
        /// <summary>
        /// 數據報解析器
        /// </summary>
        protected DatagramResolver()
        {
        }
        private bool disposed = false;
        /// <summary>
        /// 數據報解析器
        /// </summary>
        /// <param name="endTag">報文結束標記</param>
        public DatagramResolver(string endTag)
        {
            if (endTag == null)
            {
                throw (new ArgumentNullException("结束标记不能为null"));
            }
            if (endTag == "")
            {
                throw (new ArgumentException("结束标记符号不能为空字符串"));
            }
            this.endTag = endTag;
        }
        ~DatagramResolver()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).釋放其他狀態（管理對象）。
                }
                // Free your own state (unmanaged objects).釋放你自己的狀態（非託管對象）。
                // Set large fields to null.大集字段設置為null。
                disposed = true;
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        /// <summary>
        /// 解析報文
        /// </summary>
        /// <param name="rawDatagram">原始數據,返回未使用的報文片斷,該片斷會保存在Session的Datagram對像中</param>
        /// <returns>報文數組,原始數據可能包含多個報文</returns>
        public virtual string[] Resolve(ref string rawDatagram)
        {
            ArrayList datagrams = new ArrayList();
            //末尾标记位置索引
            int tagIndex = -1;
            while (true)
            {
                tagIndex = rawDatagram.IndexOf(endTag, tagIndex + 1);

                if (tagIndex == -1)
                {
                    break;
                }
                else
                {
                    //按照末尾标记把字符串分为左右两个部分
                    string newDatagram = rawDatagram.Substring(
                    0, tagIndex + endTag.Length);
                    datagrams.Add(newDatagram);

                    if (tagIndex + endTag.Length >= rawDatagram.Length)
                    {
                        rawDatagram = "";
                        break;
                    }
                    rawDatagram = rawDatagram.Substring(tagIndex + endTag.Length,
                    rawDatagram.Length - newDatagram.Length);
                    //从开始位置开始查找
                    tagIndex = 0;
                }
            }
            string[] results = new string[datagrams.Count];
            datagrams.CopyTo(results);
            return results;
        }
    }
}
