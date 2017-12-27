using Game.DataBase;
using Game.Network;
using Game.Network.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Login.Function
{
    public class FunctionParser
    {
        #region 建構函數
        private bool disposed = false;
        public NetEventArgs e;
        public MsSql mssql;
        public FunctionParser(NetEventArgs ee)
        {
            e = ee;
            mssql = new MsSql(Login.Properties.Settings.Default.ZLabSDBEntities);
            if (mssql.ErrorCode.CompareTo("0") != 0)
            {
                MessageBox.Show(mssql.ErrorMessing, mssql.ErrorCode);
                mssql.Dispose();
                mssql = new MsSql(Login.Properties.Settings.Default.ZLabSDBTestEntities);
                if (mssql.ErrorCode.CompareTo("0") != 0)
                {
                    MessageBox.Show(mssql.ErrorMessing, mssql.ErrorCode);
                    mssql.Dispose();
                }
            }
        }
        ~FunctionParser()
        {
            mssql.Dispose();
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
        #endregion
        public String ReturnFunction()
        {
            String ReturnString = String.Empty;
            JsonData contentData = JsonMapper.ToObject(e.Client.Datagram);
            e.Client.ID.VarPool["methodName"] = contentData["methodName"].ToString();
            JsonData paramObject = contentData["paramObject"];            
            switch (e.Client.ID.VarPool["methodName"].ToString())
            {
                #region 註冊
                case "registered_C2S":
                    e.Client.ID.VarPool["FirstName"] = paramObject["firstName"].ToString();
                    e.Client.ID.VarPool["LastName"] = paramObject["lastName"].ToString();
                    e.Client.ID.VarPool["MicknName"] = paramObject["nickName"].ToString();
                    e.Client.ID.VarPool["password"] = paramObject["pwd"].ToString();
                    e.Client.ID.VarPool["Birthday"] = paramObject["birthday"].ToString();
                    e.Client.ID.VarPool["BirthYear"] = e.Client.ID.VarPool["Birthday"].ToString().Split('-')[0].ToString();
                    e.Client.ID.VarPool["BirthMonth"] = e.Client.ID.VarPool["Birthday"].ToString().Split('-')[1].ToString();
                    e.Client.ID.VarPool["BirthDay"] = e.Client.ID.VarPool["Birthday"].ToString().Split('-')[2].ToString();
                    e.Client.ID.VarPool["email"] = paramObject["email"].ToString();
                    e.Client.ID.VarPool["location"] = paramObject["gender"].ToString();
                    e.Client.ID.VarPool["Gender"] = paramObject["country"].ToString();
                    Registered rd = new Registered(e);
                    e.Client.ID.VarPool["ErrorCord"]=rd.ReturnRegistered(mssql);
                    rd.Dispose();
                    ReturnString= "{\"methodName\":\"registered_S2C\",\"paramObject\":{\"rs\":\"";
                    ReturnString += e.Client.ID.VarPool["ErrorCord"];
                    ReturnString += "\"}}";
                    break;
                #endregion
                #region 登入
                case "C2S_Login":
                    e.Client.ID.VarPool["account"] = paramObject["Account"].ToString();
                    e.Client.ID.VarPool["password"] = paramObject["Password"].ToString();
                    e.Client.ID.VarPool["CheckPassword"] = Convert.ToBoolean(paramObject["CheckPassword"].ToString());
                    Logins ln = new Logins(e);
                    e.Client.ID.VarPool["ErrorCord"] = ln.ReturnLogin(mssql);
                    ln.Dispose();
                    ReturnString = "{\"methodName\":\"registered\",\"paramObject\":{\"rs\":\"";
                    ReturnString += e.Client.ID.VarPool["ErrorCord"];
                    ReturnString += "\"}}";
                    break;
                #endregion
                #region 忘記密碼
                case "C2S_ForgetPassword":
                    e.Client.ID.VarPool["account"] = paramObject["Account"].ToString();
                    ForgetPassword fpd = new ForgetPassword(e);
                    e.Client.ID.VarPool["ErrorCord"] = fpd.ReturnForgetPassword(mssql);
                    fpd.Dispose();
                    ReturnString = "{\"methodName\":\"registered\",\"paramObject\":{\"rs\":\"";
                    ReturnString += e.Client.ID.VarPool["ErrorCord"];
                    ReturnString += "\"}}";
                    break;
                #endregion
                #region 未定義指令
                default:
                    break;
                #endregion
            }
            return ReturnString;
        }
    }
}
