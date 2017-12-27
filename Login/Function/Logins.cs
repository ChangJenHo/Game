using Game.DataBase;
using Game.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login.Function
{
    public class Logins
    {
        private bool disposed = false;
        public NetEventArgs e;
        public Logins(NetEventArgs ee)
        {
            e = ee;
        }
        ~Logins()
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
        public String ReturnLogin(MsSql mssql)
        {
            String ReturnString = "00000000";
            try
            {
                if ((Boolean)e.Client.ID.VarPool["CheckPassword"])
                {
                    mssql.CommandString = String.Format("SELECT id, Mail, Photo FROM Account WHERE Mail='{0}' AND Password='{1}';", e.Client.ID.VarPool["account"].ToString(), e.Client.ID.VarPool["password"].ToString());
                }
                else
                {
                    mssql.CommandString = String.Format("SELECT id, Mail, Photo FROM Account WHERE Mail='{0}';", e.Client.ID.VarPool["account"].ToString());
                }
                if (!mssql.MsSelect(new string[] { "id", "Mail", "Photo" }, e.Client.ID.VarPool))
                {
                    ReturnString = mssql.ErrorCode;
                }
                //String id = MsSql.VarPool["id"].ToString();
                //String Mail = MsSql.VarPool["Mail"].ToString();
                //String Photo = Game.Network.Coder.BytesToHex((Byte[])MsSql.VarPool["Photo"]);
            }
            catch (Exception ex)
            {
                ReturnString = Convert.ToString(ex.GetHashCode());
            }
            return ReturnString;
        }
    }
}
