using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
//using System.Threading.Tasks;

namespace Game.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class Information
    {
        /// <summary>
        ///  取得IP V4的Address
        /// </summary>
        /// <returns></returns>
        public static String [] IPLocal()
        {
            // 取得本機名稱
            string strHostName = Dns.GetHostName();
            // 取得本機的IpHostEntry類別實體，用這個會提示已過時
            //IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
            // 取得本機的IpHostEntry類別實體，MSDN建議新的用法
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);
            String[] IPL = new String[iphostentry.AddressList.Length];
            int a = 0;
            // 取得所有 IP 位址
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                Console.WriteLine(String.Format("-- Local IP: {0} - {1}", ipaddress.ToString(), ipaddress.AddressFamily));

                // 只取得IP V4的Address
                if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    //Console.WriteLine("== Local IP: " + ipaddress.ToString());
                    IPL[a] = ipaddress.ToString();
                    a++;
                }
            }
            return IPL;
        }
        /// <summary>
        /// 驗證字串是否為合法的IP Address
        /// </summary>
        /// <param name="ipStr">IP Address</param>
        /// <returns></returns>
        public static String GetValidatedIPEx(String ipStr)
        {
            String validatedIP = string.Empty;
            //如果ip + Port的話，使用IPAddress.TryParse會無法解析成功
            //所以加入Uri來判斷看看
            Uri url;
            IPAddress ip;
            if (Uri.TryCreate(String.Format("http://{0}", ipStr), UriKind.Absolute, out url))
            {
                if (IPAddress.TryParse(url.Host, out ip))
                {
                    //合法的IP
                    validatedIP = ip.ToString();
                }
            }
            else
            {
                //可能是ipV6，所以用Uri.CheckHostName來處理
                var chkHostInfo = Uri.CheckHostName(ipStr);
                if (chkHostInfo == UriHostNameType.IPv6)
                {
                    //V6才進來處理
                    if (IPAddress.TryParse(ipStr, out ip))
                    {
                        validatedIP = ip.ToString();
                    }
                    else
                    {
                        //後面有Port Num，所以再進行處理
                        int colonPos = ipStr.LastIndexOf(":");
                        if (colonPos > 0)
                        {
                            string tempIp = ipStr.Substring(0, colonPos - 1);
                            if (IPAddress.TryParse(tempIp, out ip))
                            {
                                validatedIP = ip.ToString();
                            }
                        }
                    }
                }
            }
            return validatedIP;
        }
        /// <summary>
        /// 較驗IP地址
        /// </summary>
        /// <param name="IP">IP地址</param>
        /// <returns>true/false</returns>
        public static bool IPCheck(string IP)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(IP, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
