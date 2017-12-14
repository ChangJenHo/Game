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
        public static Hashtable VarPool = new Hashtable();
        private bool disposed = false;
        public String ErrorMessing = String.Empty;
        public MsSql()
        {
        }
        ~MsSql()
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
        public SqlDataReader MsSqlDataReader(String ConnectionString, String CommandString)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandString, Connection);
                SqlDataReader dr = Command.ExecuteReader();
                Command.Cancel();
                Connection.Close();
                Connection.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable MsDataTable(String ConnectionString, String CommandString)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandString, Connection);
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
                throw ex;
            }
        }
        public DataSet MsDataSet(String ConnectionString, String CommandString)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandString, Connection);
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
                throw ex;
            }
        }
        public Boolean MsInsert(String ConnectionString, String CommandString)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandString, Connection);
                Command.ExecuteNonQuery();
                Command.Cancel();
                Connection.Close();
                Connection.Dispose();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public Boolean MsSelect(String ConnectionString, String CommandString, String[] CSA)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                SqlCommand Command = new SqlCommand(CommandString, Connection);
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
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
