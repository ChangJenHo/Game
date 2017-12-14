using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Game.Network
{
    public class RookieIPCountryFinder
    {
        private bool disposed = false;
        public class IPRange
        {
            public uint Start;
            public uint End;
            public string CnCode;
            public string CnName;
        }
        List<IPRange> Ranges = new List<IPRange>();
        public RookieIPCountryFinder(string path)
        {
            foreach (var line in File.ReadAllLines(path))
            {
                if (line.StartsWith("#")) continue;
                //"87597056","87599103","ripencc","1338422400","ES","ESP","Spain"
                var p = line.Split(',').Select(o => o.Trim('"')).ToArray();
                Ranges.Add(new IPRange()
                {
                    Start = uint.Parse(p[0]),
                    End = uint.Parse(p[1]),
                    CnCode = p[4],
                    CnName = p[6]
                });
            }
        }
        ~RookieIPCountryFinder()
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
        public uint GetIPAddrDec(string ipAddr)
        {
            byte[] b = IPAddress.Parse(ipAddr).GetAddressBytes();
            Array.Reverse(b);
            return BitConverter.ToUInt32(b, 0);
        }
        public string GetCountryCode(string ipAddr)
        {
            uint ip = GetIPAddrDec(ipAddr);
            var range = Ranges.SingleOrDefault(o => ip >= o.Start && ip <= o.End); ;
            if (range == null)
                return "--";
            else
                return range.CnCode;
        }
    }
}
