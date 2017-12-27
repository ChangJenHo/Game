namespace Game
{
    partial class SocketClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SocketClient));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.IPNo = new System.Windows.Forms.ToolStripTextBox();
            this.PortNo = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSend = new System.Windows.Forms.ToolStripButton();
            this.SendData = new System.Windows.Forms.ToolStripTextBox();
            this.rtbe1 = new Game.Controller.RTBE();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 354);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(742, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.rtbe1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(742, 299);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(742, 354);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IPNo,
            this.PortNo,
            this.toolStripButtonStart,
            this.toolStripButtonSend,
            this.SendData});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(739, 55);
            this.toolStrip1.TabIndex = 1;
            // 
            // IPNo
            // 
            this.IPNo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IPNo.MaxLength = 15;
            this.IPNo.Name = "IPNo";
            this.IPNo.Size = new System.Drawing.Size(200, 55);
            this.IPNo.Text = "127.0.0.1";
            this.IPNo.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PortNo
            // 
            this.PortNo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PortNo.MaxLength = 5;
            this.PortNo.Name = "PortNo";
            this.PortNo.Size = new System.Drawing.Size(100, 55);
            this.PortNo.Text = "8000";
            this.PortNo.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.BackColor = System.Drawing.Color.Red;
            this.toolStripButtonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripButtonStart.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButtonStart.ForeColor = System.Drawing.Color.White;
            this.toolStripButtonStart.Image = global::Game.Properties.Resources.Red;
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Red;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(141, 52);
            this.toolStripButtonStart.Text = "Start Connect";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonSend
            // 
            this.toolStripButtonSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.toolStripButtonSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripButtonSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSend.Enabled = false;
            this.toolStripButtonSend.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButtonSend.ForeColor = System.Drawing.Color.White;
            this.toolStripButtonSend.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSend.Image")));
            this.toolStripButtonSend.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.toolStripButtonSend.Name = "toolStripButtonSend";
            this.toolStripButtonSend.Size = new System.Drawing.Size(72, 52);
            this.toolStripButtonSend.Text = "Send Data";
            this.toolStripButtonSend.Click += new System.EventHandler(this.toolStripButtonSend_Click);
            // 
            // SendData
            // 
            this.SendData.Name = "SendData";
            this.SendData.Size = new System.Drawing.Size(300, 23);
            // 
            // rtbe1
            // 
            this.rtbe1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbe1.FontName = "新細明體";
            this.rtbe1.Location = new System.Drawing.Point(0, 0);
            this.rtbe1.Margin = new System.Windows.Forms.Padding(0);
            this.rtbe1.Name = "rtbe1";
            this.rtbe1.Size = new System.Drawing.Size(742, 299);
            this.rtbe1.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "...";
            // 
            // SocketClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SocketClient";
            this.Size = new System.Drawing.Size(742, 376);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox IPNo;
        private System.Windows.Forms.ToolStripTextBox PortNo;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonSend;
        private System.Windows.Forms.ToolStripTextBox SendData;
        private Game.Controller.RTBE rtbe1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
