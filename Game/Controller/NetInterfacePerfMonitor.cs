using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;

namespace Game.Controller
{
    public partial class NetInterfacePerfMonitor : UserControl
    {
        public NetInterfacePerfMonitor()
        {
            InitializeComponent();
        }
        //private void timer1_Tick(object sender, EventArgs e)
        //{ 
        //    try
        //    {
        //        int val = (int)performanceCounter1.NextValue();
        //        while (val > 0)
        //        {
        //            Application.DoEvents();
        //            if (val >= progressBar1.Maximum)
        //            {
        //                progressBar1.Maximum *= 10;
        //                label3.Text = progressBar1.Maximum.ToString();
        //            }
        //            else
        //                break;
        //        }      
        //        progressBar1.Value = val;
        //        label1.Text = val.ToString() + " Bytes Total/Sec";

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error! " + ex.Message);
        //    }
        //}  
        public void SetTextLine(String text)
        {
            //text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff") + " " + text;
            text =  text+"\r\n";
            if (this.textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new Action<string>((msg) =>
                {
                    textBox1.AppendText(msg);
                    textBox1.ScrollToCaret();
                    textBox1.Focus();
                }), text);
            }
            else
            {
                textBox1.AppendText(text);
                textBox1.ScrollToCaret();
                textBox1.Focus();
            }
        }
        public void ShowInterfaceSpeedAndQueue()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            //Console.WriteLine("Interface information for {0}.{1}     ", computerProperties.HostName, computerProperties.DomainName);
            SetTextLine(String.Format("Interface information for {0}.{1}     ", computerProperties.HostName, computerProperties.DomainName));
            if (adapters == null || adapters.Length < 1)
            {
                //Console.WriteLine("  No network interfaces found.");
                SetTextLine("  No network interfaces found.");
                return;
            }                                
            //Console.WriteLine("  Number of interfaces .................... : {0}", adapters.Length);
            SetTextLine(String.Format("  Number of interfaces .................... : {0}", adapters.Length));
            foreach (NetworkInterface adapter in adapters)
            {
                System.Windows.Forms.Application.DoEvents();
                IPInterfaceProperties properties = adapter.GetIPProperties();
                //Console.WriteLine(String.Empty.PadLeft(80, '='));
                //Console.WriteLine("Name: {0}", adapter.Name);
                //Console.WriteLine(adapter.Description);
                //Console.WriteLine(String.Empty.PadLeft(80, '-'));
                //Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                //Console.WriteLine("  Operational status ...................... : {0}", adapter.OperationalStatus);
                //Console.WriteLine("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
                //Console.WriteLine("  Is receive only.......................... : {0}", adapter.IsReceiveOnly);
                //Console.WriteLine("  Multicast................................ : {0}", adapter.SupportsMulticast);
                //Console.WriteLine("  DNS suffix .............................. : {0}", properties.DnsSuffix);
                //Console.WriteLine("  DNS enabled ............................. : {0}", properties.IsDnsEnabled);
                //Console.WriteLine("  Dynamically configured DNS .............. : {0}", properties.IsDynamicDnsEnabled);                                                  
                SetTextLine(String.Empty.PadLeft(80, '='));
                SetTextLine(String.Format("Name: {0}", adapter.Name));          
                SetTextLine(adapter.Description);
                SetTextLine(String.Empty.PadLeft(80, '_'));
                SetTextLine(String.Format("  ID ...................................... : {0}", adapter.Id));
                SetTextLine(String.Format("  Interface type .......................... : {0}", adapter.NetworkInterfaceType));
                SetTextLine(String.Format("  Operational status ...................... : {0}", adapter.OperationalStatus));       
                SetTextLine(String.Format("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString()));
                SetTextLine(String.Format("  Is receive only.......................... : {0}", adapter.IsReceiveOnly));
                SetTextLine(String.Format("  Multicast................................ : {0}", adapter.SupportsMulticast));
                SetTextLine(String.Format("  DNS suffix .............................. : {0}", properties.DnsSuffix)); 
                SetTextLine(String.Format("  DNS enabled ............................. : {0}", properties.IsDnsEnabled));
                SetTextLine(String.Format("  Dynamically configured DNS .............. : {0}", properties.IsDynamicDnsEnabled));
                string versions = ""; 
                // Create a display string for the supported IP versions.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";              
                    IPv4InterfaceStatistics stats = adapter.GetIPv4Statistics();
                    IPv4InterfaceProperties p = properties.GetIPv4Properties();
                    //Console.WriteLine("  IP version .............................. : {0}", versions);
                    //Console.WriteLine("     Speed .................................: {0}", adapter.Speed);
                    //Console.WriteLine("     Output queue length....................: {0}", stats.OutputQueueLength); 
                    SetTextLine(String.Format("  IP version .............................. : {0}", versions));
                    SetTextLine(String.Format("     Speed .................................: {0}", adapter.Speed));
                    SetTextLine(String.Format("     Output queue length....................: {0}", stats.OutputQueueLength));
                    if (p != null)
                    {
                        //Console.WriteLine("     Index ................................ : {0}", p.Index);
                        //Console.WriteLine("     MTU .................................. : {0}", p.Mtu);
                        //Console.WriteLine("     APIPA active.......................... : {0}", p.IsAutomaticPrivateAddressingActive);
                        //Console.WriteLine("     APIPA enabled......................... : {0}", p.IsAutomaticPrivateAddressingEnabled);
                        //Console.WriteLine("     Forwarding enabled.................... : {0}", p.IsForwardingEnabled);
                        //Console.WriteLine("     Uses WINS ............................ : {0}", p.UsesWins);
                        SetTextLine(String.Format("     Index ................................ : {0}", p.Index));
                        SetTextLine(String.Format("     MTU .................................. : {0}", p.Mtu));
                        SetTextLine(String.Format("     APIPA active.......................... : {0}", p.IsAutomaticPrivateAddressingActive));
                        SetTextLine(String.Format("     APIPA enabled......................... : {0}", p.IsAutomaticPrivateAddressingEnabled));
                        SetTextLine(String.Format("     DHCP enabled.......................... : {0}", p.IsDhcpEnabled));
                        SetTextLine(String.Format("     Forwarding enabled.................... : {0}", p.IsForwardingEnabled));
                        SetTextLine(String.Format("     Uses WINS ............................ : {0}", p.UsesWins));
                    }
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                    //Console.WriteLine("  IP version .............................. : {0}", versions);
                    SetTextLine(String.Format("  IP version .............................. : {0}", versions));
                }
                //SetTextLine(String.Format("{0}",""));
                foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        SetTextLine(String.Format("  IP Address .............................. : {0}", ip.Address.ToString()));
                        SetTextLine(String.Format("  Subnet Mask ............................. : {0}", ip.IPv4Mask == null ? "No subnet defined" : ip.IPv4Mask.ToString()));
                    }
                    else
                    {
                        SetTextLine(String.Format("  IP Address .............................. : {0}", ip.Address.ToString()));
                        SetTextLine(String.Format("  Subnet Mask ............................. : {0}", ip.IPv4Mask == null ? "No subnet defined" : ip.IPv4Mask.ToString()));
                    }
                    //SetTextLine(String.Format("  IP Address .............................. : {0}", ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? ip.Address.ToString(): ip.Address.ToString()));
                }
                foreach (GatewayIPAddressInformation gipi in adapter.GetIPProperties().GatewayAddresses)
                {
                    SetTextLine(String.Format("  Default Gateway ......................... : {0}", gipi.Address.ToString()));
                }
            }
        }
        private System.Diagnostics.PerformanceCounter[] performanceCounter;
        public String[] Netname; 
        private void NetInterfacePerfMonitor_Load(object sender, EventArgs e)
        {
            ShowInterfaceSpeedAndQueue();
            int kk = 0;
            PerformanceCounterCategory categoryInstance = new PerformanceCounterCategory("Network Interface");
            foreach (string strInstanceName in categoryInstance.GetInstanceNames())
            {
                System.Windows.Forms.Application.DoEvents();
                NetInterfaceChart.Series.Add(strInstanceName);
                NetInterfaceChart.Series[strInstanceName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                NetInterfaceChart.Series[strInstanceName].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
                NetInterfaceChart.Series[strInstanceName].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                NetInterfaceChart.Series[strInstanceName].IsXValueIndexed = false;
                //NetInterfaceChart.Series[strInstanceName].IsValueShownAsLabel = false;
                //NetInterfaceChart.Series[strInstanceName].IsVisibleInLegend = false;
                kk++;
            }
            NetInterfaceChart.ResetAutoValues();
            NetInterfaceChart.ChartAreas[0].AxisY.Maximum = 1024 * 1000;//Max Y 
            NetInterfaceChart.ChartAreas[0].AxisY.Minimum = 0;
            NetInterfaceChart.ChartAreas[0].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            System.Timers.Timer[] scanLiveTimer = new System.Timers.Timer[kk];
            performanceCounter = new PerformanceCounter[kk];
            Netname = new String[kk];
            for (int i = 0; i < kk; i++)
            {
                Netname[i] = NetInterfaceChart.Series[i].Name.ToString();
                performanceCounter[i] = new System.Diagnostics.PerformanceCounter();
                performanceCounter[i].MachineName = Dns.GetHostName();
                performanceCounter[i].CategoryName = "Network Interface";
                performanceCounter[i].CounterName = "Bytes Total/sec";
                performanceCounter[i].BeginInit();
                performanceCounter[i].InstanceName = Netname[i];
                scanLiveTimer[i] = new System.Timers.Timer();
                int x = i;
                System.Diagnostics.PerformanceCounter performanceCounterx = performanceCounter[i];               
                scanLiveTimer[i].Elapsed += new System.Timers.ElapsedEventHandler((senderx, ex) => ScanMarketEvent(x, performanceCounterx));
                scanLiveTimer[i].Interval = 1000;
                scanLiveTimer[i].Start();
            }
        }
        public void SetChart(int si, int val)
        {
            try
            {
                if (NetInterfaceChart.InvokeRequired)
                {
                    NetInterfaceChart.BeginInvoke((Action)(() =>
                        {
                            NetInterfaceChart.Series[si].Points.AddY(val.ToString());
                            if (NetInterfaceChart.Series[si].Points.Count > 40) NetInterfaceChart.Series[si].Points.RemoveAt(0);
                            NetInterfaceChart.Series[si].Name = String.Format("{0} {1} Bytes Total/Sec", Netname[si], val);
                            NetInterfaceChart.Update();
                        })
                        );
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void ScanMarketEvent(int si, System.Diagnostics.PerformanceCounter performanceCounter)
        {
            // 可以讀到正確的值
            //Console.Write(string.Format("{0} ", si));   
            try
            {
                if (performanceCounter == null) return;
                int val = (int)performanceCounter.NextValue();
                SetChart(si, val);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
                Debug.WriteLine(ex.Message);
            }
        }
        

    }
}
