using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Game.Network
{
    /// <summary>
    /// 唯一的標誌一個Session,輔助Session對像在Hash表中完成特定功能
    /// </summary>
    public class SessionId
    {
        public Hashtable VarPool=new System.Collections.Hashtable();
        /// <summary>
        /// 與Session對象的Socket對象的Handle值相同,必須用這個值來初始化它
        /// </summary>
        private int _id;
        /// <summary>
        /// 返回ID值
        /// </summary>
        public int ID
        {
            get
            {
                return _id;
            }
        }
        private bool disposed = false;
        /// <summary>
        /// 會話ID
        /// </summary>
        /// <param name="id">Socket的Handle值(int)</param>
        public SessionId(int id)
        {
            _id = id;
        }
        ~SessionId()
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
        /// 重載.為了符合Hashtable鍵值特徵
        /// </summary>
        /// <param name="obj">物件</param>
        /// <returns>成功或失敗(true/false)</returns>
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                SessionId right = (SessionId)obj;
                return _id == right._id;
            }
            else if (this == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 重載.為了符合Hashtable鍵值特徵
        /// </summary>
        /// <returns>與Session對象的Socket對象的Handle值相同(int)</returns>
        public override int GetHashCode()
        {
            return _id;
        }
        /// <summary>
        /// 重載,為了方便顯示輸出
        /// </summary>
        /// <returns>與Session對象的Socket對象的Handle值相同(字串)</returns>
        public override string ToString()
        {
            return _id.ToString();
        }
    }
}
