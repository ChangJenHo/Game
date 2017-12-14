using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Diagnostics;
namespace Game.Network
{
    /// <summary>
    /// 會話類包含遠程通訊端的狀態,這些狀態包括Socket,報文內容,客戶端退出的類型(正常關閉,強制退出兩種類型)
    /// </summary>
    public class Session : ICloneable
    {
        #region 宣告
        /// <summary>
        /// 會話ID
        /// </summary>
        private SessionId _id;
        /// <summary>
        /// 客戶端發送到服務器的報文.注意:在有些情況下報文可能只是報文的片斷而不完整
        /// </summary>
        private string _datagram;
        /// <summary>
        /// 客戶端的Socket
        /// </summary>
        private Socket _cliSock;
        /// <summary>
        /// 客戶端的退出類型
        /// </summary>
        private ExitType _exitType;
        /// <summary>
        /// 退出類型枚舉
        /// </summary>
        public enum ExitType
        {
            NormalExit,
            ExceptionExit
        };
        #endregion
        #region 屬性
        /// <summary>
        /// 返回會話的ID
        /// </summary>
        public SessionId ID
        {
            get
            {
                return _id;
            }
        }
        /// <summary>
        /// 存取會話的報文
        /// </summary>
        public string Datagram
        {
            get
            {
                return _datagram;
            }
            set
            {
                _datagram = value;
            }
        }
        /// <summary>
        /// 獲得與客戶端會話關聯的Socket對象
        /// </summary>
        public Socket ClientSocket
        {
            get
            {
                return _cliSock;
            }
        }
        /// <summary>
        /// 存取客戶端的退出方式
        /// </summary>
        public ExitType TypeOfExit
        {
            get
            {
                return _exitType;
            }
            set
            {
                _exitType = value;
            }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 使用Socket对象的Handle值作为HashCode,它具有良好的线性特征.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (int)_cliSock.Handle;
        }
        /// <summary>
        /// 返回两个Session是否代表同一个客户端
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Session rightObj = (Session)obj;

            return (int)_cliSock.Handle == (int)rightObj.ClientSocket.Handle;
        }
        /// <summary>
        /// 重载ToString()方法,返回Session对象的特征
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = string.Format("Session:{0},IP:{1}",
            _id, _cliSock.RemoteEndPoint.ToString());
            //result.C
            return result;
        }
        private bool disposed = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cliSock">会话使用的Socket连接</param>
        public Session(Socket cliSock)
        {
            Debug.Assert(cliSock != null);
            _cliSock = cliSock;
            _id = new SessionId((int)cliSock.Handle);
        }
        ~Session()
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
        /// 关闭会话
        /// </summary>
        public void Close()
        {
            Debug.Assert(_cliSock != null);
            //关闭数据的接受和发送
            _cliSock.Shutdown(SocketShutdown.Both);
            //清理资源
            _cliSock.Close();
        }
        #endregion
        #region ICloneable 成員
        object System.ICloneable.Clone()
        {
            Session newSession = new Session(_cliSock);
            newSession.Datagram = _datagram;
            newSession.TypeOfExit = _exitType;
            return newSession;
        }
        #endregion
    }
}
