using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Network;

namespace Game
{
    public partial class SocketServer : UserControl
    {
        public TcpSvr ts;
        public SocketServer()
        {
            InitializeComponent();
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            switch (toolStripButtonStart.Text)
            {
                case "Start Server":
                    toolStripButtonStart.BackColor = Color.Blue;
                    toolStripButtonStart.ImageTransparentColor = Color.Blue;
                    toolStripButtonStart.BackgroundImage = Game.Properties.Resources.Blue;
                    toolStripButtonStart.Image = Game.Properties.Resources.Blue;
                    toolStripButtonStart.Text = "Stop Server";
                    PortNo.Enabled = false;
                    //ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.UTF8));
                    ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.HexString));
                    //ts.Resovlver = new DatagramResolver(Convert.ToChar(4).ToString());//EOT (end of transmission)
                    ts.ServerFull += new NetEvent(ServerFull);
                    ts.ClientConn += new NetEvent(ClientConn);
                    ts.ClientClose += new NetEvent(ClientClose);
                    ts.RecvData += new NetEvent(RecvData);
                    ts.Start();
                    rtbe1.SetText(string.Format("Server is listen{0} Start", ts.ServerSocket.LocalEndPoint.ToString()), true);
                    break;
                case "Stop Server":
                    toolStripButtonStart.BackColor = Color.Red;
                    toolStripButtonStart.ImageTransparentColor = Color.Red;
                    toolStripButtonStart.BackgroundImage = Game.Properties.Resources.Red;
                    toolStripButtonStart.Image = Game.Properties.Resources.Red;
                    toolStripButtonStart.Text = "Start Server";
                    rtbe1.SetText(string.Format("Server is listen{0} Stop", ts.ServerSocket.LocalEndPoint.ToString()), true);
                    PortNo.Enabled = true;
                    ts.Stop();
                    ts.Dispose();
                    break;
                default:
                    break;
            }
        }
        private delegate void AddItemCallback(object o);
        private delegate void DelItemCallback(String o);
        private delegate void UpdItemCallback(String o, String oo);
        public void AddItem(object o)
        {
            if (this.listView2.InvokeRequired)
            {
                AddItemCallback d = new AddItemCallback(AddItem);
                this.Invoke(d, new object[] { o });
            }
            else
            {
                // code that adds item to listView (in this case $o)
                if (o is ListViewItem)
                    listView2.Items.Add((ListViewItem)o);
                else if (o is string)
                    listView2.Items.Add((string)o);
            }
        }
        public void DelItem(String o)
        {
            if (this.listView2.InvokeRequired)
            {
                DelItemCallback d = new DelItemCallback(DelItem);
                this.Invoke(d, new object[] { o });
            }
            else
            {
                ListViewItem foundItem = listView2.FindItemWithText(o, false, 0, true);
                if (foundItem != null)
                {
                    listView2.TopItem = foundItem;
                    listView2.FindItemWithText(o, false, 0, true).Checked = true;
                    listView2.FindItemWithText(o, false, 0, true).Selected = true;
                    listView2.FindItemWithText(o, false, 0, true).Focused = true;
                    listView2.Focus();
                    listView2.SelectedItems[0].Remove();
                }
            }
        }
        public void UpdItem(String o, String oo)
        {
            if (this.listView2.InvokeRequired)
            {
                UpdItemCallback d = new UpdItemCallback(UpdItem);
                this.Invoke(d, new object[] { o, oo });
            }
            else
            {
                ListViewItem foundItem = listView2.FindItemWithText(o, false, 0, true);
                if (foundItem != null)
                {
                    listView2.TopItem = foundItem;
                    listView2.FindItemWithText(o, false, 0, true).Checked = true;
                    listView2.FindItemWithText(o, false, 0, true).Selected = true;
                    listView2.FindItemWithText(o, false, 0, true).Focused = true;
                    listView2.Focus();
                    listView2.SelectedItems[0].SubItems[3].Text = oo;
                }
            }
        }

        private void RecvData(object sender, NetEventArgs e)
        {
            string info = string.Format("recv data:{0} from:{1}.", e.Client.Datagram, e.Client);
            Console.WriteLine(info);
            Console.Write(">");
            rtbe1.SetText(info, true);


            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;

            UpdItem(e.Client.ID.ToString(), e.Client.Datagram.ToString());

            rtbe1.SetText(Game.Network.Coder.ShowHexString(e.Client.Datagram), true);

            TcpSvr svr = (TcpSvr)sender;
　　          //测试把收到的数据返回给客户端
　　          svr.Send(e.Client, e.Client.Datagram);
            //throw new NotImplementedException();
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
        }

        private void ClientClose(object sender, NetEventArgs e)
        {
            string info;
            if (e.Client.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format("A Client Session:{0} Exception Closed.", e.Client.ID);
            }
            else
            {
                info = string.Format("A Client Session:{0} Normal Closed.", e.Client.ID);
            }
            Console.WriteLine(info);
            Console.Write(">");
            rtbe1.SetText(info, true);
            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;
            //throw new NotImplementedException();
            DelItem(e.Client.ID.ToString());
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
        }

        private void ClientConn(object sender, NetEventArgs e)
        {
            string info = string.Format("A Client:{0} connect server Session:{1}. Socket Handle:{2}",
            e.Client.ClientSocket.RemoteEndPoint.ToString(),
            e.Client.ID, e.Client.ClientSocket.Handle);
            Console.WriteLine(info);
            Console.Write(">");
            rtbe1.SetText(info, true);
            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;
            //throw new NotImplementedException();
            ListViewItem lvB = new ListViewItem();
            int lenIP = e.Client.ClientSocket.RemoteEndPoint.ToString().IndexOf(':');
            int lenPort = (e.Client.ClientSocket.RemoteEndPoint.ToString().Length - lenIP) - 1;
            String IPstring = e.Client.ClientSocket.RemoteEndPoint.ToString().Substring(0, lenIP).ToString();
            lenIP++;
            String PortString = e.Client.ClientSocket.RemoteEndPoint.ToString().Substring(lenIP, lenPort).ToString();
            lvB.Text = e.Client.ID.ToString();
            lvB.SubItems.Add(IPstring);
            lvB.SubItems.Add(PortString);
            lvB.SubItems.Add("Client Connect ... Ok!!!");
            AddItem(lvB);
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
        }
        private void ServerFull(object sender, NetEventArgs e)
        {
            string info = string.Format("Server is full.the Client:{0} is refused",
          e.Client.ClientSocket.RemoteEndPoint.ToString());
　　          //Must do it
　　          //服務器滿了，必須關閉新來的客戶端連接
　　          e.Client.Close();
            Console.WriteLine(info);
            Console.Write(">");
            rtbe1.SetText(info, true);
            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;
            //throw new NotImplementedException();
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
        }
    }
}
