using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Game.Controller
{
    public partial class VistaCPUInfo : UserControl
    {
        public VistaCPUInfo()
        {
            InitializeComponent();
        }
        StringFormat format = new StringFormat();
        PerformanceCounter pc = null;
        bool firstRun = true;
        float percentOfCPU = 0, percentOfMemory = 0;  //CPU和內存分別所佔百分比，0～99  
        float cpuAimAngle = 0, memAimAngle = 0, cpuCurAngle = 0, memCurAngle = 0;

        [DllImport("kernel32")]
        public static extern void GetWindowsDirectory(StringBuilder WinDir, int count);

        [DllImport("kernel32")]
        public static extern void GetSystemDirectory(StringBuilder SysDir, int count);

        [DllImport("kernel32")]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);

        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
		[DllImport("kernel32.dll")]
        public static extern void GlobalMemoryStatusEx(ref MEMORYSTATUSEX stat); 

        [DllImport("kernel32")]
        public static extern void GetSystemTime(ref SYSTEMTIME_INFO stinfo);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetDllDirectory(string path);
		[StructLayout(LayoutKind.Sequential)]
		public struct MEMORYSTATUSEX
		{
			public uint dwLength;
			public uint dwMemoryLoad;
			public ulong ullTotalPhys;
			public ulong ullAvailPhys;
			public ulong ullTotalPageFile;
			public ulong ullAvailPageFile;
			public ulong ullTotalVirtual;
			public ulong ullAvailVirtual;
			public ulong ullAvailExtendedVirtual;
		}
        //定義內存的信息結構  
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }
        //定義CPU的信息結構  
        [StructLayout(LayoutKind.Sequential)]
        public struct CPU_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }
        //義系統時間的信息結構 
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME_INFO
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }
        private CustomRectangle positionRect = new CustomRectangle();
        /// <summary>
        /// 時鐘的位置矩形
        /// </summary>
        public CustomRectangle PositionRect
        {
            get { return positionRect; }
            set { positionRect = value; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (pc == null) pc = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            float cpu = 30f, mem = 0f;

            if (!firstRun) cpu = (float)pc.NextValue();

			
			
			
			
			
			MEMORYSTATUSEX stat =new MEMORYSTATUSEX();
            stat.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            GlobalMemoryStatusEx(ref stat);
			
			//long ram = (long)stat.ullAvailPhys/1024/1024;
   //         Console.WriteLine(stat.ullAvailPhys/1024/1024);
   //         Console.WriteLine(stat.ullTotalPhys / 1024 / 1024);
   //         Console.WriteLine(stat.ullTotalVirtual / 1024 / 1024/1024);
			
			
            MEMORY_INFO MemInfo = new MEMORY_INFO();
            MemInfo.dwLength = (uint)Marshal.SizeOf(typeof(MEMORY_INFO));
            GlobalMemoryStatus(ref MemInfo);
            mem = (float)MemInfo.dwMemoryLoad;
			//Console.WriteLine(MemInfo.dwAvailPhys / 1024/1024);
            if (firstRun || percentOfCPU != cpu)
            {
                percentOfCPU = cpu;
                float i = (float)Math.Round(percentOfCPU * 2.5) - 125f;
                timerCPU.Enabled = false;
                if ((i >= -360) && (i <= 360))
                    cpuAimAngle = i;
                else
                    cpuAimAngle = 0;
                timerCPU.Enabled = true;
            }
            if (firstRun || percentOfMemory != mem)
            {
                percentOfMemory = mem;
                timerMem.Enabled = true;
                float i = (float)Math.Round(percentOfMemory * 2.5) - 125f;
                timerMem.Enabled = false;
                memAimAngle = i;
                timerMem.Enabled = true;
            }
            if (firstRun) firstRun = false;
        }

        private void timerCPU_Tick(object sender, EventArgs e)
        {
            float i;

            if (cpuCurAngle > cpuAimAngle)
            {
                float intRotateStep = (float)Math.Max(1, Math.Round(Math.Abs(Math.Abs(cpuAimAngle) - Math.Abs(cpuCurAngle)) / 10d));
                i = cpuCurAngle - intRotateStep;
                if (i <= cpuAimAngle)
                {
                    timerCPU.Enabled = false;
                    cpuCurAngle = cpuAimAngle;
                }
                else
                    cpuCurAngle = i;
            }
            else
            {
                float intRotateStep = (float)Math.Max(1, Math.Round(Math.Abs(Math.Abs(cpuAimAngle) - Math.Abs(cpuCurAngle)) / 10d));
                i = cpuCurAngle + intRotateStep;
                if (i >= cpuAimAngle)
                {
                    timerCPU.Enabled = false;
                    cpuCurAngle = cpuAimAngle;
                }
                else
                {
                    if ((i >= -360) && (i <= 360)) cpuCurAngle = i;
                }
            }
            this.Invalidate(positionRect.ToRectangle());
        }

        private void timerMem_Tick(object sender, EventArgs e)
        {
            float i;

            if (memCurAngle > memAimAngle)
            {
                float intRotateStep = (float)Math.Max(1, Math.Round(Math.Abs(Math.Abs(memAimAngle) - Math.Abs(memCurAngle)) / 10d));
                i = memCurAngle - intRotateStep;
                if (i <= memAimAngle)
                {
                    timerMem.Enabled = false;
                    memCurAngle = memAimAngle;
                }
                else
                    memCurAngle = i;
            }
            else
            {
                float intRotateStep = (float)Math.Max(1, Math.Round(Math.Abs(Math.Abs(memAimAngle) - Math.Abs(memCurAngle)) / 10d));
                i = memCurAngle + intRotateStep;
                if (i >= memAimAngle)
                {
                    timerMem.Enabled = false;
                    memCurAngle = memAimAngle;
                }
                else
                {
                    if ((i >= -360) && (i <= 360)) memCurAngle = i;
                }
            }
            //this.Invalidate();
        }
        public enum VistaCPUInfoStyle
        {
            Classic, CoolBlack
        }
        private VistaCPUInfoStyle style = VistaCPUInfoStyle.Classic;
        public VistaCPUInfoStyle Style
        {
            get { return style; }
            set { style = value; }
        }

        private void mnuClassic_Click(object sender, EventArgs e)
        {
            this.style = VistaCPUInfoStyle.Classic;
            this.Invalidate();
        }

        private void mnuCoolBlack_Click(object sender, EventArgs e)
        {
            this.style = VistaCPUInfoStyle.CoolBlack;
            this.Invalidate();
        }
        private Image Image
        {
            get
            {
                if (style == VistaCPUInfoStyle.CoolBlack)
                    return global::Game.Properties.Resources.back_cool_lrg;
                else
                    return global::Game.Properties.Resources.back_lrg;
            }
        }
        private Image ImageDial
        {
            get
            {
                if (style == VistaCPUInfoStyle.CoolBlack)
                    return global::Game.Properties.Resources.dial_cool_lrg;
                else
                    return global::Game.Properties.Resources.dial_lrg;
            }
        }
        private Image ImageDialDot
        {
            get
            {
                if (style == VistaCPUInfoStyle.CoolBlack)
                    return global::Game.Properties.Resources.dialdot_cool_lrg;
                else
                    return global::Game.Properties.Resources.dialdot_lrg;
            }
        }
        private Image ImageDialSmall
        {
            get
            {
                if (style == VistaCPUInfoStyle.CoolBlack)
                    return global::Game.Properties.Resources.dial_cool_lrg_sml;
                else
                    return global::Game.Properties.Resources.dial_lrg_sml;
            }
        }
        private Image ImageGlass
        {
            get
            {
                if (style == VistaCPUInfoStyle.CoolBlack)
                    return global::Game.Properties.Resources.glass_cool_lrg;
                else
                    return global::Game.Properties.Resources.glass_lrg;
            }
        }
        Font textFont = new Font("Arial", 8, FontStyle.Bold);
        SolidBrush textBrush = new SolidBrush(Color.White);

        private void VistaCPUInfo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            //背景
            e.Graphics.DrawImage(Image, (int)positionRect.X, (int)positionRect.Y, 198, 159);
            //指針
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(positionRect.X + 68f, positionRect.Y + 82f);
            e.Graphics.RotateTransform(cpuCurAngle);
            e.Graphics.DrawImage(ImageDial, -5, -49, 10, 98);
            e.Graphics.ResetTransform();
            e.Graphics.TranslateTransform(positionRect.X + 143f, positionRect.Y + 50f);
            e.Graphics.RotateTransform(memCurAngle);
            e.Graphics.DrawImage(ImageDialSmall, -5, -35, 10, 70);
            e.Graphics.ResetTransform();
            //蓋板
            e.Graphics.DrawImage(ImageDialDot, (int)positionRect.X, (int)positionRect.Y, 198, 150);
            //文字
            RectangleF rect = new RectangleF((int)positionRect.X + 53, (int)positionRect.Y + 107, 35, 15);
            e.Graphics.DrawString(((int)percentOfCPU).ToString() + "%", textFont, textBrush, rect, format);
            rect = new RectangleF((int)positionRect.X + 127, (int)positionRect.Y + 66, 35, 13);
            e.Graphics.DrawString(((int)percentOfMemory).ToString() + "%", textFont, textBrush, rect, format);
            //玻璃
            e.Graphics.DrawImage(ImageGlass, (int)positionRect.X, (int)positionRect.Y, 198, 159);
        }

        private void VistaCPUInfo_Resize(object sender, EventArgs e)
        {
            if (positionRect == null) positionRect = new CustomRectangle();
            positionRect.Width = 202;
            positionRect.Height = 159;
            positionRect.Top = (int)(this.ClientSize.Height < 159 ? 0 : (this.ClientSize.Height - 159) / 2f);
            positionRect.Left = (int)(this.ClientSize.Width < 202 ? 0 : (this.ClientSize.Width - 202) / 2f);
            if (!this.timer.Enabled) this.timer.Enabled = true;
            this.Invalidate();
        }

        private void VistaCPUInfo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) contextMenuStrip1.Show(this, e.Location);
        }

        private void VistaCPUInfo_Load(object sender, EventArgs e)
        {
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
        }
    }
    public class CustomRectangle
    {
        private bool disposed = false;
        public CustomRectangle()
        {
        }
        ~CustomRectangle()
        {

            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).釋放其他狀態（管理對象）。
                }
                // Free your own state (unmanaged objects).釋放你自己的狀態（非託管對象）。
                // Set large fields to null.大集字段設置為null。
                disposed = true;
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        private float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        private float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float Left
        {
            get { return x; }
            set { x = value; }
        }
        public float Top
        {
            get { return y; }
            set { y = value; }
        }
        private float width;
        public float Width
        {
            get { return width; }
            set { width = value; }
        }
        private float height;
        public float Height
        {
            get { return height; }
            set { height = value; }
        }
        public CustomRectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public float Right
        {
            get { return x + width; }
        }
        public float Bottom
        {
            get { return y + height; }
        }
        public CustomPoint CenterPoint
        {
            get { return new CustomPoint(x + width / 2, y + height / 2); }
        }

        public CustomRectangle Clone()
        {
            return new CustomRectangle(x, y, width, height);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        public RectangleF ToRectangleF()
        {
            return new RectangleF(x, y, width, height);
        }

        public Point ToPoint()
        {
            return new Point((int)x, (int)y);
        }

        public PointF ToPointF()
        {
            return new PointF(x, y);
        }

        public bool IsPointInRectangle(int ptX, int ptY)
        {
            return (ptX >= x && ptX <= (x + width) && ptY >= y && ptY <= (y + height));
        }

        public bool IsPointFInRectangle(float ptX, float ptY)
        {
            return (ptX >= x && ptX <= (x + width) && ptY >= y && ptY <= (y + height));
        }

        public static CustomRectangle ToCustomRectangle(RectangleF re)
        {
            CustomRectangle cus = new CustomRectangle();
            cus.X = re.X;
            cus.Y = re.Y;
            cus.Width = re.Width;
            cus.Height = re.Height;
            return cus;
        }

        public static CustomRectangle FromRectangle(Rectangle rect)
        {
            return new CustomRectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static CustomRectangle FromRectangleF(RectangleF rect)
        {
            return new CustomRectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public GraphicsPath GetRoundRectBorderPath(float radus)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, radus * 2, radus * 2, 180, 90);
            path.AddArc(Right - radus * 2, y, radus * 2, radus * 2, 270, 90);
            path.AddArc(Right - radus * 2, Bottom - radus * 2, radus * 2, radus * 2, 0, 90);
            path.AddArc(x, Bottom - radus * 2, radus * 2, radus * 2, 90, 90);
            path.CloseFigure();
            return path;
        }

        public GraphicsPath GetHexagonBorderPath()
        {
            GraphicsPath path = new GraphicsPath();
            List<PointF> pts = new List<PointF>();
            pts.Add(new PointF(x + width / 2f, y));
            pts.Add(new PointF(Right, y + height / 4f));
            pts.Add(new PointF(Right, y + height * 3f / 4f));
            pts.Add(new PointF(x + width / 2f, Bottom));
            pts.Add(new PointF(x, y + height * 3f / 4f));
            pts.Add(new PointF(x, y + height / 4f));
            path.AddPolygon(pts.ToArray());
            return path;
        }

        public GraphicsPath GetTrianglePath()
        {
            GraphicsPath path = new GraphicsPath();
            List<PointF> pts = new List<PointF>();
            pts.Add(new PointF(x, y));
            pts.Add(new PointF(Right, y));
            pts.Add(new PointF(x + width / 2f, Bottom));
            path.AddPolygon(pts.ToArray());
            return path;
        }

        public int GetHotPosition(Point pt)
        {
            Point pt1, pt2, pt3, pt4, pt0;
            pt1 = new Point((int)(x + width / 6), (int)(y + height / 6));
            pt2 = new Point((int)(Right - width / 6), (int)(y + height / 6));
            pt3 = new Point((int)(Right - width / 6), (int)(Bottom - height / 6));
            pt4 = new Point((int)(x + width / 6), (int)(Bottom - height / 6));
            pt0 = CenterPoint.ToPoint();

            GraphicsPath path1 = new GraphicsPath();
            path1.AddPolygon(new Point[] { pt1, pt2, pt0 });
            if (path1.IsVisible(pt)) return 1;

            GraphicsPath path2 = new GraphicsPath();
            path2.AddPolygon(new Point[] { pt2, pt3, pt0 });
            if (path2.IsVisible(pt)) return 2;

            GraphicsPath path3 = new GraphicsPath();
            path3.AddPolygon(new Point[] { pt3, pt4, pt0 });
            if (path3.IsVisible(pt)) return 3;

            GraphicsPath path4 = new GraphicsPath();
            path4.AddPolygon(new Point[] { pt4, pt1, pt0 });
            if (path4.IsVisible(pt)) return 4;

            return -1;
        }

        public Rectangle GetSubRectangle(int hotPosition)
        {
            if (hotPosition == 1)
                return new Rectangle((int)x, (int)y, (int)width, (int)(height / 2));
            else if (hotPosition == 2)
                return new Rectangle((int)(x + width / 2), (int)y, (int)(width / 2), (int)height);
            else if (hotPosition == 3)
                return new Rectangle((int)x, (int)(y + height / 2), (int)width, (int)(height / 2));
            else if (hotPosition == 4)
                return new Rectangle((int)x, (int)y, (int)(width / 2), (int)height);
            else
                return ToRectangle();
        }
    }
    public class CustomPoint
    {
        private bool disposed = false;
        private float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        private float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public CustomPoint()
        {
            this.x = 0;
            this.y = 0;
        }
        public CustomPoint(int x, int y)
        {
            this.x = (float)x;
            this.y = (float)y;
        }
        public CustomPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        ~CustomPoint()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).釋放其他狀態（管理對象）。
                }
                // Free your own state (unmanaged objects).釋放你自己的狀態（非託管對象）。
                // Set large fields to null.大集字段設置為null。
                disposed = true;
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        private Color backColor = Color.FromArgb(0, 255, 0);
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
        private bool selected = false;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public CustomRectangle PositionRect
        {
            get { return new CustomRectangle(x - 3, y - 3, 6, 6); }
        }

        public CustomPoint Clone()
        {
            CustomPoint pt = new CustomPoint(x, y);
            pt.backColor = backColor;
            return pt;
        }

        private void DrawBorderHandler(Graphics g, CustomRectangle rect, Color clr)
        {
            g.FillRectangle(new SolidBrush(clr), rect.ToRectangleF());
            g.DrawRectangle(new Pen(Color.Black), rect.ToRectangle());
        }

        public void Draw(Graphics g)
        {
            DrawBorderHandler(g, PositionRect, backColor);
            if (selected)
            {
                //選中時邊框變
                CustomRectangle rect = new CustomRectangle(x - 4, y - 4, 8, 8);
                g.DrawRectangle(new Pen(Color.Black), rect.ToRectangle());
            }
        }

        public void DrawDiamond(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.Default;

            PointF[] pts = new PointF[] { new PointF(x, y - 4), new PointF(x - 4, y), new PointF(x, y + 4), new PointF(x + 4, y) };
            g.FillPolygon(new SolidBrush(backColor), pts);
            g.DrawPolygon(new Pen(Color.Black), pts);
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public bool MouseHitTest(PointF pt)
        {
            CustomRectangle rect = new CustomRectangle(x - 5, y - 5, 10, 10);   //判斷選中時加點誤差，使選中更加順利
            return rect.IsPointFInRectangle(pt.X, pt.Y);
        }

        public static CustomPoint FromPoint(Point pt)
        {
            return new CustomPoint(pt.X, pt.Y);
        }

        public static CustomPoint FromPointF(PointF pt)
        {
            return new CustomPoint(pt.X, pt.Y);
        }

        public Point ToPoint()
        {
            return new Point((int)x, (int)y);
        }

        public PointF ToPointF()
        {
            return new PointF(x, y);
        }
    }
}