using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
namespace Game.Controller
{
    public partial class CpuRamUsage : UserControl
    {
        public Boolean CPUtprtFl = true;
        public Boolean HDtprtFl = true;
        public CpuRamUsage()
        {
            InitializeComponent();
        }
        PerformanceCounter cpuUsage = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        PerformanceCounter ramUsage = new PerformanceCounter("Memory", "Available MBytes");
        //CPU温度
        //ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\WMI", "Select * From MSAcpi_ThermalZoneTemperature");
        //HD温度
        //ManagementObjectSearcher hos = new ManagementObjectSearcher(@"root\WMI", "Select * From MSStorageDriver_ATAPISmartData");
        ManagementObjectSearcher moSearch = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

        public float getCpuUsage(PerformanceCounter cpuUsage)
        {
            float _usage = 0;

            for (int i = 0; i < 10; i++)
            {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(200); //休息200ms以避免只抓到0或100.
                _usage += cpuUsage.NextValue();
            }
            return _usage / 9; //第一次抓到的值為零,所以捨去不計. 
        }

        public float getRamUsage(PerformanceCounter ramUsage)
        {
            return ramUsage.NextValue();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = (int)(performanceCounter1.NextValue());
            label1.Text = "Processor Time: " + progressBar1.Value.ToString() + "%";

            progressBar2.Value = (int)(performanceCounter3.NextValue());
            label2.Text = "Privileged Time: " + progressBar2.Value.ToString() + "%";

            progressBar3.Value = (int)(performanceCounter2.NextValue());
            label3.Text = "Memory: " + progressBar3.Value.ToString() + "%";

            cpuUsageChart.Series[0].Points.AddY(progressBar1.Value.ToString());
            if (cpuUsageChart.Series[0].Points.Count > 40) cpuUsageChart.Series[0].Points.RemoveAt(0);
            cpuUsageChart.Series[1].Points.AddY(progressBar2.Value.ToString());
            if (cpuUsageChart.Series[1].Points.Count > 40) cpuUsageChart.Series[1].Points.RemoveAt(0);
            cpuUsageChart.Series[2].Points.AddY(progressBar3.Value.ToString());
            if (cpuUsageChart.Series[2].Points.Count > 40) cpuUsageChart.Series[2].Points.RemoveAt(0);

            CPUtprt();
            HDtprt();
            //ProcessorId();
        }

        public String ProcessorInfo()
        {
            String ReturnCPU = "";

            try
            {
                ManagementObjectSearcher s = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                // for get all CPU info ,using the get() method of Searcher 
                // get() return all CPUs collection
                foreach (ManagementObject cpu in s.Get())
                {
                    System.Windows.Forms.Application.DoEvents();
                    // show each cpu's Infomation
                    foreach (PropertyData pd in cpu.Properties)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        //Console.WriteLine(pd.Name + "/" + pd.Type + "/" + pd.Value);
                        //show each Propertie's Name, Type and Value of current cpu
                        ReturnCPU += pd.Name + "," + pd.Value + ";";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("ProcessorInfo:{0}", ex.Message));
            }
            return ReturnCPU;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String CPUtprt()
        {
            Double CPUtprt = 0;

            if (!CPUtprtFl) return CPUtprt.ToString();
            try
            {//CPU温度
                using (ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\WMI", "Select * From MSAcpi_ThermalZoneTemperature"))
                {
                    foreach (ManagementObject mo in mos.Get())
                    {
                        System.Windows.Forms.Application.DoEvents();
                        CPUtprt = Convert.ToDouble(Convert.ToDouble(mo.GetPropertyValue("CurrentTemperature").ToString()) - 2732) / 10;
                        CPUtprtlabel.Text = ("CPU Temperature : " + CPUtprt.ToString() + " °C");
                    }
                    mos.Dispose();
                }
            }
            catch (ManagementException mex)
            {
                CPUtprtlabel.Text = mex.Message;
                CPUtprtlabel.Text = ("CPU Temperature : " + CPUtprt.ToString() + " °C");
                //Console.WriteLine(String.Format("CPUtprt:{0}", mex.ToString()));
                CPUtprtFl = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("CPUtprt:{0}", ex.Message));
                CPUtprtFl = false;
            }
            return CPUtprt.ToString();
        }

        public String HDtprt()
        {
            String HDtprt = "0";

            if (!HDtprtFl) return HDtprt;
            try
            {//HD温度
                using (ManagementObjectSearcher hos = new ManagementObjectSearcher(@"root\WMI", "Select * From MSStorageDriver_ATAPISmartData"))
                {
                    foreach (System.Management.ManagementObject ho in hos.Get())
                    {
                        System.Windows.Forms.Application.DoEvents();

                        byte[] data = (byte[])ho.GetPropertyValue("VendorSpecific");
                        HDtprt = data[3].ToString();
                        HDtprtlabel.Text = ("HD Temperature : " + HDtprt + " °C");
                    }
                    hos.Dispose();
                }
            }
            catch (ManagementException mex)
            {
                HDtprtlabel.Text = mex.Message;
                HDtprtlabel.Text = ("HD Temperature : " + HDtprt + " °C");
                // Console.WriteLine(String.Format("HDtprt:{0}", mex.Message));
                HDtprtFl = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("HDtprt:{0}", ex.Message));
                HDtprtFl = false;
            }
            return HDtprt;
        }

