using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class CompressImage
    {
        /// <summary>
        /// 隨機圖型驗證碼(數字)
        /// </summary>
        public String yzm = String.Empty;
        public System.Windows.Forms.PictureBox pictureBox1;
        public String ImageText = String.Empty;
        private bool disposed = false;
        public CompressImage()
        {
        }
        ~CompressImage()
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
            }
        }
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片地址</param>
        /// <param name="dFile">压缩后保存图片地址</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        public bool Compress(string sFile, string dFile, int flag = 90, int size = 300, bool sfsc = true)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(sFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                firstFileInfo.CopyTo(dFile);
                return true;
            }

            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                    FileInfo fi = new FileInfo(dFile);
                    if (fi.Length > 1024 * size)
                    {
                        flag = flag - 10;
                        Compress(sFile, dFile, flag, size, false);
                    }
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }
        #region === 圖型驗證 ===
        /// <summary>
        /// 產生四位隨機驗證碼
        /// </summary>
        /// <returns>隨機圖型驗證碼(數字)</returns>
        private String CreateCode()
        {
            char[] ch = new char[4];
            yzm = String.Empty;
            int i;
            int number;
            Random random = new Random();
            for (i = 0; i < 4; i++)
            {
                number = random.Next(62);//0~9,a~z,A~Z共62位
                if (number < 10)
                    ch[i] = (char)('0' + (char)number);
                else if (number < 36)
                    ch[i] = (char)('a' + (char)(number - 10));
                else
                    ch[i] = (char)('A' + (char)(number - 36));
                yzm += ch[i].ToString();
            }
            return yzm;
        }
        /// <summary>
        /// 產生隨機驗證碼圖型
        /// </summary>
        /// <param name="code">隨機圖型驗證碼(數字)</param>
        private void CreateImage(string code)
        {
            //建立位图文件，设置长宽
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling(code.Length * 22.0), 40);
            Graphics g = Graphics.FromImage(image);
            try
            {
                int i;
                //生成随即生成器
                Random random = new Random();
                //情况图片背景色
                g.Clear(Color.Gray);
                //设置图片上各字符的颜色
                int red, green, blue;
                Color[] color = new Color[4];
                for (i = 0; i < 4; i++)
                {
                    red = random.Next(256);
                    green = random.Next(256);
                    blue = random.Next(256);
                    color[i] = Color.FromArgb(red, green, blue);
                }
                //生成背景噪音线
                for (i = 0; i < 5; i++)
                {
                    int x1 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int x2 = random.Next(image.Width);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Purple), x1, y1, x2, y2);
                }
                //将产生的随即数以字体的形式写入画面
                Font font = new System.Drawing.Font("Arial", 19, System.Drawing.FontStyle.Bold);
                for (i = 0; i < 4; i++)
                {
                    g.DrawString(code.Substring(i, 1), font, new SolidBrush(color[i]), i * image.Width / 4 + 2, 4, null);
                }
                //生成前背景噪点
                Point[] zdian = new Point[150];
                for (i = 0; i < 150; i++)
                {
                    zdian[i].X = random.Next(image.Width);
                    zdian[i].Y = random.Next(image.Height);
                    image.SetPixel(zdian[i].X, zdian[i].Y, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                }
                //画图片的边框
                g.DrawRectangle(new Pen(Color.Green), 0, 0, image.Width - 1, image.Height - 1);
                this.pictureBox1.Width = image.Width;
                this.pictureBox1.Height = image.Height;
                this.pictureBox1.BackgroundImage = image;
            }
            catch (SyntaxErrorException ex)
            {
                MessageBox.Show(ex.ToString(), ImageText, MessageBoxButtons.OKCancel);
            }
        }
        #endregion
    }
}
