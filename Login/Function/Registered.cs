using Game.DataBase;
using Game.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login.Function
{
    public class Registered
    {
        private bool disposed = false;
        public NetEventArgs e;
        public Registered(NetEventArgs ee)
        {
            e = ee;
        }
        ~Registered()
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
        public String ReturnRegistered(MsSql mssql)
        {
            String ReturnString = String.Empty;
            ReturnString = "0";
            return ReturnString;
        }
    }
}
