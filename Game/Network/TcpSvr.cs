using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
namespace Game.Network
{
    public class TcpSvr
    {
        #region 定義字段
        /// <summary>
        /// HeadLength = true 電文前4碼為長度,false 一般電文
        /// </summary>
        public Boolean HeadLength=false;
        /// <summary>
        /// 默認的服務器最大連接客戶端端數據
        /// </summary>
        public const int DefaultMaxClient = 100;
        /// <summary>
        /// 接收數據緩衝區大小64K
        /// </summary>
        public const int DefaultBufferSize = 1024 * 1024;
        /// <summary>
        /// 最大數據報文大小
        /// </summary>
        public const int MaxDatagramSize = 10240 * 1024;
        /// <summary>
        /// 報文解析器
        /// </summary>
        private DatagramResolver _resolver;
        /// <summary>
        /// 通訊格式編碼解碼器
        /// </summary>
        public Coder _coder;
        /// <summary>
        /// 服務器程序使用的端口
        /// </summary>
        private ushort _port;
        /// <summary>
        /// 服務器程序允許的最大客戶端連接數
        /// </summary>
        private ushort _maxClient;
        /// <summary>
        /// 服務器的運行狀態
        /// </summary>
        private bool _isRun;
        /// <summary>
        /// 接收數據緩衝區
        /// </summary>
        private byte[] _recvDataBuffer;
        /// <summary>
        /// 服務器使用的異步Socket類，
        /// </summary>
        private Socket _svrSock;
        /// <summary>
        /// 保存所有客戶端會話的哈希表
        /// </summary>
        private Hashtable _sessionTable;
        /// <summary>
        /// 當前的連接的客戶端數
        /// </summary>
        private ushort _clientCount;
        #endregion 定義字段
        #region 事件定義
        /// <summary>
        /// 客戶端建立連接事件
        /// </summary>
        public event NetEvent ClientConn;
        /// <summary>
        /// 客戶端關閉事件
        /// </summary>
        public event NetEvent ClientClose;
        /// <summary>
        /// 服務器已經滿事件
        /// </summary>
        public event NetEvent ServerFull;
        /// <summary>
        /// 服務器接收到數據事件
        /// </summary>
        public event NetEvent RecvData;
        #endregion 事件定義
        #region 構造函數
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        /// <param name="maxClient">服务器能容纳客户端的最大能力</param>
        /// <param name="encodingMothord">通讯的编码方式</param>
        public TcpSvr(ushort port, ushort maxClient, Coder coder)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = coder;
        }
        /// <summary>
        /// 構造函數(默认使用Default编码方式)
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        /// <param name="maxClient">服务器能容纳客户端的最大能力</param>
        public TcpSvr(ushort port, ushort maxClient)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = new Coder(Coder.EncodingMothord.Default);
        }

        // <summary>
        /// 構造函數(默认使用Default编码方式和DefaultMaxClient(100)个客户端的容量)
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        public TcpSvr(ushort port)
            : this(port, DefaultMaxClient)
        {
        }
        private bool disposed = false;
        //public TcpSvr()
        //{
        //    _port = 9000;
        //    _maxClient = 100;
        //    _coder = new Coder(Coder.EncodingMothord.UTF8);
        //}
        ~TcpSvr()
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
        #endregion 構造函數
        #region 屬性
        /// <summary>
        /// 服务器的Socket对象
        /// </summary>
        public Socket ServerSocket
        {
            get
            {
                return _svrSock;
            }
        }
        /// <summary>
        /// 数据报文分析器
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
        /// 客户端会话数组,保存所有的客户端,不允许对该数组的内容进行修改
        /// </summary>
        public Hashtable SessionTable
        {
            get
            {
                return _sessionTable;
            }
        }
        /// <summary>
        /// 服务器可以容纳客户端的最大能力
        /// </summary>
        public int Capacity
        {
            get
            {
                return _maxClient;
            }
        }
        /// <summary>
        /// 当前的客户端连接数
        /// </summary>
        public int SessionCount
        {
            get
            {
                return _clientCount;
            }
        }
        /// <summary>
        /// 服务器运行状态
        /// </summary>
        public bool IsRun
        {
            get
            {
                return _isRun;
            }

        }
        #endregion 屬性
        #region 公有方法
        /// <summary>
        /// 启动服务器程序,开始监听客户端请求
        /// </summary>
        public virtual void Start()
        {
            if (_isRun)
            {
                throw (new ApplicationException("TcpSvr已经在运行."));
            }
            _sessionTable = new Hashtable(_maxClient);
            _recvDataBuffer = new byte[DefaultBufferSize];
            //初始化socket
            _svrSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定端口
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, _port);
            _svrSock.Bind(iep);
            //开始监听
            _svrSock.Listen(5);
            //设置异步方法接受客户端连接
            _svrSock.BeginAccept(new AsyncCallback(AcceptConn), _svrSock);
            _isRun = true;
        }

        /// <summary>
        /// 停止服务器程序,所有与客户端的连接将关闭
        /// </summary>
        public virtual void Stop()
        {
            if (!_isRun)
            {
                throw (new ApplicationException("TcpSvr已经停止"));
            }
            //这个条件语句，一定要在关闭所有客户端以前调用
            //否则在EndConn会出现错误
            _isRun = false;
            //关闭数据连接,负责客户端会认为是强制关闭连接
            if (_svrSock.Connected)
            {
                _svrSock.Shutdown(SocketShutdown.Both);
            }
            CloseAllClient();
            //清理资源
            _svrSock.Close();

            _sessionTable = null;
        }

        /// <summary>
        /// 关闭所有的客户端会话,与所有的客户端连接会断开
        /// </summary>
        public virtual void CloseAllClient()
        {
            foreach (Session client in _sessionTable.Values)
            {
                client.Close();
            }
            _sessionTable.Clear();
        }
        /// <summary>
        /// 关闭一个与客户端之间的会话
        /// </summary>
        /// <param name="closeClient">需要关闭的客户端会话对象</param>
        public virtual void CloseSession(Session closeClient)
        {
            Debug.Assert(closeClient != null);
            if (closeClient != null)
            {

                closeClient.Datagram = null;
                _sessionTable.Remove(closeClient.ID);
                _clientCount -= 1;

                //客户端强制关闭链接
                if (ClientClose != null)
                {
                    ClientClose(this, new NetEventArgs(closeClient));
                }
                closeClient.Close();
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="recvDataClient">接收数据的客户端会话</param>
        /// <param name="datagram">数据报文</param>
        public virtual void Send(Session recvDataClient, string datagram)
        {
            //获得数据编码
            byte[] data = _coder.GetEncodingBytes(datagram);
            if (HeadLength)
            { 
                int arrayLength = IPAddress.HostToNetworkOrder(data.Length);
                byte[] lengthByteArray = System.BitConverter.GetBytes(arrayLength);
                byte[] Bytes = new byte[data.Length + 4];
                Array.Copy(lengthByteArray, 0, Bytes, 0, 4);
                Array.Copy(data, 0, Bytes, 4, data.Length);
                recvDataClient.ClientSocket.BeginSend(Bytes, 0, Bytes.Length, SocketFlags.None,
                new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
            }else {
                recvDataClient.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
                new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
            }
        }
        #endregion 公有方法
        #region 受保護方法
        /// <summary>
        /// 关闭一个客户端Socket,首先需要关闭Session
        /// </summary>
        /// <param name="client">目标Socket对象</param>
        /// <param name="exitType">客户端退出的类型</param>
        protected virtual void CloseClient(Socket client, Session.ExitType exitType)
        {
            Debug.Assert(client != null);
            //查找该客户端是否存在,如果不存在,抛出异常
            Session closeClient = FindSession(client);

            closeClient.TypeOfExit = exitType;
            if (closeClient != null)
            {
                CloseSession(closeClient);
            }
            else
            {
                throw (new ApplicationException("需要关闭的Socket对象不存在"));
            }
        }
        /// <summary>
        /// 客户端连接处理函数
        /// </summary>
        /// <param name="iar">欲建立服务器连接的Socket对象</param>
        protected virtual void AcceptConn(IAsyncResult iar)
        {
            //如果服务器停止了服务,就不能再接收新的客户端
            if (!_isRun)
            {
                return;
            }
            //接受一个客户端的连接请求
            Socket oldserver = (Socket)iar.AsyncState;
            Socket client = oldserver.EndAccept(iar);
            //检查是否达到最大的允许的客户端数目
            if (_clientCount == _maxClient)
            {
                //服务器已满,发出通知
                if (ServerFull != null)
                {
                    ServerFull(this, new NetEventArgs(new Session(client)));
                }

            }
            else
            {

                Session newSession = new Session(client);
                _sessionTable.Add(newSession.ID, newSession);
                //客户端引用计数+1
                _clientCount++;
                //开始接受来自该客户端的数据
                client.BeginReceive(_recvDataBuffer, 0, _recvDataBuffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveData), client);
                //新的客户段连接,发出通知
                if (ClientConn != null)
                {
                    ClientConn(this, new NetEventArgs(newSession));
                }
            }
            //继续接受客户端
            _svrSock.BeginAccept(new AsyncCallback(AcceptConn), _svrSock);
        }
        /// <summary>
        /// 通过Socket对象查找Session对象
        /// </summary>
        /// <param name="client"></param>
        /// <returns>找到的Session对象,如果为null,说明并不存在该回话</returns>
        private Session FindSession(Socket client)
        {
            SessionId id = new SessionId((int)client.Handle);
            return (Session)_sessionTable[id];
        }

        /// <summary>
        /// 接受数据完成处理函数，异步的特性就体现在这个函数中，
        /// 收到数据后，会自动解析为字符串报文
        /// </summary>
        /// <param name="iar">目标客户端Socket</param>
        protected virtual void ReceiveData(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候
                //会两次执行EndReceive
                string receivedData = String.Empty;
               int recv = client.EndReceive(iar);
                if (recv == 0)
                {
                    //正常的关闭
                    CloseClient(client, Session.ExitType.NormalExit);
                    return;
                }
                if (HeadLength)
                {
                    byte[] Bytes = new byte[_recvDataBuffer.Length - 4];
                    Array.Copy(_recvDataBuffer, 4, Bytes, 0, _recvDataBuffer.Length-4);
                    receivedData = _coder.GetEncodingString(Bytes, recv);
                }
                else
                {
                 receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);
                }
                //发布收到数据的事件
                if (RecvData != null)
                {
                    Session sendDataSession = FindSession(client);

                    Debug.Assert(sendDataSession != null);
                    //如果定义了报文的尾标记,需要处理报文的多种情况
                    if (_resolver != null)
                    {
                        if (sendDataSession.Datagram != null &&
                        sendDataSession.Datagram.Length != 0)
                        {
                            //加上最后一次通讯剩余的报文片断
                            receivedData = sendDataSession.Datagram + receivedData;
                        }
                        string[] recvDatagrams = _resolver.Resolve(ref receivedData);

                        foreach (string newDatagram in recvDatagrams)
                        {
                            //深拷贝,为了保持Datagram的对立性
                            ICloneable copySession = (ICloneable)sendDataSession;
                            Session clientSession = (Session)copySession.Clone();
                            clientSession.Datagram = newDatagram;
                            //发布一个报文消息
                            RecvData(this, new NetEventArgs(clientSession));
                        }
                        //剩余的代码片断,下次接收的时候使用
                        sendDataSession.Datagram = receivedData;
                        if (sendDataSession.Datagram.Length > MaxDatagramSize)
                        {
                            sendDataSession.Datagram = null;
                        }

                    }
                    //没有定义报文的尾标记,直接交给消息订阅者使用
                    else
                    {
                        ICloneable copySession = (ICloneable)sendDataSession;
                        Session clientSession = (Session)copySession.Clone();
                        clientSession.Datagram = receivedData;
                        RecvData(this, new NetEventArgs(clientSession));
                    }

                }//end of if(RecvData!=null)
                //继续接收来自来客户端的数据
                client.BeginReceive(_recvDataBuffer, 0, _recvDataBuffer.Length, SocketFlags.None,
                new AsyncCallback(ReceiveData), client);
            }
            catch (SocketException ex)
            {
                //客户端退出
                if (10054 == ex.ErrorCode)
                {
                    //客户端强制关闭
                    CloseClient(client, Session.ExitType.ExceptionExit);
                }

            }
            catch (ObjectDisposedException ex)
            {
                //这里的实现不够优雅
                //当调用CloseSession()时,会结束数据接收,但是数据接收
                //处理中会调用int recv = client.EndReceive(iar);
                //就访问了CloseSession()已经处置的对象
                //我想这样的实现方法也是无伤大雅的.
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                }
            }

        }
        /// <summary>
        /// 发送数据完成处理函数
        /// </summary>
        /// <param name="iar">目标客户端Socket</param>
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            int sent = client.EndSend(iar);
        }
        #endregion 受保護方法
    }
    /// <summary>
    /// 網絡通訊事件模型委託
    /// </summary>
    public delegate void NetEvent(object sender, NetEventArgs e);
    
}
