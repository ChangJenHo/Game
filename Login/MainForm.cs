using Game.Network;
using Game.Network.Email;
using Game.Network.JSON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class MainForm : Form
    {
        public TcpSvr ts;
        public MainForm()
        {
            InitializeComponent();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
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
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            switch (toolStripButtonStart.Text)
            {
                case "Start Server":
                    toolStripButtonStart.BackColor = Color.Blue;
                    toolStripButtonStart.ImageTransparentColor = Color.Blue;
                    toolStripButtonStart.BackgroundImage = Login.Properties.Resources.Blue;
                    toolStripButtonStart.Image = Login.Properties.Resources.Blue;
                    toolStripButtonStart.Text = "Stop Server";
                    PortNo.Enabled = false;
                    ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.UTF8));
                    //ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.HexString));
                    //ts.Resovlver = new DatagramResolver(Convert.ToChar(4).ToString());//EOT (end of transmission)
                    ts.ServerFull += new NetEvent(ServerFull);
                    ts.ClientConn += new NetEvent(ClientConn);
                    ts.ClientClose += new NetEvent(ClientClose);
                    ts.RecvData += new NetEvent(RecvData);
                    ts.Start();
                    rtbe1.SetText(string.Format("Server is listen{0} Start", ts.ServerSocket.LocalEndPoint.ToString()), true);
                    listView2.Items.Clear();
                    SocketStatus.Text = String.Empty;
                    SocketStatusLabel.Text = String.Empty;
                    SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
                    break;
                case "Stop Server":
                    toolStripButtonStart.BackColor = Color.Red;
                    toolStripButtonStart.ImageTransparentColor = Color.Red;
                    toolStripButtonStart.BackgroundImage = Login.Properties.Resources.Red;
                    toolStripButtonStart.Image = Login.Properties.Resources.Red;
                    toolStripButtonStart.Text = "Start Server";
                    rtbe1.SetText(string.Format("Server is listen{0} Stop", ts.ServerSocket.LocalEndPoint.ToString()), true);
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
        private delegate void AddItemCallback(object o);
        private delegate void DelItemCallback(String o);
        private delegate void UpdItemCallback(String o, String oo);
        private delegate void UpdItemCallbackB(String o, String oo);
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
                info = string.Format("Client Session:{0} Normal Closed.", e.Client.ID);
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
            string info = string.Format("Client:{0} connect server Session:{1}. Socket Handle:{2}",
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
            lvB.SubItems.Add(String.Empty);
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
        private void RecvData(object sender, NetEventArgs e)
        {
            string info = string.Format("from:{0} recv data:[{1}]", e.Client, e.Client.Datagram.Replace("\r\n",""));
            Console.WriteLine(info);
            Console.Write(">");
            rtbe1.SetText(info, true);


            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;

            UpdItem(e.Client.ID.ToString(), e.Client.Datagram.ToString());

            //rtbe1.SetText(Game.Network.Coder.ShowHexString(e.Client.Datagram), true);

            String ReturnString = LoginParser(e.Client.ID.ToString(), e.Client.Datagram);


            TcpSvr svr = (TcpSvr)sender;
　　          //测试把收到的数据返回给客户端
　　          svr.Send(e.Client, ReturnString);
            string infos = string.Format("from:{0} send data:[{1}]", e.Client, ReturnString);
            Console.WriteLine(infos);
            Console.Write(">");
            rtbe1.SetText(infos, true);
            //throw new NotImplementedException();
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
        }
        public String LoginParser(String ClientID, String Datagram)
        {
            // {"Function":"C2S_Registered","FirstName":"Jen Ho","LastName":"Chang","MicknName":"Chang Jen Ho","Password":"a123736844","Birthday":"1973/01/29","email":"g9677602@gmail.com","location":"105","Gender":"","Photo":"0xFFD8FFE000104A46494600010100000100010000FFDB004300080606070605080707070909080A0C140D0C0B0B0C1912130F141D1A1F1E1D1A1C1C20242E2720222C231C1C2837292C30313434341F27393D38323C2E333432FFDB0043010909090C0B0C180D0D1832211C213232323232323232323232323232323232323232"}
            // {"Function":"C2S_Login","Account":"SDB@gmail.com","CheckPassword":"true","Password":"SDB"}
            // {"Function":"C2S_ForgetPassword", "Account":"SDB@gmail.com"}
            String Function = String.Empty;
            String account = String.Empty;
            Boolean CheckPassword = true;
            String password = String.Empty;
            String PlayID = String.Empty;
            String Photo = String.Empty;
            String ErrorCord = String.Empty;
            String GaneServer = String.Empty;
            JsonData contentData = JsonMapper.ToObject(Datagram);
            Function = contentData["Function"].ToString();

            switch (Function)
            {
                case "C2S_Registered":
                    String Sql_Registered = @"";
                    ErrorCord = C2S_Registered_SQL(Sql_Registered);

                    PlayID = "00000002";
                    GaneServer = String.Format("192.168.0.149:8001");

                    Datagram = "{\"Function\":\"S2C_Registered\",\"PlayID\":\"";
                    Datagram += PlayID;
                    Datagram += "\",\"Photo\":\"";
                    Datagram += Photo;
                    Datagram += "\",\"ErrorCord\":\"";
                    Datagram += ErrorCord;
                    Datagram += "\",\"GaneServer\":\"";
                    Datagram += GaneServer;
                    Datagram += "\"}";
                    break;
                case "C2S_Login":
                    account = contentData["Account"].ToString();
                    password = contentData["Password"].ToString();
                    CheckPassword = Convert.ToBoolean(contentData["CheckPassword"].ToString());
                    //check DB Account / Password  Yes No
                    //Get DB [PlayID] [GaneServer]
                    //String[] AA = Game.Network.Information.IPLocal();
                    ErrorCord = C2S_Login_SQL(account, CheckPassword, password);

                    PlayID = "00000001";

                    GaneServer = String.Format("192.168.0.149:8001");

                    Datagram = "{\"Function\":\"S2C_Login\",\"PlayID\":\"";
                    Datagram += PlayID;
                    Datagram += "\",\"Photo\":\"";
                    Datagram += Photo;
                    Datagram += "\",\"ErrorCord\":\"";
                    Datagram += ErrorCord;
                    Datagram += "\",\"GaneServer\":\"";
                    Datagram += GaneServer;
                    Datagram += "\"}";

                   break;
                case "C2S_ForgetPassword":
                    account = contentData["Account"].ToString();
                    Random rnd = new Random();
                    password = rnd.Next(0, 99999999).ToString().PadRight(8,'0');
                    //check DB Account Get Email
                    //write Password=GN

                    ErrorCord = C2S_ForgetPassword_SQL(account, password);

                    Datagram = "{\"Function\":\"S2C_ForgetPassword\",\"ErrorCord\":\"";
                    Datagram += ErrorCord;
                    Datagram += "\"}";
                    break;
                default:
                    break;
            }
            UpdItemB(ClientID, Datagram);
            //for (int i = 0; i < count; i++) 
            //{
            //    account = friendsData[i]["Account"].ToString();
            //    password = friendsData[i]["Password"].ToString(); 
            //}        
            return Datagram;
        }
        public String C2S_Login_SQL(String account, Boolean CheckPassword, String password)
        {
            String ReturnString = "00000000";
            return ReturnString;
        }
        public String C2S_Registered_SQL(String Data)
        {
            String ReturnString = "00000000";
            return ReturnString;
        }
        public String C2S_ForgetPassword_SQL(String account, String password)
        {
            String ReturnString = "00000000";
            Send emailsend = new Send();
            emailsend.emailTo = account;
            emailsend.subject = "SDB backup password";
            emailsend.body = String.Format("Please use the spare password to log in within 24 hours,\nPlease change your password after login\nAlternate password: [{0}]\n", password);
            emailsend.SendEmail();
            if (emailsend.ErrorData.CompareTo("") != 0)           
            {
                ReturnString = emailsend.ErrorData;
            }
            return ReturnString;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            String[] AA = Game.Network.Information.IPLocal();
            this.Text = "Login Server ";
            foreach (String s in AA)
            {
                if (s!=null) { 
                this.Text += "[";
                this.Text += s;
                this.Text += "] ";}

            }
        }
    }
    public class RootObject
    {
        public string Function { get; set; }
        public List<string> Parameter { get; set; }
    }
}
