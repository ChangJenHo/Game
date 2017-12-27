namespace Game.Controller
{
    partial class VistaCPUInfo
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerCPU = new System.Windows.Forms.Timer(this.components);
            this.timerMem = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSkin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClassic = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCoolBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerCPU
            // 
            this.timerCPU.Enabled = true;
            this.timerCPU.Tick += new System.EventHandler(this.timerCPU_Tick);
            // 
            // timerMem
            // 
            this.timerMem.Enabled = true;
            this.timerMem.Tick += new System.EventHandler(this.timerMem_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSkin});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 26);
            // 
            // mnuSkin
            // 
            this.mnuSkin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClassic,
            this.mnuCoolBlack});
            this.mnuSkin.Name = "mnuSkin";
            this.mnuSkin.Size = new System.Drawing.Size(112, 22);
            this.mnuSkin.Text = "Skin(&S)";
            // 
            // mnuClassic
            // 
            this.mnuClassic.Name = "mnuClassic";
            this.mnuClassic.Size = new System.Drawing.Size(148, 22);
            this.mnuClassic.Text = "Classic(&C)";
            this.mnuClassic.Click += new System.EventHandler(this.mnuClassic_Click);
            // 
            // mnuCoolBlack
            // 
            this.mnuCoolBlack.Name = "mnuCoolBlack";
            this.mnuCoolBlack.Size = new System.Drawing.Size(148, 22);
            this.mnuCoolBlack.Text = "CoolBlack(&O)";
            this.mnuCoolBlack.Click += new System.EventHandler(this.mnuCoolBlack_Click);
            // 
            // VistaCPUInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "VistaCPUInfo";
            this.Size = new System.Drawing.Size(202, 172);
            this.Load += new System.EventHandler(this.VistaCPUInfo_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.VistaCPUInfo_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VistaCPUInfo_MouseUp);
            this.Resize += new System.EventHandler(this.VistaCPUInfo_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerCPU;
        private System.Windows.Forms.Timer timerMem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuSkin;
        private System.Windows.Forms.ToolStripMenuItem mnuClassic;
        private System.Windows.Forms.ToolStripMenuItem mnuCoolBlack;
    }
}