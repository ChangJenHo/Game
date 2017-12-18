using Game.DataBase;
using Game.Network;
using Game.Network.Email;
using Game.Network.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class MainForm : Form
    {
        public TcpSvr ts;
        public MsSql mssql;
        public Hashtable PlayIDVarPool;
        public Boolean DebugShow = true;
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
                    toolStripButtonStart.Image = Login.Properties.Resources.connect_no_128px_14813_easyicon_net1;
                    toolStripButtonStart.ImageTransparentColor = Color.Transparent;
                    //toolStripButtonStart.BackgroundImage = Login.Properties.Resources.Blue;
                    toolStripButtonStart.Text = "Stop Server";
                    PortNo.Enabled = false;
                    ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.UTF8));
                    //ts = new TcpSvr(Convert.ToUInt16(PortNo.Text), 4096, new Coder(Coder.EncodingMothord.HexString));
                    //ts.Resovlver = new DatagramResolver(Convert.ToChar(4).ToString());//EOT (end of transmission)
                    PlayIDVarPool = new Hashtable(4096);
                    ts.ServerFull += new NetEvent(ServerFull);
                    ts.ClientConn += new NetEvent(ClientConn);
                    ts.ClientClose += new NetEvent(ClientClose);
                    ts.RecvData += new NetEvent(RecvData);
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
                    PlayIDVarPool = null;
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
            String PlayID = PlayIDVarPool[e.Client.ID].ToString();
            string info;
            
            if (e.Client.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format("Client Session:{0} Exception Closed. PlayID:{1}", e.Client.ID, PlayID);
            }
            else
            {
                info = string.Format("Client Session:{0} Normal Closed. PlayID:{1}", e.Client.ID, PlayID);
            }
            //Console.WriteLine(info);
            //Console.Write(">");
            if (DebugShow) rtbe1.SetText(info, true);
            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = info;
            //throw new NotImplementedException();
            DelItem(e.Client.ID.ToString());
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
            PlayIDVarPool.Remove(e.Client.ID);
        }
        private void ClientConn(object sender, NetEventArgs e)
        {
            string info = string.Format("Client:{0} connect server Session:{1}. Socket Handle:{2}",
            e.Client.ClientSocket.RemoteEndPoint.ToString(),
            e.Client.ID, e.Client.ClientSocket.Handle);
            PlayIDVarPool[e.Client.ID] = e.Client.ClientSocket.Handle;
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
            if (DebugShow) rtbe1.SetText(string.Format("from:{0} recv data:[{1}]", e.Client, e.Client.Datagram.Replace("\r\n", "")), true);
            
            SocketStatus.Text = e.Client.ID.ToString();
            SocketStatusLabel.Text = e.Client.Datagram.Replace("\r\n", "");
            UpdItemB(e.Client.ID.ToString(), String.Empty);
            UpdItem(e.Client.ID.ToString(), e.Client.Datagram.ToString());

            //if(DebugShow) rtbe1.SetText(Game.Network.Coder.ShowHexString(e.Client.Datagram), true);

            String ReturnString = LoginParser(e);            

            TcpSvr svr = (TcpSvr)sender;
　　        //测试把收到的数据返回给客户端
　　        svr.Send(e.Client, ReturnString);
            if (DebugShow) rtbe1.SetText(string.Format("from:{0} send data:[{1}]", e.Client, ReturnString), true);
            //throw new NotImplementedException();
            SessionCount.Text = String.Format("Current count of Client is {0}/{1}", ts.SessionCount, ts.Capacity);
            UpdItemB(e.Client.ID.ToString(), ReturnString);
        }
        public String LoginParser(NetEventArgs e)
        {
            String ClientID= e.Client.ID.ToString(); 
            String Datagram= e.Client.Datagram;
            // {"Function":"C2S_Registered","FirstName":"Jen Ho","LastName":"Chang","MicknName":"Chang Jen Ho","Password":"a123736844","Birthday":"1973/01/29","email":"g9677602@gmail.com","location":"105","Gender":"","Photo":"0xFFD8FFE000104A46494600010100000100010000FFDB004300080606070605080707070909080A0C140D0C0B0B0C1912130F141D1A1F1E1D1A1C1C20242E2720222C231C1C2837292C30313434341F27393D38323C2E333432FFDB0043010909090C0B0C180D0D1832211C213232323232323232323232323232323232323232"}
            // {"Function":"C2S_Login","Account":"SDB@gmail.com","CheckPassword":"true","Password":"SDB"}
            // {"Function":"C2S_ForgetPassword", "Account":"SDB@gmail.com"}
            JsonData contentData = JsonMapper.ToObject(Datagram);
            e.Client.ID.VarPool["Function"] = contentData["Function"].ToString();

            switch (e.Client.ID.VarPool["Function"].ToString())
            {
                case "C2S_Registered":
                    e.Client.ID.VarPool["FirstName"] = contentData["FirstName"].ToString();
                    e.Client.ID.VarPool["LastName"] = contentData["LastName"].ToString();
                    e.Client.ID.VarPool["MicknName"] = contentData["MicknName"].ToString();
                    e.Client.ID.VarPool["password"] = contentData["Password"].ToString();
                    e.Client.ID.VarPool["Birthday"] = contentData["Birthday"].ToString();
                    e.Client.ID.VarPool["BirthYear"] = e.Client.ID.VarPool["Birthday"].ToString().Split('/')[0].ToString();
                    e.Client.ID.VarPool["BirthMonth"] = e.Client.ID.VarPool["Birthday"].ToString().Split('/')[1].ToString();
                    e.Client.ID.VarPool["BirthDay"] = e.Client.ID.VarPool["Birthday"].ToString().Split('/')[2].ToString();
                    e.Client.ID.VarPool["email"] = contentData["email"].ToString();
                    e.Client.ID.VarPool["location"] = contentData["location"].ToString();
                    e.Client.ID.VarPool["Gender"] = contentData["Gender"].ToString();

                    String Sql_Registered = @"";
                    e.Client.ID.VarPool["ErrorCord"] = C2S_Registered_SQL(Sql_Registered);



                    e.Client.ID.VarPool["PlayID"] = "00000002";
                    e.Client.ID.VarPool["GaneServer"] = String.Format("192.168.0.149:8001");

                    Datagram = "{\"Function\":\"S2C_Registered\",\"PlayID\":\"";
                    Datagram += e.Client.ID.VarPool["PlayID"].ToString();
                    Datagram += "\",\"Photo\":\"";
                    Datagram += e.Client.ID.VarPool["Photo"].ToString();
                    Datagram += "\",\"ErrorCord\":\"";
                    Datagram += e.Client.ID.VarPool["ErrorCord"].ToString();
                    Datagram += "\",\"GaneServer\":\"";
                    Datagram += e.Client.ID.VarPool["GaneServer"].ToString();
                    Datagram += "\"}";
                    break;
                case "C2S_Login":
                    e.Client.ID.VarPool["account"] = contentData["Account"].ToString();
                    e.Client.ID.VarPool["password"] = contentData["Password"].ToString();
                    e.Client.ID.VarPool["CheckPassword"] = Convert.ToBoolean(contentData["CheckPassword"].ToString());
                    //check DB Account / Password  Yes No
                    //Get DB [PlayID][Photo][GaneServer]
                    e.Client.ID.VarPool["ErrorCord"] = C2S_Login_SQL(e);
                    if (e.Client.ID.VarPool["ErrorCord"].ToString().CompareTo("00000000") == 0)
                    {
                        PlayIDVarPool[e.Client.ID] = e.Client.ID.VarPool["id"].ToString();
                        e.Client.ID.VarPool["Photo"]=Game.Network.Coder.BytesToHex((Byte[])e.Client.ID.VarPool["Photo"]);
                        e.Client.ID.VarPool["GaneServer"] = String.Format("192.168.0.149:8001");
                    }
                    else
                    {
                        e.Client.ID.VarPool["id"] = String.Empty;
                        e.Client.ID.VarPool["Photo"] = null;
                        e.Client.ID.VarPool["GaneServer"] = String.Empty;
                        e.Client.ID.VarPool["Photo"] = String.Empty;
                    }
                    Datagram = "{\"Function\":\"S2C_Login\",\"PlayID\":\"";
                    Datagram += e.Client.ID.VarPool["id"].ToString();
                    Datagram += "\",\"Photo\":\"";
                    Datagram += e.Client.ID.VarPool["Photo"].ToString();
                    Datagram += "\",\"ErrorCord\":\"";
                    Datagram += e.Client.ID.VarPool["ErrorCord"].ToString();
                    Datagram += "\",\"GaneServer\":\"";
                    Datagram += e.Client.ID.VarPool["GaneServer"].ToString();
                    Datagram += "\"}";

                   break;
                case "C2S_ForgetPassword":
                    e.Client.ID.VarPool["account"] = contentData["Account"].ToString();
                    Random rnd = new Random();
                    e.Client.ID.VarPool["password"] = rnd.Next(0, 99999999).ToString().PadRight(8,'0');
                    //check DB Account Get Email
                    //write Password=GN

                    e.Client.ID.VarPool["ErrorCord"] = C2S_ForgetPassword_SQL(e);

                    Datagram = "{\"Function\":\"S2C_ForgetPassword\",\"ErrorCord\":\"";
                    Datagram += e.Client.ID.VarPool["ErrorCord"].ToString();
                    Datagram += "\"}";
                    break;
                default:
                    break;
            }
            //for (int i = 0; i < count; i++) 
            //{
            //    account = friendsData[i]["Account"].ToString();
            //    password = friendsData[i]["Password"].ToString(); 
            //}        
            return Datagram;
        }
        public String C2S_Login_SQL(NetEventArgs e)
        {
            String ReturnString = "00000000";
            try
            {
                if ((Boolean)e.Client.ID.VarPool["CheckPassword"])
                {
                    mssql.CommandString = String.Format("SELECT id, Mail, Photo FROM Account WHERE Mail='{0}' AND Password='{1}';", e.Client.ID.VarPool["account"].ToString(), e.Client.ID.VarPool["password"].ToString());
                }
                else
                {
                    mssql.CommandString = String.Format("SELECT id, Mail, Photo FROM Account WHERE Mail='{0}';", e.Client.ID.VarPool["account"].ToString());
                }
                if (!mssql.MsSelect(new string[] { "id", "Mail", "Photo" }, e.Client.ID.VarPool))
                {
                    ReturnString = mssql.ErrorCode;
                }
                //String id = MsSql.VarPool["id"].ToString();
                //String Mail = MsSql.VarPool["Mail"].ToString();
                //String Photo = Game.Network.Coder.BytesToHex((Byte[])MsSql.VarPool["Photo"]);
            }
            catch (Exception ex)
            {
                ReturnString = Convert.ToString(ex.GetHashCode());
            }
            return ReturnString;
        }
        public String C2S_Registered_SQL(String Data)
        {
            String ReturnString = "00000000";
            return ReturnString;
        }
        public String C2S_ForgetPassword_SQL(NetEventArgs e)
        {
            String ReturnString = "00000000";
            mssql.CommandString = String.Format("UPDATE Account SET Password='{0}' WHERE Mail='{1}';", e.Client.ID.VarPool["password"].ToString(), e.Client.ID.VarPool["account"].ToString());
            if (!mssql.MsUpdata())
            {
                ReturnString = mssql.ErrorCode;
            }
            else
            {
                Send emailsend = new Send();
                emailsend.emailTo = e.Client.ID.VarPool["account"].ToString();
                emailsend.subject = Login.Properties.Settings.Default.subject;
                emailsend.body = String.Format(Login.Properties.Settings.Default.body, e.Client.ID.VarPool["password"].ToString());
                emailsend.SendEmail();
                if (emailsend.ErrorData.CompareTo("") != 0)
                {
                    ReturnString = emailsend.ErrorData;
                }
            }
            return ReturnString;
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
            mssql = new MsSql(Login.Properties.Settings.Default.ZLabSDBEntities);
            if (mssql.ErrorCode.CompareTo("00000000") != 0)
            {
                MessageBox.Show(mssql.ErrorMessing, mssql.ErrorCode);
                mssql.Dispose();
                mssql = new MsSql(Login.Properties.Settings.Default.ZLabSDBTestEntities);
                if (mssql.ErrorCode.CompareTo("00000000") != 0)
                {
                    MessageBox.Show(mssql.ErrorMessing, mssql.ErrorCode);
                    mssql.Dispose();
                    this.Close();
                }
            }
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
            mssql.Dispose();
            if(ts!=null) ts.Dispose();
        }
    }
    public class RootObject
    {
        public string Function { get; set; }
        public List<string> Parameter { get; set; }
    }
}
