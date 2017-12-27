using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Settings
{
    public class Registry
    {
        private bool disposed = false;
        public String RegistryException = String.Empty;
        public String Registrykey = String.Empty;
        public String[] KeyName;
        public String[] Valueame;
        public Registry()
        {
        }
        ~Registry()
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
        /// <summary>
        /// 寫入指定註冊表
        /// </summary>
        /// <param name="name">要儲存之值的名稱</param>
        /// <param name="value">要儲存的資料</param>
        /// <returns></returns>
        public Boolean RegistrykeyWrite(string name, object value)
        {
            try {
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Registrykey);
            regkey.SetValue(name, value);
            regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 寫入特定註冊表
        /// </summary>
        /// <param name="RegistrykeyTemp">特定註冊表名稱</param>
        /// <param name="name">要儲存之值的名稱</param>
        /// <param name="value">要儲存的資料</param>
        /// <returns></returns>
        public Boolean RegistrykeyWrite(String RegistrykeyTemp, String name, object value)
        {
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegistrykeyTemp);
                regkey.SetValue(name, value);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 寫入指定註冊表
        /// </summary>
        /// <param name="name">要儲存之值的名稱</param>
        /// <param name="value">要儲存之值的名稱</param>
        /// <param name="valueKind">儲存資料時要使用的登錄資料型別</param>
        /// <returns></returns>
        public Boolean RegistrykeyWrite(string name, object value, Microsoft.Win32.RegistryValueKind valueKind)
        {
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Registrykey);
                regkey.SetValue(name, value, valueKind);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 寫入特定註冊表
        /// </summary>
        /// <param name="RegistrykeyTemp">特定註冊表名稱</param>
        /// <param name="name">要儲存之值的名稱</param>
        /// <param name="value">要儲存之值的名稱</param>
        /// <param name="valueKind">儲存資料時要使用的登錄資料型別</param>
        /// <returns></returns>
        public Boolean RegistrykeyWrite(String RegistrykeyTemp, String name, object value, Microsoft.Win32.RegistryValueKind valueKind)
        {
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegistrykeyTemp);
                regkey.SetValue(name, value, valueKind);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 讀取指定註冊表
        /// 擷取與指定名稱關聯的值。如果登錄中沒有名稱/值組，則傳回 null
        /// </summary>
        /// <param name="name">要擷取的值的名稱。這個字串不會區分大小寫</param>
        /// <returns> 與 name 關聯的值，如果找不到 name，則為 null</returns>
        public Object RegistrykeyRead(String name)
        {
                Object oo = null;
            try
            { 
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Registrykey);
                if (regkey == null) return oo;
                oo = regkey.GetValue(name);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return oo;
            }
            return oo;
        }
        /// <summary>
        /// 讀取特定註冊表
        /// 擷取與指定名稱關聯的值。如果登錄中沒有名稱/值組，則傳回 null
        /// </summary>
        /// <param name="RegistrykeyTemp">特定註冊表名稱</param>
        /// <param name="name">要擷取的值的名稱。這個字串不會區分大小寫</param>
        /// <returns> 與 name 關聯的值，如果找不到 name，則為 null</returns>
        public Object RegistrykeyRead(String RegistrykeyTemp, String name)
        {
            Object oo = null;
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistrykeyTemp);
                if (regkey == null) return oo;
                oo = regkey.GetValue(name);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return oo;
            }
            return oo;
        }
        /// <summary>
        /// 讀取指定註冊表
        /// 擷取與指定名稱關聯的值。如果找不到名稱，則傳回您提供的預設值
        /// </summary>
        /// <param name="name">要擷取的值的名稱。這個字串不會區分大小寫</param>
        /// <param name="value">name 不存在時所傳回的值</param>
        /// <returns> 與 name 關聯的值，擁有未展開的內嵌環境變數，如果找不到 name，則為 defaultValue</returns>
        public Object RegistrykeyRead(String name, Object value)
        {
            Object oo = null;
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Registrykey);
                if (regkey == null) return oo;
                oo = regkey.GetValue(name, value);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return oo;
            }
            return oo;
        }
        /// <summary>
        /// 讀取特定註冊表
        /// 擷取與指定名稱關聯的值。如果找不到名稱，則傳回您提供的預設值
        /// </summary>
        /// <param name="RegistrykeyTemp">特定註冊表名稱</param>
        /// <param name="name">要擷取的值的名稱。這個字串不會區分大小寫</param>
        /// <param name="value">>name 不存在時所傳回的值</param>
        /// <returns> 與 name 關聯的值，擁有未展開的內嵌環境變數，如果找不到 name，則為 defaultValue</returns>
        public Object RegistrykeyRead(String RegistrykeyTemp, String name, Object value)
        {
            Object oo = null;
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistrykeyTemp);
                if (regkey == null) return oo;
                oo = regkey.GetValue(name, value);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return oo;
            }
            return oo;
        }
        /// <summary>
        /// 讀取指定註冊表
        /// 擷取與指定名稱及擷取選項關聯的值。如果找不到名稱，則傳回您提供的預設值。
        /// </summary>
        /// <param name="name">要擷取的值的名稱。這個字串不會區分大小寫</param>
        /// <param name="value">name 不存在時所傳回的值</param>
        /// <param name="options">其中一個列舉值，指定擷取之值的選擇性處理</param>
        /// <returns>與 name 關聯的值，根據指定的 options 處理，如果找不到 name，則為 defaultValue</returns>
        public Object RegistrykeyRead(String name, Object value, Microsoft.Win32.RegistryValueOptions options)
        {
            Object oo = null;
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Registrykey);
                if (regkey == null) return oo;
                oo = regkey.GetValue(name, value, options);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return oo;
            }
            return oo;
        }
        /// <summary>
        /// 讀取特定註冊表
        /// 擷取與指定名稱及擷取選項關聯的值。如果找不到名稱，則傳回您提供的預設值。
        /// </summary>
        /// <param name="RegistrykeyTemp">特定註冊表名稱</param>
        /// <param name="name">要擷取的值的名稱。這個字串不會區分大小寫</param>
        /// <param name="value">name 不存在時所傳回的值</param>
        /// <param name="options">其中一個列舉值，指定擷取之值的選擇性處理</param>
        /// <returns>與 name 關聯的值，根據指定的 options 處理，如果找不到 name，則為 defaultValue</returns>
        public Object RegistrykeyRead(String RegistrykeyTemp, String name, Object value, Microsoft.Win32.RegistryValueOptions options)
        {
            Object oo = null;
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Registrykey);
                if (regkey == null) return oo;
                oo = regkey.GetValue(name, value, options);
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return oo;
            }
            return oo;
        }
        /// <summary>
        /// 列出指定註冊表
        /// </summary>
        /// <returns></returns>
        public Boolean RegistrykeyKeyValueNames()
        {
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Registrykey);
                KeyName = regkey.GetSubKeyNames();
                Valueame = regkey.GetValueNames();
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 列出特定註冊表
        /// </summary>
        /// <param name="RegistrykeyTemp">特定註冊表名稱</param>
        /// <returns></returns>
        public Boolean RegistrykeyKeyValueNames(String RegistrykeyTemp)
        {
            try
            {
                Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistrykeyTemp);
                KeyName = regkey.GetSubKeyNames();
                Valueame = regkey.GetValueNames();
                regkey.Close();
            }
            catch (Exception ex)
            {
                RegistryException = ex.Message;
                return false;
            }
            return true;
        }

        //https://dobon.net/vb/dotnet/system/registrykey.html
    }
}
