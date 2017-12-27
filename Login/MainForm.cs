using Game.Network;
using Game.Network.Email;
using Game.Network.JSON;
using Game.Network.NAT;
using Login.Function;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class MainForm : Form
    {
        public TcpSvr ts;
        public Boolean DebugShow = true;
        private delegate void AddItemCallback(object o);
        private delegate void DelItemCallback(String o);
        private delegate void UpdItemCallback(String o, String oo);
        private delegate void UpdItemCallbackB(String o, String oo);
        public Hashtable GameVarPool = new System.Collections.Hashtable();
        public MainForm()
        {
            InitializeComponent();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.Activate();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            switch (toolStripButtonStart.Text)
            {
                case "Start Server":
                    toolStripButtonStart.BackColor = Color.Blue;
                    toolStripButtonStart.Image = Login.Properties.Resources.connect_no_128px_14813_easyicon_net1;
                    toolStripButtonStart.ImageTransparentColor = Color.Transparent;
                    //toolStripButtonStart.BackgroundImage = Login.Properties.Resources.Blue;
                    toolStripButtonStart.Text = "Stop Server";
                    PortNo.Enabled = false;
                    ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.UTF8));
                    //ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.HexString));
                    //ts.Resovlver = new DatagramResolver(Convert.ToChar(4).ToString());//EOT (end of transmission)
                    ts.ClientConn += new NetEvent(ClientConn);
                    ts.ServerFull += new NetEvent(ServerFull);
                    ts.ClientClose += new NetEvent(ClientClose);
                    ts.RecvData += new NetEvent(RecvData);
                    ts.HeadLength = true;
                    ts.Start();
                    if (DebugShow) rtbe1.SetText(string.Format("Server is listen{0} Start", ts.ServerSocket.LocalEndPoint.ToString()), true);
                    listView2.Items.Clear();
                    SocketStatus.Text = String.Empty;
                    SocketStatusLabel.Text = String.Empty;
                    SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
                    break;
                case "Stop Server":
                    toolStripButtonStart.BackColor = Color.Red;
                    toolStripButtonStart.Image = Login.Properties.Resources.connect_established_128px_14214_easyicon_net1;
                    toolStripButtonStart.ImageTransparentColor = Color.Transparent;
                    //toolStripButtonStart.BackgroundImage = Login.Properties.Resources.Red;
                    toolStripButtonStart.Text = "Start Server";
                    if (DebugShow) rtbe1.SetText(string.Format("Server is listen{0} Stop", ts.ServerSocket.LocalEndPoint.ToString()), true);
                    PortNo.Enabled = true;
                    ts.Stop();
                    listView2.Items.Clear();
                    SocketStatus.Text = String.Empty;
                    SocketStatusLabel.Text = String.Empty;
                    SessionCount.Text = String.Empty;
                    ts.Dispose();
                    break;
                default:
                    break;
            }
            startServerToolStripMenuItem.Text = toolStripButtonStart.Text;
        }
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
        public void UpdItemB(String o, String oo)
        {
            if (this.listView2.InvokeRequired)
            {
                UpdItemCallbackB d = new UpdItemCallbackB(UpdItemB);
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
                    listView2.SelectedItems[0].SubItems[4].Text = oo;
                }
            }
        }
        private void ClientClose(object sender, NetEventArgs e)
        {
            string info;
            
            if (e.Client.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format("Client Session:{0} Exception Closed.", e.Client.ID);
            }
            else
            {
                info = string.Format("Client Session:{0} Normal Closed. PlayID:{1}", e.Client.ID);
            }
            //Console.WriteLine(info);
            //Console.Write(">");
            if (DebugShow) rtbe1.SetText(info, true);
            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;
            //throw new NotImplementedException();
            DelItem(e.Client.ID.ToString());
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
        }
        private void ClientConn(object sender, NetEventArgs e)
        {
            string info = string.Format("Client:{0} connect server Session:{1}. Socket Handle:{2}",
            e.Client.ClientSocket.RemoteEndPoint.ToString(),
            e.Client.ID, e.Client.ClientSocket.Handle);
            //Console.WriteLine(info);
            //Console.Write(">");
            if(DebugShow) rtbe1.SetText(info, true);
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
            lvB.SubItems.Add(String.Empty);
            AddItem(lvB);
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
            GameVarPool[e.Client.ID.ToString()] = sender;
        }
        private void ServerFull(object sender, NetEventArgs e)
        {
            string info = string.Format("Server is full.the Client:{0} is refused",
          e.Client.ClientSocket.RemoteEndPoint.ToString());
　　          //Must do it
　　          //服務器滿了，必須關閉新來的客戶端連接
　　          e.Client.Close();
            //Console.WriteLine(info);
            //Console.Write(">");
            if (DebugShow) rtbe1.SetText(info, true);
            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;
            //throw new NotImplementedException();
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
        }
        private void RecvData(object sender, NetEventArgs e)
        {
            try
            {
                if (DebugShow) rtbe1.SetText(string.Format("from:{0} recv data:[{1}]", e.Client, e.Client.Datagram.Replace("\r\n", "")), true);
                SocketStatus.Text = e.Client.ID.ToString();
                SocketStatusLabel.Text = e.Client.Datagram.Replace("\r\n", "");
                UpdItemB(e.Client.ID.ToString(), String.Empty);
                UpdItem(e.Client.ID.ToString(), e.Client.Datagram.ToString());
                //if(DebugShow) rtbe1.SetText(Game.Network.Coder.ShowHexString(e.Client.Datagram), true);
                FunctionParser fp = new FunctionParser(e);
                String ReturnString = fp.ReturnFunction();
                fp.Dispose();
                //String ReturnString = LoginParser(e);
                //ReturnString += Convert.ToChar(4).ToString();
                TcpSvr svr = (TcpSvr)sender;
                //测试把收到的数据返回给客户端
                svr.Send(e.Client, ReturnString);
                if (DebugShow) rtbe1.SetText(string.Format("from:{0} send data:[{1}]", e.Client, ReturnString), true);
                //throw new NotImplementedException();
                SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
                UpdItemB(e.Client.ID.ToString(), ReturnString);
                
            }
            catch (Exception ex)
            {
                SocketStatusLabel.Text = ex.Message;
                String Datagram = "{\"methodName\":\"Error\",\"paramObject\":{\"Cord\":\"9999\",\"text\":\"";
                Datagram += ex.Message;
                Datagram += "\"}}";
                TcpSvr svr = (TcpSvr)sender;
                //测试把收到的数据返回给客户端
                svr.Send(e.Client, Datagram);
            }
        }
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            String[] AA = Game.Network.Information.IPLocal();
            this.Text = "Login Server --- ";
            foreach (String s in AA)
            {
                if (s!=null) { 
                this.Text += "[";
                this.Text += s;
                this.Text += "]";}

            }
            this.Text += " --- Version:[";
            this.Text += AssemblyVersion;
            this.Text += "] ";
            PortNo.Text = Login.Properties.Settings.Default.ServerPort;
            if (Login.Properties.Settings.Default.AutoRun)
            {
                toolStripButtonStart_Click(sender, e);
            }
            DebugShow = Login.Properties.Settings.Default.DebugShow;
            if (DebugShow)
            {
                toolStripLabel1.Text = "Debug";
            }
            else
            {
                toolStripLabel1.Text = String.Empty;
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!PortNo.Enabled)
            {
                ts.ServerFull -= new NetEvent(ServerFull);
                ts.RecvData -= new NetEvent(RecvData);
                ts.ClientClose -= new NetEvent(ClientClose);
                ts.ClientConn -= new NetEvent(ClientConn);
                ts.Stop();
                ts.Dispose();
            }
        }

    }
}
