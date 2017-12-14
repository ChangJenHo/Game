using System;
using System.Collections.Generic;
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
                        throw (new Exception("未定義的編碼格式"));
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
                        throw (new Exception("未定义的编码格式"));
                    }
            }
        }
        public byte[] HexToBytes(string hex)
        {
            MatchCollection mc = Regex.Matches(Regex.Replace(hex, @"[^a-zA-Z0-9]+", ""), @"([a-zA-Z0-9]{2})");
            byte[] bytes = new byte[mc.Count];
            for (int i = 0; i < mc.Count; i++)
                bytes[i] = Convert.ToByte(mc[i].Value, 16);
            return bytes;
        }
        public string BytesToHex(byte[] ba, int length)
        {
            return BytesToHex(ba, 0, length);
        }
        public string BytesToHex(byte[] ba, int start, int length)
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
        public byte[] MakeArray(byte[] by, int start, int length)
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
    }
}
