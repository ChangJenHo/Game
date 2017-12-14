using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
//using System.Threading.Tasks;

namespace Game.Network.Email
{
    public class Send
    {
        private bool disposed = false;
        public Send()
        {
        }
        ~Send()
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
        /// 設定smtp主機
        /// </summary>
        //public String smtpAddress = "smtp.mail.yahoo.com";
        public String smtpAddress = "smtp.gmail.com";
        /// <summary>
        /// 設定Port
        /// </summary>
        public int portNumber = 587;
        public bool enableSSL = true;
        /// <summary>
        /// 填入寄送方email和密碼
        /// </summary>
        public String emailFrom = "SmartDartBoard@gmail.com";
        public String password = "sdbzlabdev";
        /// <summary>
        /// 收信方email
        /// </summary>
        public String emailTo = "g9677602@gmail.com";
        /// <summary>
        /// 主旨
        /// </summary>
        public String subject = "Hello";
        /// <summary>
        /// 內容
        /// /summary>
        public String body = "Hello, I'm just writing this to say Hi!";
        public String ErrorData= String.Empty;
        public String ErrorMessage = String.Empty;
        /// <summary>
        /// 傳送電子郵件
        /// </summary>
        public void SendEmail()
        {
            try { 
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                // 若你的內容是HTML格式，則為True
                mail.IsBodyHtml = false;
                //夾帶檔案
                //ail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //ail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
                SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
                smtp.Credentials = new NetworkCredential(emailFrom, password);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
           }
            }catch(Exception ex)
            {
                ErrorData = Convert.ToString( ex.GetHashCode());
                ErrorMessage = ex.Message;
            }
        }
        /// <summary>
        /// 傳送電子郵件
        /// </summary>
        /// <param name="FileName">附件路徑檔名</param>
        public void SendEmail(String[] FileName)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                // 若你的內容是HTML格式，則為True
                mail.IsBodyHtml = false;

                //夾帶檔案
                foreach (String NameFile in FileName) { 
                    mail.Attachments.Add(new Attachment(NameFile));
                }

                SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
                smtp.Credentials = new NetworkCredential(emailFrom, password);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
        }
        /// <summary>
        /// 傳送Goodle電子郵件
        /// </summary>
        public void SendGmail()
        {
            MailMessage msg = new MailMessage();
        }
        /// <summary>
        /// 傳送Goodle電子郵件
        /// </summary>
        /// <param name="MailList">附件路徑檔名List陣列</param>
        /// <param name="Subject">主旨</param>
        /// <param name="Body">內容</param>
        private void SendMailByGmail(List<string> MailList, string Subject, string Body)
        {
            MailMessage msg = new MailMessage();
            //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
            msg.To.Add(string.Join(",", MailList.ToArray()));
            msg.From = new MailAddress(emailTo, Subject, System.Text.Encoding.UTF8);
            //郵件標題 
            msg.Subject = Subject;
            //郵件標題編碼  
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //郵件內容
            msg.Body = Body;
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
            msg.Priority = MailPriority.Normal;//郵件優先級 
                                               //建立 SmtpClient 物件 並設定 Gmail的smtp主機及Port 
            #region 其它 Host
            /*
             *  outlook.com smtp.live.com port:25
             *  yahoo smtp.mail.yahoo.com.tw port:465
            */
            #endregion
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
            //設定你的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
            //Gmial 的 smtp 使用 SSL
            MySmtp.EnableSsl = true;
            MySmtp.Send(msg);
        }
    }
}
