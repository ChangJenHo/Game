using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Game.DataBase
{
    public class MsSql
    {
        private bool disposed = false;
        public String ErrorCode = String.Empty;
        public String ErrorMessing = String.Empty;
        public String ConnectionString = String.Empty;
        public String CommandString = String.Empty;
        public SqlConnection MsSqlConnection=null;
        public MsSql(String ConnectionStrings)
        {
            try
            { 
                ConnectionString = ConnectionStrings;
                MsSqlConnection = new SqlConnection(ConnectionString);
                MsSqlConnection.Open();
                ErrorCode = "00000000";
            }
            catch (Exception ex)
            {
                ErrorMessing=ex.Message;
                ErrorCode = Convert.ToString(ex.GetHashCode());
            }
        }
        public MsSql()
        {
        }
        ~MsSql()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            if (MsSqlConnection != null)
            {
                MsSqlConnection.Close();
                MsSqlConnection.Dispose();
            }
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

        public SqlDataReader MsSqlDataReader()
        {
            try
            {
                SqlCommand Command = new SqlCommand(CommandString, MsSqlConnection);
                SqlDataReader dr = Command.ExecuteReader();
                Command.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return null;
            }
        }
        public DataTable MsDataTable()
        {
            try
            {
                SqlCommand Command = new SqlCommand(CommandString, MsSqlConnection);
                SqlDataAdapter sqldataadapterX = new SqlDataAdapter(Command);
                DataSet datasetX = new DataSet();
                sqldataadapterX.Fill(datasetX);
                DataTable datatableX = datasetX.Tables[0];
                sqldataadapterX.Dispose();
                datasetX.Clear();
                datasetX.Dispose();
                Command.Dispose();
                return datatableX;
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return null;
            }
        }
        public DataSet MsDataSet()
        {
            try
            {
                SqlCommand Command = new SqlCommand(CommandString, MsSqlConnection);
                SqlDataAdapter sqldataadapterX = new SqlDataAdapter(Command);
                DataSet datasetX = new DataSet();
                sqldataadapterX.Fill(datasetX);
                sqldataadapterX.Dispose();
                return datasetX;
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return null;
            }
        }
        public int MsInsert()
        {
            try
            {
                SqlCommand Command = new SqlCommand(CommandString, MsSqlConnection);
                return Command.ExecuteNonQuery();   
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return Convert.ToInt16(ex.GetHashCode());
            }
        }
        public Boolean MsSelect(String[] CSA, Hashtable VarPool)
        {
            try
            {
                SqlCommand Command = new SqlCommand(CommandString, MsSqlConnection);
                SqlDataReader myDataReader = Command.ExecuteReader();
                VarPool.Clear();
                while (myDataReader.Read())
                {
                    foreach (String Scsa in CSA)
                    {
                        if (myDataReader[Scsa].ToString() != "")
                        {
                            VarPool[Scsa] = myDataReader[Scsa];
                        }
                    }
                }
                myDataReader.Close();
                Command.Dispose();
                if (VarPool.Count == 0)
                {
                    ErrorCode = "00000010";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return false;
            }
        }
        public Boolean MsUpdata()
        {
            try
            {
                SqlCommand Command = new SqlCommand(CommandString, MsSqlConnection);
                SqlDataAdapter sqldataadapterX = new SqlDataAdapter(Command);
                if (sqldataadapterX.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    ErrorCode = "00000000";
                }
                else
                {
                    ErrorCode = "00000011";
                }
                sqldataadapterX.Dispose();
                Command.Dispose();
                if (ErrorCode.CompareTo("00000011")==0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return false;
            }
        }
        public SqlDataReader MsSqlDataReader(String ConnectionStrings, String CommandStrings)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionStrings);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandStrings, Connection);
                SqlDataReader dr = Command.ExecuteReader();
                Command.Cancel();
                Connection.Close();
                Connection.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                throw ex;
            }
        }
        public DataTable MsDataTable(String ConnectionStrings, String CommandStrings)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionStrings);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandStrings, Connection);
                SqlDataAdapter sda = new SqlDataAdapter(Command);
                DataSet dss = new DataSet();
                dss.Clear();
                sda.Fill(dss);
                DataTable ddt = dss.Tables[0];

                sda.Dispose();
                Command.Cancel();
                Connection.Close();
                Connection.Dispose();
                return ddt;
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                throw ex;
            }
        }
        public DataSet MsDataSet(String ConnectionStrings, String CommandStrings)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionStrings);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandStrings, Connection);
                SqlDataAdapter sda = new SqlDataAdapter(Command);
                DataSet dss = new DataSet();
                sda.Fill(dss);
                sda.Dispose();
                Command.Cancel();
                Connection.Close();
                Connection.Dispose();
                return dss;
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                throw ex;
            }
        }
        public int MsInsert(String ConnectionStrings, String CommandStrings)
        {
            try
            {
                int returnint = 0;
                SqlConnection Connection = new SqlConnection(ConnectionStrings);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandStrings, Connection);
                returnint=Command.ExecuteNonQuery();
                Command.Cancel();
                Connection.Close();
                Connection.Dispose();
                return returnint;
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return Convert.ToInt16(ex.GetHashCode());
            }
        }
        public Boolean MsSelect(String ConnectionStrings, String CommandStrings, String[] CSA, Hashtable VarPool)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionStrings);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandStrings, Connection);
                SqlDataReader myDataReader = Command.ExecuteReader();               
                VarPool.Clear();
                while (myDataReader.Read())
                {
                    foreach (String Scsa in CSA)
                    {
                        if (myDataReader[Scsa].ToString() != "")
                        {
                            VarPool[Scsa] = myDataReader[Scsa];
                        }
                    }
                }
                Command.Cancel();
                Connection.Close();
                Connection.Dispose();
                if(VarPool.Count == 0)
                {
                    ErrorCode = "00000010";
                    return false;
                }else {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorCode = Convert.ToString(ex.GetHashCode());
                return false;
            }
        }
        public static String CommandStringSELECT(String DBtable, String[] DataName, String[] DataVale)
        {
            String CSS = String.Format("SELECT ");
            return CSS;
        }
        /// <summary>
        /// 新增SQL命令
        /// </summary>
        /// <param name="DBtable">資料表</param>
        /// <param name="DataName">欄位名 new string[] {"AAA","BBB"}</param>
        /// <param name="DataVale">欄位資料 new string[] {"AAA, nvarchar(max),","BBB, varbinary(max),"}</param>
        /// <returns>SQL命令字串</returns>
        public static String CommandStringINSERT(String DBtable, String[] DataName, String[] DataVale )
        {
            String CSI = String.Format("INSERT INTO [dbo].[{0}](", DBtable);
            Boolean iii = false;
            foreach (String DN in DataName)
            {
                if (iii)
                {
                    CSI += String.Format(",[{0}]", DN);
                }
                else
                { 
                    CSI += String.Format("[{0}]", DN);
                    iii = true;
                }
            }
            CSI += String.Format(")VALUES(" );
            iii = false;
            foreach (String DV in DataVale)
            {
                if (iii)
                {
                    CSI += String.Format(",<{0}>", DV);
                }
                else
                {
                    CSI += String.Format("<{0}>", DV);
                    iii = true;
                }
            }
            CSI += String.Format(")");
            return CSI;
        }

    }
}
