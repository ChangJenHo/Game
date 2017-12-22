using Game.Network;
using Game.Network.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        public TcpCli tc;
        public MainForm()
        {
            InitializeComponent();
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                switch (toolStripButtonStart.Text)
                {
                    case "Start Connect":
                        toolStripButtonStart.BackColor = Color.Blue;
                        toolStripButtonStart.ImageTransparentColor = Color.Blue;
                        toolStripButtonStart.BackgroundImage = Client.Properties.Resources.Blue;
                        toolStripButtonStart.Image = Client.Properties.Resources.Blue;
                        toolStripButtonStart.Text = "DisConnect";
                        PortNo.Enabled = false;
                        IPNo.Enabled = false;
                        tc = new TcpCli(new Coder(Coder.EncodingMothord.UTF8));
                        //tc = new TcpCli(new Coder(Coder.EncodingMothord.HexString));
                        //tc.Resovlver = new DatagramResolver(Convert.ToChar(4).ToString());//EOT (end of transmission)
                        tc.ReceivedDatagram += new NetEvent(RecvData);
                        tc.DisConnectedServer += new NetEvent(ClientClose);
                        tc.ConnectedServer += new NetEvent(ClientConn);
                        tc.ExceptionMessage += new NetEvent(ClienError);
                        tc.Connect(IPNo.Text, Convert.ToUInt16(PortNo.Text));
                        tc.HeadLength = true;
                        buttonSend.Enabled = true;
                        SendData.Enabled = true;
                        rtbe1.SetText(String.Format("Connect to [{0}]({1})", IPNo.Text, PortNo.Text), true);
                        break;
                    case "DisConnect":
                        toolStripButtonStart.BackColor = Color.Red;
                        toolStripButtonStart.ImageTransparentColor = Color.Red;
                        toolStripButtonStart.BackgroundImage = Client.Properties.Resources.Red;
                        toolStripButtonStart.Image = Client.Properties.Resources.Red;
                        toolStripButtonStart.Text = "Start Connect";
                        PortNo.Enabled = true;
                        IPNo.Enabled = true;
                        buttonSend.Enabled = false;
                        SendData.Enabled = false;
                        tc.Close();
                        tc.Dispose();
                        rtbe1.SetText(String.Format("DisConnect to [{0}]({1})", IPNo.Text, PortNo.Text), true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message.ToString();
                toolStripButtonStart.BackColor = Color.Red;
                toolStripButtonStart.ImageTransparentColor = Color.Red;
                toolStripButtonStart.BackgroundImage = Client.Properties.Resources.Red;
                toolStripButtonStart.Image = Client.Properties.Resources.Red;
                toolStripButtonStart.Text = "Start Connect";
                PortNo.Enabled = true;
                IPNo.Enabled = true;
                buttonSend.Enabled = false;
                SendData.Enabled = false;
                tc.Close();
                tc.Dispose();
                rtbe1.SetText(String.Format("DisConnect to [{0}]({1})", IPNo.Text, PortNo.Text), true);
            }

        }
        private void toolStripButtonSend_Click(object sender, EventArgs e)
        {
            try {
                //tc.Send(String.Format("{0}{1}", SendData.Text, Convert.ToChar(4).ToString()));
                //SendData.Text = String.Format("{0}{1}", SendData.Text.Length.ToString("X8"), SendData.Text);
            tc.Send(String.Format("{0}", SendData.Text));
            rtbe1.SetText(String.Format("Send to [{0}]({1})<<==[{2}]", IPNo.Text, PortNo.Text, SendData.Text), true);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
        }

        private void ClientConn(object sender, NetEventArgs e)
        {
            string info = string.Format("A Client:{0} connect server :{1}", e.Client, e.Client.ClientSocket.RemoteEndPoint.ToString());
            //Console.WriteLine(info);
            //Console.Write(">");
            rtbe1.SetText(info, true);
            //throw new NotImplementedException();
        }

        private void ClientClose(object sender, NetEventArgs e)
        {
            string info;
            if (e.Client.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format("A Client Session:{0} Exception Closed.",
                e.Client.ID);
            }
            else
            {
                info = string.Format("A Client Session:{0} Normal Closed.",
                e.Client.ID);
            }
                UpdateUItoolStripButton(toolStripButtonStart, "Start Connect", Color.Red, Client.Properties.Resources.Red);
                UpdateUIToolStripTextBox(PortNo, true);
                UpdateUIToolStripTextBox(IPNo, true);
                UpdateUITextBox(SendData, true);
                UpdateUIButton(buttonSend, false);
                tc.Close();
                tc.Dispose();
            //Console.WriteLine(info);
            //Console.Write(">");
            rtbe1.SetText(info, true);
            //throw new NotImplementedException();
        }

        private void RecvData(object sender, NetEventArgs e)
        {
            string ReturnCode = String.Empty;
            string ReturnCodeB = String.Empty;
            //string info = string.Format("recv data:{0} from:{1}.", e.Client.Datagram, e.Client);
            string info = string.Format("recv data:from:{0}.", e.Client);
            //Console.WriteLine(info);
            //Console.Write(">");
            rtbe1.SetText(info, true);
            //throw new NotImplementedException();
            JsonData contentData = JsonMapper.ToObject(e.Client.Datagram);
            JsonData friendsData = contentData["paramObject"];
            String Fdata = contentData["methodName"].ToString();
            switch (Fdata)
            {
                case "registered":
                    int count = friendsData.Count;
                    for (int i = 0; i < count; i++)
                    {
                        ReturnCode = friendsData["rs"].ToString();     // night or tom
                        ReturnCodeB = friendsData["photo"].ToString();
                    }
                    byte[] AA = Game.Network.Coder.HexToBytes(ReturnCodeB);
                    AddImage((Image)Game.Network.Coder.byteArrayToImage(AA));
                    break;
                case "S2C_Login":
                    ReturnCodeB = friendsData["Photo"].ToString();
                    byte[] BB = Game.Network.Coder.HexToBytes(ReturnCodeB);
                    AddImage((Image)Game.Network.Coder.byteArrayToImage(BB));
                    break;
                case "":
                    break;
                default:
                    break;
            }
            //rtbe1.SetText(Fdata, true);
            //rtbe1.SetText(ReturnCode, true);
        }
        private delegate void AddImageCallback(Image o);
        public void AddImage(Image o)
        {
            if (this.InvokeRequired)
            {
                AddImageCallback d = new AddImageCallback(AddImage);
                this.Invoke(d, new object[] { o });
            }
            else
            {
                // code that adds item to listView (in this case $o)
                toolStripButton2.BackgroundImage = (Image)o;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="value"></param>
        /// <param name="cc"></param>
        /// <param name="bb"></param>
        private delegate void UpdateUItoolStripButtonCallBack(ToolStripButton ctl, string value, Color cc, System.Drawing.Bitmap bb);
        private void UpdateUItoolStripButton(System.Windows.Forms.ToolStripButton ctl, string value, Color cc, System.Drawing.Bitmap bb)
        {
            if (this.InvokeRequired)
            {
                UpdateUItoolStripButtonCallBack uu = new UpdateUItoolStripButtonCallBack(UpdateUItoolStripButton);
                this.Invoke(uu, ctl, value, cc, bb);
            }
            else
            {
                ctl.Text = value;
                ctl.BackColor = cc;
                ctl.ImageTransparentColor = cc;
                ctl.BackgroundImage = bb;
                ctl.Image = bb;
            }
        }
        private delegate void UpdateUIToolStripTextBoxCallBack(ToolStripTextBox ctl, Boolean value);
        private void UpdateUIToolStripTextBox(System.Windows.Forms.ToolStripTextBox ctl, Boolean value)
        {
            if (this.InvokeRequired)
            {
                UpdateUIToolStripTextBoxCallBack uu = new UpdateUIToolStripTextBoxCallBack(UpdateUIToolStripTextBox);
                this.Invoke(uu, ctl, value);
            }
            else
            {
                ctl.Enabled = value;
            }
        }
        private delegate void UpdateUIToolStripButtonCallBack(ToolStripButton ctl, Boolean value);
        private void UpdateUIToolStripButton(System.Windows.Forms.ToolStripButton ctl, Boolean value)
        {
            if (this.InvokeRequired)
            {
                UpdateUIToolStripButtonCallBack uu = new UpdateUIToolStripButtonCallBack(UpdateUIToolStripButton);
                this.Invoke(uu, ctl, value);
            }
            else
            {
                ctl.Enabled = value;
            }
        }
        private delegate void UpdateUIButtonCallBack(Button ctl, Boolean value);
        private void UpdateUIButton(System.Windows.Forms.Button ctl, Boolean value)
        {
            if (this.InvokeRequired)
            {
                UpdateUIButtonCallBack uu = new UpdateUIButtonCallBack(UpdateUIButton);
                this.Invoke(uu, ctl, value);
            }
            else
            {
                ctl.Enabled = value;
            }
        }
        private delegate void UpdateUITextBoxCallBack(TextBox ctl, Boolean value);
        private void UpdateUITextBox(System.Windows.Forms.TextBox ctl, Boolean value)
        {
            if (this.InvokeRequired)
            {
                UpdateUITextBoxCallBack uu = new UpdateUITextBoxCallBack(UpdateUITextBox);
                this.Invoke(uu, ctl, value);
            }
            else
            {
                ctl.Enabled = value;
            }
        }
        private void ClienError(object sender, NetEventArgs e)
        {
            string info = string.Format("Error data:{0}.", e.ClientString.ToString());
            //Console.WriteLine(info);
            //Console.Write(">");
            rtbe1.SetText(info, true); ;
            toolStripStatusLabel1.Text = e.ClientString.ToString();
            UpdateUItoolStripButton(toolStripButtonStart, "Start Connect", Color.Red, Client.Properties.Resources.Red);
            UpdateUIToolStripTextBox(PortNo, true);
            UpdateUIToolStripTextBox(IPNo, true);
            UpdateUITextBox(SendData, true);
            UpdateUIButton(buttonSend, false);
            tc.Close();
            tc.Dispose();
            rtbe1.SetText(String.Format("DisConnect to [{0}]({1})", IPNo.Text, PortNo.Text), true);
        }
    }
}
