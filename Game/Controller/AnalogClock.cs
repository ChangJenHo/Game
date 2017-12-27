using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game.Controller
{
    public partial class AnalogClock : UserControl
    {
        public Boolean DefaultDateTime = true;
        const float PI = 3.141592654F;
        public DateTime dateTime;
        public float fRadius;
        public float fCenterX;
        public float fCenterY;
        public float fCenterCircleRadius;
        public float fHourLength;
        public float fMinLength;
        public float fSecLength;
        public float fHourThickness;
        public float fMinThickness;
        public float fSecThickness;
        public bool bDraw5MinuteTicks = true;
        public bool bDraw1MinuteTicks = true;
        public float fTicksThickness = 1;
        public Color hrColor = Color.DarkMagenta;
        public Color minColor = Color.Green;
        public Color secColor = Color.Red;
        public Color circleColor = Color.Red;
        public Color ticksColor = Color.Black;
        public AnalogClock()
        {
            InitializeComponent();
        }

        private void AnalogClock_Load(object sender, EventArgs e)
        {
            dateTime = DateTime.Now;
            this.AnalogClock_Resize(sender, e);
            this.TimeZones.Items.AddRange(new object[] {
            "UTC-12:00(IDL - International Date Line)",
            "UTC-11:00(MIT - Midway Standard Time)",
            "UTC-10:00(HST - Hawaii - Aleutian Standard Time)",
            "UTC-09:30(MSIT- Marquesas Islands Standard Time)",
            "UTC-09:00(AKST- Alaska Standard Time)",
            "UTC-08:00(PSTA- Pacific Standard Time A)",
            "UTC-07:00(MST - Mountain Standard Time in North America)",
            "UTC-06:00(CST - North American Central Standard Time)",
            "UTC-05:00(EST - North American Eastern Standard Time)",
            "UTC-04:30(RVT - Venezuela Standard Time)",
            "UTC-04:00(AST - Atlantic Standard Time)",
            "UTC-30:30(NST - Newfoundland standard time)",
            "UTC-03:00(SAT - South America Standard Time)",
            "UTC-02:00(BRT - Brazil Time)",
            "UTC-01:00(CVT - Cape Verde Standard Time)",
            "UTC 00:00(WET - Western Europe time zone, GMT- GMT)",
            "UTC+01:00(CET - Central European Time Zone)",
            "UTC+02:00(EET - Eastern European Time Zone)",
            "UTC+03:00(MSK - Moscow time zone)",
            "UTC+03:30(IRT - Iran Standard Time)",
            "UTC+04:00(META- Middle East time zone A)",
            "UTC+04:30(AFT - Afghanistan Standard Time)",
            "UTC+05:00(METB- time in the Middle East region B)",
            "UTC+05:30(IDT - India Standard Time)",
            "UTC+05:45(NPT - Nepal Standard Time)",
            "UTC+06:00(BHT - Bangladesh Standard Time)",
            "UTC+06:30(MRT - Myanmar Standard Time)",
            "UTC+07:00(MST - Indochina Standard Time)",
            "UTC+08:00(EAT - East Standard Time)",
            "UTC+09:00(FET - Far Eastern Standard Time)",
            "UTC+09:30(ACST- Australia and Central Standard Time)",
            "UTC+10:00(AEST- Australian Eastern Standard Time)",
            "UTC+10:30(FAST- Far East, Australia Standard Time)",
            "UTC+11:00(VTT - Vanuatu Standard Time)",
            "UTC+11:30(NFT - Norfolk Island Standard Time)",
            "UTC+12:00(PSTB- Pacific Standard Time B)",
            "UTC+12:45(CIT - Chatham Islands Standard Time)",
            "UTC+13:00(PSTC- Pacific Standard Time C)",
            "UTC+14:00(PSTD- Pacific Standard Time D)"});
            this.TimeZones.Text = "UTC 00:00(WET - Western Europe time zone, GMT- GMT)";
        }

        private void AnalogClock_Resize(object sender, EventArgs e)
        {
            this.Width = this.Height;
            this.fRadius = this.Height / 2;
            this.fCenterX = this.ClientSize.Width / 2;
            this.fCenterY = this.ClientSize.Height / 2;
            //this.fHourLength = (float)this.Height / 3 / 1.65F;
            //this.fMinLength = (float)this.Height / 3 / 1.20F;
            //this.fSecLength = (float)this.Height / 3 / 1.15F;
            this.fHourLength = (float)this.Height / 2 / 1.65F;
            this.fMinLength = (float)this.Height / 2 / 1.20F;
            this.fSecLength = (float)this.Height / 2 / 1.15F;
            this.fHourThickness = (float)this.Height / 100;
            this.fMinThickness = (float)this.Height / 150;
            this.fSecThickness = (float)this.Height / 200;
            this.fCenterCircleRadius = this.Height / 50;
            //this.Refresh();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (DefaultDateTime)
            {
                this.dateTime = DateTime.Now;
            }
            this.Refresh();
            if (DefaultDateTime)
            {
                this.AnalogClock_Resize(sender, e);
            }
        }

        private void DrawLine(float fThickness, float fLength, Color color, float fRadians, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(color, fThickness),
            fCenterX - (float)(fLength / 9 * System.Math.Sin(fRadians)),
            fCenterY + (float)(fLength / 9 * System.Math.Cos(fRadians)),
            fCenterX + (float)(fLength * System.Math.Sin(fRadians)),
            fCenterY - (float)(fLength * System.Math.Cos(fRadians)));
        }

        private void DrawPolygon(float fThickness, float fLength, Color color, float fRadians, System.Windows.Forms.PaintEventArgs e)
        {
            PointF A = new PointF((float)(fCenterX +
                     fThickness * 2 * System.Math.Sin(fRadians + PI / 2)),
                     (float)(fCenterY -
                     fThickness * 2 * System.Math.Cos(fRadians + PI / 2)));
            PointF B = new PointF((float)(fCenterX +
                     fThickness * 2 * System.Math.Sin(fRadians - PI / 2)),
                    (float)(fCenterY -
                    fThickness * 2 * System.Math.Cos(fRadians - PI / 2)));
            PointF C = new PointF((float)(fCenterX +
                     fLength * System.Math.Sin(fRadians)),
                     (float)(fCenterY -
                     fLength * System.Math.Cos(fRadians)));
            PointF D = new PointF((float)(fCenterX -
                     fThickness * 4 * System.Math.Sin(fRadians)),
                     (float)(fCenterY +
                     fThickness * 4 * System.Math.Cos(fRadians)));
            PointF[] points = { A, D, B, C };
            e.Graphics.FillPolygon(new SolidBrush(color), points);
        }

        private void AnalogClock_Paint(object sender, PaintEventArgs e)
        {
            float fRadHr = (dateTime.Hour % 12 + dateTime.Minute / 60F) * 30 * PI / 180;
            float fRadMin = (dateTime.Minute) * 6 * PI / 180;
            float fRadSec = (dateTime.Second) * 6 * PI / 180;

            DrawPolygon(this.fHourThickness,
                  this.fHourLength, hrColor, fRadHr, e);
            DrawPolygon(this.fMinThickness,
                  this.fMinLength, minColor, fRadMin, e);
            DrawLine(this.fSecThickness,
                  this.fSecLength, secColor, fRadSec, e);
            //for (int i = 0; i < 60; i++)
            //{
            //    if (this.bDraw5MinuteTicks == true && i % 5 == 0)
            //    // Draw 5 minute ticks
            //    {
            //        e.Graphics.DrawLine(new Pen(ticksColor, fTicksThickness),
            //          fCenterX +
            //          (float)(this.fRadius / 1.50F * System.Math.Sin(i * 6 * PI / 180)),
            //          fCenterY -
            //          (float)(this.fRadius / 1.50F * System.Math.Cos(i * 6 * PI / 180)),
            //          fCenterX +
            //          (float)(this.fRadius / 1.65F * System.Math.Sin(i * 6 * PI / 180)),
            //          fCenterY -
            //          (float)(this.fRadius / 1.65F * System.Math.Cos(i * 6 * PI / 180)));
            //    }
            //    else if (this.bDraw1MinuteTicks == true) // draw 1 minute ticks
            //    {
            //        e.Graphics.DrawLine(new Pen(ticksColor, fTicksThickness),
            //          fCenterX +
            //          (float)(this.fRadius / 1.50F * System.Math.Sin(i * 6 * PI / 180)),
            //          fCenterY -
            //          (float)(this.fRadius / 1.50F * System.Math.Cos(i * 6 * PI / 180)),
            //          fCenterX +
            //          (float)(this.fRadius / 1.55F * System.Math.Sin(i * 6 * PI / 180)),
            //          fCenterY -
            //          (float)(this.fRadius / 1.55F * System.Math.Cos(i * 6 * PI / 180)));
            //    }
            //}            
            //draw circle at center
            e.Graphics.FillEllipse(new SolidBrush(circleColor),
                       fCenterX - fCenterCircleRadius / 2,
                       fCenterY - fCenterCircleRadius / 2,
                       fCenterCircleRadius, fCenterCircleRadius);
        }

        public void Start()
        {
            timer1.Enabled = true;
            this.Refresh();
        }

        public void Stop()
        {
            timer1.Enabled = false;
        }
        public Color HourHandColor
        {
            get { return this.hrColor; }
            set { this.hrColor = value; }
        }

        public Color MinuteHandColor
        {
            get { return this.minColor; }
            set { this.minColor = value; }
        }

        public Color SecondHandColor
        {
            get { return this.secColor; }
            set
            {
                this.secColor = value;
                this.circleColor = value;
            }
        }

        public Color TicksColor
        {
            get { return this.ticksColor; }
            set { this.ticksColor = value; }
        }

        public bool Draw1MinuteTicks
        {
            get { return this.bDraw1MinuteTicks; }
            set { this.bDraw1MinuteTicks = value; }
        }

        public bool Draw5MinuteTicks
        {
            get { return this.bDraw5MinuteTicks; }
            set { this.bDraw5MinuteTicks = value; }
        }

        public System.Windows.Forms.Label DateTimeZonesLong = new System.Windows.Forms.Label();

        public System.Windows.Forms.ComboBox TimeZones = new System.Windows.Forms.ComboBox();

        public System.Windows.Forms.TextBox DateTimeZones = new System.Windows.Forms.TextBox();

        public long TimeInterval = 30;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UTCsign">TimeZones</param>
        /// <param name="UTCsignB">TimeZones</param>
        /// <returns></returns>
        public DateTime UserSetDataTime()
        {
            String UTCsign = TimeZones.Text.Substring(3, 3);
            String UTCsignB = TimeZones.Text.Substring(3, 1) + TimeZones.Text.Substring(7, 2);
            DateTime ndt = new System.DateTime();
            ndt = System.DateTime.UtcNow;
            if (UTCsign != " 00")
            {
                if (UTCsignB.Substring(1, 2) != "00")
                {
                    Double WQ = Convert.ToDouble(UTCsign);
                    Double WQQ = Convert.ToDouble(UTCsignB);
                    DateTime ndtQ = ndt.AddHours(WQ).AddMinutes(WQQ);
                    DateTimeZones.Text = ndtQ.ToString("yyyy/MM/dd HH:mm:ss");
                    //DateTimeZonesLong.Text = ndtQ.Ticks.ToString();
                    DateTimeZonesLong.Text = String.Format("[{0}][{1}]", ((ndtQ.Ticks / 10000000) / TimeInterval).ToString(), ((ndtQ.Ticks / 10000000) / TimeInterval).ToString("X16"));
                    return ndtQ;
                }
                else
                {
                    Double WQ = Convert.ToDouble(UTCsign);
                    DateTime ndtQ = ndt.AddHours(WQ);
                    DateTimeZones.Text = ndtQ.ToString("yyyy/MM/dd HH:mm:ss");
                    //DateTimeZonesLong.Text = ndtQ.Ticks.ToString();                                   
                    DateTimeZonesLong.Text = String.Format("[{0}][{1}]", ((ndtQ.Ticks / 10000000) / TimeInterval).ToString(), ((ndtQ.Ticks / 10000000) / TimeInterval).ToString("X16"));
                    return ndtQ;
                }
            }
            else
            {
                DateTimeZones.Text = ndt.ToString("yyyy/MM/dd HH:mm:ss");
                //DateTimeZonesLong.Text = ndt.Ticks.ToString();                                    
                DateTimeZonesLong.Text = String.Format("[{0}][{1}]", ((ndt.Ticks / 10000000) / TimeInterval).ToString(), ((ndt.Ticks / 10000000) / TimeInterval).ToString("X16"));
                return ndt;
            }
        }
    }
}