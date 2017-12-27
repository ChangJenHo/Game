using Game.DataBase;
using Game.Network;
using Game.Network.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login.Function
{
    public class ForgetPassword
    {
        private bool disposed = false;
        public NetEventArgs e;
        public ForgetPassword(NetEventArgs ee)
        {
            e = ee;
        }
        ~ForgetPassword()
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
        public String ReturnForgetPassword(MsSql mssql)
        {
            String ReturnString = "00000000";
            Random rnd = new Random();
            e.Client.ID.VarPool["password"] = rnd.Next(0, 99999999).ToString().PadRight(8, '0');
            mssql.CommandString = String.Format("UPDATE Account SET Password='{0}' WHERE Mail='{1}';", e.Client.ID.VarPool["password"].ToString(), e.Client.ID.VarPool["account"].ToString());
            if (!mssql.MsUpdata())
            {
                ReturnString = mssql.ErrorCode;
            }
            else
            {
                Send emailsend = new Send();
                emailsend.smtpAddress = Login.Properties.Settings.Default.smtpAddress;
                emailsend.portNumber = Login.Properties.Settings.Default.portNumber;
                emailsend.enableSSL = Login.Properties.Settings.Default.enableSSL;
                emailsend.emailFrom = Login.Properties.Settings.Default.EmailFrom;
                emailsend.password = Login.Properties.Settings.Default.Emailpassword;
                emailsend.emailTo = e.Client.ID.VarPool["account"].ToString();
                emailsend.subject = Login.Properties.Settings.Default.subject;
                emailsend.body = String.Format(Login.Properties.Settings.Default.body, e.Client.ID.VarPool["password"].ToString());
                emailsend.SendEmail();
                if (emailsend.ErrorData.CompareTo("") != 0)
                {
                    ReturnString = emailsend.ErrorData;
                }
            }
            return ReturnString;
        }
    }
}