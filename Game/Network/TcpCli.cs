using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
namespace Game.Network
{
    /// <summary>
    /// 使用異步Socket通訊與服務器按照一定的通訊格式通訊,請注意與服務器的通訊格式一定要一致,否則可能造成服務器程序崩潰,
    /// 整個問題沒有克服,怎麼從byte[]判斷它的編碼格式.
    /// 支持帶標記的數據報文格式的識別,以完成大數據報文的傳輸和適應惡劣的網.
    /// </summary>
    public class TcpCli
    {
        #region 宣告
        /// <summary>
        /// HeadLength = true 電文前4碼為長度,false 一般電文
        /// </summary>
        public Boolean HeadLength = false;
        /// <summary>
        /// 客戶端與服務器之間的會話類
        /// </summary>
        private Session _session;
        /// <summary>
        /// 客戶端是否已經連接服務器
        /// </summary>
        private bool _isConnected = false;
        /// <summary>
        /// 接收數據緩衝區大小64K
        /// </summary>
        public const int DefaultBufferSize = 10240 * 1024;
        /// <summary>
        /// 報文解析器
        /// </summary>
        private DatagramResolver _resolver;
        /// <summary>
        /// 通訊格式編碼解碼器
        /// </summary>
        private Coder _coder;
        /// <summary>
        /// 接收數據緩衝區
        /// </summary>
        private byte[] _recvDataBuffer = new byte[DefaultBufferSize];
        #endregion
        #region 事件定義
        //需要訂閱事件才能收到事件的通知，如果訂閱者退出，必須取消訂閱
        /// <summary>
        /// 已經連接服務器事件
        /// </summary>
        public event NetEvent ConnectedServer;
        /// <summary>
        /// 接收到數據報文事件
        /// </summary>
        public event NetEvent ReceivedDatagram;
        /// <summary>
        /// 連接斷開事件
        /// </summary>
        public event NetEvent DisConnectedServer;
        /// <summary>
        /// 錯誤事件
        /// </summary>
        public event NetEvent ExceptionMessage;
        #endregion
        #region 属性
        /// <summary>
        /// 返回客戶端與服務器之間的會話對象
        /// </summary>
        public Session ClientSession
        {
            get
            {
                return _session;
            }
        }
        /// <summary>
        /// 返回客戶端與服務器之間的連接狀態
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }
        /// <summary>
        /// 數據報文分析器
        /// </summary>
        public DatagramResolver Resovlver
        {
            get
            {
                return _resolver;
            }
            set
            {
                _resolver = value;
            }
        }
        /// <summary>
        /// 編碼解碼器
        /// </summary>
        public Coder ServerCoder
        {
            get
            {
                return _coder;
            }
        }
        #endregion
        #region 公有方法
        private bool disposed = false;
        /// <summary>
        /// TCP網絡連接服務的客戶端
        /// </summary>
        public TcpCli()
        {
            _coder = new Coder(Coder.EncodingMothord.Default);
        }
        /// <summary>
        /// 构造函数,使用一个特定的编码器来初始化
        /// </summary>
        /// <param name="_coder">报文编码器</param>
        public TcpCli(Coder coder)
        {
            _coder = coder;
        }
        ~TcpCli()
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
        /// 連接服務器
        /// </summary>
        /// <param name="ip">服務器IP地址</param>
        /// <param name="port">服務器端口</param>
        public virtual void Connect(string ip, int port)
        {
            try
            {
                if (IsConnected)
                {
                    //重新連接
                    Debug.Assert(_session != null);
                    Close();
                }
                Socket newsock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
                newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 發送數據報文
        /// </summary>
        /// <param name="datagram"></param>
        public virtual void Send(string datagram)
        {
            if (datagram.Length == 0)
            {
                return;
            }
            if (!_isConnected)
            {
                throw (new ApplicationException("沒有連接服務器，不能發送數據"));
            }
            //獲得報文的編碼字節
            byte[] data = _coder.GetEncodingBytes(datagram);
            if (HeadLength)
            {
                int arrayLength = IPAddress.HostToNetworkOrder(data.Length);
                byte[] lengthByteArray = System.BitConverter.GetBytes(arrayLength);
                byte[] Bytes = new byte[data.Length+4];
                Array.Copy(lengthByteArray, 0, Bytes,0,4);
                Array.Copy(data, 0, Bytes, 4, data.Length);
                _session.ClientSocket.BeginSend(Bytes, 0, Bytes.Length, SocketFlags.None, new AsyncCallback(SendDataEnd), _session.ClientSocket);

            }
            else
            {
                _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendDataEnd), _session.ClientSocket);
            }


        }
        /// <summary>
        /// 關閉連接
        /// </summary>
        public virtual void Close()
        {
            if (!_isConnected)
            {
                return;
            }
            _session.Close();
            _session = null;
            _isConnected = false;
        }
        #endregion
        #region 受保護方法
        /// <summary>
        /// 數據發送完成處理函數
        /// </summary>
        /// <param name="iar"></param>
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            Debug.Assert(sent != 0);
        }
        /// <summary>
        /// 建立的Tcp連接後處理過程
        /// </summary>
        /// <param name="iar">異步Socket</param>
        protected virtual void Connected(IAsyncResult iar)
        {
            try
            {
                Socket socket = (Socket)iar.AsyncState;
                socket.EndConnect(iar);
                //創建新的會話
                _session = new Session(socket);

                _isConnected = true;
                //觸發連接建立事件
                if (ConnectedServer != null)
                {
                    ConnectedServer(this, new NetEventArgs(_session));
                }
                //觸發連接建立事件
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0,
                DefaultBufferSize, SocketFlags.None,
                new AsyncCallback(RecvData), socket);
            }
            catch (Exception ex)
            {
                ExceptionMessage(this, new NetEventArgs(ex.Message));
                //throw ex;
            }
        }
        /// <summary>
        ///數據接收處理函數
        /// </summary>
        /// <param name="iar">異步Socket</param>
        protected virtual void RecvData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            try
            {
                String receivedData = String.Empty;
                int recv = remote.EndReceive(iar);
                //正常的退出
                if (recv == 0)
                {
                    _session.TypeOfExit = Session.ExitType.NormalExit;
                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                    return;
                }
                if (HeadLength)
                {
                    byte[] Bytes = new byte[_recvDataBuffer.Length - 4];
                    Array.Copy(_recvDataBuffer, 4, Bytes, 0, _recvDataBuffer.Length - 4);
                    receivedData = _coder.GetEncodingString(Bytes, recv);
                }
                else
                {
                    receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);
                }
                //通過事件發布收到的報文
                if (ReceivedDatagram != null)
                {
                    //通過報文解析器分析出報文
                    //如果定義了報文的尾標記，需要處理報文的多種情況
                    if (_resolver != null)
                    {
                        if (_session.Datagram != null &&
                        _session.Datagram.Length != 0)
                        {
                            //加上最後一次通訊剩餘的報文片斷
                            receivedData = _session.Datagram + receivedData;
                        }
                        string[] recvDatagrams = _resolver.Resolve(ref receivedData);

                            foreach (string newDatagram in recvDatagrams)
                        {
                            //因為需要保證多個不同報文獨立存在
                            ICloneable copySession = (ICloneable)_session;
                            Session clientSession = (Session)copySession.Clone();
                            clientSession.Datagram = newDatagram;
                            //發布一個報文消息
                            ReceivedDatagram(this, new NetEventArgs(clientSession));
                        }
                        //剩餘的代碼片斷，下次接收的時候使用
                        _session.Datagram = receivedData;
                    }
                    //沒有定義報文的尾標記，直接交給消息訂閱者使用
                    else
                    {
                        ICloneable copySession = (ICloneable)_session;
                        Session clientSession = (Session)copySession.Clone();
                        clientSession.Datagram = receivedData;
                        ReceivedDatagram(this, new NetEventArgs(clientSession));
                    }
                }//end of if(ReceivedDatagram != null)
                //繼續接收數據
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None,
                new AsyncCallback(RecvData), _session.ClientSocket);
            }
            catch (SocketException ex)
            {
                //客戶端退出
                if (10054 == ex.ErrorCode)
                {
                    //服務器強制的關閉連接，強制退出
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;
                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                }
                else
                {
                    throw (ex);
                }
            }
            catch (ObjectDisposedException ex)
            {
                //這裡的實現不夠優雅
                //當調用CloseSession（）時，會結束數據接收，但是數據接收
                //處理中會調用int recv = client.EndReceive（iar）;
                //就訪問了CloseSession（）已經處置的對象
                //我想這樣的實現方法也是無傷大雅的。
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                }
            }
        }
        
        #endregion
    }
}
