namespace Game.Controller
{
    partial class CpuRamUsage
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

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.performanceCounter2 = new System.Diagnostics.PerformanceCounter();
            this.cpuUsageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.performanceCounter3 = new System.Diagnostics.PerformanceCounter();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.CPUtprtlabel = new System.Windows.Forms.Label();
            this.ProcessorIdlabel = new System.Windows.Forms.Label();
            this.HDtprtlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsageChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter3)).BeginInit();
            this.SuspendLayout();
            // 
            // performanceCounter1
            // 
            this.performanceCounter1.CategoryName = "Processor";
            this.performanceCounter1.CounterName = "% Processor Time";
            this.performanceCounter1.InstanceName = "_Total";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.BackColor = System.Drawing.SystemColors.Window;
            this.progressBar1.Location = new System.Drawing.Point(3, 235);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(316, 13);
            this.progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Processor Time: 0%";
            // 
            // progressBar2
            // 
            this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar2.BackColor = System.Drawing.SystemColors.Window;
            this.progressBar2.Location = new System.Drawing.Point(3, 256);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(316, 13);
            this.progressBar2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Processor Time: 0%";
            // 
            // performanceCounter2
            // 
            this.performanceCounter2.CategoryName = "Memory";
            this.performanceCounter2.CounterName = "% Committed Bytes In Use";
            // 
            // cpuUsageChart
            // 
            this.cpuUsageChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cpuUsageChart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.cpuUsageChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.cpuUsageChart.Legends.Add(legend1);
            this.cpuUsageChart.Location = new System.Drawing.Point(3, 14);
            this.cpuUsageChart.Name = "cpuUsageChart";
            this.cpuUsageChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            this.cpuUsageChart.Series.Add(series1);
            this.cpuUsageChart.Series.Add(series2);
            this.cpuUsageChart.Series.Add(series3);
            this.cpuUsageChart.Size = new System.Drawing.Size(449, 215);
            this.cpuUsageChart.TabIndex = 2;
            // 
            // performanceCounter3
            // 
            this.performanceCounter3.CategoryName = "Processor";
            this.performanceCounter3.CounterName = "% Privileged Time";
            this.performanceCounter3.InstanceName = "_Total";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Processor Time: 0%";
            // 
            // progressBar3
            // 
            this.progressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar3.BackColor = System.Drawing.SystemColors.Window;
            this.progressBar3.Location = new System.Drawing.Point(3, 275);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(316, 13);
            this.progressBar3.TabIndex = 3;
            // 
            // CPUtprtlabel
            // 
            this.CPUtprtlabel.AutoSize = true;
            this.CPUtprtlabel.BackColor = System.Drawing.Color.Transparent;
            this.CPUtprtlabel.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CPUtprtlabel.Location = new System.Drawing.Point(167, 0);
            this.CPUtprtlabel.Name = "CPUtprtlabel";
            this.CPUtprtlabel.Size = new System.Drawing.Size(109, 11);
            this.CPUtprtlabel.TabIndex = 5;
            this.CPUtprtlabel.Text = "CPU Temperature 00 oC";
            // 
            // ProcessorIdlabel
            // 
            this.ProcessorIdlabel.AutoSize = true;
            this.ProcessorIdlabel.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ProcessorIdlabel.Location = new System.Drawing.Point(1, 0);
            this.ProcessorIdlabel.Name = "ProcessorIdlabel";
            this.ProcessorIdlabel.Size = new System.Drawing.Size(56, 11);
            this.ProcessorIdlabel.TabIndex = 6;
            this.ProcessorIdlabel.Text = "Processor Id";
            // 
            // HDtprtlabel
            // 
            this.HDtprtlabel.AutoSize = true;
            this.HDtprtlabel.BackColor = System.Drawing.Color.Transparent;
            this.HDtprtlabel.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.HDtprtlabel.Location = new System.Drawing.Point(307, 0);
            this.HDtprtlabel.Name = "HDtprtlabel";
            this.HDtprtlabel.Size = new System.Drawing.Size(103, 11);
            this.HDtprtlabel.TabIndex = 7;
            this.HDtprtlabel.Text = "HD Temperature 00 oC";
            // 
            // CpuRamUsage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.HDtprtlabel);
            this.Controls.Add(this.ProcessorIdlabel);
            this.Controls.Add(this.CPUtprtlabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.cpuUsageChart);
            this.Controls.Add(this.progressBar1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CpuRamUsage";
            this.Size = new System.Drawing.Size(455, 291);
            this.Load += new System.EventHandler(this.CpuRamUsage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsageChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        public System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Diagnostics.PerformanceCounter performanceCounter2;
        private System.Windows.Forms.DataVisualization.Charting.Chart cpuUsageChart;
        private System.Windows.Forms.Label label3;
        public System.Diagnostics.PerformanceCounter performanceCounter3;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.ProgressBar progressBar2;
        public System.Windows.Forms.ProgressBar progressBar3;
        public System.Windows.Forms.Label CPUtprtlabel;
        public System.Windows.Forms.Label ProcessorIdlabel;
        public System.Windows.Forms.Label HDtprtlabel;
    }
}