using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Game.Controller
{
    [Flags]
    public enum BatteryChargeStatus : byte
    {
        High = 1,
        Low = 2,
        Critical = 4,
        Charging = 8,
        NoSystemBattery = 128,
        Unknown = 255
    }
    public enum PowerLineStatus : byte
    {
        Offline = 0,
        Online = 1,
        Unknown = 255
    }

    public partial class PowerStatusShow : UserControl
    {
        public PowerStatusShow()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                System.Management.ManagementClass smmc = new System.Management.ManagementClass("Win32_Battery");
                System.Management.ManagementObjectCollection smmoc = smmc.GetInstances();
                foreach (System.Management.ManagementObject mo in smmoc)
                {
                    label1.Text = String.Format("{0} {1}", mo["Name"], mo["Description"]);
                    label3.Text = String.Format("Design voltage:{0} mV", mo["DesignVoltage"]);
                    label4.Text = String.Format("Full Charge Capacity:{0} mWh", mo["FullChargeCapacity"]);
                    label5.Text = String.Format("Battery remaining:{0}%", mo["EstimatedChargeRemaining"]);
                    progressBar1.Value = int.Parse(mo["EstimatedChargeRemaining"].ToString());
                    switch ((UInt16)mo["BatteryStatus"])
                    {
                        case 1:
                            label2.Text = "Discharging ";
                            break;
                        case 2:
                            label2.Text = "AC power supply ";
                            break;
                        case 3:
                            label2.Text = "Charging complete ";
                            break;
                        case 4:
                            label2.Text = "low ";
                            break;
                        case 5:
                            label2.Text = "lowest ";
                            break;
                        case 6:
                            label2.Text = "charging ";
                            break;
                        case 7:
                            label2.Text = "Charging / High ";
                            break;
                        case 8:
                            label2.Text = "Charging / Low ";
                            break;
                        case 9:
                            label2.Text = "Charging / Lowest ";
                            break;
                        case 10:
                            label2.Text = "undefined ";
                            break;
                        case 11:
                            label2.Text = "partially charged ";
                            break;
                    }
                    //Console.Write("Battery type:");
                    switch ((UInt16)mo["Chemistry"])
                    {
                        case 1:
                            label2.Text += "Other";
                            break;
                        case 2:
                            label2.Text += "unknown";
                            break;
                        case 3:
                            label2.Text += "Lead-acid battery (lead acid battery)";
                            break;
                        case 4:
                            label2.Text += "Nickel-cadmium storage battery (Ni - Cd)";
                            break;
                        case 5:
                            label2.Text += "Ni-MH rechargeable battery (Ni-MH)";
                            break;
                        case 6:
                            label2.Text += "Li-ion battery (LiB)";
                            break;
                        case 7:
                            label2.Text += "air zinc battery";
                            break;
                        case 8:
                            label2.Text += "lithium polymer battery (Lipo)";
                            break;
                    }
                }
                smmoc.Dispose();
                smmc.Dispose();
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                progressBar1.Visible = false;
                tableLayoutPanel1.Visible = false;
            }
        }
        private void ACpowerStatus()
        {
            // Create a ManagementClass object
            System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_Battery");
            // Get the ManagementObjectCollection object
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                // Display battery information
                // (Here are some of the information you can get here)
                // (Not all information can be acquired)
                Console.WriteLine("Name: {0}", mo["Name"]);
                Console.WriteLine("Device ID: {0}", mo["DeviceID"]);
                Console.WriteLine("Description: {0}", mo["Description"]);
                Console.WriteLine("Battery remaining: {0}%", mo["EstimatedChargeRemaining"]);
                Console.WriteLine("Battery remaining time: {0} minutes", mo["EstimatedRunTime"]);
                Console.WriteLine("Design Capacity: {0} mWh", mo["DesignCapacity"]);
                Console.WriteLine("Design voltage: {0} mV", mo["DesignVoltage"]);
                Console.WriteLine("Full charge capacity: {0} mVh", mo["FullChargeCapacity"]);
                Console.WriteLine("Time required for full charge: {0} minutes", mo["TimeToFullCharge"]);
                Console.WriteLine("Time taken from 0 to full charge: {0} minutes", mo["MaxRechargeTime"]);
                Console.WriteLine("Battery usage time: {0} seconds", mo["TimeOnBattery"]);
                Console.Write("Battery status:");
                switch ((UInt16)mo["BatteryStatus"])
                {
                    case 1:
                        Console.WriteLine("Discharging");
                        break;
                    case 2:
                        Console.WriteLine("AC power supply");
                        break;
                    case 3:
                        Console.WriteLine("Charging complete");
                        break;
                    case 4:
                        Console.WriteLine("low");
                        break;
                    case 5:
                        Console.WriteLine("lowest");
                        break;
                    case 6:
                        Console.WriteLine("charging");
                        break;
                    case 7:
                        Console.WriteLine("Charging / High");
                        break;
                    case 8:
                        Console.WriteLine("Charging / Low");
                        break;
                    case 9:
                        Console.WriteLine("Charging / Lowest");
                        break;
                    case 10:
                        Console.WriteLine("undefined");
                        break;
                    case 11:
                        Console.WriteLine("partially charged");
                        break;
                }

                Console.Write("Battery type:");
                switch ((UInt16)mo["Chemistry"])
                {
                    case 1:
                        Console.WriteLine("Other");
                        break;
                    case 2:
                        Console.WriteLine("unknown");
                        break;
                    case 3:
                        Console.WriteLine("Lead-acid battery (lead acid battery)");
                        break;
                    case 4:
                        Console.WriteLine("Nickel-cadmium storage battery (Ni - Cd)");
                        break;
                    case 5:
                        Console.WriteLine("Ni-MH rechargeable battery (Ni-MH)");
                        break;
                    case 6:
                        Console.WriteLine("Li-ion battery (LiB)");
                        break;
                    case 7:
                        Console.WriteLine("air zinc battery");
                        break;
                    case 8:
                        Console.WriteLine("lithium polymer battery (Lipo)");
                        break;
                }
            }
        }

        private void PowerStatusShow_Load(object sender, EventArgs e)
        {
            switch (SystemInformation.PowerStatus.BatteryChargeStatus)
            {
                case System.Windows.Forms.BatteryChargeStatus.Low:
                    //MessageBox.Show("Battery is running low.", "Low Battery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Console.WriteLine("Battery is running low.");
                    break;
                case System.Windows.Forms.BatteryChargeStatus.Critical:
                    //MessageBox.Show("Battery is critcally low.", "Critical Battery", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Console.WriteLine("Battery is critcally low.");
                    break;
                default:
                    // Battery is okay.
                    progressBar1.Visible = false;
                    tableLayoutPanel1.Visible = false;
                    timer1.Enabled = false;
                    break;
            }
        }
    }
}