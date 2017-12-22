using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using System.Threading.Tasks;

namespace Game.Network
{
    /// <summary>
    /// 通訊編碼格式提供者,為通訊服務提供編碼和解碼服務
    /// 你可以在繼承類中定制自己的編碼方式如:數據加密傳輸等
    /// </summary>
    public class Coder
    {
        /// <summary>
        /// 編碼方式
        /// </summary>
        private EncodingMothord _encodingMothord;
        protected Coder()
        {
            _encodingMothord = Coder.EncodingMothord.Default;
        }
        private bool disposed = false;
        public Coder(EncodingMothord encodingMothord)
        {
            _encodingMothord = encodingMothord;
        }
        ~Coder()
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
        public enum EncodingMothord
        {
            Default = 0,
            Unicode,
            UTF8,
            ASCII,
            HexString,
        }
        /// <summary>
        /// 通訊數據編碼(字串)
        /// </summary>
        /// <param name="dataBytes">需要解碼的數據(位元組)</param>
        /// <param name="size">尺寸(int)</param>
        /// <returns>編碼後的數據(字串)</returns>
        public virtual string GetEncodingString(byte[] dataBytes, int size)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.HexString:
                    {
                        return BytesToHex(dataBytes, 0, size);
                    }
                default:
                    {
                        return BytesToHex(dataBytes, 0, size);
                    }
            }
        }
        /// <summary>
        /// 通訊數據編碼(位元組)
        /// </summary>
        /// <param name="datagram">需要解碼的數據(字串)</param>
        /// <returns>編碼後的數據(位元組)</returns>
        public virtual byte[] GetEncodingBytes(string datagram)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetBytes(datagram);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetBytes(datagram);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetBytes(datagram);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetBytes(datagram);
                    }
                case EncodingMothord.HexString:
                    {
                        return HexToBytes(datagram);
                    }
                default:
                    {
                        return HexToBytes(datagram);
                    }
            }
        }
        public static byte[] HexToBytes(string hex)
        {
            MatchCollection mc = Regex.Matches(Regex.Replace(hex, @"[^a-zA-Z0-9]+", ""), @"([a-zA-Z0-9]{2})");
            byte[] bytes = new byte[mc.Count];
            for (int i = 0; i < mc.Count; i++)
                bytes[i] = Convert.ToByte(mc[i].Value, 16);
            return bytes;
        }
        public static string BytesToHex(byte[] ba)
        {
            return BytesToHex(ba, 0, ba.Length);
        }
        public static string BytesToHex(byte[] ba, int length)
        {
            return BytesToHex(ba, 0, length);
        }
        public static string BytesToHex(byte[] ba, int start, int length)
        {
            byte[] tempb = MakeArray(ba, start, length);
            int len = 0;
            string separator = "";
            string[] hexstrs = Array.ConvertAll<byte, string>(tempb, delegate (byte b)
            {
                switch (++len)
                {
                    case 16: separator = "\n"; len = 0; break;
                    case 8: //separator = " "; break;
                    case 4:
                    case 12: separator = " "; break;
                    default: separator = ""; break;
                }
                return b.ToString("X2") + separator;
            });
            return string.Join("", hexstrs).TrimEnd(separator.ToCharArray()).Replace(" ","").Replace("\n", "");
        }
        public static byte[] MakeArray(byte[] by, int start, int length)
        {
            if (start < 0) start = 0;
            if (length < 0) length = 0;
            byte[] bb = new byte[length];
            if (by != null)
                if (length > 0 && start < by.Length)
                    Array.Copy(by, start, bb, 0, length);
            return bb;
        }
        public static String ShowHexString(String HexStringcodes)
        {
            int Ded = HexStringcodes.Length / 2;
            String TempString = HexStringcodes;
            String returncode = String.Format("Length = Decimal: {0} Hex: {1:X}\r\n", Ded, Ded);
            int fori = Ded / 16;
            if ((Ded % 16) != 0)
            {
                fori++;
                TempString = TempString.PadRight(((fori * 16) * 2), 'F');
                //TempString.PadRight((fori * 2), 'F');
            }
            fori = (TempString.Length / 2) / 16;
            returncode += String.Format("-Address--------------------Hexadecimal--------------------------ASCII------\r\n");
            returncode += String.Format("          00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F                   \r\n");
            for (int i = 0; i < fori; i++)
            {
                returncode += String.Format("{0}  {1}  {2}\r\n", String.Format("{0:X}", (i * 16)).PadLeft(8, '0'), AddSpacing(TempString.Substring((i * 32), 32), 2, 'F'), ShowAscii(TempString.Substring((i * 32), 32), 2));
            }
            returncode += String.Format("----------------------------------------------------------------------------\r\n");
            return returncode;
        }

        public static String AddSpacing(String StrTemp, int CharNo, Char PadCode)
        {
            String returncode = "";
            StrTemp = StrTemp.PadRight((StrTemp.Length + (StrTemp.Length % CharNo)), PadCode);
            for (int i = 0; i < StrTemp.Length; i += CharNo)
            {
                returncode += String.Format("{0} ", StrTemp.Substring(i, CharNo));
            }
            return returncode;
        }

        public static String ShowAscii(String InString, int ss)
        {
            int Length = 0;
            String ReturnString = "";
            string[] strlist = new string[InString.Length / ss];
            try
            {
                for (int i = 0; i < strlist.Length; i++)
                {
                    strlist[i] = InString.Substring((i * ss), ss);
                    Length = Convert.ToInt16(strlist[i], 16);
                    if ((Length <= 31) || (Length >= 127))
                    {
                        ReturnString += ".";
                    }
                    else
                    {
                        //將unicode轉為10進制整數，然後轉為char中文
                        ReturnString += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
                }
            }
            catch (FormatException ex)
            {
                //           ReturnString = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return ReturnString;
        }
        
        /// <summary>
        /// Image轉byte[]
        /// </summary>
        /// <param name="imageIn">圖片</param>
        /// <returns>byte array</returns>
        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// byte[]轉換成Image
        /// </summary>
        /// <param name="Bytes">二進制圖片流</param>
        /// <returns>Bitmap</returns>
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }
        /// <summary>
        /// Bitmap轉byte[]
        /// </summary>
        /// <param name="Bitmap">圖片</param>
        /// <returns>byte array</returns>
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
        /// <summary>
        /// 將圖片Image轉換成Byte[]
        /// </summary>
        /// <param name="Image">image物件</param>
        /// <param name="imageFormat">字尾名</param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap Bitmap = new Bitmap(Image))
                {
                    Bitmap.Save(ms, imageFormat);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return data;
        }
        /// <summary>
        /// byte[]轉換成Image
        /// </summary>
        /// <param name="byteArrayIn">二進位制圖片流</param>
        /// <returns>Image</returns>
        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Flush();
                return returnImage;
            }
        }
        
    }
}
