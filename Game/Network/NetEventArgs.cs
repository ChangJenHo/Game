using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Game.Network
{
    /// <summary>
    /// 服務器程序的事件參數,包含了激發該事件的會話對象
    /// </summary>
    public class NetEventArgs : EventArgs
    {
        #region 客戶端與服務器之間的會話
        /// <summary>
        /// 客戶端與服務器之間的會話
        /// </summary>
        private Session _client;
        /// <summary>
        /// 客戶端與服務器之間的會話
        /// </summary>
        private String _clientString;
        #endregion
        #region 網路事件參數
        private bool disposed = false;
        /// <summary>
        /// 網路事件參數
        /// </summary>
        /// <param name="client">客戶端會話</param>
        public NetEventArgs(Session client)
        {
            if (null == client)
            {
                throw (new ArgumentNullException());
            }
            _client = client;
        }
        /// <summary>
         /// 網路事件參數
         /// </summary>
         /// <param name="client">客戶端會話</param>
        public NetEventArgs(String ClientString)
        {
            if (null == ClientString)
            {
                throw (new ArgumentNullException());
            }
            _clientString = ClientString;
        }
        ~NetEventArgs()
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
        #endregion
        #region 獲得激發該事件的會話對象

        /// <summary>
        /// 獲得激發該事件的會話對象
        /// </summary>
        public Session Client
        {
            get
            {
                return _client;
            }

        }
        /// <summary>
        /// 獲得激發該事件的會話對象
        /// </summary>
        public String ClientString
        {
            get
            {
                return _clientString;
            }

        }
        #endregion
    }
}
