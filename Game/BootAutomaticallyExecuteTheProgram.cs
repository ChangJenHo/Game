using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game
{
    public class BootAutomaticallyExecuteTheProgram
    {
        private bool disposed = false;
        public BootAutomaticallyExecuteTheProgram()
        {
        }
        ~BootAutomaticallyExecuteTheProgram()
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
        /// 開機自動執行程序
        /// </summary>
        public static void BAETP()
        {
            BAETP(Process.GetCurrentProcess().ProcessName);
        }
        /// <summary>
        /// 開機自動執行程序
        /// </summary>
        /// <param name="APPNAME">登錄檔名稱</param>
        public static void BAETP(String APPNAME)
        {
            try
            {
                //宣告登錄檔名稱
                string app_name = APPNAME;
                //選告一個字串表示本身應用程式的位置後面加的是參數"-s"
                //若沒有附帶啟動參數的話可以不加
                //string R_startPath = Application.ExecutablePath + " -S";
                string R_startPath = Application.ExecutablePath;
                //開啟登錄檔位置，這個位置是存放啟動應用程式的地方
                RegistryKey aimdir = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                //若登錄檔已經存在則刪除
                if (aimdir.GetValue(app_name) != null)
                {
                    //刪除
                    aimdir.DeleteValue(app_name, false);
                }
                //寫入登錄檔值
                aimdir.SetValue(app_name, R_startPath);
                //關閉登錄檔
                aimdir.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("登錄檔寫入失敗:" + ex.Message);
            }
        }
    }
}
