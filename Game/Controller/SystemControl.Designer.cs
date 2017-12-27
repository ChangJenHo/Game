namespace Game.Controller
{
    partial class SystemControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemControl));
            Game.Controller.CustomRectangle customRectangle1 = new Game.Controller.CustomRectangle();
            this.netInterfacePerfMonitor1 = new Game.Controller.NetInterfacePerfMonitor();
            this.powerStatus1 = new Game.Controller.PowerStatusShow();
            this.analogClock1 = new Game.Controller.AnalogClock();
            this.cpuRamUsage1 = new Game.Controller.CpuRamUsage();
            this.vistaCPUInfo1 = new Game.Controller.VistaCPUInfo();
            this.SuspendLayout();
            // 
            // netInterfacePerfMonitor1
            // 
            this.netInterfacePerfMonitor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.netInterfacePerfMonitor1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.netInterfacePerfMonitor1.Location = new System.Drawing.Point(638, 0);
            this.netInterfacePerfMonitor1.Name = "netInterfacePerfMonitor1";
            this.netInterfacePerfMonitor1.Size = new System.Drawing.Size(248, 150);
            this.netInterfacePerfMonitor1.TabIndex = 9;
            // 
            // powerStatus1
            // 
            this.powerStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.powerStatus1.BackColor = System.Drawing.Color.Transparent;
            this.powerStatus1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("powerStatus1.BackgroundImage")));
            this.powerStatus1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.powerStatus1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerStatus1.ForeColor = System.Drawing.Color.Black;
            this.powerStatus1.Location = new System.Drawing.Point(885, 0);
            this.powerStatus1.Name = "powerStatus1";
            this.powerStatus1.Size = new System.Drawing.Size(200, 150);
            this.powerStatus1.TabIndex = 8;
            // 
            // analogClock1
            // 
            this.analogClock1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analogClock1.BackColor = System.Drawing.Color.Transparent;
            this.analogClock1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("analogClock1.BackgroundImage")));
            this.analogClock1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.analogClock1.Draw1MinuteTicks = true;
            this.analogClock1.Draw5MinuteTicks = true;
            this.analogClock1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analogClock1.HourHandColor = System.Drawing.Color.DarkMagenta;
            this.analogClock1.Location = new System.Drawing.Point(1088, 0);
            this.analogClock1.MinuteHandColor = System.Drawing.Color.Green;
            this.analogClock1.Name = "analogClock1";
            this.analogClock1.SecondHandColor = System.Drawing.Color.Red;
            this.analogClock1.Size = new System.Drawing.Size(150, 150);
            this.analogClock1.TabIndex = 7;
            this.analogClock1.TicksColor = System.Drawing.Color.Black;
            // 
            // cpuRamUsage1
            // 
            this.cpuRamUsage1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cpuRamUsage1.BackColor = System.Drawing.Color.Transparent;
            this.cpuRamUsage1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuRamUsage1.Location = new System.Drawing.Point(204, 0);
            this.cpuRamUsage1.Name = "cpuRamUsage1";
            this.cpuRamUsage1.Size = new System.Drawing.Size(428, 150);
            this.cpuRamUsage1.TabIndex = 6;
            // 
            // vistaCPUInfo1
            // 
            this.vistaCPUInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vistaCPUInfo1.BackColor = System.Drawing.Color.Transparent;
            this.vistaCPUInfo1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vistaCPUInfo1.Location = new System.Drawing.Point(0, 0);
            this.vistaCPUInfo1.Margin = new System.Windows.Forms.Padding(0);
            this.vistaCPUInfo1.Name = "vistaCPUInfo1";
            customRectangle1.Height = 159F;
            customRectangle1.Left = 0F;
            customRectangle1.Top = 0F;
            customRectangle1.Width = 202F;
            customRectangle1.X = 0F;
            customRectangle1.Y = 0F;
            this.vistaCPUInfo1.PositionRect = customRectangle1;
            this.vistaCPUInfo1.Size = new System.Drawing.Size(200, 150);
            this.vistaCPUInfo1.Style = Game.Controller.VistaCPUInfo.VistaCPUInfoStyle.Classic;
            this.vistaCPUInfo1.TabIndex = 5;
            // 
            // SystemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.netInterfacePerfMonitor1);
            this.Controls.Add(this.powerStatus1);
            this.Controls.Add(this.analogClock1);
            this.Controls.Add(this.cpuRamUsage1);
            this.Controls.Add(this.vistaCPUInfo1);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SystemControl";
            this.Size = new System.Drawing.Size(1240, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private NetInterfacePerfMonitor netInterfacePerfMonitor1;
        private PowerStatusShow powerStatus1;
        private AnalogClock analogClock1;
        private CpuRamUsage cpuRamUsage1;
        private VistaCPUInfo vistaCPUInfo1;
    }
}