        public String ProcessorId()
        {
            String ProcessorIdQ = "";
            ProcessorIdlabel.Text = ProcessorIdQ;
            try
            {
                foreach (ManagementObject mObject in moSearch.Get())
                {
                    System.Windows.Forms.Application.DoEvents();
                    //this.lbCpuID.Items.Add(mObject["ProcessorId"].ToString());  
                    ProcessorIdQ = mObject["ProcessorId"].ToString();
                    ProcessorIdlabel.Text = "Processor Id : " + ProcessorIdQ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("ProcessorId:{0}", ex.Message));
            }
            return ProcessorIdQ;
        }

        private void CpuRamUsage_Load(object sender, EventArgs e)
        {
            cpuUsageChart.Series.Clear();
            cpuUsageChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;

            System.Windows.Forms.DataVisualization.Charting.Series series = this.cpuUsageChart.Series.Add("Processor Time");
            System.Windows.Forms.DataVisualization.Charting.Series series1 = this.cpuUsageChart.Series.Add("Privileged Time");
            System.Windows.Forms.DataVisualization.Charting.Series series2 = this.cpuUsageChart.Series.Add("Memory In Use");
            cpuUsageChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            cpuUsageChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            cpuUsageChart.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series.Points.Add(0);
            series1.Points.Add(1);
            series2.Points.Add(2);
            cpuUsageChart.Series[0].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            cpuUsageChart.Series[0].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            cpuUsageChart.Series[0].IsXValueIndexed = false;
            cpuUsageChart.Series[1].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            cpuUsageChart.Series[1].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            cpuUsageChart.Series[1].IsXValueIndexed = false;
            cpuUsageChart.Series[2].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            cpuUsageChart.Series[2].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            cpuUsageChart.Series[2].IsXValueIndexed = false;

            cpuUsageChart.ResetAutoValues();
            cpuUsageChart.ChartAreas[0].AxisY.Maximum = 100;//Max Y 
            cpuUsageChart.ChartAreas[0].AxisY.Minimum = 0;
            cpuUsageChart.ChartAreas[0].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            //cpuUsageChart.ChartAreas[0].AxisY.Title = "CPU Total %";
            cpuUsageChart.ChartAreas[0].AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            //cpuUsageChart.ChartAreas[1].AxisY.Maximum = 100;//Max Y 
            //cpuUsageChart.ChartAreas[1].AxisY.Minimum = 0;
            //cpuUsageChart.ChartAreas[1].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            //cpuUsageChart.ChartAreas[1].AxisY.Title = "RAM Total %";
            //cpuUsageChart.ChartAreas[1].AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            ProcessorId();
        }
    }
}